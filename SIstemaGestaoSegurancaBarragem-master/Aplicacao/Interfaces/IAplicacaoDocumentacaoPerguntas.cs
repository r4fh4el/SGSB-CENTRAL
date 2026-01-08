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
    public interface IAplicacaoDocumentacaoPerguntas : IGenericaAplicacao<DocumentacaoPerguntas>
    {
        Task AdicionarBarragem(DocumentacaoPerguntas documentacaoPerguntas);
        Task AtualizaBarragem(DocumentacaoPerguntas documentacaoPerguntas);
        Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas();
    }
}
