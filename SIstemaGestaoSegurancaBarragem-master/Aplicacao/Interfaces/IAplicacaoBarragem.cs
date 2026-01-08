using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoBarragem : IGenericaAplicacao<Barragem>
    {
        Task AdicionarBarragem(Barragem barragem);
        Task AtualizaBarragem(Barragem barragem);
        Task<List<Barragem>> ListarBarragem();

        Task<List<Barragem>> BuscarListaPorIdBarragem(int id);
        Task<bool> DeletarBarragemRelacionais(int idBarragem);
    }
}
