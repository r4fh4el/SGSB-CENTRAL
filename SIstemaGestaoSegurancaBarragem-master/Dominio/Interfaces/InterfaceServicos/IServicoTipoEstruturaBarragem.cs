using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTipoEstruturaBarragem
    {
        public Task AdicionarTipoEstruturaBarragem(TipoEstruturaBarragem tipoEstruturaBarragem);
        public Task AtualizarTipoEstruturaBarragem(TipoEstruturaBarragem tipoEstruturaBarragem);

        public Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem();
    }
}
