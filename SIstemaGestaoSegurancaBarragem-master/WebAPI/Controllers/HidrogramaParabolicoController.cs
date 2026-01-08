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
    public class HidrogramaParabolicoController : ControllerBase
    {
        private readonly IAplicacaoHidrogramaParabolico _IAplicacaoHidrogramaParabolico;

        public HidrogramaParabolicoController(IAplicacaoHidrogramaParabolico aplicacaoHidrogramaParabolico)
        {
            _IAplicacaoHidrogramaParabolico = aplicacaoHidrogramaParabolico;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarHidrogramaParabolico")]
        public async Task<List<HidrogramaParabolico>> ListarHidrogramaParabolico()
        {
            return await _IAplicacaoHidrogramaParabolico.ListarHidrogramaParabolico();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdHidrogramaParabolico")]
        public async Task<HidrogramaParabolico> BuscarPorIdHidrogramaParabolico( int id)
        {
            var objHidrogramaParabolicoModel = await _IAplicacaoHidrogramaParabolico.BuscarPorId(id);
            return objHidrogramaParabolicoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarHidrogramaParabolico")]
        public async Task<List<Notifica>> AdicionarHidrogramaParabolico(HidrogramaParabolicoModel HidrogramaParabolicoModel)
        {
            var objHidrogramaParabolicoModel = new HidrogramaParabolico()
            {
                Id = HidrogramaParabolicoModel.Id,
                BarragemId = HidrogramaParabolicoModel.BarragemId,
                valorQp = HidrogramaParabolicoModel.valorQp,
                valorTempoHora = HidrogramaParabolicoModel.valorTempoHora,
                DataAlteracao = HidrogramaParabolicoModel.DataAlteracao,
                DataCadastro = HidrogramaParabolicoModel.DataCadastro,
                valorVazao = HidrogramaParabolicoModel.valorVazao

            };

             await _IAplicacaoHidrogramaParabolico.Adicionar(objHidrogramaParabolicoModel);

            return objHidrogramaParabolicoModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaHidrogramaParabolico")]
        public async Task<List<Notifica>> AtualizaHidrogramaParabolico(HidrogramaParabolicoModel HidrogramaParabolicoModel)
        {
            var objHidrogramaParabolico = await _IAplicacaoHidrogramaParabolico.BuscarPorId(HidrogramaParabolicoModel.Id);

            objHidrogramaParabolico.Id = HidrogramaParabolicoModel.Id;
            objHidrogramaParabolico.BarragemId = HidrogramaParabolicoModel.BarragemId;
            objHidrogramaParabolico.valorQp = HidrogramaParabolicoModel.valorQp;
            objHidrogramaParabolico.valorTempoHora = HidrogramaParabolicoModel.valorTempoHora;
            objHidrogramaParabolico.DataAlteracao = HidrogramaParabolicoModel.DataAlteracao;
            objHidrogramaParabolico.DataCadastro = HidrogramaParabolicoModel.DataCadastro;
            objHidrogramaParabolico.valorVazao = HidrogramaParabolicoModel.valorVazao;

            await _IAplicacaoHidrogramaParabolico.Atualizar(objHidrogramaParabolico);

            return objHidrogramaParabolico.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirHidrogramaParabolico")]
        public async Task<List<Notifica>> ExcluirHidrogramaParabolico(HidrogramaParabolicoModel HidrogramaParabolicoModel)
        {
            var objHidrogramaParabolicoModel = await _IAplicacaoHidrogramaParabolico.BuscarPorId(HidrogramaParabolicoModel.Id);

            await _IAplicacaoHidrogramaParabolico.Excluir(objHidrogramaParabolicoModel);

            return objHidrogramaParabolicoModel.Notificacoes;
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
