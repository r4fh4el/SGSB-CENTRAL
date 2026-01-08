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
    public class VazaPicoController : ControllerBase
    {
        private readonly IAplicacaoVazaPico _IAplicacaoVazaPico;

        public VazaPicoController(IAplicacaoVazaPico aplicacaoVazaPico)
        {
            _IAplicacaoVazaPico = aplicacaoVazaPico;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarVazaPico")]
        public async Task<List<VazaPico>> ListarVazaPico()
        {
            return await _IAplicacaoVazaPico.ListarVazaPico();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdVazaPico")]
        public async Task<VazaPico> BuscarPorIdVazaPico( int id)
        {
            var objVazaPicoModel = await _IAplicacaoVazaPico.BuscarPorId(id);
            return objVazaPicoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarVazaPico")]
        public async Task<List<Notifica>> AdicionarVazaPico(VazaPicoModel vazaPicoModel)
        {
            var objVazaPicoModel = new VazaPico()
            {
                Id = vazaPicoModel.Id,
                valorAS = vazaPicoModel.valorAS,
                valorHhid = vazaPicoModel.valorHhid,
                valorYmed = vazaPicoModel.valorYmed,
                valorVhid = vazaPicoModel.valorVhid,
                BarragemId = vazaPicoModel.BarragemId
             
            };

             await _IAplicacaoVazaPico.Adicionar(objVazaPicoModel);

            return objVazaPicoModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaVazaPico")]
        public async Task<List<Notifica>> AtualizaVazaPico(VazaPicoModel vazaPicoModel)
        {
            var objVazaPico = await _IAplicacaoVazaPico.BuscarPorId(vazaPicoModel.Id);

            objVazaPico.Id = vazaPicoModel.Id;
            objVazaPico.valorAS = vazaPicoModel.valorAS;
            objVazaPico.valorHhid = vazaPicoModel.valorHhid;
            objVazaPico.valorYmed = vazaPicoModel.valorYmed;
            objVazaPico.valorVhid = vazaPicoModel.valorVhid;
            await _IAplicacaoVazaPico.Atualizar(objVazaPico);

            return objVazaPico.Notificacoes;
        }


        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/VerificarBarragemId")]
        public async Task<List<Notifica>> VerificarBarragemId(VazaPicoModel vazaPicoModel)
        {
            var objVazaPico = await _IAplicacaoVazaPico.VerificarBarragemId(vazaPicoModel.BarragemId);

            objVazaPico.Id = vazaPicoModel.Id;
            objVazaPico.valorAS = vazaPicoModel.valorAS;
            objVazaPico.valorHhid = vazaPicoModel.valorHhid;
            objVazaPico.valorYmed = vazaPicoModel.valorYmed;
            objVazaPico.valorVhid = vazaPicoModel.valorVhid;
            await _IAplicacaoVazaPico.Atualizar(objVazaPico);

            return objVazaPico.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirVazaPico")]
        public async Task<List<Notifica>> ExcluirVazaPico(VazaPicoModel vazaPicoModel)
        {
            var objVazaPicoModel = await _IAplicacaoVazaPico.BuscarPorId(vazaPicoModel.Id);

            await _IAplicacaoVazaPico.Excluir(objVazaPicoModel);

            return objVazaPicoModel.Notificacoes;
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
    }
}
