using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoZona : IGenericaAplicacao<Zona>
    {
        Task AdicionarZona(Zona zona);
        Task AtualizaZona(Zona zona);
        Task<List<Zona>> ListarZona();

        Task<List<Zona>> ListarZonaId(int id);
    }
}
