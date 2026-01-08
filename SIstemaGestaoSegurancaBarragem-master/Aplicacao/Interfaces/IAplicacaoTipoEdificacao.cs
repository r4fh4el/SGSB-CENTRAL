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
    public interface IAplicacaoTipoEdificacao : IGenericaAplicacao<TipoEdificacao>
    {
        Task AdicionarTipoEdificacao(TipoEdificacao tipoEdificacao);
        Task AtualizaTipoEdificacao(TipoEdificacao tipoEdificacao);
        Task<List<TipoEdificacao>> ListarTipoEdificacao();
    }
}
