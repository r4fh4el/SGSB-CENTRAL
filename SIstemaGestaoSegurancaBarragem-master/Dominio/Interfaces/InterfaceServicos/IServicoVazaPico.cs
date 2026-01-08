using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoVazaPico
    {
        Task AdicionarVazaPico(VazaPico vazaPico);
        Task AtualizaVazaPico(VazaPico vazaPico);
        Task<List<VazaPico>> ListarVazaPico();

        Task<VazaPico> VerificarBarragemId(int BarragemId);
    }
}
        