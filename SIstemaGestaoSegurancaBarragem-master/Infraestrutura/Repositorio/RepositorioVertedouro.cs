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
    public class RepositorioVertedouro : RepositorioGenerico<Vertedouro>, IVertedouro
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioVertedouro() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Vertedouro>> ListarVertedouro(Expression<Func<Vertedouro, bool>> exVertedouro)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Vertedouro.Where(exVertedouro).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Vertedouro>> ListarVertedouroBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Vertedouro.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
