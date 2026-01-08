using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoDanoPotencialAssociado : IGenericaAplicacao<DanoPotencialAssociado>
    {
        Task AdicionarBarragem(DanoPotencialAssociado danoPotencialAssociado);
        Task AtualizaBarragem(DanoPotencialAssociado danoPotencialAssociado);
        Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado();
        Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int id);
    }
}
