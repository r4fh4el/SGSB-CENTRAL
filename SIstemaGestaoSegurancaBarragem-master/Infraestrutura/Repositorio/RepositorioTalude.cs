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
    public class RepositorioTalude : RepositorioGenerico<Talude>, ITalude
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTalude() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Talude>> ListarTalude(Expression<Func<Talude, bool>> exTaludes)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Talude.Where(exTaludes).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Talude>> ListarTaludeBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Talude.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
