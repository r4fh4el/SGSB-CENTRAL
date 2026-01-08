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
    public interface IAplicacaoTipoMaterialBarragem : IGenericaAplicacao<TipoMaterialBarragem>
    {
        Task AdicionarBarragem(TipoMaterialBarragem tipoMaterialBarragem);
        Task AtualizaBarragem(TipoMaterialBarragem tipoMaterialBarragem);
        Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem();
    }
}
