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
    public class RepositorioDescarregadorFundo : RepositorioGenerico<DescarregadorFundo>, IDescarregadorFundo
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioDescarregadorFundo() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundo(Expression<Func<DescarregadorFundo, bool>> exDescarregadorFundos)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DescarregadorFundo.Where(exDescarregadorFundos).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DescarregadorFundo.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
