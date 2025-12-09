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
import { Textarea } from "@/components/ui/textarea";
import { trpc } from "@/lib/trpc";
import { Download, Edit, FileText, Plus, Trash2, Upload } from "lucide-react";
import { useState } from "react";
import { toast } from "sonner";

export default function Documentos() {
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingDocumento, setEditingDocumento] = useState<any>(null);
  const [selectedBarragem, setSelectedBarragem] = useState<number | null>(null);
  const [uploading, setUploading] = useState(false);
  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  const { data: barragens } = trpc.barragens.list.useQuery();
  const { data: documentos, refetch: refetchDocumentos } = trpc.documentos.listByBarragem.useQuery(
    { barragemId: selectedBarragem! },
    { enabled: !!selectedBarragem }
  );

  const createDocumento = trpc.documentos.create.useMutation();
  const updateDocumento = trpc.documentos.update.useMutation();
  const deleteDocumento = trpc.documentos.delete.useMutation();
  const uploadDocumento = trpc.documentos.upload.useMutation();

  const [documentoForm, setDocumentoForm] = useState({
    titulo: "",
    tipo: "",
    descricao: "",
  });

  if (!selectedBarragem && barragens && barragens.length > 0) {
    setSelectedBarragem(barragens[0].id);
  }

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setSelectedFile(e.target.files[0]);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedBarragem) return;

    try {
      setUploading(true);

      let arquivoUrl = editingDocumento?.arquivoUrl || "";
      let nomeArquivo = editingDocumento?.arquivoNome || "";
      let tamanhoArquivo = editingDocumento?.arquivoTamanho || 0;

      // Se há arquivo novo, fazer upload
      if (selectedFile) {
        const reader = new FileReader();
        reader.onload = async (event) => {
          const base64 = event.target?.result as string;
          
          const uploadResult = await uploadDocumento.mutateAsync({
            fileName: selectedFile.name,
            fileData: base64,
            contentType: selectedFile.type,
          });

          arquivoUrl = uploadResult.url;
          nomeArquivo = selectedFile.name;
          tamanhoArquivo = selectedFile.size;

          // Salvar documento
          await saveDocumento(arquivoUrl, nomeArquivo, tamanhoArquivo);
        };
        reader.readAsDataURL(selectedFile);
      } else {
        // Salvar sem novo arquivo
        await saveDocumento(arquivoUrl, nomeArquivo, tamanhoArquivo);
      }
    } catch (error: any) {
      toast.error(error.message || "Erro ao salvar documento");
      setUploading(false);
    }
  };

  const saveDocumento = async (arquivoUrl: string, nomeArquivo: string, tamanhoArquivo: number) => {
    try {
      const data = {
        ...documentoForm,
        barragemId: selectedBarragem!,
        arquivoUrl,
        arquivoNome: nomeArquivo,
        arquivoTamanho: tamanhoArquivo,
      };

      if (editingDocumento) {
        await updateDocumento.mutateAsync({ id: editingDocumento.id, data: data as any });
        toast.success("Documento atualizado!");
      } else {
        await createDocumento.mutateAsync(data as any);
        toast.success("Documento enviado!");
      }

      setDialogOpen(false);
      setEditingDocumento(null);
      resetForm();
      setSelectedFile(null);
      refetchDocumentos();
    } finally {
      setUploading(false);
    }
  };

  const handleEdit = (doc: any) => {
    setEditingDocumento(doc);
    setDocumentoForm({
      titulo: doc.titulo || "",
      tipo: doc.tipo || "",
      descricao: doc.descricao || "",
    });
    setDialogOpen(true);
  };

  const handleDelete = async (id: number) => {
    if (!confirm("Excluir este documento?")) return;
    try {
      await deleteDocumento.mutateAsync({ id });
      toast.success("Documento excluído!");
      refetchDocumentos();
    } catch (error: any) {
      toast.error(error.message || "Erro ao excluir");
    }
  };

  const resetForm = () => {
    setDocumentoForm({
      titulo: "",
      tipo: "",
      descricao: "",
    });
  };

  const formatFileSize = (bytes: number) => {
    if (bytes === 0) return "0 Bytes";
    const k = 1024;
    const sizes = ["Bytes", "KB", "MB", "GB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round(bytes / Math.pow(k, i) * 100) / 100 + " " + sizes[i];
  };

  const getTipoColor = (tipo: string) => {
    const colors: Record<string, string> = {
      "Projeto": "bg-blue-100 text-blue-800",
      "Relatório": "bg-green-100 text-green-800",
      "Laudo": "bg-purple-100 text-purple-800",
      "PAE": "bg-red-100 text-red-800",
      "PSB": "bg-orange-100 text-orange-800",
      "Licença": "bg-yellow-100 text-yellow-800",
      "Outro": "bg-gray-100 text-gray-800",
    };
    return colors[tipo] || colors["Outro"];
  };

  return (
    <DashboardLayout>
      <div className="space-y-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-3xl font-bold tracking-tight">Documentos</h1>
            <p className="text-muted-foreground mt-1">Gestão de documentos técnicos</p>
          </div>
        </div>

        <Card>
          <CardHeader>
            <CardTitle>Selecione uma Barragem</CardTitle>
          </CardHeader>
          <CardContent>
            <Select value={selectedBarragem?.toString()} onValueChange={(v) => setSelectedBarragem(parseInt(v))}>
              <SelectTrigger>
                <SelectValue placeholder="Selecione uma barragem" />
              </SelectTrigger>
              <SelectContent>
                {barragens?.map((b: any) => (
                  <SelectItem key={b.id} value={b.id.toString()}>
                    {b.nome}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </CardContent>
        </Card>

        {selectedBarragem && (
          <>
            <div className="flex justify-end">
              <Dialog open={dialogOpen} onOpenChange={(open) => {
                setDialogOpen(open);
                if (!open) { setEditingDocumento(null); resetForm(); setSelectedFile(null); }
              }}>
                <DialogTrigger asChild>
                  <Button><Upload className="h-4 w-4 mr-2" />Enviar Documento</Button>
                </DialogTrigger>
                <DialogContent className="max-w-2xl">
                  <DialogHeader>
                    <DialogTitle>{editingDocumento ? "Editar" : "Enviar"} Documento</DialogTitle>
                    <DialogDescription>Preencha os dados do documento</DialogDescription>
                  </DialogHeader>
                  <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                      <Label>Título *</Label>
                      <Input value={documentoForm.titulo} onChange={(e) => setDocumentoForm({...documentoForm, titulo: e.target.value})} required />
                    </div>
                    <div>
                      <Label>Tipo *</Label>
                      <Select value={documentoForm.tipo} onValueChange={(v) => setDocumentoForm({...documentoForm, tipo: v})} required>
                        <SelectTrigger>
                          <SelectValue placeholder="Selecione o tipo" />
                        </SelectTrigger>
                        <SelectContent>
                          <SelectItem value="Projeto">Projeto</SelectItem>
                          <SelectItem value="Relatório">Relatório</SelectItem>
                          <SelectItem value="Laudo">Laudo</SelectItem>
                          <SelectItem value="PAE">PAE - Plano de Ação de Emergência</SelectItem>
                          <SelectItem value="PSB">PSB - Plano de Segurança da Barragem</SelectItem>
                          <SelectItem value="Licença">Licença Ambiental</SelectItem>
                          <SelectItem value="Outro">Outro</SelectItem>
                        </SelectContent>
                      </Select>
                    </div>
                    {!editingDocumento && (
                      <div>
                        <Label>Arquivo *</Label>
                        <Input type="file" onChange={handleFileChange} required />
                        {selectedFile && (
                          <p className="text-sm text-muted-foreground mt-1">
                            {selectedFile.name} ({formatFileSize(selectedFile.size)})
                          </p>
                        )}
                      </div>
                    )}
                    <div>
                      <Label>Descrição</Label>
                      <Textarea rows={3} value={documentoForm.descricao} onChange={(e) => setDocumentoForm({...documentoForm, descricao: e.target.value})} />
                    </div>
                    <DialogFooter>
                      <Button type="button" variant="outline" onClick={() => setDialogOpen(false)} disabled={uploading}>Cancelar</Button>
                      <Button type="submit" disabled={uploading || createDocumento.isPending || updateDocumento.isPending}>
                        {uploading ? "Enviando..." : "Salvar"}
                      </Button>
                    </DialogFooter>
                  </form>
                </DialogContent>
              </Dialog>
            </div>

            {documentos && documentos.length > 0 ? (
              <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {documentos.map((doc: any) => (
                  <Card key={doc.id} className="hover:shadow-lg transition-shadow">
                    <CardHeader>
                      <div className="flex items-start justify-between">
                        <FileText className="h-10 w-10 text-primary" />
                        <span className={`text-xs px-2 py-1 rounded-full ${getTipoColor(doc.tipo)}`}>
                          {doc.tipo}
                        </span>
                      </div>
                      <CardTitle className="text-lg mt-2">{doc.titulo}</CardTitle>
                      <CardDescription>
                        {doc.arquivoNome} • {formatFileSize(doc.arquivoTamanho || 0)}
                      </CardDescription>
                    </CardHeader>
                    <CardContent>
                      {doc.descricao && (
                        <p className="text-sm text-muted-foreground mb-4 line-clamp-2">{doc.descricao}</p>
                      )}
                      <div className="text-xs text-muted-foreground mb-4">
                        Enviado em {doc.createdAt ? new Date(doc.createdAt).toLocaleDateString("pt-BR") : "-"}
                      </div>
                      <div className="flex gap-2">
                        <Button variant="outline" size="sm" className="flex-1" asChild>
                          <a href={doc.arquivoUrl} target="_blank" rel="noopener noreferrer">
                            <Download className="h-4 w-4 mr-1" />
                            Baixar
                          </a>
                        </Button>
                        <Button variant="outline" size="sm" onClick={() => handleEdit(doc)}>
                          <Edit className="h-4 w-4" />
                        </Button>
                        <Button variant="outline" size="sm" className="text-destructive" onClick={() => handleDelete(doc.id)}>
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
                  <FileText className="h-16 w-16 text-muted-foreground mx-auto mb-4" />
                  <p className="text-xl font-medium mb-2">Nenhum documento</p>
                  <Button onClick={() => setDialogOpen(true)}>
                    <Upload className="h-4 w-4 mr-2" />Enviar Documento
                  </Button>
                </CardContent>
              </Card>
            )}
          </>
        )}
      </div>
    </DashboardLayout>
  );
}

