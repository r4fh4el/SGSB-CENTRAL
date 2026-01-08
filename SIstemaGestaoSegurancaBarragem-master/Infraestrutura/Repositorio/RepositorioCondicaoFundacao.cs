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
    public class RepositorioCondicaoFundacao : RepositorioGenerico<CondicaoFundacao>, ICondicaoFundacao
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioCondicaoFundacao() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<CondicaoFundacao>> ListarCondicaoFundacao(Expression<Func<CondicaoFundacao, bool>> exCondicaoFundacaos)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.CondicaoFundacao.Where(exCondicaoFundacaos).AsNoTracking().ToListAsync();
            }
        }
    }
}
