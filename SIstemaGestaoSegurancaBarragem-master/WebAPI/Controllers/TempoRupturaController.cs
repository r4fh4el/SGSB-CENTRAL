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
    public class TempoRupturaController : ControllerBase
    {
        private readonly IAplicacaoTempoRuptura _IAplicacaoTempoRuptura;

        public TempoRupturaController(IAplicacaoTempoRuptura aplicacaoTempoRuptura)
        {
            _IAplicacaoTempoRuptura = aplicacaoTempoRuptura;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTempoRuptura")]
        public async Task<List<TempoRuptura>> ListarTempoRuptura()
        {
            return await _IAplicacaoTempoRuptura.ListarTempoRuptura();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTempoRuptura")]
        public async Task<TempoRuptura> BuscarPorIdTempoRuptura( int id)
        {
            var objTempoRupturaModel = await _IAplicacaoTempoRuptura.BuscarPorId(id);
            return objTempoRupturaModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTempoRuptura")]
        public async Task<List<Notifica>> AdicionarTempoRuptura(TempoRupturaModel TempoRupturaModel)
        {
            var objTempoRupturaModel = new TempoRuptura()
            {
                Id = TempoRupturaModel.Id,
                BarragemId = TempoRupturaModel.BarragemId,
                Barragem = TempoRupturaModel.Barragem,
                valorTempoRuptura = TempoRupturaModel.valorTempoRuptura,
                DataAlteracao = TempoRupturaModel.DataAlteracao,
                DataCadastro = TempoRupturaModel.DataCadastro
                
            };

             await _IAplicacaoTempoRuptura.Adicionar(objTempoRupturaModel);

            return objTempoRupturaModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTempoRuptura")]
        public async Task<List<Notifica>> AtualizaTempoRuptura(TempoRupturaModel TempoRupturaModel)
        {
            var objTempoRuptura = await _IAplicacaoTempoRuptura.BuscarPorId(TempoRupturaModel.Id);

            objTempoRuptura.Id = TempoRupturaModel.Id;
            objTempoRuptura.BarragemId = TempoRupturaModel.BarragemId;
            objTempoRuptura.Barragem = TempoRupturaModel.Barragem;
            objTempoRuptura.valorTempoRuptura = TempoRupturaModel.valorTempoRuptura;
            objTempoRuptura.DataAlteracao = TempoRupturaModel.DataAlteracao;
            objTempoRuptura.DataCadastro = TempoRupturaModel.DataCadastro;
            await _IAplicacaoTempoRuptura.Atualizar(objTempoRuptura);

            return objTempoRuptura.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTempoRuptura")]
        public async Task<List<Notifica>> ExcluirTempoRuptura(TempoRupturaModel TempoRupturaModel)
        {
            var objTempoRupturaModel = await _IAplicacaoTempoRuptura.BuscarPorId(TempoRupturaModel.Id);

            await _IAplicacaoTempoRuptura.Excluir(objTempoRupturaModel);

            return objTempoRupturaModel.Notificacoes;
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
