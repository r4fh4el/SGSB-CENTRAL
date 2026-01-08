using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTempoRuptura
    {
        Task AdicionarTempoRuptura(TempoRuptura TempoRuptura);
        Task AtualizaTempoRuptura(TempoRuptura TempoRuptura);
        Task<List<TempoRuptura>> ListarTempoRuptura();
    }
}
