using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTipoMaterialBarragem
    {
        Task AdicionarTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem);
        Task AtualizaTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem);
        Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem();
    }
}
