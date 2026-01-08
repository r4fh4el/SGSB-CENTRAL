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
    public interface IAplicacaoRotaFuga : IGenericaAplicacao<RotaFuga>
    {
        Task AdicionarBarragem(RotaFuga RotaFuga);
        Task AtualizaBarragem(RotaFuga RotaFuga);
        Task<List<RotaFuga>> ListarRotaFuga();

        Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id);
    }
}
