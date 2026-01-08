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
    public class TipoEmpreendedorController : ControllerBase
    {
        private readonly IAplicacaoTipoEmpreendedor _IAplicacaoTipoEmpreendedor;

        public TipoEmpreendedorController(IAplicacaoTipoEmpreendedor aplicacaoTipoEmpreendedor)
        {
            _IAplicacaoTipoEmpreendedor = aplicacaoTipoEmpreendedor;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTipoEmpreendedor")]
        public async Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor()
        {
            return await _IAplicacaoTipoEmpreendedor.ListarTipoEmpreendedor();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTipoEmpreendedor")]
        public async Task<TipoEmpreendedor> BuscarPorIdTipoEmpreendedor( int id)
        {
            var objTipoEmpreendedorModel = await _IAplicacaoTipoEmpreendedor.BuscarPorId(id);
            return objTipoEmpreendedorModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTipoEmpreendedor")]
        public async Task<List<Notifica>> AdicionarTipoEmpreendedor(TipoEmpreendedorModel tipoEmpreendedorModel)
        {
            var objTipoEmpreendedorModel = new TipoEmpreendedor()
            {
                Id = tipoEmpreendedorModel.Id,
                Nome = tipoEmpreendedorModel.Nome
            };

             await _IAplicacaoTipoEmpreendedor.Adicionar(objTipoEmpreendedorModel);

            return objTipoEmpreendedorModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTipoEmpreendedor")]
        public async Task<List<Notifica>> AtualizaTipoEmpreendedor(TipoEmpreendedorModel tipoEmpreendedorModel)
        {
            var objTipoEmpreendedor = await _IAplicacaoTipoEmpreendedor.BuscarPorId(tipoEmpreendedorModel.Id);

            objTipoEmpreendedor.Id = tipoEmpreendedorModel.Id;
            objTipoEmpreendedor.Nome = tipoEmpreendedorModel.Nome;
            objTipoEmpreendedor.DataAlteracao = DateTime.Now;

            await _IAplicacaoTipoEmpreendedor.Atualizar(objTipoEmpreendedor);

            return objTipoEmpreendedor.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTipoEmpreendedor")]
        public async Task<List<Notifica>> ExcluirTipoEmpreendedor(TipoEmpreendedorModel tipoEmpreendedorModel)
        {
            var objTipoEmpreendedorModel = await _IAplicacaoTipoEmpreendedor.BuscarPorId(tipoEmpreendedorModel.Id);

            await _IAplicacaoTipoEmpreendedor.Excluir(objTipoEmpreendedorModel);

            return objTipoEmpreendedorModel.Notificacoes;
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
