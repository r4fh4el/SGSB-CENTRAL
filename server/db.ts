import sqlServer from "mssql/msnodesqlv8.js";
import { ENV } from "./_core/env";
import { runQuery, type SqlRequest } from "./_core/sqlserver";
import type {
  InsertAlerta,
  InsertAuditoria,
  InsertBarragem,
  InsertChecklist,
  InsertDocumento,
  InsertEstrutura,
  InsertHidrometria,
  InsertInstrumento,
  InsertLeitura,
  InsertManutencao,
  InsertOcorrencia,
  InsertPerguntaChecklist,
  InsertRespostaChecklist,
  InsertUser,
} from "@shared/dbTypes";

type SqlFieldConfig = {
  column?: string;
  type: any;
  transform?: (value: unknown) => unknown;
};

function buildUpdateFragments(
  data: Record<string, unknown>,
  config: Record<string, SqlFieldConfig>
) {
  const updates: string[] = [];
  const setters: Array<(request: SqlRequest) => void> = [];

  for (const [key, fieldConfig] of Object.entries(config)) {
    const value = data[key];
    if (value !== undefined) {
      const column = fieldConfig.column ?? key;
      updates.push(`${column} = @${column}`);
      setters.push((request) => {
        const transformed =
          value === null
            ? null
            : fieldConfig.transform
            ? fieldConfig.transform(value)
            : value;
        request.input(column, fieldConfig.type, transformed);
      });
    }
  }

  return {
    updates,
    apply: (request: SqlRequest) => setters.forEach((setter) => setter(request)),
  };
}

function toDate(value: unknown): Date | null {
  if (value === null || value === undefined) {
    return null;
  }
  if (value instanceof Date) {
    return Number.isNaN(value.getTime()) ? null : value;
  }
  const date = new Date(value as string);
  return Number.isNaN(date.getTime()) ? null : date;
}

function parseJsonColumn<T>(value: string | null | undefined): T | null {
  if (!value) {
    return null;
  }
  try {
    return JSON.parse(value) as T;
  } catch (error) {
    console.warn("[SQL Server] Failed to parse JSON column", error);
    return null;
  }
}

// ============================================================================
// USUÁRIOS
// ============================================================================

export async function upsertUser(user: InsertUser): Promise<void> {
  if (!user.id) {
    throw new Error("User ID is required for upsert");
  }
  const now = new Date();

  await runQuery(
    `MERGE dbo.users AS target
     USING (SELECT @id AS id) AS source
     ON target.id = source.id
     WHEN MATCHED THEN
       UPDATE SET
         name = @name,
         email = @email,
         loginMethod = @loginMethod,
         role = COALESCE(@role, target.role),
         ativo = COALESCE(@ativo, target.ativo),
         lastSignedIn = COALESCE(@lastSignedIn, target.lastSignedIn)
     WHEN NOT MATCHED THEN
       INSERT (id, name, email, loginMethod, role, ativo, createdAt, lastSignedIn)
       VALUES (
         @id,
         @name,
         @email,
         @loginMethod,
         COALESCE(@role, 'visualizador'),
         COALESCE(@ativo, 1),
         COALESCE(@createdAt, SYSDATETIME()),
         COALESCE(@lastSignedIn, SYSDATETIME())
       );`,
    (request) => {
      request.input("id", sqlServer.NVarChar(64), user.id);
      request.input("name", sqlServer.NVarChar(255), user.name ?? null);
      request.input("email", sqlServer.NVarChar(320), user.email ?? null);
      request.input("loginMethod", sqlServer.NVarChar(64), user.loginMethod ?? null);
      request.input(
        "role",
        sqlServer.NVarChar(32),
        user.role ?? (user.id === ENV.ownerId ? "admin" : null)
      );
      request.input("ativo", sqlServer.Bit, user.ativo ?? null);
      request.input("createdAt", sqlServer.DateTime2, user.createdAt ?? now);
      request.input("lastSignedIn", sqlServer.DateTime2, user.lastSignedIn ?? now);
    }
  );
}

export async function getUser(id: string) {
  const result = await runQuery<InsertUser>(
    "SELECT TOP 1 * FROM dbo.users WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.NVarChar(64), id);
    }
  );

  return result.recordset[0];
}

export async function getAllUsers() {
  const result = await runQuery<InsertUser>(
    "SELECT * FROM dbo.users ORDER BY name"
  );
  return result.recordset;
}

export async function updateUserRole(userId: string, role: string) {
  await runQuery(
    "UPDATE dbo.users SET role = @role WHERE id = @id",
    (request) => {
      request.input("role", sqlServer.NVarChar(32), role);
      request.input("id", sqlServer.NVarChar(64), userId);
    }
  );
}

export async function toggleUserStatus(userId: string) {
  const user = await getUser(userId);
  if (!user) return;

  await runQuery(
    "UPDATE dbo.users SET ativo = @ativo WHERE id = @id",
    (request) => {
      request.input("ativo", sqlServer.Bit, user.ativo ? 0 : 1);
      request.input("id", sqlServer.NVarChar(64), userId);
    }
  );
}

// ============================================================================
// BARRAGENS
// ============================================================================

