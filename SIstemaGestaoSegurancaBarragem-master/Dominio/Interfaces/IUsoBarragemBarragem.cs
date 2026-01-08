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
    public interface IUsoBarragemBarragem : IGenericos<UsoBarragemBarragem>
    {
        Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem(Expression<Func<UsoBarragemBarragem, bool>> exUsoBarragemBarragem);

        Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId(int idBarragem);
    }
}
