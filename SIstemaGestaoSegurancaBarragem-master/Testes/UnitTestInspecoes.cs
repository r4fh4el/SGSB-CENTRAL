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
    public class UnitTestInspecoes
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IInspecoes> _mockInspecoes;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoInspecoes> _mockServicoInspecoes;

        public UnitTestInspecoes()
        {
            _mockInspecoes = new Mock<IInspecoes>();
            _mockServicoInspecoes = new Mock<IServicoInspecoes>();
        }

        [Fact]
        public async Task TestAdicionarInspecoes()
        {
            // Arrange
            var inspecao = new Inspecoes { Id = 1 };
            _mockInspecoes.Setup(x => x.Adicionar(It.IsAny<Inspecoes>())).Returns(Task.CompletedTask);

            // Act
            await _mockInspecoes.Object.Adicionar(inspecao);

            // Assert
            _mockInspecoes.Verify(x => x.Adicionar(It.IsAny<Inspecoes>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarInspecoes()
        {
            // Arrange
            var inspecao = new Inspecoes { Id = 1 };
            _mockInspecoes.Setup(x => x.Atualizar(It.IsAny<Inspecoes>())).Returns(Task.CompletedTask);

            // Act
            await _mockInspecoes.Object.Atualizar(inspecao);

            // Assert
            _mockInspecoes.Verify(x => x.Atualizar(It.IsAny<Inspecoes>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var inspecao = new Inspecoes { Id = 1 };
            _mockInspecoes.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(inspecao);

            // Act
            var result = await _mockInspecoes.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarInspecoes()
        {
            // Arrange
            var listaInspecoes = new List<Inspecoes> { new Inspecoes { Id = 1 } };
            _mockServicoInspecoes.Setup(x => x.ListarInspecoes()).ReturnsAsync(listaInspecoes);

            // Act
            var result = await _mockServicoInspecoes.Object.ListarInspecoes();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirInspecoes()
        {
            // Arrange
            var inspecao = new Inspecoes { Id = 1 };
            _mockInspecoes.Setup(x => x.Excluir(It.IsAny<Inspecoes>())).Returns(Task.CompletedTask);

            // Act
            await _mockInspecoes.Object.Excluir(inspecao);

            // Assert
            _mockInspecoes.Verify(x => x.Excluir(It.IsAny<Inspecoes>()), Times.Once);
        }

        [Fact]
        public async Task TestAdicionarInspecoesServico()
        {
            // Arrange
            var inspecao = new Inspecoes { Id = 1 };
            _mockServicoInspecoes.Setup(x => x.AdicionarInspecoes(It.IsAny<Inspecoes>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInspecoes.Object.AdicionarInspecoes(inspecao);

            // Assert
            _mockServicoInspecoes.Verify(x => x.AdicionarInspecoes(It.IsAny<Inspecoes>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaInspecoesServico()
        {
            // Arrange
            var inspecao = new Inspecoes { Id = 1 };
            _mockServicoInspecoes.Setup(x => x.AtualizaInspecoes(It.IsAny<Inspecoes>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInspecoes.Object.AtualizaInspecoes(inspecao);

            // Assert
            _mockServicoInspecoes.Verify(x => x.AtualizaInspecoes(It.IsAny<Inspecoes>()), Times.Once);
        }

        [Fact]
        public async Task TestListarInspecoesBarragemId()
        {
            // Arrange
            var listaInspecoes = new List<Inspecoes> { new Inspecoes { Id = 1 } };
            _mockServicoInspecoes.Setup(x => x.ListarInspecoesBarragemId(It.IsAny<int>())).ReturnsAsync(listaInspecoes);

            // Act
            var result = await _mockServicoInspecoes.Object.ListarInspecoesBarragemId(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
