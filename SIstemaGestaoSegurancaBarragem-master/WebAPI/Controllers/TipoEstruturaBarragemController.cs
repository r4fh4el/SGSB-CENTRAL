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
    public class TipoEstruturaBarragemController : ControllerBase
    {
        private readonly IAplicacaoTipoEstruturaBarragem _IAplicacaoTipoEstruturaBarragem;

        public TipoEstruturaBarragemController(IAplicacaoTipoEstruturaBarragem aplicacaoTipoEstruturaBarragem)
        {
            _IAplicacaoTipoEstruturaBarragem = aplicacaoTipoEstruturaBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTipoEstruturaBarragem")]
        public async Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem()
        {
            return await _IAplicacaoTipoEstruturaBarragem.ListarTipoEstruturaBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTipoEstruturaBarragem")]
        public async Task<TipoEstruturaBarragem> BuscarPorIdTipoEstruturaBarragem(int id)
        {
            var objTipoEstruturaBarragemModel = await _IAplicacaoTipoEstruturaBarragem.BuscarPorId(id);
            return objTipoEstruturaBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTipoEstruturaBarragem")]
        public async Task<List<Notifica>> AdicionarTipoEstruturaBarragem(TipoEstruturaBarragemModel tipoMaterialBarragemModel)
        {
            var objTipoEstruturaBarragemModel = new TipoEstruturaBarragem()
            {
                Id = tipoMaterialBarragemModel.Id,
                Nome = tipoMaterialBarragemModel.Nome
            };

             await _IAplicacaoTipoEstruturaBarragem.AdicionarTipoEstruturaBarragem(objTipoEstruturaBarragemModel);

            return objTipoEstruturaBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTipoEstruturaBarragem")]
        public async Task<List<Notifica>> AtualizaTipoEstruturaBarragem(TipoEstruturaBarragemModel tipoMaterialBarragemModel)
        {
            var objTipoEstruturaBarragem = await _IAplicacaoTipoEstruturaBarragem.BuscarPorId(tipoMaterialBarragemModel.Id);

            objTipoEstruturaBarragem.Id = tipoMaterialBarragemModel.Id;
            objTipoEstruturaBarragem.Nome = tipoMaterialBarragemModel.Nome;
            objTipoEstruturaBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoTipoEstruturaBarragem.AtualizarTipoEstruturaBarragem(objTipoEstruturaBarragem);

            return objTipoEstruturaBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTipoEstruturaBarragem")]
        public async Task<List<Notifica>> ExcluirTipoEstruturaBarragem(TipoEstruturaBarragemModel tipoMaterialBarragemModel)
        {
            var objTipoEstruturaBarragemModel = await _IAplicacaoTipoEstruturaBarragem.BuscarPorId(tipoMaterialBarragemModel.Id);

            await _IAplicacaoTipoEstruturaBarragem.Excluir(objTipoEstruturaBarragemModel);

            return objTipoEstruturaBarragemModel.Notificacoes;
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
