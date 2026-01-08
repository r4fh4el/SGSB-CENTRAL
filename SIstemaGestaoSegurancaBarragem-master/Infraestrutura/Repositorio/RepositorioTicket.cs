using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioTicket : RepositorioGenerico<Ticket>, ITicket
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTicket() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Ticket>> ListarTicket(Expression<Func<Ticket, bool>> exTicket)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Ticket.Where(exTicket).AsNoTracking().ToListAsync();
            }
        }
    }
}
