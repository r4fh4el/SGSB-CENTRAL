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
    public class DocumentacaoProjetoConstrucaoOperacaoController : ControllerBase
    {
        private readonly IAplicacaoDocumentacaoProjetoConstrucaoOperacao _IAplicacaoDocumentacaoProjetoConstrucaoOperacao;

        public DocumentacaoProjetoConstrucaoOperacaoController(IAplicacaoDocumentacaoProjetoConstrucaoOperacao aplicacaoDocumentacaoProjetoConstrucaoOperacao)
        {
            _IAplicacaoDocumentacaoProjetoConstrucaoOperacao = aplicacaoDocumentacaoProjetoConstrucaoOperacao;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarDocumentacaoProjetoConstrucaoOperacao")]
        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao()
        {
            return await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.ListarDocumentacaoProjetoConstrucaoOperacao();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdDocumentacaoProjetoConstrucaoOperacao")]
        public async Task<DocumentacaoProjetoConstrucaoOperacao> BuscarPorIdDocumentacaoProjetoConstrucaoOperacao( int id)
        {
            var objDocumentacaoProjetoConstrucaoOperacao = await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.BuscarPorId(id);
            return objDocumentacaoProjetoConstrucaoOperacao;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarDocumentacaoProjetoConstrucaoOperacao")]
        public async Task<List<Notifica>> AdicionarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacaoModel documentacaoProjetoConstrucaoOperacao)
        {
            var objDocumentacaoProjetoConstrucaoOperacao = new DocumentacaoProjetoConstrucaoOperacao();

          
            objDocumentacaoProjetoConstrucaoOperacao.BarragemId = documentacaoProjetoConstrucaoOperacao.BarragemId;
            objDocumentacaoProjetoConstrucaoOperacao.Pergunta = documentacaoProjetoConstrucaoOperacao.Pergunta;
            objDocumentacaoProjetoConstrucaoOperacao.Resposta = documentacaoProjetoConstrucaoOperacao.Resposta;

            await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.Adicionar(objDocumentacaoProjetoConstrucaoOperacao);

            return objDocumentacaoProjetoConstrucaoOperacao.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaDocumentacaoProjetoConstrucaoOperacao")]
        public async Task<List<Notifica>> AtualizaDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacaoModel documentacaoProjetoConstrucaoOperacao)
        {
            var objDocumentacaoProjetoConstrucaoOperacao = await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.BuscarPorId(documentacaoProjetoConstrucaoOperacao.Id);

            objDocumentacaoProjetoConstrucaoOperacao.Id = documentacaoProjetoConstrucaoOperacao.Id;
            objDocumentacaoProjetoConstrucaoOperacao.BarragemId = documentacaoProjetoConstrucaoOperacao.BarragemId;
            objDocumentacaoProjetoConstrucaoOperacao.Pergunta = documentacaoProjetoConstrucaoOperacao.Pergunta;
            objDocumentacaoProjetoConstrucaoOperacao.Resposta = documentacaoProjetoConstrucaoOperacao.Resposta;
         
            objDocumentacaoProjetoConstrucaoOperacao.DataAlteracao = DateTime.Now;

            await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.Atualizar(objDocumentacaoProjetoConstrucaoOperacao);

            return objDocumentacaoProjetoConstrucaoOperacao.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirDocumentacaoProjetoConstrucaoOperacao")]
        public async Task<List<Notifica>> ExcluirDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacaoModel documentacaoProjetoConstrucaoOperacao)
        {
            var objDocumentacaoProjetoConstrucaoOperacao = await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.BuscarPorId(documentacaoProjetoConstrucaoOperacao.Id);

            await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.Excluir(objDocumentacaoProjetoConstrucaoOperacao);

            return objDocumentacaoProjetoConstrucaoOperacao.Notificacoes;
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
        [HttpPost("/API/ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId")]
        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId([FromBody] int idBarragem)
        {
            List<DocumentacaoProjetoConstrucaoOperacao> lstDocumentacaoProjetoConstrucaoOperacao = new List<DocumentacaoProjetoConstrucaoOperacao>();
            lstDocumentacaoProjetoConstrucaoOperacao = await _IAplicacaoDocumentacaoProjetoConstrucaoOperacao.ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(idBarragem);


            return lstDocumentacaoProjetoConstrucaoOperacao;
        }
    }
}

