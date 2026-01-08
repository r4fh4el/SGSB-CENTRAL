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
    public class TomadaDaguaController : ControllerBase
    {
        private readonly IAplicacaoTomadaDagua _IAplicacaoTomadaDagua;

        public TomadaDaguaController(IAplicacaoTomadaDagua aplicacaoTomadaDagua)
        {
            _IAplicacaoTomadaDagua = aplicacaoTomadaDagua;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTomadaDagua")]
        public async Task<List<TomadaDagua>> ListarTomadaDagua()
        {
            return await _IAplicacaoTomadaDagua.ListarTomadaDagua();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTomadaDagua")]
        public async Task<TomadaDagua> BuscarPorIdTomadaDagua(int id)
        {
            var objTomadaDaguaModel = await _IAplicacaoTomadaDagua.BuscarPorId(id);
            return objTomadaDaguaModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTomadaDagua")]
        public async Task<List<Notifica>> AdicionarTomadaDagua(TomadaDaguaModel tomadaDaguaModel)
        {
            var objTomadaDagua = new TomadaDagua();

           objTomadaDagua.ComandoDistancia = tomadaDaguaModel.ComandoDistancia;
            objTomadaDagua.Comprimento = tomadaDaguaModel.Comprimento;
            objTomadaDagua.ControleEntrada = tomadaDaguaModel.ControleEntrada;
            objTomadaDagua.ControleSaida = tomadaDaguaModel.ControleSaida;
            objTomadaDagua.CotasTomadasDaguaEntrada = tomadaDaguaModel.CotasTomadasDaguaEntrada;
            objTomadaDagua.FonteAlternativaEnergia = tomadaDaguaModel.FonteAlternativaEnergia;
            objTomadaDagua.Localizacao = tomadaDaguaModel.Localizacao;
            objTomadaDagua.PossibilidadeManobraManual = tomadaDaguaModel.PossibilidadeManobraManual;
            objTomadaDagua.BarragemId = tomadaDaguaModel.BarragemId;
            objTomadaDagua.DataAlteracao = DateTime.Now;
            objTomadaDagua.DataCadastro = DateTime.Now;

            await _IAplicacaoTomadaDagua.AdicionarTomadaDagua(objTomadaDagua);

            return objTomadaDagua.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTomadaDagua")]
        public async Task<List<Notifica>> AtualizaTomadaDagua(TomadaDaguaModel tomadaDaguaModel)
        {
            var objTomadaDagua = await _IAplicacaoTomadaDagua.BuscarPorId(tomadaDaguaModel.Id);

            objTomadaDagua.Id = tomadaDaguaModel.Id;
       
            objTomadaDagua.ComandoDistancia = tomadaDaguaModel.ComandoDistancia;
            objTomadaDagua.Comprimento = tomadaDaguaModel.Comprimento;
            objTomadaDagua.ControleEntrada = tomadaDaguaModel.ControleEntrada;
            objTomadaDagua.ControleSaida = tomadaDaguaModel.ControleSaida;
            objTomadaDagua.CotasTomadasDaguaEntrada = tomadaDaguaModel.CotasTomadasDaguaEntrada;
            objTomadaDagua.FonteAlternativaEnergia = tomadaDaguaModel.FonteAlternativaEnergia;
            objTomadaDagua.Localizacao = tomadaDaguaModel.Localizacao;
            objTomadaDagua.PossibilidadeManobraManual = tomadaDaguaModel.PossibilidadeManobraManual;
            objTomadaDagua.BarragemId = tomadaDaguaModel.BarragemId;
  
            objTomadaDagua.DataAlteracao = DateTime.Now;

            await _IAplicacaoTomadaDagua.AtualizaTomadaDagua(objTomadaDagua);

            return objTomadaDagua.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTomadaDagua")]
        public async Task<List<Notifica>> ExcluirTomadaDagua(TomadaDagua tomadaDaguaModel)
        {
            var objTomadaDaguaModel = await _IAplicacaoTomadaDagua.BuscarPorId(tomadaDaguaModel.Id);

            await _IAplicacaoTomadaDagua.Excluir(objTomadaDaguaModel);

            return objTomadaDaguaModel.Notificacoes;
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
        [HttpPost("/API/ListarTomadaDaguaBarragemId")]
        public async Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId([FromBody] int idBarragem)
        {
            List<TomadaDagua> lstTomadaDagua = new List<TomadaDagua>();
            lstTomadaDagua = await _IAplicacaoTomadaDagua.ListarTomadaDaguaBarragemId(idBarragem);


            return lstTomadaDagua;
        }
    }
}
