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
    public interface IAplicacaoDadosGerais : IGenericaAplicacao<DadosGerais>
    {
        Task AdicionarDadosGerais(DadosGerais dadosGerais);
        Task AtualizaDadosGerais(DadosGerais dadosGerais);
        Task<List<DadosGerais>> ListarDadosGerais();
        Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem);
    }
}
