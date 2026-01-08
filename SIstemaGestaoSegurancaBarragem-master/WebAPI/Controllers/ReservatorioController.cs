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
    public class ReservatorioController : ControllerBase
    {
        private readonly IAplicacaoReservatorio _IAplicacaoReservatorio;

        public ReservatorioController(IAplicacaoReservatorio aplicacaoReservatorio)
        {
            _IAplicacaoReservatorio = aplicacaoReservatorio;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarReservatorio")]
        public async Task<List<Reservatorio>> ListarReservatorio()
        {
            return await _IAplicacaoReservatorio.ListarReservatorio();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdReservatorio")]
        public async Task<Reservatorio> BuscarPorIdReservatorio(int id)
        {
            var objReservatorio = await _IAplicacaoReservatorio.BuscarPorId(id);
            return objReservatorio;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarReservatorio")]
        public async Task<List<Notifica>> AdicionarReservatorio([FromBody] ReservatorioModel reservatorioModel)
        {
            {
                var reservatorio = new Reservatorio()
                {
                    BarragemId = reservatorioModel.BarragemId,
                    DataAlteracao = reservatorioModel.DataAlteracao,
                    DataCadastro = reservatorioModel.DataCadastro,
                    BordaLivre = reservatorioModel.BordaLivre,
                    MaximoMaximorum = reservatorioModel.MaximoMaximorum,
                    //Id = reservatorioModel.Id,
                    CapacidadeTotalReservatorio = reservatorioModel.CapacidadeTotalReservatorio,
                    MaximoNormal = reservatorioModel.MaximoNormal,
                    MinimoOperacional = reservatorioModel.MinimoOperacional,
                    VolumeMorto = reservatorioModel.VolumeMorto,
                    VolumeUtilReservatorio = reservatorioModel.VolumeUtilReservatorio

                }
                ;

                await _IAplicacaoReservatorio.AdicionarReservatorio(reservatorio);

                return reservatorio.Notificacoes;
            }
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaReservatorio")]
        public async Task<List<Notifica>> AtualizaReservatorio([FromBody] ReservatorioModel reservatorioModel)
        {
            var objReservatorio = await _IAplicacaoReservatorio.BuscarPorId(reservatorioModel.Id);

            objReservatorio.Id = reservatorioModel.Id;
            objReservatorio.MaximoNormal = reservatorioModel.MaximoNormal;
            objReservatorio.MinimoOperacional = reservatorioModel.MinimoOperacional;
            objReservatorio.MaximoMaximorum = reservatorioModel.MaximoMaximorum;
            objReservatorio.BordaLivre = reservatorioModel.BordaLivre;
            objReservatorio.CapacidadeTotalReservatorio = reservatorioModel.CapacidadeTotalReservatorio;
            objReservatorio.VolumeMorto = reservatorioModel.VolumeMorto;
            objReservatorio.VolumeUtilReservatorio = reservatorioModel.VolumeUtilReservatorio;
         
            objReservatorio.DataAlteracao = DateTime.Now;

            await _IAplicacaoReservatorio.AtualizaReservatorio(objReservatorio);

            return objReservatorio.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirReservatorio")]
        public async Task<List<Notifica>> ExcluirReservatorio(Reservatorio reservatorioModel)
        {
            var objReservatorio = await _IAplicacaoReservatorio.BuscarPorId(reservatorioModel.Id);

            await _IAplicacaoReservatorio.Excluir(objReservatorio);

            return objReservatorio.Notificacoes;
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
        [HttpPost("/API/ListarReservatorioBarragemId")]
        public async Task<List<Reservatorio>> ListarReservatorioBarragemId([FromBody] int idBarragem)
        {
            List<Reservatorio> lstReservatorio = new List<Reservatorio>();
            lstReservatorio = await _IAplicacaoReservatorio.ListarReservatorioBarragemId(idBarragem);


            return lstReservatorio;
        }
    }
}
