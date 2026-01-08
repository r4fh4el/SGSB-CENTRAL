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
    public interface IAplicacaoPontoEncontro : IGenericaAplicacao<PontoEncontro>
    {
        Task AdicionarPontoEncontro(PontoEncontro PontoEncontro);
        Task AtualizaPontoEncontro(PontoEncontro PontoEncontro);
        Task<List<PontoEncontro>> ListarPontoEncontro();
    }
}
