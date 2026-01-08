using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Testes
{
    public class UnitTestTipoEmpreendedor : IAplicacaoTipoEmpreendedor
    {
        // INTERFACE DOMINIO
        ITipoEmpreendedor _ITipoEmpreendedor;

        // INTERFACE SERVICO
        IServicoTipoEmpreendedor _IServicoTipoEmpreendedor;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTipoEmpreendedor(ITipoEmpreendedor iTipoEmpreendedor, IServicoTipoEmpreendedor iServicoTipoEmpreendedor)
        {
            _ITipoEmpreendedor = iTipoEmpreendedor;
            _IServicoTipoEmpreendedor = iServicoTipoEmpreendedor;
        }

        public async Task Adicionar(TipoEmpreendedor objeto)
        {
            await _ITipoEmpreendedor.Adicionar(objeto);
        }

        public Task AdicionarBarragem(TipoEmpreendedor tipoEmpreendedor)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor)
        {
            await _IServicoTipoEmpreendedor.AdicionarTipoEmpreendedor(tipoEmpreendedor);
        }

        public Task AtualizaBarragem(TipoEmpreendedor tipoEmpreendedor)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoEmpreendedor objeto)
        {
            await _ITipoEmpreendedor.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor)
        {
            await _IServicoTipoEmpreendedor.AtualizaTipoEmpreendedor(tipoEmpreendedor);
        }

        public async Task<TipoEmpreendedor> BuscarPorId(int id)
        {
            return await _ITipoEmpreendedor.BuscarPorId(id);
        }

        public async Task Excluir(TipoEmpreendedor objeto)
        {
            await _ITipoEmpreendedor.Excluir(objeto);
        }

        public async Task<List<TipoEmpreendedor>> Listar()
        {
            return await _ITipoEmpreendedor.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor()
        {
            return await _IServicoTipoEmpreendedor.ListarTipoEmpreendedor();
        }
    }

    public class UnitTestTipoEmpreendedorTests
    {
        private readonly Mock<ITipoEmpreendedor> _mockITipoEmpreendedor;
        private readonly Mock<IServicoTipoEmpreendedor> _mockIServicoTipoEmpreendedor;
        private readonly UnitTestTipoEmpreendedor _unitTestTipoEmpreendedor;

        public UnitTestTipoEmpreendedorTests()
        {
            _mockITipoEmpreendedor = new Mock<ITipoEmpreendedor>();
            _mockIServicoTipoEmpreendedor = new Mock<IServicoTipoEmpreendedor>();
            _unitTestTipoEmpreendedor = new UnitTestTipoEmpreendedor(_mockITipoEmpreendedor.Object, _mockIServicoTipoEmpreendedor.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITipoEmpreendedor()
        {
            var tipoEmpreendedor = new TipoEmpreendedor();
            await _unitTestTipoEmpreendedor.Adicionar(tipoEmpreendedor);

            _mockITipoEmpreendedor.Verify(m => m.Adicionar(tipoEmpreendedor), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITipoEmpreendedor()
        {
            var tipoEmpreendedor = new TipoEmpreendedor();
            await _unitTestTipoEmpreendedor.Atualizar(tipoEmpreendedor);

            _mockITipoEmpreendedor.Verify(m => m.Atualizar(tipoEmpreendedor), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITipoEmpreendedor()
        {
            var tipoEmpreendedor = new TipoEmpreendedor();
            await _unitTestTipoEmpreendedor.Excluir(tipoEmpreendedor);

            _mockITipoEmpreendedor.Verify(m => m.Excluir(tipoEmpreendedor), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITipoEmpreendedor()
        {
            var tiposEmpreendedor = new List<TipoEmpreendedor> { new TipoEmpreendedor() };
            _mockITipoEmpreendedor.Setup(m => m.Listar()).ReturnsAsync(tiposEmpreendedor);

            var result = await _unitTestTipoEmpreendedor.Listar();

            Assert.Equal(tiposEmpreendedor, result);
        }

        [Fact]
        public async Task ListarTipoEmpreendedor_CallsListarTipoEmpreendedorOnIServicoTipoEmpreendedor()
        {
            var tiposEmpreendedor = new List<TipoEmpreendedor> { new TipoEmpreendedor() };
            _mockIServicoTipoEmpreendedor.Setup(m => m.ListarTipoEmpreendedor()).ReturnsAsync(tiposEmpreendedor);

            var result = await _unitTestTipoEmpreendedor.ListarTipoEmpreendedor();

            Assert.Equal(tiposEmpreendedor, result);
        }
    }
}
