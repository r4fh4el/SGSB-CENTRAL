using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoHidrogramaTriangula
    {
        Task AdicionarHidrogramaTriangula(HidrogramaTriangula HidrogramaTriangula);
        Task AtualizaHidrogramaTriangula(HidrogramaTriangula HidrogramaTriangula);
        Task<List<HidrogramaTriangula>> ListarHidrogramaTriangula();
    }
}
