using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTempoConcentracao
    {
        Task AdicionarTempoConcentracao(TempoConcentracao tempoConcentracao);
        Task AtualizaTempoConcentracao(TempoConcentracao tempoConcentracao);
        Task<List<TempoConcentracao>> ListarTempoConcentracao();
    }
}
