using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoBarragem
    {
        Task AdicionarBarragem(Barragem tipoMaterialBarragem);
        Task AtualizaBarragem(Barragem tipoMaterialBarragem);
        Task<List<Barragem>> ListarBarragem();

        Task<List<Barragem>> BuscarListaPorIdBarragem(int id);

        Task<bool> DeletarBarragemRelacionais(int idBarragem);
    }
}
