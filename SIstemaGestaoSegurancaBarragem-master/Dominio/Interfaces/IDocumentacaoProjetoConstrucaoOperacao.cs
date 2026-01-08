using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IDocumentacaoProjetoConstrucaoOperacao : IGenericos<DocumentacaoProjetoConstrucaoOperacao>
    {
        Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao(Expression<Func<DocumentacaoProjetoConstrucaoOperacao, bool>> exDocumentacaoProjetoConstrucaoOperacao);
        Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem);
    }
}
