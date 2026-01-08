using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTicket
    {
        Task AdicionarTicket(Ticket Ticket);
        Task AtualizaTicket(Ticket Ticket);
        Task<List<Ticket>> ListarTicket();
    }
}
