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
    public interface IDescarregadorFundo : IGenericos<DescarregadorFundo>
    {
        Task<List<DescarregadorFundo>> ListarDescarregadorFundo(Expression<Func<DescarregadorFundo, bool>> exDescarregadorFundo);
        Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId(int idBarragem);
    }
}
