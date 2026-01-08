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
    public class UsoBarragemBarragemController : ControllerBase
    {
        private readonly IAplicacaoUsoBarragemBarragem _IAplicacaoUsoBarragemBarragem;

        public UsoBarragemBarragemController(IAplicacaoUsoBarragemBarragem aplicacaoUsoBarragemBarragem)
        {
            _IAplicacaoUsoBarragemBarragem = aplicacaoUsoBarragemBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarUsoBarragemBarragem")]
        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem()
        {
            return await _IAplicacaoUsoBarragemBarragem.ListarUsoBarragemBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdUsoBarragemBarragem")]
        public async Task<UsoBarragemBarragem> BuscarPorIdUsoBarragemBarragem(int id)
        {
            var objUsoBarragemBarragemModel = await _IAplicacaoUsoBarragemBarragem.BuscarPorId(id);
            return objUsoBarragemBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarUsoBarragemBarragem")]
        public async Task<List<Notifica>> AdicionarUsoBarragemBarragem([FromBody] UsoBarragemBarragemModel UsoBarragemBarragemModel)
        {
            var UsoBarragemBarragem = new UsoBarragemBarragem()
            {
                BarragemId = UsoBarragemBarragemModel.BarragemId,
                DataAlteracao= UsoBarragemBarragemModel.DataAlteracao,
                DataCadastro = UsoBarragemBarragemModel.DataCadastro,
                UsoBarragemId = UsoBarragemBarragemModel.UsoBarragemId,
                Id = UsoBarragemBarragemModel.Id,
                    
            }
            ;

             await _IAplicacaoUsoBarragemBarragem.AdicionarUsoBarragemBarragem(UsoBarragemBarragem);

            return UsoBarragemBarragem.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaUsoBarragemBarragem")]
        public async Task<List<Notifica>> AtualizaUsoBarragemBarragem([FromBody]UsoBarragemBarragemModel UsoBarragemBarragemModel)
        {
            var objUsoBarragemBarragem = await _IAplicacaoUsoBarragemBarragem.BuscarPorId(UsoBarragemBarragemModel.Id);

            objUsoBarragemBarragem.Id = UsoBarragemBarragemModel.Id;
            objUsoBarragemBarragem.BarragemId = UsoBarragemBarragemModel.BarragemId;
            objUsoBarragemBarragem.DataCadastro = UsoBarragemBarragemModel.DataCadastro;
            objUsoBarragemBarragem.UsoBarragemId = UsoBarragemBarragemModel.UsoBarragemId;
           
            objUsoBarragemBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoUsoBarragemBarragem.AtualizaUsoBarragemBarragem(objUsoBarragemBarragem);

            return objUsoBarragemBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirUsoBarragemBarragem")]
        public async Task<List<Notifica>> ExcluirUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragemModel)
        {
            var objUsoBarragemBarragemModel = await _IAplicacaoUsoBarragemBarragem.BuscarPorId(UsoBarragemBarragemModel.Id);

            await _IAplicacaoUsoBarragemBarragem.Excluir(objUsoBarragemBarragemModel);

            return objUsoBarragemBarragemModel.Notificacoes;
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
        [HttpPost("/API/ListarUsoBarragemBarragemBarragemId")]
        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId([FromBody] int idBarragem)
        {
            List<UsoBarragemBarragem> lstUsoBarragemBarragem = new List<UsoBarragemBarragem>();
            lstUsoBarragemBarragem = await _IAplicacaoUsoBarragemBarragem.ListarUsoBarragemBarragemBarragemId(idBarragem);
         
            
            return lstUsoBarragemBarragem;
        }
    }
}
