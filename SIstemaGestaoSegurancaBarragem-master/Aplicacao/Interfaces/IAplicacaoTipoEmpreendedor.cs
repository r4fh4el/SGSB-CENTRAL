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
    public interface IAplicacaoTipoEmpreendedor : IGenericaAplicacao<TipoEmpreendedor>
    {
        Task AdicionarBarragem(TipoEmpreendedor tipoEmpreendedor);
        Task AtualizaBarragem(TipoEmpreendedor tipoEmpreendedor);
        Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor();
    }
}
