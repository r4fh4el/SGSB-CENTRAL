using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoDocumentacaoProjetoConstrucaoOperacao : IGenericaAplicacao<DocumentacaoProjetoConstrucaoOperacao>
    {
        Task AdicionarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao);
        Task AtualizaDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao);
        Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao();
        Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem);
    }
}
