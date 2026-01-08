using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoDadosGerais
    {
        Task AdicionarDadosGerais(DadosGerais dadosGerais);
        Task AtualizaDadosGerais(DadosGerais dadosGerais);
        Task<List<DadosGerais>> ListarDadosGerais();
        Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem);
    }
}
