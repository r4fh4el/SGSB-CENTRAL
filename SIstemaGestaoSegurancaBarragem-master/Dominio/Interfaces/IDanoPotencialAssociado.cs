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
    public interface IDanoPotencialAssociado : IGenericos<DanoPotencialAssociado>
    {
        Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado(Expression<Func<DanoPotencialAssociado, bool>> exDanoPotencialAssociado);
        Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int id);
    }
}