export async function createBarragem(data: InsertBarragem) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.barragens (
      codigo,
      nome,
      rio,
      bacia,
      municipio,
      estado,
      latitude,
      longitude,
      tipo,
      finalidade,
      altura,
      comprimento,
      volumeReservatorio,
      areaReservatorio,
      nivelMaximoNormal,
      nivelMaximoMaximorum,
      nivelMinimo,
      proprietario,
      operador,
      anoInicioConstrucao,
      anoInicioOperacao,
      categoriaRisco,
      danoPotencialAssociado,
      status,
      observacoes
    ) OUTPUT INSERTED.id VALUES (
      @codigo,
      @nome,
      @rio,
      @bacia,
      @municipio,
      @estado,
      @latitude,
      @longitude,
      @tipo,
      @finalidade,
      @altura,
      @comprimento,
      @volumeReservatorio,
      @areaReservatorio,
      @nivelMaximoNormal,
      @nivelMaximoMaximorum,
      @nivelMinimo,
      @proprietario,
      @operador,
      @anoInicioConstrucao,
      @anoInicioOperacao,
      @categoriaRisco,
      @danoPotencialAssociado,
      @status,
      @observacoes
    );`,
    (request) => {
      request.input("codigo", sqlServer.NVarChar(50), data.codigo);
      request.input("nome", sqlServer.NVarChar(255), data.nome);
      request.input("rio", sqlServer.NVarChar(255), data.rio ?? null);
      request.input("bacia", sqlServer.NVarChar(255), data.bacia ?? null);
      request.input("municipio", sqlServer.NVarChar(255), data.municipio ?? null);
      request.input("estado", sqlServer.NVarChar(2), data.estado ?? null);
      request.input("latitude", sqlServer.NVarChar(50), data.latitude ?? null);
      request.input("longitude", sqlServer.NVarChar(50), data.longitude ?? null);
      request.input("tipo", sqlServer.NVarChar(100), data.tipo ?? null);
      request.input("finalidade", sqlServer.NVarChar(255), data.finalidade ?? null);
      request.input("altura", sqlServer.NVarChar(50), data.altura ?? null);
      request.input("comprimento", sqlServer.NVarChar(50), data.comprimento ?? null);
      request.input("volumeReservatorio", sqlServer.NVarChar(50), data.volumeReservatorio ?? null);
      request.input("areaReservatorio", sqlServer.NVarChar(50), data.areaReservatorio ?? null);
      request.input("nivelMaximoNormal", sqlServer.NVarChar(50), data.nivelMaximoNormal ?? null);
      request.input("nivelMaximoMaximorum", sqlServer.NVarChar(50), data.nivelMaximoMaximorum ?? null);
      request.input("nivelMinimo", sqlServer.NVarChar(50), data.nivelMinimo ?? null);
      request.input("proprietario", sqlServer.NVarChar(255), data.proprietario ?? null);
      request.input("operador", sqlServer.NVarChar(255), data.operador ?? null);
      request.input("anoInicioConstrucao", sqlServer.Int, data.anoInicioConstrucao ?? null);
      request.input("anoInicioOperacao", sqlServer.Int, data.anoInicioOperacao ?? null);
      request.input("categoriaRisco", sqlServer.NVarChar(8), data.categoriaRisco ?? null);
      request.input("danoPotencialAssociado", sqlServer.NVarChar(16), data.danoPotencialAssociado ?? null);
      request.input("status", sqlServer.NVarChar(32), data.status ?? "ativa");
      request.input("observacoes", sqlServer.NVarChar(sqlServer.MAX), data.observacoes ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getAllBarragens() {
  const result = await runQuery<InsertBarragem & { id: number }>(
    "SELECT * FROM dbo.barragens ORDER BY nome"
  );
  return result.recordset;
}

export async function getBarragemById(id: number) {
  const result = await runQuery<InsertBarragem & { id: number }>(
    "SELECT TOP 1 * FROM dbo.barragens WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
  return result.recordset[0];
}

export async function updateBarragem(id: number, data: Partial<InsertBarragem>) {
  const updates: string[] = [];
  const paramSetters: Array<(request: SqlRequest) => void> = [];

  const addField = (
    column: string,
    value: unknown,
    setter: (request: SqlRequest) => void
  ) => {
    if (value !== undefined) {
      updates.push(`${column} = @${column}`);
      paramSetters.push(setter);
    }
  };

  addField("codigo", data.codigo, (request) =>
    request.input("codigo", sqlServer.NVarChar(50), data.codigo ?? null)
  );
  addField("nome", data.nome, (request) =>
    request.input("nome", sqlServer.NVarChar(255), data.nome ?? null)
  );
  addField("rio", data.rio, (request) =>
    request.input("rio", sqlServer.NVarChar(255), data.rio ?? null)
  );
  addField("bacia", data.bacia, (request) =>
    request.input("bacia", sqlServer.NVarChar(255), data.bacia ?? null)
  );
  addField("municipio", data.municipio, (request) =>
    request.input("municipio", sqlServer.NVarChar(255), data.municipio ?? null)
  );
  addField("estado", data.estado, (request) =>
    request.input("estado", sqlServer.NVarChar(2), data.estado ?? null)
  );
  addField("latitude", data.latitude, (request) =>
    request.input("latitude", sqlServer.NVarChar(50), data.latitude ?? null)
  );
  addField("longitude", data.longitude, (request) =>
    request.input("longitude", sqlServer.NVarChar(50), data.longitude ?? null)
  );
  addField("tipo", data.tipo, (request) =>
    request.input("tipo", sqlServer.NVarChar(100), data.tipo ?? null)
  );
  addField("finalidade", data.finalidade, (request) =>
    request.input("finalidade", sqlServer.NVarChar(255), data.finalidade ?? null)
  );
  addField("altura", data.altura, (request) =>
    request.input("altura", sqlServer.NVarChar(50), data.altura ?? null)
  );
  addField("comprimento", data.comprimento, (request) =>
    request.input("comprimento", sqlServer.NVarChar(50), data.comprimento ?? null)
  );
  addField("volumeReservatorio", data.volumeReservatorio, (request) =>
    request.input(
      "volumeReservatorio",
      sqlServer.NVarChar(50),
      data.volumeReservatorio ?? null
    )
  );
  addField("areaReservatorio", data.areaReservatorio, (request) =>
    request.input(
      "areaReservatorio",
      sqlServer.NVarChar(50),
      data.areaReservatorio ?? null
    )
  );
  addField("nivelMaximoNormal", data.nivelMaximoNormal, (request) =>
    request.input(
      "nivelMaximoNormal",
      sqlServer.NVarChar(50),
      data.nivelMaximoNormal ?? null
    )
  );
  addField("nivelMaximoMaximorum", data.nivelMaximoMaximorum, (request) =>
    request.input(
      "nivelMaximoMaximorum",
      sqlServer.NVarChar(50),
      data.nivelMaximoMaximorum ?? null
    )
  );
  addField("nivelMinimo", data.nivelMinimo, (request) =>
    request.input("nivelMinimo", sqlServer.NVarChar(50), data.nivelMinimo ?? null)
  );
  addField("proprietario", data.proprietario, (request) =>
    request.input("proprietario", sqlServer.NVarChar(255), data.proprietario ?? null)
  );
  addField("operador", data.operador, (request) =>
    request.input("operador", sqlServer.NVarChar(255), data.operador ?? null)
  );
  addField("anoInicioConstrucao", data.anoInicioConstrucao, (request) =>
    request.input(
      "anoInicioConstrucao",
      sqlServer.Int,
      data.anoInicioConstrucao ?? null
    )
  );
  addField("anoInicioOperacao", data.anoInicioOperacao, (request) =>
    request.input("anoInicioOperacao", sqlServer.Int, data.anoInicioOperacao ?? null)
  );
  addField("categoriaRisco", data.categoriaRisco, (request) =>
    request.input(
      "categoriaRisco",
      sqlServer.NVarChar(8),
      data.categoriaRisco ?? null
    )
  );
  addField("danoPotencialAssociado", data.danoPotencialAssociado, (request) =>
    request.input(
      "danoPotencialAssociado",
      sqlServer.NVarChar(16),
      data.danoPotencialAssociado ?? null
    )
  );
  addField("status", data.status, (request) =>
    request.input("status", sqlServer.NVarChar(32), data.status ?? null)
  );
  addField("observacoes", data.observacoes, (request) =>
    request.input(
      "observacoes",
      sqlServer.NVarChar(sqlServer.MAX),
      data.observacoes ?? null
    )
  );

  if (updates.length === 0) return;

  updates.push("updatedAt = SYSDATETIME()");

  await runQuery(
    `UPDATE dbo.barragens SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      paramSetters.forEach((setter) => setter(request));
    }
  );
}

