using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoZona
    {
        Task AdicionarZona(Zona zona);
        Task AtualizaZona(Zona zona);
        Task<List<Zona>> ListarZona();

        Task<List<Zona>> ListarZonaId(int id);
    }
}
