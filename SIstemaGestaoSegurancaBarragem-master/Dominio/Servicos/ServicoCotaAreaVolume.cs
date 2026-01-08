using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoCotaAreaVolume : IServicoCotaAreaVolume
    {
        private readonly ICotaAreaVolume _ICotaAreaVolume;

        public ServicoCotaAreaVolume(ICotaAreaVolume cotaAreaVolume) 
        {
            _ICotaAreaVolume = cotaAreaVolume;
        }

        public async Task AdicionarCotaAreaVolume(CotaAreaVolume cotaAreaVolume)
        {
            
                cotaAreaVolume.DataAlteracao = DateTime.Now;
                cotaAreaVolume.DataCadastro = DateTime.Now;
               await _ICotaAreaVolume.Adicionar(cotaAreaVolume);
          
        }

        public async Task AtualizaCotaAreaVolume(CotaAreaVolume cotaAreaVolume)
        {
           
                cotaAreaVolume.DataAlteracao = DateTime.Now;
                cotaAreaVolume.DataCadastro = cotaAreaVolume.DataCadastro;
                await _ICotaAreaVolume.Atualizar(cotaAreaVolume);
        
        }

        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolume()
        {
            return  await _ICotaAreaVolume.ListarCotaAreaVolume(n => n.Cota != null);
        }

        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem)
        {
            return await _ICotaAreaVolume.ListarCotaAreaVolumeBarragemId(idBarragem);
        }
    }
}
