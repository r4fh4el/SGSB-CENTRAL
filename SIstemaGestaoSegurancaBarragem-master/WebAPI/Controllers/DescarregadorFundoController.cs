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
    public class DescarregadorFundoController : ControllerBase
    {
        private readonly IAplicacaoDescarregadorFundo _IAplicacaoDescarregadorFundo;

        public DescarregadorFundoController(IAplicacaoDescarregadorFundo aplicacaoDescarregadorFundo)
        {
            _IAplicacaoDescarregadorFundo = aplicacaoDescarregadorFundo;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarDescarregadorFundo")]
        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundo()
        {
            return await _IAplicacaoDescarregadorFundo.ListarDescarregadorFundo();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdDescarregadorFundo")]
        public async Task<DescarregadorFundo> BuscarPorIdDescarregadorFundo(int id)
        {
            var objDescarregadorFundoModel = await _IAplicacaoDescarregadorFundo.BuscarPorId(id);
            return objDescarregadorFundoModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarDescarregadorFundo")]
        public async Task<List<Notifica>> AdicionarDescarregadorFundo(DescarregadorFundoModel model)
        {
            var descarregadorFundo = new DescarregadorFundo()
            {
                CotaSoleiraEntrada = model.CotaSoleiraEntrada,
                ComandoDistancia = model.ComandoDistancia,

                BarragemId = model.BarragemId,
                Comprimento = model.Comprimento,
                DataAlteracao = model.DataAlteracao,
                DataCadastro = model.DataCadastro,
                Dimensao = model.Dimensao,
                
                FonteAlternativaEnergia = model.FonteAlternativaEnergia,
                Localizacao = model.Localizacao,
                Tipo = model.Tipo,
                TipoAcionamentoComporta = model.TipoAcionamentoComporta,
                TipoComporta = model.TipoComporta




            };

            await _IAplicacaoDescarregadorFundo.AdicionarDescarregadorFundo(descarregadorFundo);

            return descarregadorFundo.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaDescarregadorFundo")]
        public async Task<List<Notifica>> AtualizaDescarregadorFundo(DescarregadorFundo tipoMaterialBarragemModel)
        {
            var objDescarregadorFundo = await _IAplicacaoDescarregadorFundo.BuscarPorId(tipoMaterialBarragemModel.Id);

            objDescarregadorFundo.Id = tipoMaterialBarragemModel.Id;
            objDescarregadorFundo.ComandoDistancia = tipoMaterialBarragemModel.ComandoDistancia;
            objDescarregadorFundo.TipoAcionamentoComporta = tipoMaterialBarragemModel.TipoAcionamentoComporta;
            objDescarregadorFundo.TipoComporta = tipoMaterialBarragemModel.TipoComporta;
            objDescarregadorFundo.Comprimento = tipoMaterialBarragemModel.Comprimento;
            objDescarregadorFundo.CotaSoleiraEntrada = tipoMaterialBarragemModel.CotaSoleiraEntrada;
            objDescarregadorFundo.Dimensao = tipoMaterialBarragemModel.Dimensao;
            objDescarregadorFundo.FonteAlternativaEnergia = tipoMaterialBarragemModel.FonteAlternativaEnergia;
            objDescarregadorFundo.Localizacao = tipoMaterialBarragemModel.Localizacao;
            objDescarregadorFundo.Tipo = tipoMaterialBarragemModel.Tipo;
            objDescarregadorFundo.TipoAcionamentoComporta = tipoMaterialBarragemModel.TipoAcionamentoComporta;
            objDescarregadorFundo.BarragemId = tipoMaterialBarragemModel.BarragemId;

            objDescarregadorFundo.DataAlteracao = DateTime.Now;

            await _IAplicacaoDescarregadorFundo.AtualizaDescarregadorFundo(objDescarregadorFundo);

            return objDescarregadorFundo.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirDescarregadorFundo")]
        public async Task<List<Notifica>> ExcluirDescarregadorFundo(DescarregadorFundo tipoMaterialBarragemModel)
        {
            var objDescarregadorFundoModel = await _IAplicacaoDescarregadorFundo.BuscarPorId(tipoMaterialBarragemModel.Id);

            await _IAplicacaoDescarregadorFundo.Excluir(objDescarregadorFundoModel);

            return objDescarregadorFundoModel.Notificacoes;
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
        [HttpPost("/API/ListarDescarregadorFundoBarragemId")]
        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId([FromBody] int idBarragem)
        {
            List<DescarregadorFundo> lstDescarregadorFundo = new List<DescarregadorFundo>();
            lstDescarregadorFundo = await _IAplicacaoDescarregadorFundo.ListarDescarregadorFundoBarragemId(idBarragem);


            return lstDescarregadorFundo;
        }
    }
}

