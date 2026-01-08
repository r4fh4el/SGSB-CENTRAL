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
    public class CondicaoFundacaoController : ControllerBase
    {
        private readonly IAplicacaoCondicaoFundacao _IAplicacaoCondicaoFundacao;

        public CondicaoFundacaoController(IAplicacaoCondicaoFundacao aplicacaoCondicaoFundacao)
        {
            _IAplicacaoCondicaoFundacao = aplicacaoCondicaoFundacao;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarCondicaoFundacao")]
        public async Task<List<CondicaoFundacao>> ListarCondicaoFundacao()
        {
            return await _IAplicacaoCondicaoFundacao.ListarCondicaoFundacao();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdCondicaoFundacao")]
        public async Task<CondicaoFundacao> BuscarPorIdCondicaoFundacao(int id)
        {
            var objCondicaoFundacaoModel = await _IAplicacaoCondicaoFundacao.BuscarPorId(id);
            return objCondicaoFundacaoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarCondicaoFundacao")]
        public async Task<List<Notifica>> AdicionarCondicaoFundacao(CondicaoFundacaoModel tipoMaterialBarragemModel)
        {
            var objCondicaoFundacaoModel = new CondicaoFundacao()
            {
                Id = tipoMaterialBarragemModel.Id,
                Nome = tipoMaterialBarragemModel.Nome
            };

             await _IAplicacaoCondicaoFundacao.AdicionarCondicaoFundacao(objCondicaoFundacaoModel);

            return objCondicaoFundacaoModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AtualizaCondicaoFundacao")]
        public async Task<List<Notifica>> AtualizaCondicaoFundacao(CondicaoFundacaoModel tipoMaterialBarragemModel)
        {
            var objCondicaoFundacao = await _IAplicacaoCondicaoFundacao.BuscarPorId(tipoMaterialBarragemModel.Id);

            objCondicaoFundacao.Id = tipoMaterialBarragemModel.Id;
            objCondicaoFundacao.Nome = tipoMaterialBarragemModel.Nome;
            objCondicaoFundacao.DataAlteracao = DateTime.Now;

            await _IAplicacaoCondicaoFundacao.AtualizaCondicaoFundacao(objCondicaoFundacao);

            return objCondicaoFundacao.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirCondicaoFundacao")]
        public async Task<List<Notifica>> ExcluirCondicaoFundacao(CondicaoFundacaoModel tipoMaterialBarragemModel)
        {
            var objCondicaoFundacaoModel = await _IAplicacaoCondicaoFundacao.BuscarPorId(tipoMaterialBarragemModel.Id);

            await _IAplicacaoCondicaoFundacao.Excluir(objCondicaoFundacaoModel);

            return objCondicaoFundacaoModel.Notificacoes;
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
