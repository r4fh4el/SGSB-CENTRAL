using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTipoEdificacaoBarragem
    {
        Task AdicionarTipoEdificacaoBarragem(TipoEdificacaoBarragem TipoEdificacaoBarragem);
        Task AtualizaTipoEdificacaoBarragem(TipoEdificacaoBarragem TipoEdificacaoBarragem);
        Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem();
        Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem);
   
    }
}
