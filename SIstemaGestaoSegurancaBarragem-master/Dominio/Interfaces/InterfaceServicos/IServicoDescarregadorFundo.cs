using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoDescarregadorFundo
    {
        Task AdicionarDescarregadorFundo(DescarregadorFundo descarregadorFundo);
        Task AtualizaDescarregadorFundo(DescarregadorFundo descarregadorFundo);
        Task<List<DescarregadorFundo>> ListarDescarregadorFundo();

        Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId(int idBarragem);
    }
}
