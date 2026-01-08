using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoCaracteristicaBacia
    {
        Task AdicionarCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia);
        Task AtualizaCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia);
        Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia();
    }
}
