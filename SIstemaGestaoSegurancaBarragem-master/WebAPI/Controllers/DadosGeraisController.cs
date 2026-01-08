using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosGeraisController : ControllerBase
    {
        private readonly IAplicacaoDadosGerais _IAplicacaoDadosGerais;

        public DadosGeraisController(IAplicacaoDadosGerais aplicacaoDadosGerais)
        {
            _IAplicacaoDadosGerais = aplicacaoDadosGerais;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarDadosGerais")]
        public async Task<List<DadosGerais>> ListarDadosGerais()
        {
            return await _IAplicacaoDadosGerais.ListarDadosGerais();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdDadosGerais")]
        public async Task<DadosGerais> BuscarPorIdDadosGerais(int id)
        {
            var objDadosGerais = await _IAplicacaoDadosGerais.BuscarPorId(id);
            return objDadosGerais;
        }
        
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ListarDadosGeraisBarragemId")]
        public async Task<List<DadosGeraisViewModel>> ListarDadosGeraisBarragemId([FromBody]int idBarragem)
        {
            List<DadosGeraisViewModel> lstDadosGeraisModel = new List<DadosGeraisViewModel> ();
            var objDadosGerais = await _IAplicacaoDadosGerais.ListarDadosGeraisBarragemId(idBarragem);
            foreach (var model in objDadosGerais)
            {
                var dados = new DadosGeraisViewModel()
                {
                    DataAlteracao = model.DataAlteracao,
                    DataCadastro = model.DataCadastro,
                    EmailEmpreendedor = model.EmailEmpreendedor,
                    EnderecoEmpreendedor = model.EnderecoEmpreendedor,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    NomeEmpreendedor = model.NomeEmpreendedor,
                    QuantidadeBarragem = model.QuantidadeBarragem,
                    ResponsavelEmpreendedor = model.ResponsavelEmpreendedor,
                    TelefoneEmpreendedor = model.TelefoneEmpreendedor,
                    TipoEmpreendedor = model.TipoEmpreendedor,
                    BarragemId = model.BarragemId,
                    Id = model.Id
                };
                lstDadosGeraisModel.Add(dados);
            }
            return lstDadosGeraisModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarDadosGerais")]
        public async Task<List<Notifica>> AdicionarDadosGerais(DadosGeraisViewModel model)
        {
            var dados = new DadosGerais()
            {
                DataAlteracao = model.DataAlteracao,
                DataCadastro = model.DataCadastro,
                EmailEmpreendedor = model.EmailEmpreendedor,
                EnderecoEmpreendedor = model.EnderecoEmpreendedor,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                NomeEmpreendedor = model.NomeEmpreendedor,
                QuantidadeBarragem = model.QuantidadeBarragem,
                ResponsavelEmpreendedor = model.ResponsavelEmpreendedor,
                TelefoneEmpreendedor = model.TelefoneEmpreendedor,
                TipoEmpreendedor = model.TipoEmpreendedor,
                BarragemId = model.BarragemId,
                Id = model.Id
            };

            await _IAplicacaoDadosGerais.AdicionarDadosGerais(dados);

            return dados.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaDadosGerais")]
        public async Task<List<Notifica>> AtualizaDadosGerais([FromBody] DadosGeraisViewModel dadosGeraisModel)
        {
            var objDadosGerais = await _IAplicacaoDadosGerais.BuscarPorId(dadosGeraisModel.Id);

            objDadosGerais.Id = dadosGeraisModel.Id;
            objDadosGerais.NomeEmpreendedor = dadosGeraisModel.NomeEmpreendedor;
            objDadosGerais.EmailEmpreendedor = dadosGeraisModel.EmailEmpreendedor;
            objDadosGerais.EnderecoEmpreendedor = dadosGeraisModel.EnderecoEmpreendedor;
            objDadosGerais.Latitude = dadosGeraisModel.Latitude;
            objDadosGerais.Longitude = dadosGeraisModel.Longitude;
            objDadosGerais.QuantidadeBarragem = dadosGeraisModel.QuantidadeBarragem;
            objDadosGerais.ResponsavelEmpreendedor = dadosGeraisModel.ResponsavelEmpreendedor;
            objDadosGerais.TelefoneEmpreendedor = dadosGeraisModel.TelefoneEmpreendedor;
            objDadosGerais.TipoEmpreendedor = dadosGeraisModel.TipoEmpreendedor;
            objDadosGerais.DataAlteracao = DateTime.Now;
            objDadosGerais.BarragemId = dadosGeraisModel.BarragemId;
            await _IAplicacaoDadosGerais.AtualizaDadosGerais(objDadosGerais);

            return objDadosGerais.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirDadosGerais")]
        public async Task<List<Notifica>> ExcluirDadosGerais(DadosGerais dadosGeraisModel)
        {
            var objDadosGerais = await _IAplicacaoDadosGerais.BuscarPorId(dadosGeraisModel.Id);

            await _IAplicacaoDadosGerais.Excluir(objDadosGerais);

            return objDadosGerais.Notificacoes;
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