export async function deleteBarragem(id: number) {
  await runQuery(
    "DELETE FROM dbo.barragens WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// ESTRUTURAS
// ============================================================================

export async function createEstrutura(data: InsertEstrutura) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.estruturas (
      barragemId,
      codigo,
      nome,
      tipo,
      descricao,
      localizacao,
      coordenadas,
      ativo
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @codigo,
      @nome,
      @tipo,
      @descricao,
      @localizacao,
      @coordenadas,
      COALESCE(@ativo, 1)
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("codigo", sqlServer.NVarChar(50), data.codigo);
      request.input("nome", sqlServer.NVarChar(255), data.nome);
      request.input("tipo", sqlServer.NVarChar(100), data.tipo);
      request.input("descricao", sqlServer.NVarChar(sqlServer.MAX), data.descricao ?? null);
      request.input("localizacao", sqlServer.NVarChar(255), data.localizacao ?? null);
      request.input("coordenadas", sqlServer.NVarChar(100), data.coordenadas ?? null);
      request.input("ativo", sqlServer.Bit, data.ativo ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getEstruturasByBarragem(barragemId: number) {
  const result = await runQuery<InsertEstrutura & { id: number }>(
    `SELECT * FROM dbo.estruturas WHERE barragemId = @barragemId ORDER BY nome`,
    (request) => {
      request.input("barragemId", sqlServer.Int, barragemId);
    }
  );

  return result.recordset;
}

export async function updateEstrutura(id: number, data: Partial<InsertEstrutura>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    barragemId: { type: sqlServer.Int },
    codigo: { type: sqlServer.NVarChar(50) },
    nome: { type: sqlServer.NVarChar(255) },
    tipo: { type: sqlServer.NVarChar(100) },
    descricao: { type: sqlServer.NVarChar(sqlServer.MAX) },
    localizacao: { type: sqlServer.NVarChar(255) },
    coordenadas: { type: sqlServer.NVarChar(100) },
    ativo: { type: sqlServer.Bit },
  });

  if (updates.length === 0) return;

  await runQuery(
    `UPDATE dbo.estruturas SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteEstrutura(id: number) {
  await runQuery(
    "DELETE FROM dbo.estruturas WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// INSTRUMENTOS
// ============================================================================

export async function createInstrumento(data: InsertInstrumento) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.instrumentos (
      barragemId,
      estruturaId,
      codigo,
      tipo,
      localizacao,
      estaca,
      cota,
      coordenadas,
      dataInstalacao,
      fabricante,
      modelo,
      numeroSerie,
      nivelNormal,
      nivelAlerta,
      nivelCritico,
      formula,
      unidadeMedida,
      limiteInferior,
      limiteSuperior,
      frequenciaLeitura,
      responsavel,
      qrCode,
      codigoBarras,
      status,
      observacoes,
      ativo
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @estruturaId,
      @codigo,
      @tipo,
      @localizacao,
      @estaca,
      @cota,
      @coordenadas,
      @dataInstalacao,
      @fabricante,
      @modelo,
      @numeroSerie,
      @nivelNormal,
      @nivelAlerta,
      @nivelCritico,
      @formula,
      @unidadeMedida,
      @limiteInferior,
      @limiteSuperior,
      @frequenciaLeitura,
      @responsavel,
      @qrCode,
      @codigoBarras,
      COALESCE(@status, 'ativo'),
      @observacoes,
      COALESCE(@ativo, 1)
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("estruturaId", sqlServer.Int, data.estruturaId ?? null);
      request.input("codigo", sqlServer.NVarChar(50), data.codigo);
      request.input("tipo", sqlServer.NVarChar(100), data.tipo);
      request.input("localizacao", sqlServer.NVarChar(255), data.localizacao ?? null);
      request.input("estaca", sqlServer.NVarChar(50), data.estaca ?? null);
      request.input("cota", sqlServer.NVarChar(50), data.cota ?? null);
      request.input("coordenadas", sqlServer.NVarChar(100), data.coordenadas ?? null);
      request.input("dataInstalacao", sqlServer.DateTime2, toDate(data.dataInstalacao));
      request.input("fabricante", sqlServer.NVarChar(255), data.fabricante ?? null);
      request.input("modelo", sqlServer.NVarChar(255), data.modelo ?? null);
      request.input("numeroSerie", sqlServer.NVarChar(100), data.numeroSerie ?? null);
      request.input("nivelNormal", sqlServer.NVarChar(50), data.nivelNormal ?? null);
      request.input("nivelAlerta", sqlServer.NVarChar(50), data.nivelAlerta ?? null);
      request.input("nivelCritico", sqlServer.NVarChar(50), data.nivelCritico ?? null);
      request.input("formula", sqlServer.NVarChar(sqlServer.MAX), data.formula ?? null);
      request.input("unidadeMedida", sqlServer.NVarChar(50), data.unidadeMedida ?? null);
      request.input("limiteInferior", sqlServer.NVarChar(50), data.limiteInferior ?? null);
      request.input("limiteSuperior", sqlServer.NVarChar(50), data.limiteSuperior ?? null);
      request.input("frequenciaLeitura", sqlServer.NVarChar(100), data.frequenciaLeitura ?? null);
      request.input("responsavel", sqlServer.NVarChar(255), data.responsavel ?? null);
      request.input("qrCode", sqlServer.NVarChar(255), data.qrCode ?? null);
      request.input("codigoBarras", sqlServer.NVarChar(255), data.codigoBarras ?? null);
      request.input("status", sqlServer.NVarChar(32), data.status ?? null);
      request.input("observacoes", sqlServer.NVarChar(sqlServer.MAX), data.observacoes ?? null);
      request.input("ativo", sqlServer.Bit, data.ativo ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getAllInstrumentos(barragemId?: number) {
  const query = barragemId
    ? `SELECT * FROM dbo.instrumentos WHERE barragemId = @barragemId ORDER BY codigo`
    : `SELECT * FROM dbo.instrumentos ORDER BY codigo`;

  const result = await runQuery<InsertInstrumento & { id: number }>(
    query,
    (request) => {
      if (barragemId !== undefined) {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    }
  );

  return result.recordset;
}

export async function getInstrumentoById(id: number) {
  const result = await runQuery<InsertInstrumento & { id: number }>(
    `SELECT TOP 1 * FROM dbo.instrumentos WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );

  return result.recordset[0];
}

export async function getInstrumentoByCodigo(codigo: string) {
  const result = await runQuery<InsertInstrumento & { id: number }>(
    `SELECT TOP 1 * FROM dbo.instrumentos WHERE codigo = @codigo`,
    (request) => {
      request.input("codigo", sqlServer.NVarChar(50), codigo);
    }
  );

  return result.recordset[0];
}

export async function updateInstrumento(id: number, data: Partial<InsertInstrumento>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    barragemId: { type: sqlServer.Int },
    estruturaId: { type: sqlServer.Int },
    codigo: { type: sqlServer.NVarChar(50) },
    tipo: { type: sqlServer.NVarChar(100) },
    localizacao: { type: sqlServer.NVarChar(255) },
    estaca: { type: sqlServer.NVarChar(50) },
    cota: { type: sqlServer.NVarChar(50) },
    coordenadas: { type: sqlServer.NVarChar(100) },
    dataInstalacao: { type: sqlServer.DateTime2, transform: toDate },
    fabricante: { type: sqlServer.NVarChar(255) },
    modelo: { type: sqlServer.NVarChar(255) },
    numeroSerie: { type: sqlServer.NVarChar(100) },
    nivelNormal: { type: sqlServer.NVarChar(50) },
    nivelAlerta: { type: sqlServer.NVarChar(50) },
    nivelCritico: { type: sqlServer.NVarChar(50) },
    formula: { type: sqlServer.NVarChar(sqlServer.MAX) },
    unidadeMedida: { type: sqlServer.NVarChar(50) },
    limiteInferior: { type: sqlServer.NVarChar(50) },
    limiteSuperior: { type: sqlServer.NVarChar(50) },
    frequenciaLeitura: { type: sqlServer.NVarChar(100) },
    responsavel: { type: sqlServer.NVarChar(255) },
    qrCode: { type: sqlServer.NVarChar(255) },
    codigoBarras: { type: sqlServer.NVarChar(255) },
    status: { type: sqlServer.NVarChar(32) },
    observacoes: { type: sqlServer.NVarChar(sqlServer.MAX) },
    ativo: { type: sqlServer.Bit },
  });

  if (updates.length === 0) return;

  updates.push("updatedAt = SYSDATETIME()");

  await runQuery(
    `UPDATE dbo.instrumentos SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteInstrumento(id: number) {
  await runQuery(
    "DELETE FROM dbo.instrumentos WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// LEITURAS
// ============================================================================

export async function createLeitura(data: InsertLeitura) {
  const instrumento = await getInstrumentoById(data.instrumentoId);
  if (instrumento) {
    const valor = parseFloat(data.valor);
    const nivelAlerta = instrumento.nivelAlerta ? parseFloat(instrumento.nivelAlerta) : null;
    const nivelCritico = instrumento.nivelCritico ? parseFloat(instrumento.nivelCritico) : null;

    if (nivelCritico && !Number.isNaN(valor) && valor >= nivelCritico) {
      data.inconsistencia = true;
      data.tipoInconsistencia = "Acima do nível crítico";
    } else if (nivelAlerta && !Number.isNaN(valor) && valor >= nivelAlerta) {
      data.inconsistencia = true;
      data.tipoInconsistencia = "Acima do nível de alerta";
    }
  }

  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.leituras (
      instrumentoId,
      usuarioId,
      dataHora,
      valor,
      nivelMontante,
      inconsistencia,
      tipoInconsistencia,
      observacoes,
      origem,
      latitude,
      longitude
    ) OUTPUT INSERTED.id VALUES (
      @instrumentoId,
      @usuarioId,
      @dataHora,
      @valor,
      @nivelMontante,
      COALESCE(@inconsistencia, 0),
      @tipoInconsistencia,
      @observacoes,
      COALESCE(@origem, 'mobile'),
      @latitude,
      @longitude
    );`,
    (request) => {
      request.input("instrumentoId", sqlServer.Int, data.instrumentoId);
      request.input("usuarioId", sqlServer.NVarChar(64), data.usuarioId);
      request.input("dataHora", sqlServer.DateTime2, toDate(data.dataHora));
      request.input("valor", sqlServer.NVarChar(50), data.valor);
      request.input("nivelMontante", sqlServer.NVarChar(50), data.nivelMontante ?? null);
      request.input("inconsistencia", sqlServer.Bit, data.inconsistencia ?? null);
      request.input("tipoInconsistencia", sqlServer.NVarChar(100), data.tipoInconsistencia ?? null);
      request.input("observacoes", sqlServer.NVarChar(sqlServer.MAX), data.observacoes ?? null);
      request.input("origem", sqlServer.NVarChar(16), data.origem ?? null);
      request.input("latitude", sqlServer.NVarChar(50), data.latitude ?? null);
      request.input("longitude", sqlServer.NVarChar(50), data.longitude ?? null);
    }
  );

  const insertedId = result.recordset[0]?.id ?? 0;

  if (data.inconsistencia && instrumento && insertedId) {
    const tipoInconsistenciaLower = data.tipoInconsistencia?.toLowerCase();

    await createAlerta({
      barragemId: instrumento.barragemId,
      tipo: "Leitura com inconsistência",
      severidade:
        tipoInconsistenciaLower && tipoInconsistenciaLower.includes("crít")
          ? "critico"
          : "alerta",
      titulo: `Leitura fora do padrão - ${instrumento.codigo}`,
      mensagem: `O instrumento ${instrumento.codigo} apresentou leitura ${
        tipoInconsistenciaLower ?? "fora do padrão"
      }: ${data.valor} ${instrumento.unidadeMedida || ""}`,
      instrumentoId: data.instrumentoId,
      leituraId: insertedId,
    });
  }

  return insertedId;
}

