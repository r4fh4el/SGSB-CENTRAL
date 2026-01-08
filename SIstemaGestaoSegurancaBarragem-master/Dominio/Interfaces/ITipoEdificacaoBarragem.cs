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
    public interface ITipoEdificacaoBarragem : IGenericos<TipoEdificacaoBarragem>
    {
        Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem(Expression<Func<TipoEdificacaoBarragem, bool>> exTipoEdificacaoBarragem);
        Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem);
    }
}
