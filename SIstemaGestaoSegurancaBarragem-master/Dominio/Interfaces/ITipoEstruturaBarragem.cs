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
    public interface ITipoEstruturaBarragem : IGenericos<TipoEstruturaBarragem>     
    {
        Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem(Expression<Func<TipoEstruturaBarragem, bool>> exTipoEstruturaBarragem);
    }
}
