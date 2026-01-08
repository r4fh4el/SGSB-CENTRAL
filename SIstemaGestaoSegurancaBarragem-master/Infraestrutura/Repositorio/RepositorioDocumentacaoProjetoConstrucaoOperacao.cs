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
    public class RepositorioDocumentacaoProjetoConstrucaoOperacao : RepositorioGenerico<DocumentacaoProjetoConstrucaoOperacao>, IDocumentacaoProjetoConstrucaoOperacao
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioDocumentacaoProjetoConstrucaoOperacao() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao(Expression<Func<DocumentacaoProjetoConstrucaoOperacao, bool>> exDocumentacaoProjetoConstrucaoOperacao)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DocumentacaoProjetoConstrucaoOperacao.Where(exDocumentacaoProjetoConstrucaoOperacao).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DocumentacaoProjetoConstrucaoOperacao.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}