export async function getLeiturasByInstrumento(instrumentoId: number, limit = 50) {
  const result = await runQuery<InsertLeitura & { id: number }>(
    `SELECT TOP (@limit) *
     FROM dbo.leituras
     WHERE instrumentoId = @instrumentoId
     ORDER BY dataHora DESC`,
    (request) => {
      request.input("limit", sqlServer.Int, limit);
      request.input("instrumentoId", sqlServer.Int, instrumentoId);
    }
  );

  return result.recordset;
}

export async function getUltimaLeitura(instrumentoId: number) {
  const result = await runQuery<InsertLeitura & { id: number }>(
    `SELECT TOP 1 *
     FROM dbo.leituras
     WHERE instrumentoId = @instrumentoId
     ORDER BY dataHora DESC`,
    (request) => {
      request.input("instrumentoId", sqlServer.Int, instrumentoId);
    }
  );

  return result.recordset[0];
}

export async function getLeiturasComInconsistencia(barragemId?: number, limit?: number) {
  const topClause = limit !== undefined ? `TOP (@limit)` : "";
  const whereClause =
    barragemId !== undefined
      ? "WHERE l.inconsistencia = 1 AND i.barragemId = @barragemId"
      : "WHERE l.inconsistencia = 1";

  const result = await runQuery<{ leitura: string | null; instrumento: string | null }>(
    `SELECT ${topClause}
      (SELECT l.* FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS leitura,
      (SELECT i.* FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS instrumento
     FROM dbo.leituras AS l
     INNER JOIN dbo.instrumentos AS i ON l.instrumentoId = i.id
     ${whereClause}
     ORDER BY l.dataHora DESC`,
    (request) => {
      if (limit !== undefined) {
        request.input("limit", sqlServer.Int, limit);
      }
      if (barragemId !== undefined) {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    }
  );

  return result.recordset.map(
    (row: { leitura: string | null; instrumento: string | null }) => ({
    leitura:
      parseJsonColumn<InsertLeitura & { id: number }>(row.leitura) ??
      ({} as InsertLeitura & { id: number }),
    instrumento:
      parseJsonColumn<InsertInstrumento & { id: number }>(row.instrumento) ??
      ({} as InsertInstrumento & { id: number }),
    })
  );
}

// ============================================================================
// CHECKLISTS
// ============================================================================

export async function createChecklist(data: InsertChecklist) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.checklists (
      barragemId,
      usuarioId,
      data,
      tipo,
      inspetor,
      climaCondicoes,
      status,
      consultorId,
      dataAvaliacao,
      comentariosConsultor,
      observacoesGerais,
      latitude,
      longitude
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @usuarioId,
      @data,
      COALESCE(@tipo, 'mensal'),
      @inspetor,
      @climaCondicoes,
      COALESCE(@status, 'em_andamento'),
      @consultorId,
      @dataAvaliacao,
      @comentariosConsultor,
      @observacoesGerais,
      @latitude,
      @longitude
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("usuarioId", sqlServer.NVarChar(64), data.usuarioId);
      request.input("data", sqlServer.DateTime2, toDate(data.data));
      request.input("tipo", sqlServer.NVarChar(32), data.tipo ?? null);
      request.input("inspetor", sqlServer.NVarChar(255), data.inspetor ?? null);
      request.input("climaCondicoes", sqlServer.NVarChar(255), data.climaCondicoes ?? null);
      request.input("status", sqlServer.NVarChar(32), data.status ?? null);
      request.input("consultorId", sqlServer.NVarChar(64), data.consultorId ?? null);
      request.input("dataAvaliacao", sqlServer.DateTime2, toDate(data.dataAvaliacao));
      request.input("comentariosConsultor", sqlServer.NVarChar(sqlServer.MAX), data.comentariosConsultor ?? null);
      request.input("observacoesGerais", sqlServer.NVarChar(sqlServer.MAX), data.observacoesGerais ?? null);
      request.input("latitude", sqlServer.NVarChar(50), data.latitude ?? null);
      request.input("longitude", sqlServer.NVarChar(50), data.longitude ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getChecklistsByBarragem(barragemId: number, limit = 20) {
  const result = await runQuery<InsertChecklist & { id: number }>(
    `SELECT TOP (@limit) *
     FROM dbo.checklists
     WHERE barragemId = @barragemId
     ORDER BY data DESC`,
    (request) => {
      request.input("limit", sqlServer.Int, limit);
      request.input("barragemId", sqlServer.Int, barragemId);
    }
  );

  return result.recordset;
}

export async function getChecklistById(id: number) {
  const result = await runQuery<InsertChecklist & { id: number }>(
    `SELECT TOP 1 * FROM dbo.checklists WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );

  return result.recordset[0];
}

export async function updateChecklist(id: number, data: Partial<InsertChecklist>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    tipo: { type: sqlServer.NVarChar(32) },
    inspetor: { type: sqlServer.NVarChar(255) },
    climaCondicoes: { type: sqlServer.NVarChar(255) },
    status: { type: sqlServer.NVarChar(32) },
    consultorId: { type: sqlServer.NVarChar(64) },
    dataAvaliacao: { type: sqlServer.DateTime2, transform: toDate },
    comentariosConsultor: { type: sqlServer.NVarChar(sqlServer.MAX) },
    observacoesGerais: { type: sqlServer.NVarChar(sqlServer.MAX) },
    latitude: { type: sqlServer.NVarChar(50) },
    longitude: { type: sqlServer.NVarChar(50) },
  });

  if (updates.length === 0) return;

  updates.push("updatedAt = SYSDATETIME()");

  await runQuery(
    `UPDATE dbo.checklists SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteChecklist(id: number) {
  await runQuery(
    "DELETE FROM dbo.checklists WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// PERGUNTAS E RESPOSTAS DO CHECKLIST
// ============================================================================

export async function createPerguntaChecklist(data: InsertPerguntaChecklist) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.perguntasChecklist (
      barragemId,
      categoria,
      pergunta,
      ordem,
      ativo
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @categoria,
      @pergunta,
      @ordem,
      COALESCE(@ativo, 1)
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId ?? null);
      request.input("categoria", sqlServer.NVarChar(100), data.categoria);
      request.input("pergunta", sqlServer.NVarChar(sqlServer.MAX), data.pergunta);
      request.input("ordem", sqlServer.Int, data.ordem);
      request.input("ativo", sqlServer.Bit, data.ativo ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getPerguntasChecklist(barragemId?: number) {
  const query = barragemId !== undefined
    ? `SELECT * FROM dbo.perguntasChecklist
       WHERE barragemId = @barragemId AND ativo = 1
       ORDER BY categoria, ordem`
    : `SELECT * FROM dbo.perguntasChecklist
       WHERE ativo = 1
       ORDER BY categoria, ordem`;

  const result = await runQuery<InsertPerguntaChecklist & { id: number }>(
    query,
    (request) => {
      if (barragemId !== undefined) {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    }
  );

  return result.recordset;
}

export async function createRespostaChecklist(data: InsertRespostaChecklist) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.respostasChecklist (
      checklistId,
      perguntaId,
      resposta,
      situacaoAnterior,
      comentario,
      fotos
    ) OUTPUT INSERTED.id VALUES (
      @checklistId,
      @perguntaId,
      @resposta,
      @situacaoAnterior,
      @comentario,
      @fotos
    );`,
    (request) => {
      request.input("checklistId", sqlServer.Int, data.checklistId);
      request.input("perguntaId", sqlServer.Int, data.perguntaId);
      request.input("resposta", sqlServer.NVarChar(4), data.resposta);
      request.input("situacaoAnterior", sqlServer.NVarChar(4), data.situacaoAnterior ?? null);
      request.input("comentario", sqlServer.NVarChar(sqlServer.MAX), data.comentario ?? null);
      request.input("fotos", sqlServer.NVarChar(sqlServer.MAX), data.fotos ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getRespostasByChecklist(checklistId: number) {
  const result = await runQuery<{ resposta: string | null; pergunta: string | null }>(
    `SELECT
      (SELECT r.* FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS resposta,
      (SELECT p.* FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS pergunta
     FROM dbo.respostasChecklist AS r
     INNER JOIN dbo.perguntasChecklist AS p ON r.perguntaId = p.id
     WHERE r.checklistId = @checklistId
     ORDER BY p.categoria, p.ordem`,
    (request) => {
      request.input("checklistId", sqlServer.Int, checklistId);
    }
  );

  return result.recordset.map(
    (row: { resposta: string | null; pergunta: string | null }) => ({
    resposta:
      parseJsonColumn<InsertRespostaChecklist & { id: number }>(row.resposta) ??
      ({} as InsertRespostaChecklist & { id: number }),
    pergunta:
      parseJsonColumn<InsertPerguntaChecklist & { id: number }>(row.pergunta) ??
      ({} as InsertPerguntaChecklist & { id: number }),
    })
  );
}

// ============================================================================
// OCORRÊNCIAS
// ============================================================================

export async function createOcorrencia(data: InsertOcorrencia) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.ocorrencias (
      barragemId,
      estruturaId,
      usuarioRegistroId,
      dataHoraRegistro,
      estrutura,
      relato,
      fotos,
      titulo,
      descricao,
      dataOcorrencia,
      localOcorrencia,
      acaoImediata,
      responsavel,
      categoria,
      severidade,
      tipo,
      status,
      usuarioAvaliacaoId,
      dataAvaliacao,
      comentariosAvaliacao,
      dataConclusao,
      comentariosConclusao,
      latitude,
      longitude
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @estruturaId,
      @usuarioRegistroId,
      @dataHoraRegistro,
      @estrutura,
      @relato,
      @fotos,
      @titulo,
      @descricao,
      @dataOcorrencia,
      @localOcorrencia,
      @acaoImediata,
      @responsavel,
      @categoria,
      @severidade,
      @tipo,
      COALESCE(@status, 'pendente'),
      @usuarioAvaliacaoId,
      @dataAvaliacao,
      @comentariosAvaliacao,
      @dataConclusao,
      @comentariosConclusao,
      @latitude,
      @longitude
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("estruturaId", sqlServer.Int, data.estruturaId ?? null);
      request.input("usuarioRegistroId", sqlServer.NVarChar(64), data.usuarioRegistroId);
      request.input("dataHoraRegistro", sqlServer.DateTime2, toDate(data.dataHoraRegistro));
      request.input("estrutura", sqlServer.NVarChar(255), data.estrutura);
      request.input("relato", sqlServer.NVarChar(sqlServer.MAX), data.relato);
      request.input("fotos", sqlServer.NVarChar(sqlServer.MAX), data.fotos ?? null);
      request.input("titulo", sqlServer.NVarChar(255), data.titulo ?? null);
      request.input("descricao", sqlServer.NVarChar(sqlServer.MAX), data.descricao ?? null);
      request.input("dataOcorrencia", sqlServer.DateTime2, toDate(data.dataOcorrencia));
      request.input("localOcorrencia", sqlServer.NVarChar(255), data.localOcorrencia ?? null);
      request.input("acaoImediata", sqlServer.NVarChar(sqlServer.MAX), data.acaoImediata ?? null);
      request.input("responsavel", sqlServer.NVarChar(255), data.responsavel ?? null);
      request.input("categoria", sqlServer.NVarChar(100), data.categoria ?? null);
      request.input("severidade", sqlServer.NVarChar(16), data.severidade ?? null);
      request.input("tipo", sqlServer.NVarChar(100), data.tipo ?? null);
      request.input("status", sqlServer.NVarChar(32), data.status ?? null);
      request.input("usuarioAvaliacaoId", sqlServer.NVarChar(64), data.usuarioAvaliacaoId ?? null);
      request.input("dataAvaliacao", sqlServer.DateTime2, toDate(data.dataAvaliacao));
      request.input("comentariosAvaliacao", sqlServer.NVarChar(sqlServer.MAX), data.comentariosAvaliacao ?? null);
      request.input("dataConclusao", sqlServer.DateTime2, toDate(data.dataConclusao));
      request.input("comentariosConclusao", sqlServer.NVarChar(sqlServer.MAX), data.comentariosConclusao ?? null);
      request.input("latitude", sqlServer.NVarChar(50), data.latitude ?? null);
      request.input("longitude", sqlServer.NVarChar(50), data.longitude ?? null);
    }
  );

  const insertedId = result.recordset[0]?.id ?? 0;

  if (insertedId && data.severidade && ["alta", "critica"].includes(data.severidade)) {
    await createAlerta({
      barragemId: data.barragemId,
      tipo: "Nova ocorrência",
      severidade: data.severidade === "critica" ? "critico" : "alerta",
      titulo: `Nova ocorrência registrada - ${data.estrutura}`,
      mensagem: data.relato.substring(0, 200),
      ocorrenciaId: insertedId,
    });
  }

  return insertedId;
}

export async function getOcorrenciasByBarragem(barragemId: number, status?: string) {
  const query = status
    ? `SELECT * FROM dbo.ocorrencias
       WHERE barragemId = @barragemId AND status = @status
       ORDER BY dataHoraRegistro DESC`
    : `SELECT * FROM dbo.ocorrencias
       WHERE barragemId = @barragemId
       ORDER BY dataHoraRegistro DESC`;

  const result = await runQuery<InsertOcorrencia & { id: number }>(
    query,
    (request) => {
      request.input("barragemId", sqlServer.Int, barragemId);
      if (status) {
        request.input("status", sqlServer.NVarChar(32), status);
      }
    }
  );

  return result.recordset;
}

export async function getOcorrenciaById(id: number) {
  const result = await runQuery<InsertOcorrencia & { id: number }>(
    `SELECT TOP 1 * FROM dbo.ocorrencias WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );

  return result.recordset[0];
}

export async function updateOcorrencia(id: number, data: Partial<InsertOcorrencia>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    barragemId: { type: sqlServer.Int },
    estruturaId: { type: sqlServer.Int },
    estrutura: { type: sqlServer.NVarChar(255) },
    relato: { type: sqlServer.NVarChar(sqlServer.MAX) },
    fotos: { type: sqlServer.NVarChar(sqlServer.MAX) },
    titulo: { type: sqlServer.NVarChar(255) },
    descricao: { type: sqlServer.NVarChar(sqlServer.MAX) },
    dataOcorrencia: { type: sqlServer.DateTime2, transform: toDate },
    localOcorrencia: { type: sqlServer.NVarChar(255) },
    acaoImediata: { type: sqlServer.NVarChar(sqlServer.MAX) },
    responsavel: { type: sqlServer.NVarChar(255) },
    categoria: { type: sqlServer.NVarChar(100) },
    severidade: { type: sqlServer.NVarChar(16) },
    tipo: { type: sqlServer.NVarChar(100) },
    status: { type: sqlServer.NVarChar(32) },
    usuarioAvaliacaoId: { type: sqlServer.NVarChar(64) },
    dataAvaliacao: { type: sqlServer.DateTime2, transform: toDate },
    comentariosAvaliacao: { type: sqlServer.NVarChar(sqlServer.MAX) },
    dataConclusao: { type: sqlServer.DateTime2, transform: toDate },
    comentariosConclusao: { type: sqlServer.NVarChar(sqlServer.MAX) },
    latitude: { type: sqlServer.NVarChar(50) },
    longitude: { type: sqlServer.NVarChar(50) },
  });

  if (updates.length === 0) return;

  updates.push("updatedAt = SYSDATETIME()");

  await runQuery(
    `UPDATE dbo.ocorrencias SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteOcorrencia(id: number) {
  await runQuery(
    "DELETE FROM dbo.ocorrencias WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// HIDROMETRIA
// ============================================================================

export async function createHidrometria(data: InsertHidrometria) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.hidrometria (
      barragemId,
      usuarioId,
      dataLeitura,
      dataHora,
      nivelMontante,
      nivelJusante,
      nivelReservatorio,
      vazao,
      vazaoAfluente,
      vazaoDefluente,
      vazaoVertedouro,
      volumeReservatorio,
      volumeArmazenado,
      observacoes
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @usuarioId,
      @dataLeitura,
      @dataHora,
      @nivelMontante,
      @nivelJusante,
      @nivelReservatorio,
      @vazao,
      @vazaoAfluente,
      @vazaoDefluente,
      @vazaoVertedouro,
      @volumeReservatorio,
      @volumeArmazenado,
      @observacoes
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("usuarioId", sqlServer.NVarChar(64), data.usuarioId);
      request.input("dataLeitura", sqlServer.DateTime2, toDate(data.dataLeitura));
      request.input("dataHora", sqlServer.DateTime2, toDate(data.dataHora));
      request.input("nivelMontante", sqlServer.NVarChar(50), data.nivelMontante ?? null);
      request.input("nivelJusante", sqlServer.NVarChar(50), data.nivelJusante ?? null);
      request.input("nivelReservatorio", sqlServer.NVarChar(50), data.nivelReservatorio ?? null);
      request.input("vazao", sqlServer.NVarChar(50), data.vazao ?? null);
      request.input("vazaoAfluente", sqlServer.NVarChar(50), data.vazaoAfluente ?? null);
      request.input("vazaoDefluente", sqlServer.NVarChar(50), data.vazaoDefluente ?? null);
      request.input("vazaoVertedouro", sqlServer.NVarChar(50), data.vazaoVertedouro ?? null);
      request.input("volumeReservatorio", sqlServer.NVarChar(50), data.volumeReservatorio ?? null);
      request.input("volumeArmazenado", sqlServer.NVarChar(50), data.volumeArmazenado ?? null);
      request.input("observacoes", sqlServer.NVarChar(sqlServer.MAX), data.observacoes ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getHidrometriaByBarragem(barragemId: number, limit = 100) {
  const result = await runQuery<InsertHidrometria & { id: number }>(
    `SELECT TOP (@limit) *
     FROM dbo.hidrometria
     WHERE barragemId = @barragemId
     ORDER BY dataHora DESC`,
    (request) => {
      request.input("limit", sqlServer.Int, limit);
      request.input("barragemId", sqlServer.Int, barragemId);
    }
  );

  return result.recordset;
}

export async function getUltimaHidrometria(barragemId: number) {
  const result = await runQuery<InsertHidrometria & { id: number }>(
    `SELECT TOP 1 *
     FROM dbo.hidrometria
     WHERE barragemId = @barragemId
     ORDER BY dataHora DESC`,
    (request) => {
      request.input("barragemId", sqlServer.Int, barragemId);
    }
  );

  return result.recordset[0];
}

export async function updateHidrometria(id: number, data: Partial<InsertHidrometria>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    barragemId: { type: sqlServer.Int },
    usuarioId: { type: sqlServer.NVarChar(64) },
    dataLeitura: { type: sqlServer.DateTime2, transform: toDate },
    dataHora: { type: sqlServer.DateTime2, transform: toDate },
    nivelMontante: { type: sqlServer.NVarChar(50) },
    nivelJusante: { type: sqlServer.NVarChar(50) },
    nivelReservatorio: { type: sqlServer.NVarChar(50) },
    vazao: { type: sqlServer.NVarChar(50) },
    vazaoAfluente: { type: sqlServer.NVarChar(50) },
    vazaoDefluente: { type: sqlServer.NVarChar(50) },
    vazaoVertedouro: { type: sqlServer.NVarChar(50) },
    volumeReservatorio: { type: sqlServer.NVarChar(50) },
    volumeArmazenado: { type: sqlServer.NVarChar(50) },
    observacoes: { type: sqlServer.NVarChar(sqlServer.MAX) },
  });

  if (updates.length === 0) return;

  await runQuery(
    `UPDATE dbo.hidrometria SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteHidrometria(id: number) {
  await runQuery(
    "DELETE FROM dbo.hidrometria WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// DOCUMENTOS
// ============================================================================

export async function createDocumento(data: InsertDocumento) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.documentos (
      barragemId,
      usuarioId,
      tipo,
      categoria,
      titulo,
      descricao,
      arquivoUrl,
      arquivoNome,
      arquivoTamanho,
      arquivoTipo,
      versao,
      documentoPaiId,
      dataValidade,
      tags
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @usuarioId,
      @tipo,
      @categoria,
      @titulo,
      @descricao,
      @arquivoUrl,
      @arquivoNome,
      @arquivoTamanho,
      @arquivoTipo,
      @versao,
      @documentoPaiId,
      @dataValidade,
      @tags
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("usuarioId", sqlServer.NVarChar(64), data.usuarioId);
      request.input("tipo", sqlServer.NVarChar(100), data.tipo);
      request.input("categoria", sqlServer.NVarChar(100), data.categoria ?? null);
      request.input("titulo", sqlServer.NVarChar(255), data.titulo);
      request.input("descricao", sqlServer.NVarChar(sqlServer.MAX), data.descricao ?? null);
      request.input("arquivoUrl", sqlServer.NVarChar(500), data.arquivoUrl);
      request.input("arquivoNome", sqlServer.NVarChar(255), data.arquivoNome);
      request.input("arquivoTamanho", sqlServer.Int, data.arquivoTamanho ?? null);
      request.input("arquivoTipo", sqlServer.NVarChar(100), data.arquivoTipo ?? null);
      request.input("versao", sqlServer.NVarChar(50), data.versao ?? null);
      request.input("documentoPaiId", sqlServer.Int, data.documentoPaiId ?? null);
      request.input("dataValidade", sqlServer.DateTime2, toDate(data.dataValidade));
      request.input("tags", sqlServer.NVarChar(500), data.tags ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getDocumentosByBarragem(barragemId: number, tipo?: string) {
  const query = tipo
    ? `SELECT * FROM dbo.documentos
       WHERE barragemId = @barragemId AND tipo = @tipo
       ORDER BY createdAt DESC`
    : `SELECT * FROM dbo.documentos
       WHERE barragemId = @barragemId
       ORDER BY createdAt DESC`;

  const result = await runQuery<InsertDocumento & { id: number }>(
    query,
    (request) => {
      request.input("barragemId", sqlServer.Int, barragemId);
      if (tipo) {
        request.input("tipo", sqlServer.NVarChar(100), tipo);
      }
    }
  );

  return result.recordset;
}

export async function getDocumentoById(id: number) {
  const result = await runQuery<InsertDocumento & { id: number }>(
    `SELECT TOP 1 * FROM dbo.documentos WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );

  return result.recordset[0];
}

export async function updateDocumento(id: number, data: Partial<InsertDocumento>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    barragemId: { type: sqlServer.Int },
    usuarioId: { type: sqlServer.NVarChar(64) },
    tipo: { type: sqlServer.NVarChar(100) },
    categoria: { type: sqlServer.NVarChar(100) },
    titulo: { type: sqlServer.NVarChar(255) },
    descricao: { type: sqlServer.NVarChar(sqlServer.MAX) },
    arquivoUrl: { type: sqlServer.NVarChar(500) },
    arquivoNome: { type: sqlServer.NVarChar(255) },
    arquivoTamanho: { type: sqlServer.Int },
    arquivoTipo: { type: sqlServer.NVarChar(100) },
    versao: { type: sqlServer.NVarChar(50) },
    documentoPaiId: { type: sqlServer.Int },
    dataValidade: { type: sqlServer.DateTime2, transform: toDate },
    tags: { type: sqlServer.NVarChar(500) },
  });

  if (updates.length === 0) return;

  updates.push("updatedAt = SYSDATETIME()");

  await runQuery(
    `UPDATE dbo.documentos SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteDocumento(id: number) {
  await runQuery(
    "DELETE FROM dbo.documentos WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// MANUTENÇÕES
// ============================================================================

export async function createManutencao(data: InsertManutencao) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.manutencoes (
      barragemId,
      estruturaId,
      ocorrenciaId,
      tipo,
      titulo,
      descricao,
      dataProgramada,
      responsavel,
      dataInicio,
      dataConclusao,
      status,
      custoEstimado,
      custoReal,
      observacoes
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @estruturaId,
      @ocorrenciaId,
      @tipo,
      @titulo,
      @descricao,
      @dataProgramada,
      @responsavel,
      @dataInicio,
      @dataConclusao,
      COALESCE(@status, 'planejada'),
      @custoEstimado,
      @custoReal,
      @observacoes
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("estruturaId", sqlServer.Int, data.estruturaId ?? null);
      request.input("ocorrenciaId", sqlServer.Int, data.ocorrenciaId ?? null);
      request.input("tipo", sqlServer.NVarChar(32), data.tipo);
      request.input("titulo", sqlServer.NVarChar(255), data.titulo);
      request.input("descricao", sqlServer.NVarChar(sqlServer.MAX), data.descricao ?? null);
      request.input("dataProgramada", sqlServer.DateTime2, toDate(data.dataProgramada));
      request.input("responsavel", sqlServer.NVarChar(255), data.responsavel ?? null);
      request.input("dataInicio", sqlServer.DateTime2, toDate(data.dataInicio));
      request.input("dataConclusao", sqlServer.DateTime2, toDate(data.dataConclusao));
      request.input("status", sqlServer.NVarChar(32), data.status ?? null);
      request.input("custoEstimado", sqlServer.NVarChar(50), data.custoEstimado ?? null);
      request.input("custoReal", sqlServer.NVarChar(50), data.custoReal ?? null);
      request.input("observacoes", sqlServer.NVarChar(sqlServer.MAX), data.observacoes ?? null);
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getManutencoesByBarragem(barragemId: number, status?: string) {
  const query = status
    ? `SELECT * FROM dbo.manutencoes
       WHERE barragemId = @barragemId AND status = @status
       ORDER BY dataProgramada DESC`
    : `SELECT * FROM dbo.manutencoes
       WHERE barragemId = @barragemId
       ORDER BY dataProgramada DESC`;

  const result = await runQuery<InsertManutencao & { id: number }>(
    query,
    (request) => {
      request.input("barragemId", sqlServer.Int, barragemId);
      if (status) {
        request.input("status", sqlServer.NVarChar(32), status);
      }
    }
  );

  return result.recordset;
}

export async function updateManutencao(id: number, data: Partial<InsertManutencao>) {
  const { updates, apply } = buildUpdateFragments(data as Record<string, unknown>, {
    barragemId: { type: sqlServer.Int },
    estruturaId: { type: sqlServer.Int },
    ocorrenciaId: { type: sqlServer.Int },
    tipo: { type: sqlServer.NVarChar(32) },
    titulo: { type: sqlServer.NVarChar(255) },
    descricao: { type: sqlServer.NVarChar(sqlServer.MAX) },
    dataProgramada: { type: sqlServer.DateTime2, transform: toDate },
    responsavel: { type: sqlServer.NVarChar(255) },
    dataInicio: { type: sqlServer.DateTime2, transform: toDate },
    dataConclusao: { type: sqlServer.DateTime2, transform: toDate },
    status: { type: sqlServer.NVarChar(32) },
    custoEstimado: { type: sqlServer.NVarChar(50) },
    custoReal: { type: sqlServer.NVarChar(50) },
    observacoes: { type: sqlServer.NVarChar(sqlServer.MAX) },
  });

  if (updates.length === 0) return;

  updates.push("updatedAt = SYSDATETIME()");

  await runQuery(
    `UPDATE dbo.manutencoes SET ${updates.join(", ")} WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
      apply(request);
    }
  );
}

export async function deleteManutencao(id: number) {
  await runQuery(
    "DELETE FROM dbo.manutencoes WHERE id = @id",
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// ALERTAS
// ============================================================================

export async function createAlerta(data: InsertAlerta) {
  const result = await runQuery<{ id: number }>(
    `INSERT INTO dbo.alertas (
      barragemId,
      tipo,
      severidade,
      titulo,
      mensagem,
      instrumentoId,
      leituraId,
      ocorrenciaId,
      destinatarios,
      lido,
      dataLeitura,
      acaoTomada,
      dataAcao
    ) OUTPUT INSERTED.id VALUES (
      @barragemId,
      @tipo,
      @severidade,
      @titulo,
      @mensagem,
      @instrumentoId,
      @leituraId,
      @ocorrenciaId,
      @destinatarios,
      COALESCE(@lido, 0),
      @dataLeitura,
      @acaoTomada,
      @dataAcao
    );`,
    (request) => {
      request.input("barragemId", sqlServer.Int, data.barragemId);
      request.input("tipo", sqlServer.NVarChar(100), data.tipo);
      request.input("severidade", sqlServer.NVarChar(16), data.severidade);
      request.input("titulo", sqlServer.NVarChar(255), data.titulo);
      request.input("mensagem", sqlServer.NVarChar(sqlServer.MAX), data.mensagem);
      request.input("instrumentoId", sqlServer.Int, data.instrumentoId ?? null);
      request.input("leituraId", sqlServer.Int, data.leituraId ?? null);
      request.input("ocorrenciaId", sqlServer.Int, data.ocorrenciaId ?? null);
      request.input("destinatarios", sqlServer.NVarChar(sqlServer.MAX), data.destinatarios ?? null);
      request.input("lido", sqlServer.Bit, data.lido ?? null);
      request.input("dataLeitura", sqlServer.DateTime2, toDate(data.dataLeitura));
      request.input("acaoTomada", sqlServer.NVarChar(sqlServer.MAX), data.acaoTomada ?? null);
      request.input("dataAcao", sqlServer.DateTime2, toDate(data.dataAcao));
    }
  );

  return result.recordset[0]?.id ?? 0;
}

export async function getAlertasByBarragem(barragemId: number, lido?: boolean) {
  const query = lido !== undefined
    ? `SELECT * FROM dbo.alertas
       WHERE barragemId = @barragemId AND lido = @lido
       ORDER BY createdAt DESC`
    : `SELECT * FROM dbo.alertas
       WHERE barragemId = @barragemId
       ORDER BY createdAt DESC`;

  const result = await runQuery<InsertAlerta & { id: number }>(
    query,
    (request) => {
      request.input("barragemId", sqlServer.Int, barragemId);
      if (lido !== undefined) {
        request.input("lido", sqlServer.Bit, lido);
      }
    }
  );

  return result.recordset;
}

export async function marcarAlertaComoLido(id: number) {
  await runQuery(
    `UPDATE dbo.alertas SET lido = 1, dataLeitura = SYSDATETIME() WHERE id = @id`,
    (request) => {
      request.input("id", sqlServer.Int, id);
    }
  );
}

// ============================================================================
// AUDITORIA
// ============================================================================

export async function registrarAuditoria(data: InsertAuditoria) {
  try {
    await runQuery(
      `INSERT INTO dbo.auditoria (
        usuarioId,
        acao,
        entidade,
        entidadeId,
        detalhes,
        ip,
        userAgent
      ) VALUES (
        @usuarioId,
        @acao,
        @entidade,
        @entidadeId,
        @detalhes,
        @ip,
        @userAgent
      );`,
      (request) => {
        request.input("usuarioId", sqlServer.NVarChar(64), data.usuarioId ?? null);
        request.input("acao", sqlServer.NVarChar(100), data.acao);
        request.input("entidade", sqlServer.NVarChar(100), data.entidade);
        request.input("entidadeId", sqlServer.Int, data.entidadeId ?? null);
        request.input("detalhes", sqlServer.NVarChar(sqlServer.MAX), data.detalhes ?? null);
        request.input("ip", sqlServer.NVarChar(50), data.ip ?? null);
        request.input("userAgent", sqlServer.NVarChar(500), data.userAgent ?? null);
      }
    );
  } catch (error) {
    console.error("[Auditoria] Erro ao registrar:", error);
  }
}

// ============================================================================
// DASHBOARD E ESTATÍSTICAS
// ============================================================================

export async function getDashboardData(barragemId: number) {
  const [
    ultimasInconsistencias,
    ultimasOcorrenciasResult,
    ultimosChecklistsResult,
    ultimaHidro,
    alertasNaoLidosResult,
    totalInstrumentosResult,
    ocorrenciasPendentesResult,
  ] = await Promise.all([
    getLeiturasComInconsistencia(barragemId, 10),
    runQuery<InsertOcorrencia & { id: number }>(
      `SELECT TOP (10) *
       FROM dbo.ocorrencias
       WHERE barragemId = @barragemId
       ORDER BY dataHoraRegistro DESC`,
      (request) => {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    ),
    runQuery<InsertChecklist & { id: number }>(
      `SELECT TOP (5) *
       FROM dbo.checklists
       WHERE barragemId = @barragemId
       ORDER BY data DESC`,
      (request) => {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    ),
    getUltimaHidrometria(barragemId),
    runQuery<InsertAlerta & { id: number }>(
      `SELECT *
       FROM dbo.alertas
       WHERE barragemId = @barragemId AND lido = 0
       ORDER BY createdAt DESC`,
      (request) => {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    ),
    runQuery<{ count: number }>(
      `SELECT COUNT(*) AS count
       FROM dbo.instrumentos
       WHERE barragemId = @barragemId AND ativo = 1`,
      (request) => {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    ),
    runQuery<{ count: number }>(
      `SELECT COUNT(*) AS count
       FROM dbo.ocorrencias
       WHERE barragemId = @barragemId AND status = 'pendente'`,
      (request) => {
        request.input("barragemId", sqlServer.Int, barragemId);
      }
    ),
  ]);

  const alertasNaoLidos = alertasNaoLidosResult.recordset;

  return {
    ultimasInconsistencias,
    ultimasOcorrencias: ultimasOcorrenciasResult.recordset,
    ultimosChecklists: ultimosChecklistsResult.recordset,
    ultimaHidrometria: ultimaHidro ?? null,
    alertasNaoLidos,
    estatisticas: {
      totalInstrumentos: totalInstrumentosResult.recordset[0]?.count ?? 0,
      ocorrenciasPendentes: ocorrenciasPendentesResult.recordset[0]?.count ?? 0,
      alertasNaoLidos: alertasNaoLidos.length,
    },
  };
}

