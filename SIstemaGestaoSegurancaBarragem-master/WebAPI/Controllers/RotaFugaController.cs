using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaFugaController : ControllerBase
    {
        private readonly IAplicacaoRotaFuga _IAplicacaoRotaFuga;

        public RotaFugaController(IAplicacaoRotaFuga aplicacaoRotaFuga)
        {
            _IAplicacaoRotaFuga = aplicacaoRotaFuga;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarRotaFuga")]
        public async Task<List<RotaFuga>> ListarRotaFuga()
        {
            return await _IAplicacaoRotaFuga.ListarRotaFuga();
        }


        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarRotaFugaApp")]
        public async Task<IActionResult> ListarRotaFugaApp()
        {
            List<RotaFugaModel> rotaFugaModels = new List<RotaFugaModel>();
            var ListarRotaFuga = await _IAplicacaoRotaFuga.ListarRotaFuga();

            foreach (var item in ListarRotaFuga)
            {
                RotaFugaModel rotaFugaModel = new RotaFugaModel()
                {
                    BarragemId = item.BarragemId,
                    DataAlteracao = item.DataAlteracao,
                    DataCadastro = item.DataCadastro,
                    Id = item.Id,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude

                };

                rotaFugaModels.Add(rotaFugaModel);
            }
            return StatusCode(200, rotaFugaModels);

        
        }


        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdRotaFuga")]
        public async Task<RotaFuga> BuscarPorIdRotaFuga( int id)
        {
            var objRotaFugaModel = await _IAplicacaoRotaFuga.BuscarPorId(id);
            return objRotaFugaModel;
        }


        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarListPorIdRotaFuga")]
        public async Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id)
        {
            var objRotaFugaModel = await _IAplicacaoRotaFuga.BuscarListPorIdRotaFuga(id);
            return objRotaFugaModel;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarRotaFuga")]
        public async Task<List<Notifica>> AdicionarRotaFuga(RotaFugaModel RotaFugaModel)
        {
            var objRotaFugaModel = new RotaFuga()
            {
                Id = RotaFugaModel.Id,
                Nome = RotaFugaModel.Nome,
                BarragemId = RotaFugaModel.BarragemId,
                Latitude = RotaFugaModel.Latitude,
                Longitude = RotaFugaModel.Longitude,
                DataAlteracao = RotaFugaModel.DataAlteracao,
                DataCadastro = RotaFugaModel.DataCadastro
            };

             await _IAplicacaoRotaFuga.Adicionar(objRotaFugaModel);

            return objRotaFugaModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaRotaFuga")]
        public async Task<List<Notifica>> AtualizaRotaFuga(RotaFugaModel RotaFugaModel)
        {
            //var objRotaFuga = await _IAplicacaoRotaFuga.BuscarPorId(RotaFugaModel.Id);

            var objRotaFugaModel = new RotaFuga()
            {
                Id = RotaFugaModel.Id,
                Nome = RotaFugaModel.Nome,
                BarragemId = RotaFugaModel.BarragemId,
                Latitude = RotaFugaModel.Latitude,
                Longitude = RotaFugaModel.Longitude,
                DataAlteracao = RotaFugaModel.DataAlteracao,
                DataCadastro = RotaFugaModel.DataCadastro
            };

            await _IAplicacaoRotaFuga.Atualizar(objRotaFugaModel);

            return objRotaFugaModel.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirRotaFuga")]
        public async Task<List<Notifica>> ExcluirRotaFuga(RotaFugaModel RotaFugaModel)
        {
            var objRotaFugaModel = await _IAplicacaoRotaFuga.BuscarPorId(RotaFugaModel.Id);

            await _IAplicacaoRotaFuga.Excluir(objRotaFugaModel);

            return objRotaFugaModel.Notificacoes;
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
