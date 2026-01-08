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
    public interface IReservatorio : IGenericos<Reservatorio>
    {
        Task<List<Reservatorio>> ListarReservatorio(Expression<Func<Reservatorio, bool>> exReservatorio);
        Task<List<Reservatorio>> ListarReservatorioBarragemId(int idBarragem);

       
    }
}
