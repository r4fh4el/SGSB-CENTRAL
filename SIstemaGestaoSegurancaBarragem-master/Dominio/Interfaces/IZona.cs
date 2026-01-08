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
    public interface IZona : IGenericos<Zona>
    {
        Task<List<Zona>> ListarZona(Expression<Func<Zona, bool>> exZona);

        Task<List<Zona>> ListarZonaId(int id);
    }
}
