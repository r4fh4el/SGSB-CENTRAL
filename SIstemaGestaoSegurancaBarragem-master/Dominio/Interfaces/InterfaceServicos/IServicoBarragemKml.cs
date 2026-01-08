using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoBarragemKml
    {
        Task AdicionarBarragemKml(BarragemKml BarragemKml);
        Task AtualizaBarragemKml(BarragemKml BarragemKml);
        Task<List<BarragemKml>> ListarBarragemKml();
    }
}
