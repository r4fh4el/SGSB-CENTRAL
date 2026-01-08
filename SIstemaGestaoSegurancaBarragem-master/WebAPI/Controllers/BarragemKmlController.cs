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
    public class BarragemKmlController : ControllerBase
    {
        private readonly IAplicacaoBarragemKml _IAplicacaoBarragemKml;

        public BarragemKmlController(IAplicacaoBarragemKml aplicacaoBarragemKml)
        {
            _IAplicacaoBarragemKml = aplicacaoBarragemKml;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarBarragemKml")]
        public async Task<List<BarragemKml>> ListarBarragemKml()
        {
            return await _IAplicacaoBarragemKml.ListarBarragemKml();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdBarragemKml")]
        public async Task<BarragemKml> BuscarPorIdBarragemKml( int id)
        {
            var objBarragemKmlModel = await _IAplicacaoBarragemKml.BuscarPorId(id);
            return objBarragemKmlModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarBarragemKml")]
        public async Task<List<Notifica>> AdicionarBarragemKml(BarragemKmlModel BarragemKmlModel)
        {
            var objBarragemKmlModel = new BarragemKml()
            {
                Id = BarragemKmlModel.Id,
                Coordenadas = BarragemKmlModel.Coordenadas,
                BarragemId = BarragemKmlModel.BarragemId,
                DataAlteracao = DateTime.Now,
                DataCadastro = DateTime.Now
                
        };

             await _IAplicacaoBarragemKml.Adicionar(objBarragemKmlModel);

            return objBarragemKmlModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaBarragemKml")]
        public async Task<List<Notifica>> AtualizaBarragemKml(BarragemKmlModel BarragemKmlModel)
        {
            var objBarragemKml = await _IAplicacaoBarragemKml.BuscarPorId(BarragemKmlModel.Id);

            objBarragemKml.Id = BarragemKmlModel.Id;
            objBarragemKml.Coordenadas = BarragemKmlModel.Coordenadas;
            objBarragemKml.DataAlteracao = DateTime.Now;
            objBarragemKml.BarragemId = BarragemKmlModel.BarragemId;

            await _IAplicacaoBarragemKml.Atualizar(objBarragemKml);

            return objBarragemKml.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirBarragemKml")]
        public async Task<List<Notifica>> ExcluirBarragemKml(BarragemKmlModel BarragemKmlModel)
        {
            var objBarragemKmlModel = await _IAplicacaoBarragemKml.BuscarPorId(BarragemKmlModel.Id);

            await _IAplicacaoBarragemKml.Excluir(objBarragemKmlModel);

            return objBarragemKmlModel.Notificacoes;
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
