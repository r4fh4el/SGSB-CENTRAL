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
    public class PontoEncontroController : ControllerBase
    {
        private readonly IAplicacaoPontoEncontro _IAplicacaoPontoEncontro;

        public PontoEncontroController(IAplicacaoPontoEncontro aplicacaoPontoEncontro)
        {
            _IAplicacaoPontoEncontro = aplicacaoPontoEncontro;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarPontoEncontro")]
        public async Task<List<PontoEncontro>> ListarPontoEncontro()
        {
            return await _IAplicacaoPontoEncontro.ListarPontoEncontro();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdPontoEncontro")]
        public async Task<PontoEncontro> BuscarPorIdPontoEncontro( int id)
        {
            var objPontoEncontroModel = await _IAplicacaoPontoEncontro.BuscarPorId(id);
            return objPontoEncontroModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarPontoEncontro")]
        public async Task<List<Notifica>> AdicionarPontoEncontro(PontoEncontroModel pontoEncontroModel)
        {
            var objPontoEncontroModel = new PontoEncontro()
            {
                Id = pontoEncontroModel.Id,
                Nome = pontoEncontroModel.Nome,
                BarragemId = pontoEncontroModel.BarragemId,
                Latitude = pontoEncontroModel.Latitude,
                Longitude = pontoEncontroModel.Longitude,
                DataAlteracao = pontoEncontroModel.DataAlteracao,
                DataCadastro = pontoEncontroModel.DataCadastro,
            };

             await _IAplicacaoPontoEncontro.Adicionar(objPontoEncontroModel);

            return objPontoEncontroModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaPontoEncontro")]
        public async Task<List<Notifica>> AtualizaPontoEncontro(PontoEncontroModel pontoEncontroModel)
        {
            var objPontoEncontro = await _IAplicacaoPontoEncontro.BuscarPorId(pontoEncontroModel.Id);

            objPontoEncontro.Id = pontoEncontroModel.Id;
            objPontoEncontro.BarragemId = pontoEncontroModel.BarragemId;
            objPontoEncontro.Nome = pontoEncontroModel.Nome;
            objPontoEncontro.Longitude = pontoEncontroModel.Longitude;
            objPontoEncontro.Latitude = pontoEncontroModel.Latitude;
            objPontoEncontro.DataAlteracao = DateTime.Now;
            objPontoEncontro.DataCadastro = DateTime.Now;
         

            await _IAplicacaoPontoEncontro.Atualizar(objPontoEncontro);

            return objPontoEncontro.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirPontoEncontro")]
        public async Task<List<Notifica>> ExcluirPontoEncontro(PontoEncontroModel pontoEncontroModel)
        {
            var objPontoEncontroModel = await _IAplicacaoPontoEncontro.BuscarPorId(pontoEncontroModel.Id);

            await _IAplicacaoPontoEncontro.Excluir(objPontoEncontroModel);

            return objPontoEncontroModel.Notificacoes;
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
