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
    public interface ISistemaDrenagemBarragem : IGenericos<SistemaDrenagemBarragem>
    {
        Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragem(Expression<Func<SistemaDrenagemBarragem, bool>> exSistemaDrenagemBarragem);
        Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragemBarragemId(int idBarragem);
    }
}
