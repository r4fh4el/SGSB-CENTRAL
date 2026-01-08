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
    public class RepositorioDanoPotencialAssociado : RepositorioGenerico<DanoPotencialAssociado>, IDanoPotencialAssociado
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioDanoPotencialAssociado() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado(Expression<Func<DanoPotencialAssociado, bool>> exDanoPotencialAssociado)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DanoPotencialAssociado.Where(exDanoPotencialAssociado).AsNoTracking().ToListAsync();
            }
        }     
        
        public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int id)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DanoPotencialAssociado.Where( x => x.BarragemId == id).AsNoTracking().FirstOrDefaultAsync( CancellationToken.None);
            }
        }
    }
}
