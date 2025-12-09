import sql from "mssql/msnodesqlv8.js";
import type {
  config as SqlConfig,
  ConnectionPool,
  IResult as SqlResult,
  Request as SqlRequest,
  Transaction as SqlTransaction,
} from "mssql";

declare global {
  // eslint-disable-next-line no-var
  var __SQL_POOL__: ConnectionPool | undefined;
}

const sanitizeHost = (value: string): string => {
  const trimmed = value.trim();
  return /^\(local\)$/i.test(trimmed) ? "localhost" : trimmed;
};

const serverInput = process.env.SQLSERVER_SERVER ?? "localhost";
const instanceInput = process.env.SQLSERVER_INSTANCE;

const [inputHost, inputInstance] = serverInput.split("\\", 2);
const hostPart = sanitizeHost(inputHost ?? serverInput);
const instancePart = instanceInput ?? inputInstance ?? "SQLEXPRESS";
const rawServer = `${hostPart}\\${instancePart}`;
const database = process.env.SQLSERVER_DATABASE ?? "sgsb";
const trustedConnection = (process.env.SQLSERVER_TRUSTED_CONNECTION ?? "true") === "true";
const user = process.env.SQLSERVER_USER;
const password = process.env.SQLSERVER_PASSWORD;
const domain = process.env.SQLSERVER_DOMAIN;
const driver = process.env.SQLSERVER_DRIVER ?? "msnodesqlv8";
const odbcDriver =
  process.env.SQLSERVER_ODBC_DRIVER ?? "ODBC Driver 17 for SQL Server";

function formatSqlError(error: unknown) {
  if (error instanceof Error) {
    const rawMessage = (error as any).message;
    const formatted: Record<string, unknown> = {
      name: error.name,
      message:
        typeof rawMessage === "object" ? JSON.stringify(rawMessage) : rawMessage,
      code: (error as any).code,
    };

    const originalError = (error as any).originalError;
    if (originalError) {
      formatted.originalError =
        typeof originalError === "object"
          ? JSON.stringify(originalError)
          : originalError;
      if (originalError?.message) {
        formatted.originalMessage = originalError.message;
      }
    }

    return formatted;
  }

  if (typeof error === "object") {
    try {
      return JSON.stringify(error);
    } catch (_err) {
      return String(error);
    }
  }

  return error;
}

const baseOptions: SqlConfig["options"] = {
  trustServerCertificate: true,
};

let config: SqlConfig;

const connectionString =
  `Server=${rawServer};` +
  `Database=${database};` +
  `Driver={${odbcDriver}};` +
  "Encrypt=Yes;TrustServerCertificate=Yes;" +
  (trustedConnection
    ? "Trusted_Connection=Yes;"
    : user && password
    ? `Uid=${user};Pwd=${password};`
    : "");

if (driver === "msnodesqlv8") {
  config = {
    connectionString,
    options: {},
  } as SqlConfig;
} else {
  config = {
    server: hostPart,
    database,
    driver,
    options: {
      ...baseOptions,
      encrypt: false,
      instanceName: instancePart,
    },
    ...(trustedConnection
      ? {
          authentication: {
            type: "ntlm",
            options: {
              domain: domain ?? "",
              userName: user ?? "",
              password: password ?? "",
            },
          },
        }
      : {
          user,
          password,
        }),
  };
}

function createPool() {
  const pool = new sql.ConnectionPool(config);
  const poolConnect = pool
    .connect()
    .then(() => {
      console.log(`[SQL Server] Connected to ${rawServer}/${database}`);
      return pool;
    })
    .catch((error: unknown) => {
      console.error("[SQL Server] Failed to connect", formatSqlError(error));
      console.error("[SQL Server] Raw error details:", error);
      throw error;
    });

  pool.on("error", (error: Error) => {
    console.error("[SQL Server] Connection pool error", error);
  });

  return poolConnect;
}

let poolPromise: Promise<ConnectionPool> | undefined;

export async function getSqlPool(): Promise<ConnectionPool> {
  if (!poolPromise) {
    if (process.env.NODE_ENV !== "production" && globalThis.__SQL_POOL__) {
      poolPromise = Promise.resolve(globalThis.__SQL_POOL__);
    } else {
      poolPromise = createPool();
      if (process.env.NODE_ENV !== "production") {
        poolPromise!.then((pool) => {
          globalThis.__SQL_POOL__ = pool;
        });
      }
    }
  }
  return poolPromise!;
}

export async function runQuery<T = unknown>(
  query: string,
  input?: (request: SqlRequest) => void
): Promise<SqlResult<T>> {
  const pool = await getSqlPool();
  const request = pool.request();
  if (input) {
    input(request);
  }
  return request.query(query) as SqlResult<T>;
}

export type { SqlRequest, SqlTransaction };

