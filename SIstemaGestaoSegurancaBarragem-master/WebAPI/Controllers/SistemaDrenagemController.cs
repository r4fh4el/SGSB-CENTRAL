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
    public class SistemaDrenagemController : ControllerBase
    {
        private readonly IAplicacaoSistemaDrenagem _IAplicacaoSistemaDrenagem;

        public SistemaDrenagemController(IAplicacaoSistemaDrenagem aplicacaoSistemaDrenagem)
        {
            _IAplicacaoSistemaDrenagem = aplicacaoSistemaDrenagem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarSistemaDrenagem")]
        public async Task<List<SistemaDrenagem>> ListarSistemaDrenagem()
        {
            return await _IAplicacaoSistemaDrenagem.ListarSistemaDrenagem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdSistemaDrenagem")]
        public async Task<SistemaDrenagem> BuscarPorIdSistemaDrenagem(int id)
        {
            var objSistemaDrenagemModel = await _IAplicacaoSistemaDrenagem.BuscarPorId(id);
            return objSistemaDrenagemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarSistemaDrenagem")]
        public async Task<List<Notifica>> AdicionarSistemaDrenagem(SistemaDrenagemModel sistemaDrenagemModel)
        {
            var objSistemaDrenagemModel = new SistemaDrenagem()
            {
                Id = sistemaDrenagemModel.Id,
                Nome = sistemaDrenagemModel.Nome
            };

             await _IAplicacaoSistemaDrenagem.AdicionarSistemaDrenagem(objSistemaDrenagemModel);

            return objSistemaDrenagemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaSistemaDrenagem")]
        public async Task<List<Notifica>> AtualizaSistemaDrenagem(SistemaDrenagemModel sistemaDrenagemModel)
        {
            var objSistemaDrenagem = await _IAplicacaoSistemaDrenagem.BuscarPorId(sistemaDrenagemModel.Id);

            objSistemaDrenagem.Id = sistemaDrenagemModel.Id;
            objSistemaDrenagem.Nome = sistemaDrenagemModel.Nome;
            objSistemaDrenagem.DataAlteracao = DateTime.Now;

            await _IAplicacaoSistemaDrenagem.AtualizaSistemaDrenagem(objSistemaDrenagem);

            return objSistemaDrenagem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirSistemaDrenagem")]
        public async Task<List<Notifica>> ExcluirSistemaDrenagem(SistemaDrenagemModel sistemaDrenagemModel)
        {
            var objSistemaDrenagemModel = await _IAplicacaoSistemaDrenagem.BuscarPorId(sistemaDrenagemModel.Id);

            await _IAplicacaoSistemaDrenagem.Excluir(objSistemaDrenagemModel);

            return objSistemaDrenagemModel.Notificacoes;
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
