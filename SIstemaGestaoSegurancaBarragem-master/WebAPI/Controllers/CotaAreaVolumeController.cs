using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotaAreaVolumeController : ControllerBase
    {
        private readonly IAplicacaoCotaAreaVolume _IAplicacaoCotaAreaVolume;

        public CotaAreaVolumeController(IAplicacaoCotaAreaVolume aplicacaoCotaAreaVolume)
        {
            _IAplicacaoCotaAreaVolume = aplicacaoCotaAreaVolume;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarCotaAreaVolume")]
        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolume()
        {
            return await _IAplicacaoCotaAreaVolume.ListarCotaAreaVolume();
        }

      
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdCotaAreaVolume")]
        public async Task<CotaAreaVolume> BuscarPorIdCotaAreaVolume( int id)
        {
            var objCotaAreaVolumeModel = await _IAplicacaoCotaAreaVolume.BuscarPorId(id);
            return objCotaAreaVolumeModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarCotaAreaVolume")]
        public async Task<List<Notifica>> AdicionarCotaAreaVolume(CotaAreaVolumeModel cotaAreaVolume)
        {
            var objCotaAreaVolumeModel = new CotaAreaVolume()
            {
                Id = cotaAreaVolume.Id,
                Cota = cotaAreaVolume.Cota,
                VolumeAcumulado = cotaAreaVolume.VolumeAcumulado,
                AreaSuperficial = cotaAreaVolume.AreaSuperficial,
                BarragemId = cotaAreaVolume.BarragemId,
                DataAlteracao = DateTime.Now,
                DataCadastro = DateTime.Now
            };

            await _IAplicacaoCotaAreaVolume.Adicionar(objCotaAreaVolumeModel);


            return objCotaAreaVolumeModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AtualizaCotaAreaVolume")]
        public async Task<List<Notifica>> AtualizaCotaAreaVolume(CotaAreaVolumeModel cotaAreaVolumeModel)
        {
            var objCotaAreaVolume = await _IAplicacaoCotaAreaVolume.BuscarPorId(cotaAreaVolumeModel.Id);

            objCotaAreaVolume.Id = cotaAreaVolumeModel.Id;
            objCotaAreaVolume.VolumeAcumulado = cotaAreaVolumeModel.VolumeAcumulado;
            objCotaAreaVolume.Cota = cotaAreaVolumeModel.Cota;
            objCotaAreaVolume.AreaSuperficial = cotaAreaVolumeModel.AreaSuperficial;
            objCotaAreaVolume.BarragemId = cotaAreaVolumeModel.BarragemId;
           
            objCotaAreaVolume.DataAlteracao = DateTime.Now;

            await _IAplicacaoCotaAreaVolume.Atualizar(objCotaAreaVolume);

            return objCotaAreaVolume.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirCotaAreaVolume")]
        public async Task<bool> ExcluirCotaAreaVolume([FromBody] int id)
        {
            var objCotaAreaVolumeModel = await _IAplicacaoCotaAreaVolume.BuscarPorId(id);

            await _IAplicacaoCotaAreaVolume.Excluir(objCotaAreaVolumeModel);

            return true;
        }

  
        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }
            else 
            {
                return string.Empty;
            }
        }


        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarCotaAreaVolumeBarragemId")]
        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem)
        {
            List<CotaAreaVolume> lstCotaAreaVolume = new List<CotaAreaVolume>();
            lstCotaAreaVolume = await _IAplicacaoCotaAreaVolume.ListarCotaAreaVolumeBarragemId(idBarragem);


            return lstCotaAreaVolume;
        }
    }
}
