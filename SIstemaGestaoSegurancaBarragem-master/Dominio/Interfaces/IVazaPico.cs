using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IVazaPico : IGenericos<VazaPico>
    {
        Task<List<VazaPico>> ListarVazaPico(Expression<Func<VazaPico, bool>> exVazaPico);

        Task<VazaPico> VerificarBarragemId(int BarragemId);
    }
}
