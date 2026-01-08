using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoDocumentacaoPerguntas
    {
        Task AdicionarDocumentacaoPerguntas(DocumentacaoPerguntas tipoMaterialBarragem);
        Task AtualizaDocumentacaoPerguntas(DocumentacaoPerguntas tipoMaterialBarragem);
        Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas();
    }
}
