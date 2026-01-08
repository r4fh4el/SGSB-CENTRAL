using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoRotaFuga
    {
        public Task AdicionarRotaFuga(RotaFuga RotaFuga);
        public Task AtualizaRotaFuga(RotaFuga RotaFuga);

        public Task<List<RotaFuga>> ListarRotaFuga();

        public Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id);
    }
}
