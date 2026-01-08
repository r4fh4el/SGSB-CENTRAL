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
    public interface IAplicacaoTempoConcentracao : IGenericaAplicacao<TempoConcentracao>
    {
        Task AdicionarBarragem(TempoConcentracao tempoConcentracao);
        Task AtualizaBarragem(TempoConcentracao tempoConcentracao);
        Task<List<TempoConcentracao>> ListarTempoConcentracao();
    }
}
