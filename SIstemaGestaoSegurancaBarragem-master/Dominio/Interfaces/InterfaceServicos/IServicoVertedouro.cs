using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoVertedouro
    {
        Task AdicionarVertedouro(Vertedouro tipoMaterialBarragem);
        Task AtualizaVertedouro(Vertedouro tipoMaterialBarragem);
        Task<List<Vertedouro>> ListarVertedouro();

        Task<List<Vertedouro>> ListarVertedouroBarragemId(int idBarragem);
    }
}
