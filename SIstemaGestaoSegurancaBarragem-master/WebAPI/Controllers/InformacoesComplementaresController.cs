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
    public class InformacoesComplementaresController : ControllerBase
    {
        private readonly IAplicacaoInformacoesComplementares _IAplicacaoInformacoesComplementares;

        public InformacoesComplementaresController(IAplicacaoInformacoesComplementares aplicacaoInformacoesComplementares)
        {
            _IAplicacaoInformacoesComplementares = aplicacaoInformacoesComplementares;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarInformacoesComplementares")]
        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementares()
        {
            return await _IAplicacaoInformacoesComplementares.ListarInformacoesComplementares();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdInformacoesComplementares")]
        public async Task<InformacoesComplementares> BuscarPorIdInformacoesComplementares( int id)
        {
            var objInformacoesComplementaresModel = await _IAplicacaoInformacoesComplementares.BuscarPorId(id);
            return objInformacoesComplementaresModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarInformacoesComplementares")]
        public async Task<List<Notifica>> AdicionarInformacoesComplementares([FromBody]InformacoesComplementaresModel model)
        {
            var informacoesComplementaresModel = new InformacoesComplementares()
            {
                Id = model.Id,

                DataAlteracao = model.DataAlteracao,

                DataCadastro = model.DataCadastro,
                BarragemId = model.BarragemId,
                AnoUltimaReforma = model.BarragemId,
                HistoricoIndicidenteAcidente = model.HistoricoIndicidenteAcidente,
                NumeroPortariaLicencaOperacao = model.NumeroPortariaLicencaOperacao,
                NomeSetor = model.NomeSetor,
                NumeroPortariaOutorga = model.NumeroPortariaOutorga,

                PossuiEdificacaoLocalBarragem = model.PossuiEdificacaoLocalBarragem,
                PossuiEscritorioLocalBarragem = model.PossuiEscritorioLocalBarragem,
                TemEquipeOperacaoBarragem = model.TemEquipeOperacaoBarragem,
                TemLicencaOperacao = model.TemLicencaOperacao,
                TemHistoricoIndicidenteAcidente = model.TemHistoricoIndicidenteAcidente,
                TemOperador24 = model.TemOperador24,
                TemOutorgaConstrucao = model.TemOutorgaConstrucao,
                TemVigia = model.TemVigia,
                VazaoMinimaRestituicaoAno = model.VazaoMinimaRestituicaoAno,
            };

            await _IAplicacaoInformacoesComplementares.Adicionar(informacoesComplementaresModel);

            return informacoesComplementaresModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AtualizaInformacoesComplementare")]
        public async Task<List<Notifica>> AtualizaInformacoesComplementare(InformacoesComplementaresModel informacoesComplementaresModel)
        {
            var objInformacoesComplementares = await _IAplicacaoInformacoesComplementares.BuscarPorId(informacoesComplementaresModel.Id);

            objInformacoesComplementares.Id = informacoesComplementaresModel.Id;

                objInformacoesComplementares.DataAlteracao = informacoesComplementaresModel.DataAlteracao;

                objInformacoesComplementares.DataCadastro = informacoesComplementaresModel.DataCadastro;
                objInformacoesComplementares.BarragemId = informacoesComplementaresModel.BarragemId;
                objInformacoesComplementares.AnoUltimaReforma = informacoesComplementaresModel.BarragemId;
                objInformacoesComplementares.HistoricoIndicidenteAcidente = informacoesComplementaresModel.HistoricoIndicidenteAcidente;
                objInformacoesComplementares.NumeroPortariaLicencaOperacao = informacoesComplementaresModel.NumeroPortariaLicencaOperacao;
                objInformacoesComplementares.NomeSetor = informacoesComplementaresModel.NomeSetor;
                objInformacoesComplementares.NumeroPortariaOutorga = informacoesComplementaresModel.NumeroPortariaOutorga;

                objInformacoesComplementares.PossuiEdificacaoLocalBarragem = informacoesComplementaresModel.PossuiEdificacaoLocalBarragem;
                objInformacoesComplementares.PossuiEscritorioLocalBarragem = informacoesComplementaresModel.PossuiEscritorioLocalBarragem;
                objInformacoesComplementares.TemEquipeOperacaoBarragem = informacoesComplementaresModel.TemEquipeOperacaoBarragem;
                objInformacoesComplementares.TemLicencaOperacao = informacoesComplementaresModel.TemLicencaOperacao;
                objInformacoesComplementares.TemHistoricoIndicidenteAcidente = informacoesComplementaresModel.TemHistoricoIndicidenteAcidente;
                objInformacoesComplementares.TemOperador24 = informacoesComplementaresModel.TemOperador24;
                objInformacoesComplementares.TemOutorgaConstrucao = informacoesComplementaresModel.TemOutorgaConstrucao;
                objInformacoesComplementares.TemVigia = informacoesComplementaresModel.TemVigia;
                objInformacoesComplementares.VazaoMinimaRestituicaoAno = informacoesComplementaresModel.VazaoMinimaRestituicaoAno;

            await _IAplicacaoInformacoesComplementares.Atualizar(objInformacoesComplementares);

            return objInformacoesComplementares.Notificacoes;
        }


        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirInformacoesComplementares")]
        public async Task<List<Notifica>> ExcluirInformacoesComplementares(InformacoesComplementares informacoesComplementaresModel)
        {
            var objInformacoesComplementaresModel = await _IAplicacaoInformacoesComplementares.BuscarPorId(informacoesComplementaresModel.Id);

            await _IAplicacaoInformacoesComplementares.Excluir(objInformacoesComplementaresModel);

            return objInformacoesComplementaresModel.Notificacoes;
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
        [HttpPost("/API/ListarInformacoesComplementaresBarragemId")]
        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementaresBarragemId([FromBody] int idBarragem)
        {
            List<InformacoesComplementares> lstInformacoesComplementares = new List<InformacoesComplementares>();
            lstInformacoesComplementares = await _IAplicacaoInformacoesComplementares.ListarInformacoesComplementaresBarragemId(idBarragem);


            return lstInformacoesComplementares;
        }
    }
}
