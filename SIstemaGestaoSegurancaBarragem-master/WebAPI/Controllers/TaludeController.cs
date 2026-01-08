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
    public class TaludeController : ControllerBase
    {
        private readonly IAplicacaoTalude _IAplicacaoTalude;

        public TaludeController(IAplicacaoTalude aplicacaoTalude)
        {
            _IAplicacaoTalude = aplicacaoTalude;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTalude")]
        public async Task<List<Talude>> ListarTalude()
        {
            return await _IAplicacaoTalude.ListarTalude();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTalude")]
        public async Task<Talude> BuscarPorIdTalude(int id)
        {
            var objTaludeModel = await _IAplicacaoTalude.BuscarPorId(id);
            return objTaludeModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTalude")]
        public async Task<List<Notifica>> AdicionarTalude([FromBody] TaludeModel taludeModel)
        {
            var talude = new Talude()
            {
                BarragemId = taludeModel.BarragemId,
                DataAlteracao= taludeModel.DataAlteracao,
                DataCadastro = taludeModel.DataCadastro,
                InclinacaoTaludeJusante = taludeModel.InclinacaoTaludeJusante,
                InclinacaoTaludeMontante = taludeModel.InclinacaoTaludeMontante,
                Id = taludeModel.Id,
                TipoProtecaoSuperficieJusante = taludeModel.TipoProtecaoSuperficieJusante,
                TipoProtecaoSuperficieMontante = taludeModel.TipoProtecaoSuperficieMontante
                   
            }
            ;

             await _IAplicacaoTalude.AdicionarTalude(talude);

            return talude.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTalude")]
        public async Task<List<Notifica>> AtualizaTalude([FromBody]TaludeModel taludeModel)
        {
            var objTalude = await _IAplicacaoTalude.BuscarPorId(taludeModel.Id);

            objTalude.Id = taludeModel.Id;
            objTalude.InclinacaoTaludeJusante = taludeModel.InclinacaoTaludeJusante;
            objTalude.InclinacaoTaludeMontante = taludeModel.InclinacaoTaludeMontante;
            objTalude.TipoProtecaoSuperficieJusante = taludeModel.TipoProtecaoSuperficieJusante;
            objTalude.TipoProtecaoSuperficieMontante = taludeModel.TipoProtecaoSuperficieMontante;
           
            objTalude.DataAlteracao = DateTime.Now;

            await _IAplicacaoTalude.AtualizaTalude(objTalude);

            return objTalude.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTalude")]
        public async Task<List<Notifica>> ExcluirTalude(Talude taludeModel)
        {
            var objTaludeModel = await _IAplicacaoTalude.BuscarPorId(taludeModel.Id);

            await _IAplicacaoTalude.Excluir(objTaludeModel);

            return objTaludeModel.Notificacoes;
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
        [HttpPost("/API/ListarTaludeBarragemId")]
        public async Task<List<Talude>> ListarTaludeBarragemId([FromBody] int idBarragem)
        {
            List<Talude> lstTalude = new List<Talude>();
            lstTalude = await _IAplicacaoTalude.ListarTaludeBarragemId(idBarragem);
         
            
            return lstTalude;
        }
    }
}
