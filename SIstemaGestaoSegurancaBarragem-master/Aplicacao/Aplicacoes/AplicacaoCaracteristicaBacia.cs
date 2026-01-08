using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoCaracteristicaBacia : IAplicacaoCaracteristicaBacia
    {
        // INTERFACE DOMINIO
        ICaracteristicaBacia _ICaracteristicaBacia;

        // INTERFACE SERVICO
        IServicoCaracteristicaBacia _IServicoCaracteristicaBacia;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoCaracteristicaBacia(ICaracteristicaBacia iCaracteristicaBacia, IServicoCaracteristicaBacia iServicoCaracteristicaBacia)
        {
            _ICaracteristicaBacia = iCaracteristicaBacia;
            _IServicoCaracteristicaBacia = iServicoCaracteristicaBacia;
        }

        public async Task Adicionar(CaracteristicaBacia Objeto)
        {
           await _ICaracteristicaBacia.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(CaracteristicaBacia caracteristicaBacia)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia)
        {
            await _IServicoCaracteristicaBacia.AdicionarCaracteristicaBacia(caracteristicaBacia);
        }

        public Task AtualizaBarragem(CaracteristicaBacia caracteristicaBacia)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(CaracteristicaBacia Objeto)
        {
            await _ICaracteristicaBacia.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia)
        {
            await _IServicoCaracteristicaBacia.AtualizaCaracteristicaBacia(caracteristicaBacia);
        }

        public async Task<CaracteristicaBacia> BuscarPorId(int Id)
        {
            return await _ICaracteristicaBacia.BuscarPorId(Id);
        }

        public async Task Excluir(CaracteristicaBacia Objeto)
        {
            await _ICaracteristicaBacia.Excluir(Objeto);
        }

        public async  Task<List<CaracteristicaBacia>> Listar()
        {
            return await _ICaracteristicaBacia.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia()
        {
            return await _IServicoCaracteristicaBacia.ListarCaracteristicaBacia();
        }
    }
}
