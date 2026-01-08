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
    public interface IAplicacaoIndiceCaracterizacaoBH : IGenericaAplicacao<IndiceCaracterizacaoBH>
    {
        Task AdicionarBarragem(IndiceCaracterizacaoBH indiceCaracterizacaoBH);
        Task AtualizaBarragem(IndiceCaracterizacaoBH indiceCaracterizacaoBH);
        Task<List<IndiceCaracterizacaoBH>> ListarIndiceCaracterizacaoBH();
    }
}
