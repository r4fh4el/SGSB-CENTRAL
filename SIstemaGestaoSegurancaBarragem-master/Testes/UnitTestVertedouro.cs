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
    public class UnitTestVertedouro : IAplicacaoVertedouro
    {
        // INTERFACE DOMINIO
        IVertedouro _IVertedouro;

        // INTERFACE SERVIÇO
        IServicoVertedouro _IServicoVertedouro;

        //CONSTRUTOR COM INJEÇÃO DE INDEPENDÊNCIA
        public UnitTestVertedouro(IVertedouro iVertedouro, IServicoVertedouro iServicoVertedouro)
        {
            _IVertedouro = iVertedouro;
            _IServicoVertedouro = iServicoVertedouro;
        }

        public async Task Adicionar(Vertedouro Objeto)
        {
            await _IVertedouro.Adicionar(Objeto);
        }

        public async Task AdicionarVertedouro(Vertedouro tipoMaterialBarragem)
        {
            await _IServicoVertedouro.AdicionarVertedouro(tipoMaterialBarragem);
        }

        public async Task Atualizar(Vertedouro Objeto)
        {
            await _IVertedouro.Atualizar(Objeto);
        }

        public async Task AtualizaVertedouro(Vertedouro tipoMaterialBarragem)
        {
            await _IServicoVertedouro.AtualizaVertedouro(tipoMaterialBarragem);
        }

        public async Task<Vertedouro> BuscarPorId(int Id)
        {
            return await _IVertedouro.BuscarPorId(Id);
        }

        public async Task Excluir(Vertedouro Objeto)
        {
            await _IVertedouro.Excluir(Objeto);
        }

        public async Task<List<Vertedouro>> Listar()
        {
            return await _IVertedouro.Listar();
        }

        public async Task<List<Vertedouro>> ListarVertedouro()
        {
            return await _IServicoVertedouro.ListarVertedouro();
        }

        public async Task<List<Vertedouro>> ListarVertedouroBarragemId(int idBarragem)
        {
            return await _IServicoVertedouro.ListarVertedouroBarragemId(idBarragem);
        }
    }

    public class UnitTestVertedouroTests
    {
        private readonly Mock<IVertedouro> _mockIVertedouro;
        private readonly Mock<IServicoVertedouro> _mockIServicoVertedouro;
        private readonly UnitTestVertedouro _unitTestVertedouro;

        public UnitTestVertedouroTests()
        {
            _mockIVertedouro = new Mock<IVertedouro>();
            _mockIServicoVertedouro = new Mock<IServicoVertedouro>();
            _unitTestVertedouro = new UnitTestVertedouro(_mockIVertedouro.Object, _mockIServicoVertedouro.Object);
        }

        [Fact]
        public async Task Adicionar_CallsAdicionarOnIVertedouro()
        {
            var vertedouro = new Vertedouro();
            _mockIVertedouro.Setup(m => m.Adicionar(vertedouro)).Returns(Task.CompletedTask);

            await _unitTestVertedouro.Adicionar(vertedouro);

            _mockIVertedouro.Verify(m => m.Adicionar(vertedouro), Times.Once);
        }

        [Fact]
        public async Task Atualizar_CallsAtualizarOnIVertedouro()
        {
            var vertedouro = new Vertedouro();
            _mockIVertedouro.Setup(m => m.Atualizar(vertedouro)).Returns(Task.CompletedTask);

            await _unitTestVertedouro.Atualizar(vertedouro);

            _mockIVertedouro.Verify(m => m.Atualizar(vertedouro), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_CallsBuscarPorIdOnIVertedouro()
        {
            int id = 1;
            var expectedVertedouro = new Vertedouro();
            _mockIVertedouro.Setup(m => m.BuscarPorId(id)).ReturnsAsync(expectedVertedouro);

            var result = await _unitTestVertedouro.BuscarPorId(id);

            Assert.Equal(expectedVertedouro, result);
            _mockIVertedouro.Verify(m => m.BuscarPorId(id), Times.Once);
        }

        [Fact]
        public async Task Excluir_CallsExcluirOnIVertedouro()
        {
            var vertedouro = new Vertedouro();
            _mockIVertedouro.Setup(m => m.Excluir(vertedouro)).Returns(Task.CompletedTask);

            await _unitTestVertedouro.Excluir(vertedouro);

            _mockIVertedouro.Verify(m => m.Excluir(vertedouro), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnIVertedouro()
        {
            var expectedList = new List<Vertedouro> { new Vertedouro() };
            _mockIVertedouro.Setup(m => m.Listar()).ReturnsAsync(expectedList);

            var result = await _unitTestVertedouro.Listar();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task ListarVertedouro_CallsListarVertedouroOnIServicoVertedouro()
        {
            var expectedList = new List<Vertedouro> { new Vertedouro() };
            _mockIServicoVertedouro.Setup(m => m.ListarVertedouro()).ReturnsAsync(expectedList);

            var result = await _unitTestVertedouro.ListarVertedouro();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task ListarVertedouroBarragemId_CallsListarVertedouroBarragemIdOnIServicoVertedouro()
        {
            int idBarragem = 1;
            var expectedList = new List<Vertedouro> { new Vertedouro() };
            _mockIServicoVertedouro.Setup(m => m.ListarVertedouroBarragemId(idBarragem)).ReturnsAsync(expectedList);

            var result = await _unitTestVertedouro.ListarVertedouroBarragemId(idBarragem);

            Assert.Equal(expectedList, result);
        }
    }
}
