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
    public class DanoPotencialAssociadoController : ControllerBase
    {
        private readonly IAplicacaoDanoPotencialAssociado _IAplicacaoDanoPotencialAssociado;

        public DanoPotencialAssociadoController(IAplicacaoDanoPotencialAssociado aplicacaoDanoPotencialAssociado)
        {
            _IAplicacaoDanoPotencialAssociado = aplicacaoDanoPotencialAssociado;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarDanoPotencialAssociado")]
        public async Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado()
        {
            return await _IAplicacaoDanoPotencialAssociado.ListarDanoPotencialAssociado();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdDanoPotencialAssociado")]
        public async Task<DanoPotencialAssociado> BuscarPorIdDanoPotencialAssociado( int id)
        {
            var objDanoPotencialAssociadoModel = await _IAplicacaoDanoPotencialAssociado.BuscarPorId(id);
            return objDanoPotencialAssociadoModel;
        } 
        
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/GetDanoPotencialAssociadoBarragemId")]
        public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId( int id)
        {
            var objDanoPotencialAssociadoModel = await _IAplicacaoDanoPotencialAssociado.GetDanoPotencialAssociadoBarragemId(id);
            return objDanoPotencialAssociadoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarDanoPotencialAssociado")]
        public async Task<List<Notifica>> AdicionarDanoPotencialAssociado(DanoPotencialAssociadoModel danoPotencialAssociadoModel)
        {
            var objDanoPotencialAssociadoModel = new DanoPotencialAssociado()
            {
                Id = danoPotencialAssociadoModel.Id,
                IA_Q2 = danoPotencialAssociadoModel.IA_Q2,

                BarragemId = danoPotencialAssociadoModel.BarragemId,
                DpaTotal = danoPotencialAssociadoModel.DpaTotal,
                IA_Q1 = danoPotencialAssociadoModel.IA_Q1,
                ISE_Q1 = danoPotencialAssociadoModel.ISE_Q1,
                IA_Resposta = danoPotencialAssociadoModel.IA_Resposta,
                ISE_Q2 = danoPotencialAssociadoModel.ISE_Q2,
                ISE_Q3 = danoPotencialAssociadoModel.ISE_Q3,
                ISE_Resposta = danoPotencialAssociadoModel.ISE_Resposta, 
                PPV_Q1 = danoPotencialAssociadoModel.PPV_Q1,
                PPV_Q2 = danoPotencialAssociadoModel.PPV_Q2,
                PPV_Q3 = danoPotencialAssociadoModel.PPV_Q3,
                PPV_Q4 = danoPotencialAssociadoModel.PPV_Q4,
                PPV_Resposta = danoPotencialAssociadoModel.PPV_Resposta,
                VTR_Q1 = danoPotencialAssociadoModel.VTR_Q1,
                VTR_Q2 = danoPotencialAssociadoModel.VTR_Q2,
                VTR_Q3 = danoPotencialAssociadoModel.VTR_Q3,
                VTR_Q4 = danoPotencialAssociadoModel.VTR_Q4,
        
                VTR_Resposta = danoPotencialAssociadoModel.ISE_Resposta,
                DataAlteracao = DateTime.Now,
                DataCadastro = DateTime.Now


            };

             await _IAplicacaoDanoPotencialAssociado.Adicionar(objDanoPotencialAssociadoModel);

            return objDanoPotencialAssociadoModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaDanoPotencialAssociado")]
        public async Task<List<Notifica>> AtualizaDanoPotencialAssociado(DanoPotencialAssociadoModel danoPotencialAssociadoModel)
        {
            var objDanoPotencialAssociado = await _IAplicacaoDanoPotencialAssociado.BuscarPorId(danoPotencialAssociadoModel.Id);

            objDanoPotencialAssociado.Id = danoPotencialAssociadoModel.Id;
                objDanoPotencialAssociado.IA_Q2 = danoPotencialAssociadoModel.IA_Q2;

                objDanoPotencialAssociado.BarragemId = danoPotencialAssociadoModel.BarragemId;
                objDanoPotencialAssociado.DpaTotal = danoPotencialAssociadoModel.DpaTotal;
                objDanoPotencialAssociado.IA_Q1 = danoPotencialAssociadoModel.IA_Q1;
                objDanoPotencialAssociado.ISE_Q1 = danoPotencialAssociadoModel.ISE_Q1;
                objDanoPotencialAssociado.IA_Resposta = danoPotencialAssociadoModel.IA_Resposta;
                objDanoPotencialAssociado.ISE_Q2 = danoPotencialAssociadoModel.ISE_Q2;
                objDanoPotencialAssociado.ISE_Q3 = danoPotencialAssociadoModel.ISE_Q3;
                objDanoPotencialAssociado.ISE_Resposta = danoPotencialAssociadoModel.ISE_Resposta; 
                objDanoPotencialAssociado.PPV_Q1 = danoPotencialAssociadoModel.PPV_Q1;
                objDanoPotencialAssociado.PPV_Q2 = danoPotencialAssociadoModel.PPV_Q2;
                objDanoPotencialAssociado.PPV_Q3 = danoPotencialAssociadoModel.PPV_Q3;
                objDanoPotencialAssociado.PPV_Q4 = danoPotencialAssociadoModel.PPV_Q4;
                objDanoPotencialAssociado.PPV_Resposta = danoPotencialAssociadoModel.PPV_Resposta;
                objDanoPotencialAssociado.VTR_Q1 = danoPotencialAssociadoModel.VTR_Q1;
                objDanoPotencialAssociado.VTR_Q2 = danoPotencialAssociadoModel.VTR_Q2;
                objDanoPotencialAssociado.VTR_Q3 = danoPotencialAssociadoModel.VTR_Q3;
                objDanoPotencialAssociado.VTR_Q4 = danoPotencialAssociadoModel.VTR_Q4;

            objDanoPotencialAssociado.VTR_Resposta = danoPotencialAssociadoModel.ISE_Resposta;





            await _IAplicacaoDanoPotencialAssociado.Atualizar(objDanoPotencialAssociado);

            return objDanoPotencialAssociado.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirDanoPotencialAssociado")]
        public async Task<List<Notifica>> ExcluirDanoPotencialAssociado(DanoPotencialAssociadoModel danoPotencialAssociadoModel)
        {
            var objDanoPotencialAssociadoModel = await _IAplicacaoDanoPotencialAssociado.BuscarPorId(danoPotencialAssociadoModel.Id);

            await _IAplicacaoDanoPotencialAssociado.Excluir(objDanoPotencialAssociadoModel);

            return objDanoPotencialAssociadoModel.Notificacoes;
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
