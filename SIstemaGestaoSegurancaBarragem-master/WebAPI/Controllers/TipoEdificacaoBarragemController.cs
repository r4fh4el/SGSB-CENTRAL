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
    public class TipoEdificacaoBarragemController : ControllerBase
    {
        private readonly IAplicacaoTipoEdificacaoBarragem _IAplicacaoTipoEdificacaoBarragem;

        public TipoEdificacaoBarragemController(IAplicacaoTipoEdificacaoBarragem aplicacaoTipoEdificacaoBarragem)
        {
            _IAplicacaoTipoEdificacaoBarragem = aplicacaoTipoEdificacaoBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTipoEdificacaoBarragem")]
        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem()
        {
            return await _IAplicacaoTipoEdificacaoBarragem.ListarTipoEdificacaoBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTipoEdificacaoBarragem")]
        public async Task<TipoEdificacaoBarragem> BuscarPorIdTipoEdificacaoBarragem( int id)
        {
            var objTipoEdificacaoBarragemModel = await _IAplicacaoTipoEdificacaoBarragem.BuscarPorId(id);
            return objTipoEdificacaoBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTipoEdificacaoBarragem")]
        public async Task<List<Notifica>> AdicionarTipoEdificacaoBarragem(TipoEdificacaoBarragemModel TipoEdificacaoBarragemModel)
        {
            var objTipoEdificacaoBarragemModel = new TipoEdificacaoBarragem()
            {
                Id = TipoEdificacaoBarragemModel.Id,
                BarragemId = TipoEdificacaoBarragemModel.BarragemId,
                TipoEdificacaoId = TipoEdificacaoBarragemModel.TipoEdificacaoId,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

             await _IAplicacaoTipoEdificacaoBarragem.Adicionar(objTipoEdificacaoBarragemModel);

            return objTipoEdificacaoBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTipoEdificacaoBarragem")]
        public async Task<List<Notifica>> AtualizaTipoEdificacaoBarragem(TipoEdificacaoBarragemModel TipoEdificacaoBarragemModel)
        {
            var objTipoEdificacaoBarragem = await _IAplicacaoTipoEdificacaoBarragem.BuscarPorId(TipoEdificacaoBarragemModel.Id);

            objTipoEdificacaoBarragem.Id = TipoEdificacaoBarragemModel.Id;
            objTipoEdificacaoBarragem.BarragemId = TipoEdificacaoBarragemModel.BarragemId;
            objTipoEdificacaoBarragem.TipoEdificacaoId = TipoEdificacaoBarragemModel.TipoEdificacaoId;
            objTipoEdificacaoBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoTipoEdificacaoBarragem.Atualizar(objTipoEdificacaoBarragem);

            return objTipoEdificacaoBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTipoEdificacaoBarragem")]
        public async Task<List<Notifica>> ExcluirTipoEdificacaoBarragem(TipoEdificacaoBarragemModel TipoEdificacaoBarragemModel)
        {
            var objTipoEdificacaoBarragemModel = await _IAplicacaoTipoEdificacaoBarragem.BuscarPorId(TipoEdificacaoBarragemModel.Id);

            await _IAplicacaoTipoEdificacaoBarragem.Excluir(objTipoEdificacaoBarragemModel);

            return objTipoEdificacaoBarragemModel.Notificacoes;
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
        [HttpPost("/API/ListarTipoEdificacaoBarragemBarragemId")]
        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId([FromBody] int idBarragem)
        {
            List<TipoEdificacaoBarragem> lstTipoEdificacaoBarragem = new List<TipoEdificacaoBarragem>();
            lstTipoEdificacaoBarragem = await _IAplicacaoTipoEdificacaoBarragem.ListarTipoEdificacaoBarragemBarragemId(idBarragem);


            return lstTipoEdificacaoBarragem;
        }
    }
}
