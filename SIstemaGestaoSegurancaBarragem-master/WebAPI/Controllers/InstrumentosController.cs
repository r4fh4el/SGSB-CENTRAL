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
    public class InstrumentosController : ControllerBase
    {
        private readonly IAplicacaoInstrumentos _IAplicacaoInstrumentos;

        public InstrumentosController(IAplicacaoInstrumentos aplicacaoInstrumentos)
        {
            _IAplicacaoInstrumentos = aplicacaoInstrumentos;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarInstrumentos")]
        public async Task<List<Instrumentos>> ListarInstrumentos()
        {
            return await _IAplicacaoInstrumentos.ListarInstrumentos();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdInstrumentos")]
        public async Task<Instrumentos> BuscarPorIdInstrumentos( int id)
        {
            var objInstrumentosModel = await _IAplicacaoInstrumentos.BuscarPorId(id);
            return objInstrumentosModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarInstrumentos")]
        public async Task<List<Notifica>> AdicionarInstrumentos(InstrumentosModel instrumentos)
        {
            var objInstrumentosModel = new Instrumentos()
            {
                Id = instrumentos.Id,
                Nome = instrumentos.Nome
            };

             await _IAplicacaoInstrumentos.Adicionar(objInstrumentosModel);

            return objInstrumentosModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaInstrumentos")]
        public async Task<List<Notifica>> AtualizaInstrumentos(InstrumentosModel instrumentos)
        {
            var objInstrumentos = await _IAplicacaoInstrumentos.BuscarPorId(instrumentos.Id);

            objInstrumentos.Id = instrumentos.Id;
            objInstrumentos.Nome = instrumentos.Nome;
            objInstrumentos.DataAlteracao = DateTime.Now;

            await _IAplicacaoInstrumentos.Atualizar(objInstrumentos);

            return objInstrumentos.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirInstrumentos")]
        public async Task<List<Notifica>> ExcluirInstrumentos(InstrumentosModel instrumentos)
        {
            var objInstrumentosModel = await _IAplicacaoInstrumentos.BuscarPorId(instrumentos.Id);

            await _IAplicacaoInstrumentos.Excluir(objInstrumentosModel);

            return objInstrumentosModel.Notificacoes;
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
