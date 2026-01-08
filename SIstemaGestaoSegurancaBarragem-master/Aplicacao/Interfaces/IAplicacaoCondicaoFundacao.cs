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
    public interface IAplicacaoCondicaoFundacao : IGenericaAplicacao<CondicaoFundacao>
    {
        Task AdicionarCondicaoFundacao(CondicaoFundacao condicaoFundacao);
        Task AtualizaCondicaoFundacao(CondicaoFundacao condicaoFundacao);
        Task<List<CondicaoFundacao>> ListarCondicaoFundacao();
    }
}
