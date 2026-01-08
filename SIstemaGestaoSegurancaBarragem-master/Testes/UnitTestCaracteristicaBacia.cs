using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Testes
{ 
    public class UnitTestCaracteristicaBacia : IAplicacaoCaracteristicaBacia
    {
        private readonly ICaracteristicaBacia _ICaracteristicaBacia;
        private readonly IServicoCaracteristicaBacia _IServicoCaracteristicaBacia;

        // Construtor com injeção de dependência
        public UnitTestCaracteristicaBacia(ICaracteristicaBacia iCaracteristicaBacia, IServicoCaracteristicaBacia iServicoCaracteristicaBacia)
        {
            _ICaracteristicaBacia = iCaracteristicaBacia ?? throw new ArgumentNullException(nameof(iCaracteristicaBacia));
            _IServicoCaracteristicaBacia = iServicoCaracteristicaBacia ?? throw new ArgumentNullException(nameof(iServicoCaracteristicaBacia));
        }

        public async Task Adicionar(CaracteristicaBacia objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            await _ICaracteristicaBacia.Adicionar(objeto);
        }

        public async Task AdicionarBarragem(CaracteristicaBacia caracteristicaBacia)
        {
            if (caracteristicaBacia == null) throw new ArgumentNullException(nameof(caracteristicaBacia));
            // Implementar lógica específica para adicionar barragem, se necessário
            throw new NotImplementedException();
        }

        public async Task AdicionarCaracteristicaBacia(CaracteristicaBacia caracteristicaBacia)
        {
            if (caracteristicaBacia == null) throw new ArgumentNullException(nameof(caracteristicaBacia));
            await _IServicoCaracteristicaBacia.AdicionarCaracteristicaBacia(caracteristicaBacia);
        }

        public async Task Atualizar(CaracteristicaBacia objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            await _ICaracteristicaBacia.Atualizar(objeto);
        }

        public async Task AtualizaBarragem(CaracteristicaBacia caracteristicaBacia)
        {
            if (caracteristicaBacia == null) throw new ArgumentNullException(nameof(caracteristicaBacia));
            // Implementar lógica específica para atualizar barragem, se necessário
            throw new NotImplementedException();
        }

        public async Task<CaracteristicaBacia> BuscarPorId(int id)
        {
            return await _ICaracteristicaBacia.BuscarPorId(id);
        }

        public async Task Excluir(CaracteristicaBacia objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            await _ICaracteristicaBacia.Excluir(objeto);
        }

        public async Task<List<CaracteristicaBacia>> Listar()
        {
            return await _ICaracteristicaBacia.Listar();
        }

        public async Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia()
        {
            return await _IServicoCaracteristicaBacia.ListarCaracteristicaBacia();
        }
    }
}
