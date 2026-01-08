using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IInstrumentosBarragem : IGenericos<InstrumentosBarragem>
    {
        Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem(Expression<Func<InstrumentosBarragem, bool>> exInstrumentosBarragem);
        Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId(int idBarragem);
    
    }
}
