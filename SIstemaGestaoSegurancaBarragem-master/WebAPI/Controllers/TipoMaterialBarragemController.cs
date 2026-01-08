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
    public class TipoMaterialBarragemController : ControllerBase
    {
        private readonly IAplicacaoTipoMaterialBarragem _IAplicacaoTipoMaterialBarragem;

        public TipoMaterialBarragemController(IAplicacaoTipoMaterialBarragem aplicacaoTipoMaterialBarragem)
        {
            _IAplicacaoTipoMaterialBarragem = aplicacaoTipoMaterialBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTipoMaterialBarragem")]
        public async Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem()
        {
            return await _IAplicacaoTipoMaterialBarragem.ListarTipoMaterialBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTipoMaterialBarragem")]
        public async Task<TipoMaterialBarragem> BuscarPorIdTipoMaterialBarragem( int id)
        {
            var objTipoMaterialBarragemModel = await _IAplicacaoTipoMaterialBarragem.BuscarPorId(id);
            return objTipoMaterialBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTipoMaterialBarragem")]
        public async Task<List<Notifica>> AdicionarTipoMaterialBarragem(TipoMaterialBarragemModel tipoMaterialBarragemModel)
        {
            var objTipoMaterialBarragemModel = new TipoMaterialBarragem()
            {
                Id = tipoMaterialBarragemModel.Id,
                Nome = tipoMaterialBarragemModel.Nome
            };

             await _IAplicacaoTipoMaterialBarragem.Adicionar(objTipoMaterialBarragemModel);

            return objTipoMaterialBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTipoMaterialBarragem")]
        public async Task<List<Notifica>> AtualizaTipoMaterialBarragem(TipoMaterialBarragemModel tipoMaterialBarragemModel)
        {
            var objTipoMaterialBarragem = await _IAplicacaoTipoMaterialBarragem.BuscarPorId(tipoMaterialBarragemModel.Id);

            objTipoMaterialBarragem.Id = tipoMaterialBarragemModel.Id;
            objTipoMaterialBarragem.Nome = tipoMaterialBarragemModel.Nome;
            objTipoMaterialBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoTipoMaterialBarragem.Atualizar(objTipoMaterialBarragem);

            return objTipoMaterialBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTipoMaterialBarragem")]
        public async Task<List<Notifica>> ExcluirTipoMaterialBarragem(TipoMaterialBarragemModel tipoMaterialBarragemModel)
        {
            var objTipoMaterialBarragemModel = await _IAplicacaoTipoMaterialBarragem.BuscarPorId(tipoMaterialBarragemModel.Id);

            await _IAplicacaoTipoMaterialBarragem.Excluir(objTipoMaterialBarragemModel);

            return objTipoMaterialBarragemModel.Notificacoes;
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
