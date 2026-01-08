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
    public interface IAplicacaoTomadaDagua : IGenericaAplicacao<TomadaDagua>
    {
        Task AdicionarTomadaDagua(TomadaDagua tomadaDagua);
        Task AtualizaTomadaDagua(TomadaDagua tomadaDagua);
        Task<List<TomadaDagua>> ListarTomadaDagua();

        Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem);
    }
}
