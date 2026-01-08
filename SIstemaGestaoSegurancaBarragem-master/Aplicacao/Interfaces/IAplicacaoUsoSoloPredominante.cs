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
    public interface IAplicacaoUsoSoloPredominante : IGenericaAplicacao<UsoSoloPredominante>
    {
        Task AdicionarBarragem(UsoSoloPredominante usoSoloPredominante);
        Task AtualizaBarragem(UsoSoloPredominante usoSoloPredominante);
        Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante();
    }
}
