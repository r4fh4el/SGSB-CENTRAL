using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoDocumentacaoProjetoConstrucaoOperacao
    {
        Task AdicionarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao);
        Task AtualizaDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao);
        Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao();
        Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem);
    }
}
