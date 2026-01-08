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
    public class UsoSoloPredominanteController : ControllerBase
    {
        private readonly IAplicacaoUsoSoloPredominante _IAplicacaoUsoSoloPredominante;

        public UsoSoloPredominanteController(IAplicacaoUsoSoloPredominante aplicacaoUsoSoloPredominante)
        {
            _IAplicacaoUsoSoloPredominante = aplicacaoUsoSoloPredominante;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarUsoSoloPredominante")]
        public async Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante()
        {
            return await _IAplicacaoUsoSoloPredominante.ListarUsoSoloPredominante();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdUsoSoloPredominante")]
        public async Task<UsoSoloPredominante> BuscarPorIdUsoSoloPredominante( int id)
        {
            var objUsoSoloPredominanteModel = await _IAplicacaoUsoSoloPredominante.BuscarPorId(id);
            return objUsoSoloPredominanteModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarUsoSoloPredominante")]
        public async Task<List<Notifica>> AdicionarUsoSoloPredominante(UsoSoloPredominanteModel usoSoloPredominanteModel)
        {
            var objUsoSoloPredominanteModel = new UsoSoloPredominante()
            {
                Id = usoSoloPredominanteModel.Id,
                Nome = usoSoloPredominanteModel.Nome
            };

             await _IAplicacaoUsoSoloPredominante.Adicionar(objUsoSoloPredominanteModel);

            return objUsoSoloPredominanteModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaUsoSoloPredominante")]
        public async Task<List<Notifica>> AtualizaUsoSoloPredominante(UsoSoloPredominanteModel usoSoloPredominanteModel)
        {
            var objUsoSoloPredominante = await _IAplicacaoUsoSoloPredominante.BuscarPorId(usoSoloPredominanteModel.Id);

            objUsoSoloPredominante.Id = usoSoloPredominanteModel.Id;
            objUsoSoloPredominante.Nome = usoSoloPredominanteModel.Nome;
            objUsoSoloPredominante.DataAlteracao = DateTime.Now;

            await _IAplicacaoUsoSoloPredominante.Atualizar(objUsoSoloPredominante);

            return objUsoSoloPredominante.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirUsoSoloPredominante")]
        public async Task<List<Notifica>> ExcluirUsoSoloPredominante(UsoSoloPredominanteModel usoSoloPredominanteModel)
        {
            var objUsoSoloPredominanteModel = await _IAplicacaoUsoSoloPredominante.BuscarPorId(usoSoloPredominanteModel.Id);

            await _IAplicacaoUsoSoloPredominante.Excluir(objUsoSoloPredominanteModel);

            return objUsoSoloPredominanteModel.Notificacoes;
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
