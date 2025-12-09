import DashboardLayout from "@/components/DashboardLayout";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Textarea } from "@/components/ui/textarea";
import { trpc } from "@/lib/trpc";
import { Activity, Edit, Gauge, Plus, Trash2 } from "lucide-react";
import { useState } from "react";
import { toast } from "sonner";

export default function Instrumentos() {
  const [dialogOpen, setDialogOpen] = useState(false);
  const [leituraDialogOpen, setLeituraDialogOpen] = useState(false);
  const [editingInstrumento, setEditingInstrumento] = useState<any>(null);
  const [selectedInstrumento, setSelectedInstrumento] = useState<any>(null);
  const [selectedBarragem, setSelectedBarragem] = useState<number | null>(null);

  // Queries
  const { data: barragens } = trpc.barragens.list.useQuery();
  const { data: instrumentos, refetch: refetchInstrumentos } = trpc.instrumentos.list.useQuery(
    { barragemId: selectedBarragem! },
    { enabled: !!selectedBarragem }
  );

  // Mutations
  const createInstrumento = trpc.instrumentos.create.useMutation();
  const updateInstrumento = trpc.instrumentos.update.useMutation();
  const deleteInstrumento = trpc.instrumentos.delete.useMutation();
  const createLeitura = trpc.instrumentos.createLeitura.useMutation();

  // Form states
  const [instrumentoForm, setInstrumentoForm] = useState({
    codigo: "",
    tipo: "",
    localizacao: "",
    coordenadas: "",
    dataInstalacao: "",
    unidadeMedida: "",
    limiteInferior: "",
    limiteSuperior: "",
    frequenciaLeitura: "",
    responsavel: "",
    observacoes: "",
    status: "ativo" as "ativo" | "inativo" | "manutencao",
  });

  const [leituraForm, setLeituraForm] = useState({
    valor: "",
    observacoes: "",
  });

  // Selecionar primeira barragem automaticamente
  if (!selectedBarragem && barragens && barragens.length > 0) {
    setSelectedBarragem(barragens[0].id);
  }

  const handleSubmitInstrumento = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedBarragem) return;

    try {
      const data = {
        ...instrumentoForm,
        barragemId: selectedBarragem,
        dataInstalacao: instrumentoForm.dataInstalacao || undefined,
      };

      if (editingInstrumento) {
        await updateInstrumento.mutateAsync({
          id: editingInstrumento.id,
          data,
        });
        toast.success("Instrumento atualizado com sucesso!");
      } else {
        await createInstrumento.mutateAsync(data);
        toast.success("Instrumento cadastrado com sucesso!");
      }

      setDialogOpen(false);
      setEditingInstrumento(null);
      resetInstrumentoForm();
      refetchInstrumentos();
    } catch (error: any) {
      toast.error(error.message || "Erro ao salvar instrumento");
    }
  };

  const handleSubmitLeitura = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedInstrumento) return;

    try {
      await createLeitura.mutateAsync({
        instrumentoId: selectedInstrumento.id,
        valor: leituraForm.valor,
        dataHora: new Date(),
        observacoes: leituraForm.observacoes || undefined,
      });

      toast.success("Leitura registrada com sucesso!");
      setLeituraDialogOpen(false);
      resetLeituraForm();
    } catch (error: any) {
      toast.error(error.message || "Erro ao registrar leitura");
    }
  };

  const handleEdit = (instrumento: any) => {
    setEditingInstrumento(instrumento);
    setInstrumentoForm({
      codigo: instrumento.codigo || "",
      tipo: instrumento.tipo || "",
      localizacao: instrumento.localizacao || "",
      coordenadas: instrumento.coordenadas || "",
      dataInstalacao: instrumento.dataInstalacao
        ? new Date(instrumento.dataInstalacao).toISOString().split("T")[0]
        : "",
      unidadeMedida: instrumento.unidadeMedida || "",
      limiteInferior: instrumento.limiteInferior || "",
      limiteSuperior: instrumento.limiteSuperior || "",
      frequenciaLeitura: instrumento.frequenciaLeitura || "",
      responsavel: instrumento.responsavel || "",
      observacoes: instrumento.observacoes || "",
      status: instrumento.status || "ativo",
    });
    setDialogOpen(true);
  };

  const handleDelete = async (id: number) => {
    if (!confirm("Tem certeza que deseja excluir este instrumento?")) return;

    try {
      await deleteInstrumento.mutateAsync({ id });
      toast.success("Instrumento excluído com sucesso!");
      refetchInstrumentos();
    } catch (error: any) {
      toast.error(error.message || "Erro ao excluir instrumento");
    }
  };

  const resetInstrumentoForm = () => {
    setInstrumentoForm({
      codigo: "",
      tipo: "",
      localizacao: "",
      coordenadas: "",
      dataInstalacao: "",
      unidadeMedida: "",
      limiteInferior: "",
      limiteSuperior: "",
      frequenciaLeitura: "",
      responsavel: "",
      observacoes: "",
      status: "ativo",
    });
  };

  const resetLeituraForm = () => {
    setLeituraForm({
      valor: "",
      observacoes: "",
    });
  };

  return (
    <DashboardLayout>
      <div className="space-y-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-3xl font-bold tracking-tight">Instrumentação</h1>
            <p className="text-muted-foreground mt-1">Gerenciamento de instrumentos e leituras</p>
          </div>
        </div>

        {/* Seletor de Barragem */}
        <Card>
          <CardHeader>
            <CardTitle>Selecione uma Barragem</CardTitle>
          </CardHeader>
          <CardContent>
            <Select
              value={selectedBarragem?.toString()}
              onValueChange={(value) => setSelectedBarragem(parseInt(value))}
            >
              <SelectTrigger>
                <SelectValue placeholder="Selecione uma barragem" />
              </SelectTrigger>
              <SelectContent>
                {barragens?.map((barragem: any) => (
                  <SelectItem key={barragem.id} value={barragem.id.toString()}>
                    {barragem.nome}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </CardContent>
        </Card>

        {selectedBarragem && (
          <>
            <div className="flex justify-end">
              <Dialog
                open={dialogOpen}
                onOpenChange={(open) => {
                  setDialogOpen(open);
                  if (!open) {
                    setEditingInstrumento(null);
                    resetInstrumentoForm();
                  }
                }}
              >
                <DialogTrigger asChild>
                  <Button>
                    <Plus className="h-4 w-4 mr-2" />
                    Novo Instrumento
                  </Button>
                </DialogTrigger>
                <DialogContent className="max-w-2xl max-h-[90vh] overflow-y-auto">
                  <DialogHeader>
                    <DialogTitle>
                      {editingInstrumento ? "Editar Instrumento" : "Novo Instrumento"}
                    </DialogTitle>
                    <DialogDescription>Preencha os dados do instrumento</DialogDescription>
                  </DialogHeader>
                  <form onSubmit={handleSubmitInstrumento} className="space-y-4">
                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <Label htmlFor="codigo">Código *</Label>
                        <Input
                          id="codigo"
                          value={instrumentoForm.codigo}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, codigo: e.target.value })
                          }
                          required
                        />
                      </div>
                      <div>
                        <Label htmlFor="tipo">Tipo *</Label>
                        <Select
                          value={instrumentoForm.tipo}
                          onValueChange={(value) =>
                            setInstrumentoForm({ ...instrumentoForm, tipo: value })
                          }
                        >
                          <SelectTrigger>
                            <SelectValue placeholder="Selecione" />
                          </SelectTrigger>
                          <SelectContent>
                            <SelectItem value="Piezômetro">Piezômetro</SelectItem>
                            <SelectItem value="Medidor de Vazão">Medidor de Vazão</SelectItem>
                            <SelectItem value="Marco Superficial">Marco Superficial</SelectItem>
                            <SelectItem value="Medidor de Nível">Medidor de Nível</SelectItem>
                            <SelectItem value="Pluviômetro">Pluviômetro</SelectItem>
                            <SelectItem value="Inclinômetro">Inclinômetro</SelectItem>
                            <SelectItem value="Outro">Outro</SelectItem>
                          </SelectContent>
                        </Select>
                      </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <Label htmlFor="localizacao">Localização</Label>
                        <Input
                          id="localizacao"
                          value={instrumentoForm.localizacao}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, localizacao: e.target.value })
                          }
                        />
                      </div>
                      <div>
                        <Label htmlFor="coordenadas">Coordenadas</Label>
                        <Input
                          id="coordenadas"
                          value={instrumentoForm.coordenadas}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, coordenadas: e.target.value })
                          }
                          placeholder="Lat, Long"
                        />
                      </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <Label htmlFor="dataInstalacao">Data de Instalação</Label>
                        <Input
                          id="dataInstalacao"
                          type="date"
                          value={instrumentoForm.dataInstalacao}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, dataInstalacao: e.target.value })
                          }
                        />
                      </div>
                      <div>
                        <Label htmlFor="unidadeMedida">Unidade de Medida</Label>
                        <Input
                          id="unidadeMedida"
                          value={instrumentoForm.unidadeMedida}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, unidadeMedida: e.target.value })
                          }
                          placeholder="Ex: m, m³/s, mm"
                        />
                      </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <Label htmlFor="limiteInferior">Limite Inferior</Label>
                        <Input
                          id="limiteInferior"
                          value={instrumentoForm.limiteInferior}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, limiteInferior: e.target.value })
                          }
                        />
                      </div>
                      <div>
                        <Label htmlFor="limiteSuperior">Limite Superior</Label>
                        <Input
                          id="limiteSuperior"
                          value={instrumentoForm.limiteSuperior}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, limiteSuperior: e.target.value })
                          }
                        />
                      </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <Label htmlFor="frequenciaLeitura">Frequência de Leitura</Label>
                        <Input
                          id="frequenciaLeitura"
                          value={instrumentoForm.frequenciaLeitura}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, frequenciaLeitura: e.target.value })
                          }
                          placeholder="Ex: Diária, Semanal"
                        />
                      </div>
                      <div>
                        <Label htmlFor="responsavel">Responsável</Label>
                        <Input
                          id="responsavel"
                          value={instrumentoForm.responsavel}
                          onChange={(e) =>
                            setInstrumentoForm({ ...instrumentoForm, responsavel: e.target.value })
                          }
                        />
                      </div>
                    </div>

                    <div>
                      <Label htmlFor="status">Status</Label>
                      <Select
                        value={instrumentoForm.status}
                        onValueChange={(value: any) =>
                          setInstrumentoForm({ ...instrumentoForm, status: value })
                        }
                      >
                        <SelectTrigger>
                          <SelectValue />
                        </SelectTrigger>
                        <SelectContent>
                          <SelectItem value="ativo">Ativo</SelectItem>
                          <SelectItem value="inativo">Inativo</SelectItem>
                          <SelectItem value="manutencao">Em Manutenção</SelectItem>
                        </SelectContent>
                      </Select>
                    </div>

                    <div>
                      <Label htmlFor="observacoes">Observações</Label>
                      <Textarea
                        id="observacoes"
                        value={instrumentoForm.observacoes}
                        onChange={(e) =>
                          setInstrumentoForm({ ...instrumentoForm, observacoes: e.target.value })
                        }
                        rows={3}
                      />
                    </div>

                    <DialogFooter>
                      <Button type="button" variant="outline" onClick={() => setDialogOpen(false)}>
                        Cancelar
                      </Button>
                      <Button
                        type="submit"
                        disabled={createInstrumento.isPending || updateInstrumento.isPending}
                      >
                        {createInstrumento.isPending || updateInstrumento.isPending
                          ? "Salvando..."
                          : "Salvar"}
                      </Button>
                    </DialogFooter>
                  </form>
                </DialogContent>
              </Dialog>
            </div>

            {/* Lista de Instrumentos */}
            {instrumentos && instrumentos.length > 0 ? (
              <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {instrumentos.map((instrumento: any) => (
                  <Card key={instrumento.id} className="hover:shadow-lg transition-shadow">
                    <CardHeader>
                      <div className="flex items-start justify-between">
                        <div className="flex-1">
                          <CardTitle className="flex items-center gap-2">
                            <Gauge className="h-5 w-5" />
                            {instrumento.codigo}
                          </CardTitle>
                          <CardDescription className="mt-1">{instrumento.tipo}</CardDescription>
                        </div>
                        <span
                          className={`text-xs px-2 py-1 rounded-full ${
                            instrumento.status === "ativo"
                              ? "bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-200"
                              : instrumento.status === "manutencao"
                                ? "bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-200"
                                : "bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-200"
                          }`}
                        >
                          {instrumento.status}
                        </span>
                      </div>
                    </CardHeader>
                    <CardContent>
                      <div className="space-y-2 text-sm">
                        {instrumento.localizacao && (
                          <div>
                            <span className="font-medium">Local:</span> {instrumento.localizacao}
                          </div>
                        )}
                        {instrumento.unidadeMedida && (
                          <div>
                            <span className="font-medium">Unidade:</span> {instrumento.unidadeMedida}
                          </div>
                        )}
                        {instrumento.frequenciaLeitura && (
                          <div>
                            <span className="font-medium">Frequência:</span>{" "}
                            {instrumento.frequenciaLeitura}
                          </div>
                        )}
                      </div>
                      <div className="flex gap-2 mt-4">
                        <Button
                          variant="outline"
                          size="sm"
                          className="flex-1"
                          onClick={() => {
                            setSelectedInstrumento(instrumento);
                            setLeituraDialogOpen(true);
                          }}
                        >
                          <Activity className="h-4 w-4 mr-1" />
                          Leitura
                        </Button>
                        <Button variant="outline" size="sm" onClick={() => handleEdit(instrumento)}>
                          <Edit className="h-4 w-4" />
                        </Button>
                        <Button
                          variant="outline"
                          size="sm"
                          className="text-destructive hover:bg-destructive hover:text-destructive-foreground"
                          onClick={() => handleDelete(instrumento.id)}
                        >
                          <Trash2 className="h-4 w-4" />
                        </Button>
                      </div>
                    </CardContent>
                  </Card>
                ))}
              </div>
            ) : (
              <Card>
                <CardContent className="text-center py-12">
                  <Gauge className="h-16 w-16 text-muted-foreground mx-auto mb-4" />
                  <p className="text-xl font-medium mb-2">Nenhum instrumento cadastrado</p>
                  <p className="text-muted-foreground mb-4">Comece cadastrando o primeiro instrumento</p>
                  <Button onClick={() => setDialogOpen(true)}>
                    <Plus className="h-4 w-4 mr-2" />
                    Cadastrar Instrumento
                  </Button>
                </CardContent>
              </Card>
            )}
          </>
        )}

        {/* Dialog de Registro de Leitura */}
        <Dialog open={leituraDialogOpen} onOpenChange={setLeituraDialogOpen}>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Registrar Leitura</DialogTitle>
              <DialogDescription>
                {selectedInstrumento?.codigo} - {selectedInstrumento?.tipo}
              </DialogDescription>
            </DialogHeader>
            <form onSubmit={handleSubmitLeitura} className="space-y-4">
              <div>
                <Label htmlFor="valor">Valor da Leitura *</Label>
                <Input
                  id="valor"
                  type="text"
                  value={leituraForm.valor}
                  onChange={(e) => setLeituraForm({ ...leituraForm, valor: e.target.value })}
                  required
                  placeholder={`Valor em ${selectedInstrumento?.unidadeMedida || ""}`}
                />
              </div>
              <div>
                <Label htmlFor="observacoesLeitura">Observações</Label>
                <Textarea
                  id="observacoesLeitura"
                  value={leituraForm.observacoes}
                  onChange={(e) => setLeituraForm({ ...leituraForm, observacoes: e.target.value })}
                  rows={3}
                />
              </div>
              <DialogFooter>
                <Button type="button" variant="outline" onClick={() => setLeituraDialogOpen(false)}>
                  Cancelar
                </Button>
                <Button type="submit" disabled={createLeitura.isPending}>
                  {createLeitura.isPending ? "Salvando..." : "Salvar Leitura"}
                </Button>
              </DialogFooter>
            </form>
          </DialogContent>
        </Dialog>
      </div>
    </DashboardLayout>
  );
}

