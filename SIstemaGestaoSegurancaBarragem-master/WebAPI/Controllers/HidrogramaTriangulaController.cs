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
    public class HidrogramaTriangulaController : ControllerBase
    {
        private readonly IAplicacaoHidrogramaTriangula _IAplicacaoHidrogramaTriangula;

        public HidrogramaTriangulaController(IAplicacaoHidrogramaTriangula aplicacaoHidrogramaTriangula)
        {
            _IAplicacaoHidrogramaTriangula = aplicacaoHidrogramaTriangula;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarHidrogramaTriangula")]
        public async Task<List<HidrogramaTriangula>> ListarHidrogramaTriangula()
        {
            return await _IAplicacaoHidrogramaTriangula.ListarHidrogramaTriangula();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdHidrogramaTriangula")]
        public async Task<HidrogramaTriangula> BuscarPorIdHidrogramaTriangula( int id)
        {
            var objHidrogramaTriangulaModel = await _IAplicacaoHidrogramaTriangula.BuscarPorId(id);
            return objHidrogramaTriangulaModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarHidrogramaTriangula")]
        public async Task<List<Notifica>> AdicionarHidrogramaTriangula(HidrogramaTriangulaModel HidrogramaTriangulaModel)
        {
            var objHidrogramaTriangulaModel = new HidrogramaTriangula()
            {
                Id = HidrogramaTriangulaModel.Id,
                valorQp = HidrogramaTriangulaModel.valorQp,
                valorTempoPico = HidrogramaTriangulaModel.valorTempoPico,
                volumesReservatorio = HidrogramaTriangulaModel.volumesReservatorio,
                DataCadastro = HidrogramaTriangulaModel.DataCadastro,
                DataAlteracao = HidrogramaTriangulaModel.DataAlteracao,
                BarragemId = HidrogramaTriangulaModel.BarragemId

            };

             await _IAplicacaoHidrogramaTriangula.Adicionar(objHidrogramaTriangulaModel);

            return objHidrogramaTriangulaModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaHidrogramaTriangula")]
        public async Task<List<Notifica>> AtualizaHidrogramaTriangula(HidrogramaTriangulaModel HidrogramaTriangulaModel)
        {
            var objHidrogramaTriangula = await _IAplicacaoHidrogramaTriangula.BuscarPorId(HidrogramaTriangulaModel.Id);

            objHidrogramaTriangula.Id = HidrogramaTriangulaModel.Id;
            objHidrogramaTriangula.valorQp = HidrogramaTriangulaModel.valorQp;
                objHidrogramaTriangula.valorTempoPico = HidrogramaTriangulaModel.valorTempoPico;
                objHidrogramaTriangula.volumesReservatorio = HidrogramaTriangulaModel.volumesReservatorio;
               objHidrogramaTriangula.DataCadastro = HidrogramaTriangulaModel.DataCadastro;
                objHidrogramaTriangula.DataAlteracao = HidrogramaTriangulaModel.DataAlteracao;
                objHidrogramaTriangula.BarragemId = HidrogramaTriangulaModel.BarragemId;

            await _IAplicacaoHidrogramaTriangula.Atualizar(objHidrogramaTriangula);

            return objHidrogramaTriangula.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirHidrogramaTriangula")]
        public async Task<List<Notifica>> ExcluirHidrogramaTriangula(HidrogramaTriangulaModel HidrogramaTriangulaModel)
        {
            var objHidrogramaTriangulaModel = await _IAplicacaoHidrogramaTriangula.BuscarPorId(HidrogramaTriangulaModel.Id);

            await _IAplicacaoHidrogramaTriangula.Excluir(objHidrogramaTriangulaModel);

            return objHidrogramaTriangulaModel.Notificacoes;
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
