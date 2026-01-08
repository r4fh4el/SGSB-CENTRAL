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
    public interface ITomadaDagua : IGenericos<TomadaDagua>
    {
        Task<List<TomadaDagua>> ListarTomadaDagua(Expression<Func<TomadaDagua, bool>> exTomadaDagua);

        Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem);
    }
}
