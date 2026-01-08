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
    public class RepositorioInspecoes : RepositorioGenerico<Inspecoes>, IInspecoes
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioInspecoes() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Inspecoes>> ListarInspecoes(Expression<Func<Inspecoes, bool>> exInspecoess)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Inspecoes.Where(exInspecoess).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Inspecoes>> ListarInspecoesBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Inspecoes.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
