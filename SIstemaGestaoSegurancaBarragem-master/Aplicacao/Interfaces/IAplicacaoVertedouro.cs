using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoVertedouro : IGenericaAplicacao<Vertedouro>
    {
        Task AdicionarVertedouro(Vertedouro tipoMaterialBarragem);
        Task AtualizaVertedouro(Vertedouro tipoMaterialBarragem);
        Task<List<Vertedouro>> ListarVertedouro();

        Task<List<Vertedouro>> ListarVertedouroBarragemId(int idBarragem);
    }
}
