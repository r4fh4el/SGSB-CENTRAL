using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoInstrumentosBarragem : IGenericaAplicacao<InstrumentosBarragem>
    {
        Task AdicionarInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem);
        Task AtualizaInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem);
        Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem();

        Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId(int idBarragem);

    }
}
