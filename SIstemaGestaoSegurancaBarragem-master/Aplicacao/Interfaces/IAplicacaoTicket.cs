using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoTicket : IGenericaAplicacao<Ticket>
    {
        Task AdicionarTicket(Ticket Ticket);
        Task AtualizaTicket(Ticket Ticket);
        Task<List<Ticket>> ListarTicket();
    }
}
