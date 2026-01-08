using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoInstrumentos
    {
        Task AdicionarInstrumentos(Instrumentos instrumentos);
        Task AtualizaInstrumentos(Instrumentos instrumentos);
        Task<List<Instrumentos>> ListarInstrumentos();
    }
}
