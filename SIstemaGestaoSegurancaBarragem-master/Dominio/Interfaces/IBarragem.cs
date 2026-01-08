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
    public interface IBarragem : IGenericos<Barragem>
    {
        Task<List<Barragem>> ListarBarragem(Expression<Func<Barragem, bool>> exBarragem);

        Task<List<Barragem>> BuscarListaPorIdBarragem(int id);
        Task<bool> DeletarBarragemRelacionais(int idBarragem);


    }
}
