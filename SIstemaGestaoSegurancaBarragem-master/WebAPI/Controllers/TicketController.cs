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
    public class TicketController : ControllerBase
    {
        private readonly IAplicacaoTicket _IAplicacaoTicket;

        public TicketController(IAplicacaoTicket aplicacaoTicket)
        {
            _IAplicacaoTicket = aplicacaoTicket;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarTicket")]
        public async Task<List<Ticket>> ListarTicket()
        {
            return await _IAplicacaoTicket.ListarTicket();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdTicket")]
        public async Task<Ticket> BuscarPorIdTicket( int id)
        {
            var objTicketModel = await _IAplicacaoTicket.BuscarPorId(id);
            return objTicketModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarTicket")]
        public async Task<List<Notifica>> AdicionarTicket(TicketModel TicketModel)
        {
            var objTicketModel = new Ticket()
            {
                Id = TicketModel.Id,
                TIcket = TicketModel.TIcket ,
                DataAlteracao = TicketModel.DataAlteracao,
                Descricao = TicketModel.Descricao,
                Titulo = TicketModel.Titulo,
                IdUsuario = TicketModel.IdUsuario,
                Status = TicketModel.Status,
                DataCadastro = TicketModel.DataCadastro

            };

             await _IAplicacaoTicket.Adicionar(objTicketModel);

            return objTicketModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaTicket")]
        public async Task<List<Notifica>> AtualizaTicket(TicketModel TicketModel)
        {
            var objTicket = await _IAplicacaoTicket.BuscarPorId(TicketModel.Id);

            objTicket.Id = TicketModel.Id;
            objTicket.Descricao = TicketModel.Descricao;
            objTicket.Titulo = TicketModel.Titulo;
            objTicket.TIcket = TicketModel.TIcket;
            objTicket.IdUsuario = TicketModel.IdUsuario;
            objTicket.TIcket = TicketModel.TIcket;
            objTicket.Status = TicketModel.Status;
            objTicket.DataAlteracao = DateTime.Now;
            objTicket.DataCadastro = DateTime.Now;

            await _IAplicacaoTicket.Atualizar(objTicket);

            return objTicket.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirTicket")]
        public async Task<List<Notifica>> ExcluirTicket(TicketModel TicketModel)
        {
            var objTicketModel = await _IAplicacaoTicket.BuscarPorId(TicketModel.Id);

            await _IAplicacaoTicket.Excluir(objTicketModel);

            return objTicketModel.Notificacoes;
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
