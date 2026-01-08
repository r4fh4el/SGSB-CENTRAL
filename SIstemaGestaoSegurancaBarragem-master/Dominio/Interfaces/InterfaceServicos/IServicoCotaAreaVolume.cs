using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoCotaAreaVolume
    {
        Task AdicionarCotaAreaVolume(CotaAreaVolume cotaAreaVolume);
        Task AtualizaCotaAreaVolume(CotaAreaVolume cotaAreaVolume);
        Task<List<CotaAreaVolume>> ListarCotaAreaVolume();

        Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem);
    }
}
