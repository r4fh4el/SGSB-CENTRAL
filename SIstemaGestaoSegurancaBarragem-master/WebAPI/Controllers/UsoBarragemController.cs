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
    public class UsoBarragemController : ControllerBase
    {
        private readonly IAplicacaoUsoBarragem _IAplicacaoUsoBarragem;

        public UsoBarragemController(IAplicacaoUsoBarragem aplicacaoUsoBarragem)
        {
            _IAplicacaoUsoBarragem = aplicacaoUsoBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarUsoBarragem")]
        public async Task<List<UsoBarragem>> ListarUsoBarragem()
        {
            return await _IAplicacaoUsoBarragem.ListarUsoBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdUsoBarragem")]
        public async Task<UsoBarragem> BuscarPorIdUsoBarragem( int id)
        {
            var objUsoBarragemModel = await _IAplicacaoUsoBarragem.BuscarPorId(id);
            return objUsoBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarUsoBarragem")]
        public async Task<List<Notifica>> AdicionarUsoBarragem(UsoBarragemModel tipoEmpreendedorModel)
        {
            var objUsoBarragemModel = new UsoBarragem()
            {
                Id = tipoEmpreendedorModel.Id,
                Nome = tipoEmpreendedorModel.Nome
            };

             await _IAplicacaoUsoBarragem.Adicionar(objUsoBarragemModel);

            return objUsoBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaUsoBarragem")]
        public async Task<List<Notifica>> AtualizaUsoBarragem(UsoBarragemModel tipoEmpreendedorModel)
        {
            var objUsoBarragem = await _IAplicacaoUsoBarragem.BuscarPorId(tipoEmpreendedorModel.Id);

            objUsoBarragem.Id = tipoEmpreendedorModel.Id;
            objUsoBarragem.Nome = tipoEmpreendedorModel.Nome;
            objUsoBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoUsoBarragem.Atualizar(objUsoBarragem);

            return objUsoBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirUsoBarragem")]
        public async Task<List<Notifica>> ExcluirUsoBarragem(UsoBarragemModel tipoEmpreendedorModel)
        {
            var objUsoBarragemModel = await _IAplicacaoUsoBarragem.BuscarPorId(tipoEmpreendedorModel.Id);

            await _IAplicacaoUsoBarragem.Excluir(objUsoBarragemModel);

            return objUsoBarragemModel.Notificacoes;
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
