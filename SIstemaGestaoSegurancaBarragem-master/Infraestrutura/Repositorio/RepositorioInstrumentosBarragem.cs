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
    public class RepositorioInstrumentosBarragem : RepositorioGenerico<InstrumentosBarragem>, IInstrumentosBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioInstrumentosBarragem()
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem(Expression<Func<InstrumentosBarragem, bool>> exInstrumentosBarragems)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.InstrumentosBarragem.Where(exInstrumentosBarragems).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.InstrumentosBarragem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
