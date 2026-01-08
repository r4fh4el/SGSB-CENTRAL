using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ICotaAreaVolume : IGenericos<CotaAreaVolume>
    {
        Task<List<CotaAreaVolume>> ListarCotaAreaVolume(Expression<Func<CotaAreaVolume, bool>> exCotaAreaVolume);
        Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem);
    }
}
