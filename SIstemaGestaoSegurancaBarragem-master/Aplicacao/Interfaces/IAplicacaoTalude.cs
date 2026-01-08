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
    public interface IAplicacaoTalude : IGenericaAplicacao<Talude>
    {
        Task AdicionarTalude(Talude talude);
        Task AtualizaTalude(Talude talude);
        Task<List<Talude>> ListarTalude();
        Task<List<Talude>> ListarTaludeBarragemId(int idBarragem);
    }
}
