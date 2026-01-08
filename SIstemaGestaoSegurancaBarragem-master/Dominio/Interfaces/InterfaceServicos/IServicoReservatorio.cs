using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
   
    public interface IServicoReservatorio
    {
        Task AdicionarReservatorio(Reservatorio reservatorio);
        Task AtualizaReservatorio(Reservatorio reservatorio);
        Task<List<Reservatorio>> ListarReservatorio();

        Task<List<Reservatorio>> ListarReservatorioBarragemId(int idBarragem);
    }
}
