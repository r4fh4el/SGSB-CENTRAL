using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoDanoPotencialAssociado
    {
        Task AdicionarDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociado);
        Task AtualizaDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociado);
        Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado();
        Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int id);
    }
}
