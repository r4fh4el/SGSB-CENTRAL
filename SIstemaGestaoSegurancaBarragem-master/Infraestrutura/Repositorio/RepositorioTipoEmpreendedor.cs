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
    public class RepositorioTipoEmpreendedor : RepositorioGenerico<TipoEmpreendedor>, ITipoEmpreendedor
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTipoEmpreendedor() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor(Expression<Func<TipoEmpreendedor, bool>> exTipoEmpreendedor)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TipoEmpreendedor.Where(exTipoEmpreendedor).AsNoTracking().ToListAsync();
            }
        }
    }
}
