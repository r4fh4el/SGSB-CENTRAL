using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTalude
    {
        Task AdicionarTalude(Talude talude);
        Task AtualizaTalude(Talude talude);
        Task<List<Talude>> ListarTalude();

        Task<List<Talude>> ListarTaludeBarragemId(int idBarragem);
    }
}
