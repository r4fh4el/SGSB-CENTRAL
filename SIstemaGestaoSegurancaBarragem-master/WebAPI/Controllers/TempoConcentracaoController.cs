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
    public class TempoConcentracaoController : ControllerBase
    {
        private readonly IAplicacaoTempoConcentracao _IAplicacaoTempoConcentracao;

        public TempoConcentracaoController(IAplicacaoTempoConcentracao aplicacaoTempoConcentracao)
        {
            _IAplicacaoTempoConcentracao = aplicacaoTempoConcentracao;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTempoConcentracao")]
        public async Task<List<TempoConcentracao>> ListarTempoConcentracao()
        {
            return await _IAplicacaoTempoConcentracao.ListarTempoConcentracao();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTempoConcentracao")]
        public async Task<TempoConcentracao> BuscarPorIdTempoConcentracao( int id)
        {
            var objTempoConcentracaoModel = await _IAplicacaoTempoConcentracao.BuscarPorId(id);
            return objTempoConcentracaoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTempoConcentracao")]
        public async Task<List<Notifica>> AdicionarTempoConcentracao( TempoConcentracaoModel tempoConcentracaoModel)
        {
            var objTempoConcentracao = new TempoConcentracao();
            objTempoConcentracao.Id = tempoConcentracaoModel.Id;
            objTempoConcentracao.AreaDrenagem_A = tempoConcentracaoModel.AreaDrenagem_A;
            objTempoConcentracao.ComprimentoRioPrincipal_L = tempoConcentracaoModel.ComprimentoRioPrincipal_L;
            objTempoConcentracao.DeclividadeBacia_S = tempoConcentracaoModel.DeclividadeBacia_S;
            objTempoConcentracao.DataAlteracao = tempoConcentracaoModel.DataAlteracao;
            objTempoConcentracao.DataCadastro = tempoConcentracaoModel.DataCadastro;
            objTempoConcentracao.ResultadoCarter = tempoConcentracaoModel.ResultadoCarter;
            objTempoConcentracao.ResultadoCorpsEngineers = tempoConcentracaoModel.ResultadoCorpsEngineers;
            objTempoConcentracao.ResultadoDooge = tempoConcentracaoModel.ResultadoDooge;
            objTempoConcentracao.ResultadoVenTeChow = tempoConcentracaoModel.ResultadoVenTeChow;
            objTempoConcentracao.ResultadoKirpich = tempoConcentracaoModel.ResultadoKirpich;
            objTempoConcentracao.BarragemId = tempoConcentracaoModel.BarragemId;

            await _IAplicacaoTempoConcentracao.Adicionar(objTempoConcentracao);

            return objTempoConcentracao.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTempoConcentracao")]
        public async Task<List<Notifica>> AtualizaTempoConcentracao(TempoConcentracao tempoConcentracaoModel)
        {
            var objTempoConcentracao = await _IAplicacaoTempoConcentracao.BuscarPorId(tempoConcentracaoModel.Id);

            objTempoConcentracao.Id = tempoConcentracaoModel.Id;
            objTempoConcentracao.NomePropriedade = tempoConcentracaoModel.NomePropriedade;
            objTempoConcentracao.AreaDrenagem_A = tempoConcentracaoModel.AreaDrenagem_A;
            objTempoConcentracao.ComprimentoRioPrincipal_L = tempoConcentracaoModel.ComprimentoRioPrincipal_L;
            objTempoConcentracao.DeclividadeBacia_S = tempoConcentracaoModel.DeclividadeBacia_S;
            objTempoConcentracao.DataAlteracao = tempoConcentracaoModel.DataAlteracao;
            objTempoConcentracao.DataCadastro = tempoConcentracaoModel.DataCadastro;
            objTempoConcentracao.ResultadoCarter = tempoConcentracaoModel.ResultadoCarter;
            objTempoConcentracao.ResultadoCorpsEngineers = tempoConcentracaoModel.ResultadoCorpsEngineers;
            objTempoConcentracao.ResultadoDooge = tempoConcentracaoModel.ResultadoDooge;
            objTempoConcentracao.ResultadoVenTeChow = tempoConcentracaoModel.ResultadoVenTeChow;
            objTempoConcentracao.ResultadoKirpich = tempoConcentracaoModel.ResultadoKirpich;
            objTempoConcentracao.BarragemId = tempoConcentracaoModel.BarragemId;

            objTempoConcentracao.DataAlteracao = DateTime.Now;

            await _IAplicacaoTempoConcentracao.Atualizar(objTempoConcentracao);

            return objTempoConcentracao.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTempoConcentracao")]
        public async Task<List<Notifica>> ExcluirTempoConcentracao(TempoConcentracao tempoConcentracaoModel)
        {
            var objTempoConcentracaoModel = await _IAplicacaoTempoConcentracao.BuscarPorId(tempoConcentracaoModel.Id);

            await _IAplicacaoTempoConcentracao.Excluir(objTempoConcentracaoModel);

            return objTempoConcentracaoModel.Notificacoes;
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
