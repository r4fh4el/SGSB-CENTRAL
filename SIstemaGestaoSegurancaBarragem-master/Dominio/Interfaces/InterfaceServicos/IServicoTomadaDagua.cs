using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTomadaDagua
    {
        Task AdicionarTomadaDagua(TomadaDagua tomadaDagua);
        Task AtualizaTomadaDagua(TomadaDagua tomadaDagua);
        Task<List<TomadaDagua>> ListarTomadaDagua();

        Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem);
    }
}
