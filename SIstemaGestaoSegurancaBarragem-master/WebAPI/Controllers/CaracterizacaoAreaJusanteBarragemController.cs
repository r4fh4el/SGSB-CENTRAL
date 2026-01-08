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
    public class CaracterizacaoAreaJusanteBarragemController : ControllerBase
    {
        private readonly IAplicacaoCaracterizacaoAreaJusanteBarragem _IAplicacaoCaracterizacaoAreaJusanteBarragem;

        public CaracterizacaoAreaJusanteBarragemController(IAplicacaoCaracterizacaoAreaJusanteBarragem aplicacaoCaracterizacaoAreaJusanteBarragem)
        {
            _IAplicacaoCaracterizacaoAreaJusanteBarragem = aplicacaoCaracterizacaoAreaJusanteBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarCaracterizacaoAreaJusanteBarragem")]
        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragem()
        {
            return await _IAplicacaoCaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdCaracterizacaoAreaJusanteBarragem")]
        public async Task<CaracterizacaoAreaJusanteBarragem> BuscarPorIdCaracterizacaoAreaJusanteBarragem(int id)
        {
            var objCaracterizacaoAreaJusanteBarragem = await _IAplicacaoCaracterizacaoAreaJusanteBarragem.BuscarPorId(id);
            return objCaracterizacaoAreaJusanteBarragem;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionaCaracterizacaoAreaJusanteBarragem")]
        public async Task<bool> AdicionaCaracterizacaoAreaJusanteBarragem([FromBody] CaracterizacaoAreaJusanteBarragemModel caracterizacaoAreaJusanteBarragemModel)
        {
            var caracterizacaoAreaJusanteBarragem = new CaracterizacaoAreaJusanteBarragem()
            {
                BarragemId = caracterizacaoAreaJusanteBarragemModel.BarragemId,
                DistantciaKm = caracterizacaoAreaJusanteBarragemModel.DistantciaKm,
                TipoEdificacaoId = caracterizacaoAreaJusanteBarragemModel.TipoEdificacaoId
            

            };



            await _IAplicacaoCaracterizacaoAreaJusanteBarragem.Adicionar(caracterizacaoAreaJusanteBarragem);

            return true;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AtualizaCaracterizacaoAreaJusanteBarragem")]
        public async Task<List<Notifica>> AtualizaCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragemModel)
        {
            var objCaracterizacaoAreaJusanteBarragem = await _IAplicacaoCaracterizacaoAreaJusanteBarragem.BuscarPorId(caracterizacaoAreaJusanteBarragemModel.Id);

            objCaracterizacaoAreaJusanteBarragem.Id = caracterizacaoAreaJusanteBarragemModel.Id;
            objCaracterizacaoAreaJusanteBarragem.DistantciaKm = caracterizacaoAreaJusanteBarragemModel.DistantciaKm;
            objCaracterizacaoAreaJusanteBarragem.TipoEdificacao = caracterizacaoAreaJusanteBarragemModel.TipoEdificacao;
            objCaracterizacaoAreaJusanteBarragem.TipoEdificacaoId = caracterizacaoAreaJusanteBarragemModel.TipoEdificacaoId;
        
            objCaracterizacaoAreaJusanteBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoCaracterizacaoAreaJusanteBarragem.AtualizaCaracterizacaoAreaJusanteBarragem(objCaracterizacaoAreaJusanteBarragem);

            return objCaracterizacaoAreaJusanteBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirCaracterizacaoAreaJusanteBarragem")]
        public async Task<List<Notifica>> ExcluirCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragemModel)
        {
            var objCaracterizacaoAreaJusanteBarragem = await _IAplicacaoCaracterizacaoAreaJusanteBarragem.BuscarPorId(caracterizacaoAreaJusanteBarragemModel.Id);

            await _IAplicacaoCaracterizacaoAreaJusanteBarragem.Excluir(objCaracterizacaoAreaJusanteBarragem);

            return objCaracterizacaoAreaJusanteBarragem.Notificacoes;
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
        [HttpPost("/API/ListarCaracterizacaoAreaJusanteBarragemBarragemId")]
        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragemBarragemId([FromBody] int idBarragem)
        {
            List<CaracterizacaoAreaJusanteBarragem> lstCaracterizacaoAreaJusanteBarragem = new List<CaracterizacaoAreaJusanteBarragem>();
            lstCaracterizacaoAreaJusanteBarragem = await _IAplicacaoCaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragemBarragemId(idBarragem);


            return lstCaracterizacaoAreaJusanteBarragem;
        }
    }
}

