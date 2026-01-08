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
    public interface IAplicacaoTipoEdificacaoBarragem : IGenericaAplicacao<TipoEdificacaoBarragem>
    {
        Task AdicionarTipoEdificacaoBarragem(TipoEdificacaoBarragem TipoEdificacaoBarragem);
        Task AtualizaTipoEdificacaoBarragem(TipoEdificacaoBarragem TipoEdificacaoBarragem);
        Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem();
        Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem);
    }
}
