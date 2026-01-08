using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoTipoEmpreendedor
    {
        Task AdicionarTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor);
        Task AtualizaTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor);
        Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor();
    }
}
