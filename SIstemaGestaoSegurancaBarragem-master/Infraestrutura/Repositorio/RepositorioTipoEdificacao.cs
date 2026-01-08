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
    public class RepositorioTipoEdificacao : RepositorioGenerico<TipoEdificacao>, ITipoEdificacao
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTipoEdificacao() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TipoEdificacao>> ListarTipoEdificacao(Expression<Func<TipoEdificacao, bool>> exTipoEdificacao)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TipoEdificacao.Where(exTipoEdificacao).AsNoTracking().ToListAsync();
            }
        }
    }
}
