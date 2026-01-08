using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoHidrogramaParabolico
    {
        Task AdicionarHidrogramaParabolico(HidrogramaParabolico HidrogramaParabolico);
        Task AtualizaHidrogramaParabolico(HidrogramaParabolico HidrogramaParabolico);
        Task<List<HidrogramaParabolico>> ListarHidrogramaParabolico();
    }
}
