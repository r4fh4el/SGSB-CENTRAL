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
    public interface IAplicacaoCotaAreaVolume : IGenericaAplicacao<CotaAreaVolume>
    {
        Task AdicionarCotaAreaVolume(CotaAreaVolume cotaAreaVolume);
        Task AtualizaCotaAreaVolume(CotaAreaVolume cotaAreaVolume);
        Task<List<CotaAreaVolume>> ListarCotaAreaVolume();
        Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem);
    }
}
