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
    public class CategoriaRiscoController : ControllerBase
    {
        private readonly IAplicacaoCategoriaRisco _IAplicacaoCategoriaRisco;

        public CategoriaRiscoController(IAplicacaoCategoriaRisco aplicacaoCategoriaRisco)
        {
            _IAplicacaoCategoriaRisco = aplicacaoCategoriaRisco;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarCategoriaRisco")]
        public async Task<List<CategoriaRisco>> ListarCategoriaRisco()
        {
            return await _IAplicacaoCategoriaRisco.ListarCategoriaRisco();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdCategoriaRisco")]
        public async Task<CategoriaRisco> BuscarPorIdCategoriaRisco( int id)
        {
            var objCategoriaRiscoModel = await _IAplicacaoCategoriaRisco.BuscarPorId(id);
            return objCategoriaRiscoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarCategoriaRisco")]
        public async Task<List<Notifica>> AdicionarCategoriaRisco(CategoriaRiscoModel CategoriaRiscoModel)
        {
            var objCategoriaRiscoModel = new CategoriaRisco()
            {
                Id = CategoriaRiscoModel.Id,
                CT_A = CategoriaRiscoModel.CT_A,

                BarragemId = CategoriaRiscoModel.BarragemId,
                CT_B = CategoriaRiscoModel.CT_B,
                CT_C = CategoriaRiscoModel.CT_C,
                CT_D = CategoriaRiscoModel.CT_D,
                CT_E = CategoriaRiscoModel.CT_E,
                EC_H = CategoriaRiscoModel.EC_H,
                EC_I = CategoriaRiscoModel.EC_I,
                EC_J = CategoriaRiscoModel.EC_J,
                EC_L = CategoriaRiscoModel.EC_L,
                PS_N = CategoriaRiscoModel.PS_N,
                PS_O = CategoriaRiscoModel.PS_O,
                PS_P = CategoriaRiscoModel.PS_P,
                PS_Q = CategoriaRiscoModel.PS_Q,
          
                
                PS_R = CategoriaRiscoModel.PS_R,
                ValorTotal = CategoriaRiscoModel.ValorTotal,
                ValorTotalEC = CategoriaRiscoModel.ValorTotalEC,
                DataAlteracao = DateTime.Now,
                DataCadastro = DateTime.Now


            };

             await _IAplicacaoCategoriaRisco.Adicionar(objCategoriaRiscoModel);

            return objCategoriaRiscoModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaCategoriaRisco")]
        public async Task<List<Notifica>> AtualizaCategoriaRisco(CategoriaRiscoModel CategoriaRiscoModel)
        {
            var objCategoriaRisco = await _IAplicacaoCategoriaRisco.BuscarPorId(CategoriaRiscoModel.Id);

            objCategoriaRisco.Id = CategoriaRiscoModel.Id;
                objCategoriaRisco.EC_H = CategoriaRiscoModel.EC_H;

                objCategoriaRisco.BarragemId = CategoriaRiscoModel.BarragemId;
                objCategoriaRisco.EC_L = CategoriaRiscoModel.EC_L;
          
                objCategoriaRisco.ValorTotal = CategoriaRiscoModel.ValorTotal;
                objCategoriaRisco.ValorTotalEC = CategoriaRiscoModel.ValorTotalEC;
                objCategoriaRisco.EC_H = CategoriaRiscoModel.EC_H;
                objCategoriaRisco.EC_I = CategoriaRiscoModel.EC_I;
                objCategoriaRisco.EC_J = CategoriaRiscoModel.EC_J; 
                objCategoriaRisco.EC_L= CategoriaRiscoModel.EC_L;
                objCategoriaRisco.CT_C = CategoriaRiscoModel.CT_C;
                objCategoriaRisco.CT_E = CategoriaRiscoModel.CT_E;
                objCategoriaRisco.CT_A = CategoriaRiscoModel.CT_A;
                objCategoriaRisco.CT_B = CategoriaRiscoModel.CT_B;
                objCategoriaRisco.CT_D = CategoriaRiscoModel.CT_D;
                objCategoriaRisco.PS_N = CategoriaRiscoModel.PS_N;
                objCategoriaRisco.PS_O = CategoriaRiscoModel.PS_O;
                objCategoriaRisco.PS_P= CategoriaRiscoModel.PS_P;

            objCategoriaRisco.PS_Q = CategoriaRiscoModel.PS_Q;
            objCategoriaRisco.PS_R = CategoriaRiscoModel.PS_R;





            await _IAplicacaoCategoriaRisco.Atualizar(objCategoriaRisco);

            return objCategoriaRisco.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirCategoriaRisco")]
        public async Task<List<Notifica>> ExcluirCategoriaRisco(CategoriaRiscoModel CategoriaRiscoModel)
        {
            var objCategoriaRiscoModel = await _IAplicacaoCategoriaRisco.BuscarPorId(CategoriaRiscoModel.Id);

            await _IAplicacaoCategoriaRisco.Excluir(objCategoriaRiscoModel);

            return objCategoriaRiscoModel.Notificacoes;
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
