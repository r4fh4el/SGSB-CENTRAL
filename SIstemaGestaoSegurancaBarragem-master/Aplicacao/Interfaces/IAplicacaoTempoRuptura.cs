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
    public interface IAplicacaoTempoRuptura : IGenericaAplicacao<TempoRuptura>
    {
        Task AdicionarBarragem(TempoRuptura TempoRuptura);
        Task AtualizaBarragem(TempoRuptura TempoRuptura);
        Task<List<TempoRuptura>> ListarTempoRuptura();
    }
}
