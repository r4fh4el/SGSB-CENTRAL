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
    public class RepositorioVazaPico : RepositorioGenerico<VazaPico>, IVazaPico
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioVazaPico() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<VazaPico>> ListarVazaPico(Expression<Func<VazaPico, bool>> exVazaPico)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.VazaPico.Where(exVazaPico).AsNoTracking().ToListAsync();
            }
        }

        public async Task<VazaPico> VerificarBarragemId(int BarragemId)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.VazaPico.Where(x => x.BarragemId == BarragemId).FirstOrDefaultAsync();
            }
        }
    }
}
