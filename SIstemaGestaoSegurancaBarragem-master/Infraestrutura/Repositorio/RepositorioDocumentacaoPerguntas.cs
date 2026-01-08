using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioDocumentacaoPerguntas : RepositorioGenerico<DocumentacaoPerguntas>, IDocumentacaoPerguntas
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioDocumentacaoPerguntas() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas(Expression<Func<DocumentacaoPerguntas, bool>> exDocumentacaoPerguntas)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DocumentacaoPerguntas.Where(exDocumentacaoPerguntas).AsNoTracking().ToListAsync();
            }
        }
    }
}
