using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoCaracteristicaBacia : IServicoCaracteristicaBacia
    {
        private readonly ICaracteristicaBacia _ICaracteristicaBacia;

        public ServicoCaracteristicaBacia(ICaracteristicaBacia caracteristicaBacia) 
        {
            _ICaracteristicaBacia = caracteristicaBacia;
        }

        public async Task AdicionarCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia)
        {
           
             
               await _ICaracteristicaBacia.Adicionar(caracteristicaBacia);
            
        }

        public async Task AtualizaCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia)
        {
            
                await _ICaracteristicaBacia.Atualizar(caracteristicaBacia);
        
        }

        public async Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia()
        {
            return  await _ICaracteristicaBacia.ListarCaracteristicaBacia(n => n.nome != null);
        }
    }
}
