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
    public interface ITalude : IGenericos<Talude>
    {
        Task<List<Talude>> ListarTalude(Expression<Func<Talude, bool>> exTalude);

        Task<List<Talude>> ListarTaludeBarragemId(int idBarragem);
    }
}
