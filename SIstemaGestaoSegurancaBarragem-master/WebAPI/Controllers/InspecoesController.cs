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
    public class InspecoesController : ControllerBase
    {
        private readonly IAplicacaoInspecoes _IAplicacaoInspecoes;

        public InspecoesController(IAplicacaoInspecoes aplicacaoInspecoes)
        {
            _IAplicacaoInspecoes = aplicacaoInspecoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarInspecoes")]
        public async Task<List<Inspecoes>> ListarInspecoes()
        {
            return await _IAplicacaoInspecoes.ListarInspecoes();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdInspecoes")]
        public async Task<Inspecoes> BuscarPorIdInspecoes(int id)
        {
            var objInspecoes = await _IAplicacaoInspecoes.BuscarPorId(id);
            return objInspecoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarInspecoes")]
        public async Task<List<Notifica>> AdicionarInspecoes(InspecoesModel inspecoesModel)
        {
            var objInspecoes = new Inspecoes();
            objInspecoes.Id = inspecoesModel.Id;
            objInspecoes.Nome_Setor = inspecoesModel.Nome_Setor;

            objInspecoes.BarragemId = inspecoesModel.BarragemId;
            objInspecoes.DataEstudoRompimento = inspecoesModel.DataEstudoRompimento;
            objInspecoes.DataPlanoAcaoEmergencia = inspecoesModel.DataPlanoAcaoEmergencia;
            objInspecoes.DataRevisaoPeriodicaRecente = inspecoesModel.DataRevisaoPeriodicaRecente;
            objInspecoes.DataUltimaInspecaoEspecial = inspecoesModel.DataUltimaInspecaoEspecial;
            objInspecoes.EnumFrequencia = inspecoesModel.EnumFrequencia;
            objInspecoes.PossuiEstudoRompimento = inspecoesModel.PossuiEstudoRompimento;
            objInspecoes.PossuiPae = inspecoesModel.PossuiPae;

            await _IAplicacaoInspecoes.AdicionarInspecoes(objInspecoes);

            return objInspecoes.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaInspecoes")]
        public async Task<List<Notifica>> AtualizaInspecoes(InspecoesModel inspecoesModel)
        {
            var objInspecoes = await _IAplicacaoInspecoes.BuscarPorId(inspecoesModel.Id);

            objInspecoes.Id = inspecoesModel.Id;
            objInspecoes.Nome_Setor = inspecoesModel.Nome_Setor;
    
            objInspecoes.BarragemId = inspecoesModel.BarragemId;
            objInspecoes.DataEstudoRompimento = inspecoesModel.DataEstudoRompimento;
            objInspecoes.DataPlanoAcaoEmergencia = inspecoesModel.DataPlanoAcaoEmergencia;
            objInspecoes.DataRevisaoPeriodicaRecente = inspecoesModel.DataRevisaoPeriodicaRecente;
            objInspecoes.DataUltimaInspecaoEspecial = inspecoesModel.DataUltimaInspecaoEspecial;
            objInspecoes.EnumFrequencia = inspecoesModel.EnumFrequencia;
            objInspecoes.PossuiEstudoRompimento = inspecoesModel.PossuiEstudoRompimento;
            objInspecoes.PossuiPae = inspecoesModel.PossuiPae;
           
            objInspecoes.DataAlteracao = DateTime.Now;

            await _IAplicacaoInspecoes.AtualizaInspecoes(objInspecoes);

            return objInspecoes.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirInspecoes")]
        public async Task<List<Notifica>> ExcluirInspecoes(Inspecoes inspecoesModel)
        {
            var objInspecoes = await _IAplicacaoInspecoes.BuscarPorId(inspecoesModel.Id);

            await _IAplicacaoInspecoes.Excluir(objInspecoes);

            return objInspecoes.Notificacoes;
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
        [HttpPost("/API/ListarInspecoesBarragemId")]
        public async Task<List<Inspecoes>> ListarInspecoesBarragemId([FromBody] int idBarragem)
        {
            List<Inspecoes> lstInspecoes = new List<Inspecoes>();
            lstInspecoes = await _IAplicacaoInspecoes.ListarInspecoesBarragemId(idBarragem);


            return lstInspecoes;
        }
    }
}
