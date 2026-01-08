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
    public interface IAplicacaoVazaPico : IGenericaAplicacao<VazaPico>
    {
        Task AdicionarBarragem(VazaPico vazaPico);
        Task AtualizaBarragem(VazaPico vazaPico);
        Task<List<VazaPico>> ListarVazaPico();

        Task<VazaPico> VerificarBarragemId(int BarragemId);
    }
}
