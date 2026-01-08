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
    public class RepositorioRotaFuga : RepositorioGenerico<RotaFuga>, IRotaFuga
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioRotaFuga() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }

        public async Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.RotaFuga.Where(x => x.BarragemId == id).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<RotaFuga>> ListarRotaFuga(Expression<Func<RotaFuga, bool>> exRotaFuga)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.RotaFuga.Where(exRotaFuga).AsNoTracking().ToListAsync();
            }
        }
    }
}
