using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoUsoSoloPredominante
    {
        Task AdicionarUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante);
        Task AtualizaUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante);
        Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante();
    }
}
