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
    public interface IRotaFuga : IGenericos<RotaFuga>
    {
        Task<List<RotaFuga>> ListarRotaFuga(Expression<Func<RotaFuga, bool>> exRotaFuga);

        Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id);
    }
}
