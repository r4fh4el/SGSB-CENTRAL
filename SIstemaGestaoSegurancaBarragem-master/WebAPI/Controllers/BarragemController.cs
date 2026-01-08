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
    public class BarragemController : ControllerBase
    {
        private readonly IAplicacaoBarragem _IAplicacaoBarragem;

        public BarragemController(IAplicacaoBarragem aplicacaoBarragem)
        {
            _IAplicacaoBarragem = aplicacaoBarragem;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/Listar")]
        public async Task<List<Barragem>> Listar()
        {
            return await _IAplicacaoBarragem.Listar();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarBarragem")]
        public async Task<List<Barragem>> ListarBarragem()
        {
            return await _IAplicacaoBarragem.ListarBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdBarragem")]
        public async Task<Barragem> BuscarPorIdBarragem(int id)
        {
            var objBarragemModel = await _IAplicacaoBarragem.BuscarPorId(id);
            return objBarragemModel;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarListaPorIdBarragem")]
        public async Task<List<Barragem>> BuscarListaPorIdBarragem(int id)
        {
            var lstBarragemModel = await _IAplicacaoBarragem.BuscarListaPorIdBarragem(id);
            return lstBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarBarragem")]
        public async Task<List<Notifica>> AdicionarBarragem(BarragemModel barragem)
        {
            var objBarragemModel = new Barragem()
            {
                Id = barragem.Id,
                CotaCoroamentoBarragem = barragem.CotaCoroamentoBarragem,
                AlturaMAxima = barragem.AlturaMAxima,
                AnoConclusaoObra = barragem.AnoConclusaoObra,
                BaciaHidrograficaAbrangencia = barragem.BaciaHidrograficaAbrangencia,
                Nome = barragem.Nome,
                ComprimentoCoroamento = barragem.ComprimentoCoroamento,
                Longitude = barragem.Longitude,
                Latitude = barragem.Latitude,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                //CondicaoFundacao = barragem.CondicaoFundacao,
                CondicaoFundacaoId = barragem.CondicaoFundacaoId,
                CursoDaguaBarrado = barragem.CursoDaguaBarrado,
                //DataAlteracao = DateTime.Now,
                //DataCadastro = DateTime.Now,
                IdadeBarragem = barragem.IdadeBarragem,
                LarguraCoroamentoBarragem = barragem.LarguraCoroamentoBarragem,
                //TipoEstruturaBarragem = barragem.TipoEstruturaBarragem,
                //TipoFundacao = barragem.TipoFundacao,
                //TipoMaterialBarragem = barragem.TipoMaterialBarragem,
                Tipo_Estrutura_Id = barragem.Tipo_Estrutura_Id,
                Tipo_Material_Id = barragem.Tipo_Material_Id

            };

            await _IAplicacaoBarragem.AdicionarBarragem(objBarragemModel);

            return objBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaBarragem")]
        public async Task<List<Notifica>> AtualizaBarragem(BarragemModel barragem)
        {
            var objBarragem = await _IAplicacaoBarragem.BuscarPorId(barragem.Id);

            objBarragem.Id = barragem.Id;
            objBarragem.AlturaMAxima = barragem.AlturaMAxima;
            objBarragem.AnoConclusaoObra = barragem.AnoConclusaoObra;
            objBarragem.BaciaHidrograficaAbrangencia = barragem.BaciaHidrograficaAbrangencia;
            objBarragem.Nome = barragem.Nome;
            objBarragem.ComprimentoCoroamento = barragem.ComprimentoCoroamento;
            //objBarragem.CondicaoFundacao = barragem.CondicaoFundacao;
            objBarragem.CondicaoFundacaoId = barragem.CondicaoFundacaoId;
            objBarragem.CotaCoroamentoBarragem = barragem.CotaCoroamentoBarragem;
            objBarragem.LarguraCoroamentoBarragem = barragem.LarguraCoroamentoBarragem;
            objBarragem.IdadeBarragem = barragem.IdadeBarragem;
            objBarragem.LarguraCoroamentoBarragem = barragem.LarguraCoroamentoBarragem;
            //objBarragem.TipoEstruturaBarragem = barragem.TipoEstruturaBarragem;
            //objBarragem.TipoFundacao = barragem.TipoFundacao;
            //objBarragem.TipoMaterialBarragem = barragem.TipoMaterialBarragem;
            objBarragem.Tipo_Estrutura_Id = barragem.Tipo_Estrutura_Id;
            objBarragem.Tipo_Material_Id = barragem.Tipo_Material_Id;
            objBarragem.Longitude = barragem.Longitude;
            objBarragem.Latitude = barragem.Latitude;

            objBarragem.DataAlteracao = DateTime.Now;


            await _IAplicacaoBarragem.AtualizaBarragem(objBarragem);

            return objBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirBarragem")]
        public async Task<List<Notifica>> ExcluirBarragem(BarragemModel barragem)
        {
            var objBarragemModel = await _IAplicacaoBarragem.BuscarPorId(barragem.Id);

            await _IAplicacaoBarragem.Excluir(objBarragemModel);

            return objBarragemModel.Notificacoes;
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
        [HttpPost("/API/DeletarBarragemRelacionais")]
        public async Task<bool> DeletarBarragemRelacionais([FromBody] int id)
        {
            return await _IAplicacaoBarragem.DeletarBarragemRelacionais(id);

        }

    }
}
