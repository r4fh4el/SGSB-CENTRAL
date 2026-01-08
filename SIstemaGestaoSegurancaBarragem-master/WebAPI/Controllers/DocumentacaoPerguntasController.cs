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
    public class DocumentacaoPerguntasController : ControllerBase
    {
        private readonly IAplicacaoDocumentacaoPerguntas _IAplicacaoDocumentacaoPerguntas;

        public DocumentacaoPerguntasController(IAplicacaoDocumentacaoPerguntas aplicacaoDocumentacaoPerguntas)
        {
            _IAplicacaoDocumentacaoPerguntas = aplicacaoDocumentacaoPerguntas;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarDocumentacaoPerguntas")]
        public async Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas()
        {
            return await _IAplicacaoDocumentacaoPerguntas.ListarDocumentacaoPerguntas();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdDocumentacaoPerguntas")]
        public async Task<DocumentacaoPerguntas> BuscarPorIdDocumentacaoPerguntas( int id)
        {
            var objDocumentacaoPerguntasModel = await _IAplicacaoDocumentacaoPerguntas.BuscarPorId(id);
            return objDocumentacaoPerguntasModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarDocumentacaoPerguntas")]
        public async Task<List<Notifica>> AdicionarDocumentacaoPerguntas(DocumentacaoPerguntasModel documentacaoPerguntasModel)
        {
            var objDocumentacaoPerguntasModel = new DocumentacaoPerguntas()
            {
                Id = documentacaoPerguntasModel.Id,
                Pergunta     = documentacaoPerguntasModel.Pergunta
            };

             await _IAplicacaoDocumentacaoPerguntas.Adicionar(objDocumentacaoPerguntasModel);

            return objDocumentacaoPerguntasModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaDocumentacaoPerguntas")]
        public async Task<List<Notifica>> AtualizaDocumentacaoPerguntas(DocumentacaoPerguntasModel documentacaoPerguntasModel)
        {
            var objDocumentacaoPerguntas = await _IAplicacaoDocumentacaoPerguntas.BuscarPorId(documentacaoPerguntasModel.Id);

            objDocumentacaoPerguntas.Id = documentacaoPerguntasModel.Id;
            objDocumentacaoPerguntas.Pergunta = documentacaoPerguntasModel.Pergunta;
            objDocumentacaoPerguntas.DataAlteracao = DateTime.Now;

            await _IAplicacaoDocumentacaoPerguntas.Atualizar(objDocumentacaoPerguntas);

            return objDocumentacaoPerguntas.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirDocumentacaoPerguntas")]
        public async Task<List<Notifica>> ExcluirDocumentacaoPerguntas(DocumentacaoPerguntasModel documentacaoPerguntasModel)
        {
            var objDocumentacaoPerguntasModel = await _IAplicacaoDocumentacaoPerguntas.BuscarPorId(documentacaoPerguntasModel.Id);

            await _IAplicacaoDocumentacaoPerguntas.Excluir(objDocumentacaoPerguntasModel);

            return objDocumentacaoPerguntasModel.Notificacoes;
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
