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
    public class InstrumentosBarragemController : ControllerBase
    {
        private readonly IAplicacaoInstrumentosBarragem _IAplicacaoInstrumentosBarragem;

        public InstrumentosBarragemController(IAplicacaoInstrumentosBarragem aplicacaoInstrumentosBarragem)
        {
            _IAplicacaoInstrumentosBarragem = aplicacaoInstrumentosBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarInstrumentosBarragem")]
        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem()
        {
            return await _IAplicacaoInstrumentosBarragem.ListarInstrumentosBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdInstrumentosBarragem")]
        public async Task<InstrumentosBarragem> BuscarPorIdInstrumentosBarragem( int id)
        {
            var objInstrumentosBarragemModel = await _IAplicacaoInstrumentosBarragem.BuscarPorId(id);
            return objInstrumentosBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarInstrumentosBarragem")]
        public async Task<List<Notifica>> AdicionarInstrumentosBarragem([FromBody] InstrumentosBarragemModel instrumentosBarragem)
        {
            var objInstrumentosBarragemModel = new InstrumentosBarragem()
            {
                Id = instrumentosBarragem.Id,
                BarragemId = instrumentosBarragem.BarragemId,
                InstrumentosId = instrumentosBarragem.InstrumentosId,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                
            };

             await _IAplicacaoInstrumentosBarragem.Adicionar(objInstrumentosBarragemModel);

            return objInstrumentosBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaInstrumentosBarragem")]
        public async Task<List<Notifica>> AtualizaInstrumentosBarragem([FromBody] InstrumentosBarragemModel InstrumentosBarragem)
        {
            var objInstrumentosBarragem = await _IAplicacaoInstrumentosBarragem.BuscarPorId(InstrumentosBarragem.Id);

            objInstrumentosBarragem.Id = InstrumentosBarragem.Id;
            objInstrumentosBarragem.BarragemId = InstrumentosBarragem.BarragemId;
            objInstrumentosBarragem.InstrumentosId = InstrumentosBarragem.InstrumentosId;
            
            objInstrumentosBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoInstrumentosBarragem.Atualizar(objInstrumentosBarragem);

            return objInstrumentosBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirInstrumentosBarragem")]
        public async Task<List<Notifica>> ExcluirInstrumentosBarragem(InstrumentosBarragemModel InstrumentosBarragem)
        {
            var objInstrumentosBarragemModel = await _IAplicacaoInstrumentosBarragem.BuscarPorId(InstrumentosBarragem.Id);

            await _IAplicacaoInstrumentosBarragem.Excluir(objInstrumentosBarragemModel);

            return objInstrumentosBarragemModel.Notificacoes;
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
        [HttpPost("/API/ListarInstrumentosBarragemBarragemId")]
        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId([FromBody] int idBarragem)
        {
            List<InstrumentosBarragem> lstInstrumentosBarragem = new List<InstrumentosBarragem>();
            lstInstrumentosBarragem = await _IAplicacaoInstrumentosBarragem.ListarInstrumentosBarragemBarragemId(idBarragem);


            return lstInstrumentosBarragem;
        }
    }
}
