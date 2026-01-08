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
    public class VertedouroController : ControllerBase
    {
        private readonly IAplicacaoVertedouro _IAplicacaoVertedouro;

        public VertedouroController(IAplicacaoVertedouro aplicacaoVertedouro)
        {
            _IAplicacaoVertedouro = aplicacaoVertedouro;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarVertedouro")]
        public async Task<List<Vertedouro>> ListarVertedouro()
        {
            return await _IAplicacaoVertedouro.ListarVertedouro();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpHead("/API/BuscarPorIdVertedouro")]
        public async Task<Vertedouro> BuscarPorIdVertedouro(VertedouroModel vertedouroModel)
        {
            var objVertedouroModel = await _IAplicacaoVertedouro.BuscarPorId(vertedouroModel.Id);
            return objVertedouroModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarVertedouro")]
        public async Task<List<Notifica>> AdicionarVertedouro(VertedouroModel vertedouroModel)
        {
            var objVertedouroModel = new Vertedouro()
            {
                //Id = vertedouroModel.Id,
                LocalizacaoVertedouro = vertedouroModel.LocalizacaoVertedouro,
                LarguraTotalVertedouro = vertedouroModel.LarguraTotalVertedouro,
                CotaSoleiraVetedouro = vertedouroModel.CotaSoleiraVetedouro,
                Altura = vertedouroModel.Altura,
                ComprimentoTotalVertedouro = vertedouroModel.ComprimentoTotalVertedouro,
                QuantidadeComportas = vertedouroModel.QuantidadeComportas,
                TempoRetornoVazaoMaximaProjetoAnos = vertedouroModel.TempoRetornoVazaoMaximaProjetoAnos,
                TipoAcionamentoComportas = vertedouroModel.TipoAcionamentoComportas,
                TipoEstruturaExtravasoraPrincipal = vertedouroModel.TipoEstruturaExtravasoraPrincipal,
                VazaoMaximaProjetoVertedor = vertedouroModel.VazaoMaximaProjetoVertedor,
                VertedouroControle = vertedouroModel.VertedouroControle,
                VetedouroAuxiliar = vertedouroModel.VetedouroAuxiliar,
                DataCadastro  = DateTime.Now,
                DataAlteracao = DateTime.Now,
                BarragemId = vertedouroModel.BarragemId,
            };

             await _IAplicacaoVertedouro.AdicionarVertedouro(objVertedouroModel);

            return objVertedouroModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaVertedouro")]
        public async Task<List<Notifica>> AtualizaVertedouro(VertedouroModel vertedouroModel)
        {
            var objVertedouro = await _IAplicacaoVertedouro.BuscarPorId(vertedouroModel.Id);

            objVertedouro.Id = vertedouroModel.Id > 0 ? vertedouroModel.Id : objVertedouro.Id;
            objVertedouro.LocalizacaoVertedouro = !string.IsNullOrEmpty(vertedouroModel.LocalizacaoVertedouro)  ? vertedouroModel.LocalizacaoVertedouro : objVertedouro.LocalizacaoVertedouro;
            objVertedouro.LarguraTotalVertedouro = vertedouroModel.LarguraTotalVertedouro > 0 ? vertedouroModel.LarguraTotalVertedouro : objVertedouro.LarguraTotalVertedouro;
            objVertedouro.CotaSoleiraVetedouro = vertedouroModel.CotaSoleiraVetedouro > 0 ? vertedouroModel.CotaSoleiraVetedouro : objVertedouro.CotaSoleiraVetedouro;
            objVertedouro.Altura = vertedouroModel.Altura > 0 ? vertedouroModel.Altura : objVertedouro.Altura;
            objVertedouro.ComprimentoTotalVertedouro = vertedouroModel.ComprimentoTotalVertedouro > 0 ? vertedouroModel.ComprimentoTotalVertedouro : objVertedouro.ComprimentoTotalVertedouro;
            objVertedouro.QuantidadeComportas = vertedouroModel.QuantidadeComportas > 0 ? vertedouroModel.QuantidadeComportas : objVertedouro.QuantidadeComportas;
            objVertedouro.TempoRetornoVazaoMaximaProjetoAnos = vertedouroModel.TempoRetornoVazaoMaximaProjetoAnos > 0 ? vertedouroModel.TempoRetornoVazaoMaximaProjetoAnos : objVertedouro.TempoRetornoVazaoMaximaProjetoAnos;
            objVertedouro.TipoAcionamentoComportas = vertedouroModel.TipoAcionamentoComportas;
            objVertedouro.TipoEstruturaExtravasoraPrincipal = !string.IsNullOrEmpty(vertedouroModel.TipoEstruturaExtravasoraPrincipal) ? vertedouroModel.TipoEstruturaExtravasoraPrincipal : objVertedouro.TipoEstruturaExtravasoraPrincipal;
            objVertedouro.VazaoMaximaProjetoVertedor = vertedouroModel.VazaoMaximaProjetoVertedor > 0 ? vertedouroModel.VazaoMaximaProjetoVertedor : objVertedouro.VazaoMaximaProjetoVertedor;
            objVertedouro.VertedouroControle = vertedouroModel.VertedouroControle;
            objVertedouro.VetedouroAuxiliar = vertedouroModel.VetedouroAuxiliar;
            objVertedouro.DataCadastro = objVertedouro.DataCadastro;
            objVertedouro.DataAlteracao = DateTime.Now;
            objVertedouro.BarragemId = vertedouroModel.BarragemId;
            await _IAplicacaoVertedouro.AtualizaVertedouro(objVertedouro);

            return objVertedouro.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpDelete("/API/ExcluirVertedouro")]
        public async Task<List<Notifica>> ExcluirVertedouro(VertedouroModel vertedouroModel)
        {
            var objVertedouroModel = await _IAplicacaoVertedouro.BuscarPorId(vertedouroModel.Id);

            await _IAplicacaoVertedouro.Excluir(objVertedouroModel);

            return objVertedouroModel.Notificacoes;
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
        [HttpPost("/API/ListarVertedouroBarragemId")]
        public async Task<List<Vertedouro>> ListarVertedouroBarragemId([FromBody] int idBarragem)
        {
            List<Vertedouro> lstVertedouro = new List<Vertedouro>();
            lstVertedouro = await _IAplicacaoVertedouro.ListarVertedouroBarragemId(idBarragem);


            return lstVertedouro;
        }
    }
}
