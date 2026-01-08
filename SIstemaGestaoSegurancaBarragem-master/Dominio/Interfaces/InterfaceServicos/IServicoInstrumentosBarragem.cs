using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoInstrumentosBarragem
    {
        Task AdicionarInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem);
        Task AtualizaInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem);
        Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem();
        Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId(int idBarragem);
    }
}
