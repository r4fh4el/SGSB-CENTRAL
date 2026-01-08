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
    public interface IAplicacaoDescarregadorFundo : IGenericaAplicacao<DescarregadorFundo>
    {
        Task AdicionarDescarregadorFundo(DescarregadorFundo descarregadorFundo);
        Task AtualizaDescarregadorFundo(DescarregadorFundo descarregadorFundo);
        Task<List<DescarregadorFundo>> ListarDescarregadorFundo();

        Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId(int idBarragem);
    }
}
