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
    public class RepositorioTomadaDagua : RepositorioGenerico<TomadaDagua>, ITomadaDagua
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTomadaDagua() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TomadaDagua>> ListarTomadaDagua(Expression<Func<TomadaDagua, bool>> exTomadaDaguas)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TomadaDagua.Where(exTomadaDaguas).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TomadaDagua.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
