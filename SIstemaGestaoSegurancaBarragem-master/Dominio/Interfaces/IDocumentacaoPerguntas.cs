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
    public interface IDocumentacaoPerguntas : IGenericos<DocumentacaoPerguntas>
    {
        Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas(Expression<Func<DocumentacaoPerguntas, bool>> exDocumentacaoPerguntas);
    }
}
