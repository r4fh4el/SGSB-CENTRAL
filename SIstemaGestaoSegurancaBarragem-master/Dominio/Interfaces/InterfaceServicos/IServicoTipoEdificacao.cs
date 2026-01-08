using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTipoEdificacao
    {
        Task AdicionarTipoEdificacao(TipoEdificacao tipoEdificacao);
        Task AtualizaTipoEdificacao(TipoEdificacao tipoEdificacao);
        Task<List<TipoEdificacao>> ListarTipoEdificacao();
    }
}
