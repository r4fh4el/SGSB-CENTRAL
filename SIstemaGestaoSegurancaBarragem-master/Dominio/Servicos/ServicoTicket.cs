using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoTicket : IServicoTicket
    {
        private readonly ITicket _ITicket;

        public ServicoTicket(ITicket Ticket) 
        {
            _ITicket = Ticket;
        }

        public async Task AdicionarTicket(Ticket Ticket)
        {
            var validarNome = Ticket.ValidadePropriedadeSring(Ticket.NomePropriedade , "Titulo");

            if (validarNome)
            {
                Ticket.DataAlteracao = DateTime.Now;
                Ticket.DataCadastro = DateTime.Now;
               await _ITicket.Adicionar(Ticket);
            }
        }

        public async Task AtualizaTicket(Ticket Ticket)
        {
            var validarNome = Ticket.ValidadePropriedadeSring(Ticket.Titulo, "Titulo");

            if (validarNome)
            {
                Ticket.DataAlteracao = DateTime.Now;
                Ticket.DataCadastro = Ticket.DataCadastro;
                await _ITicket.Atualizar(Ticket);
        }
        }

        public async Task<List<Ticket>> ListarTicket()
        {
            return  await _ITicket.ListarTicket(n => n.Titulo != null);
        }
    }
}
