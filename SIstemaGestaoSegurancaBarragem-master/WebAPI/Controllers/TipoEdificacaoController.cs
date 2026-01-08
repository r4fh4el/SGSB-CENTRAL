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
    public class TipoEdificacaoController : ControllerBase
    {
        private readonly IAplicacaoTipoEdificacao _IAplicacaoTipoEdificacao;

        public TipoEdificacaoController(IAplicacaoTipoEdificacao aplicacaoTipoEdificacao)
        {
            _IAplicacaoTipoEdificacao = aplicacaoTipoEdificacao;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTipoEdificacao")]
        public async Task<List<TipoEdificacao>> ListarTipoEdificacao()
        {
            return await _IAplicacaoTipoEdificacao.ListarTipoEdificacao();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTipoEdificacao")]
        public async Task<TipoEdificacao> BuscarPorIdTipoEdificacao( int id)
        {
            var objTipoEdificacaoModel = await _IAplicacaoTipoEdificacao.BuscarPorId(id);
            return objTipoEdificacaoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTipoEdificacao")]
        public async Task<List<Notifica>> AdicionarTipoEdificacao(TipoEdificacaoModel tipoEdificacaoModel)
        {
            var objTipoEdificacaoModel = new TipoEdificacao()
            {
                Id = tipoEdificacaoModel.Id,
                Nome = tipoEdificacaoModel.Nome
            };

             await _IAplicacaoTipoEdificacao.Adicionar(objTipoEdificacaoModel);

            return objTipoEdificacaoModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTipoEdificacao")]
        public async Task<List<Notifica>> AtualizaTipoEdificacao(TipoEdificacaoModel tipoEdificacaoModel)
        {
            var objTipoEdificacao = await _IAplicacaoTipoEdificacao.BuscarPorId(tipoEdificacaoModel.Id);

            objTipoEdificacao.Id = tipoEdificacaoModel.Id;
            objTipoEdificacao.Nome = tipoEdificacaoModel.Nome;
            objTipoEdificacao.DataAlteracao = DateTime.Now;

            await _IAplicacaoTipoEdificacao.Atualizar(objTipoEdificacao);

            return objTipoEdificacao.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTipoEdificacao")]
        public async Task<List<Notifica>> ExcluirTipoEdificacao(TipoEdificacaoModel tipoEdificacaoModel)
        {
            var objTipoEdificacaoModel = await _IAplicacaoTipoEdificacao.BuscarPorId(tipoEdificacaoModel.Id);

            await _IAplicacaoTipoEdificacao.Excluir(objTipoEdificacaoModel);

            return objTipoEdificacaoModel.Notificacoes;
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
