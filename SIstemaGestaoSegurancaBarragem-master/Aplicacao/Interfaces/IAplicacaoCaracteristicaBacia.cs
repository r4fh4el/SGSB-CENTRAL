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
    public interface IAplicacaoCaracteristicaBacia : IGenericaAplicacao<CaracteristicaBacia>
    {
        Task AdicionarBarragem(CaracteristicaBacia caracteristicaBacia);
        Task AtualizaBarragem(CaracteristicaBacia caracteristicaBacia);
        Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia();
    }
}
