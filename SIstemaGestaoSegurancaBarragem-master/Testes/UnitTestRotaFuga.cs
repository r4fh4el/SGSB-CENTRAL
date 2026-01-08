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
    public class UnitTestRotaFuga
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IRotaFuga> _mockRotaFuga;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoRotaFuga> _mockServicoRotaFuga;

        public UnitTestRotaFuga()
        {
            _mockRotaFuga = new Mock<IRotaFuga>();
            _mockServicoRotaFuga = new Mock<IServicoRotaFuga>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var rotaFuga = new RotaFuga { Id = 1 };
            _mockRotaFuga.Setup(x => x.Adicionar(It.IsAny<RotaFuga>())).Returns(Task.CompletedTask);

            // Act
            await _mockRotaFuga.Object.Adicionar(rotaFuga);

            // Assert
            _mockRotaFuga.Verify(x => x.Adicionar(It.IsAny<RotaFuga>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var rotaFuga = new RotaFuga { Id = 1 };
            _mockRotaFuga.Setup(x => x.Atualizar(It.IsAny<RotaFuga>())).Returns(Task.CompletedTask);

            // Act
            await _mockRotaFuga.Object.Atualizar(rotaFuga);

            // Assert
            _mockRotaFuga.Verify(x => x.Atualizar(It.IsAny<RotaFuga>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var rotaFuga = new RotaFuga { Id = 1 };
            _mockRotaFuga.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(rotaFuga);

            // Act
            var result = await _mockRotaFuga.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var rotaFuga = new RotaFuga { Id = 1 };
            _mockRotaFuga.Setup(x => x.Excluir(It.IsAny<RotaFuga>())).Returns(Task.CompletedTask);

            // Act
            await _mockRotaFuga.Object.Excluir(rotaFuga);

            // Assert
            _mockRotaFuga.Verify(x => x.Excluir(It.IsAny<RotaFuga>()), Times.Once);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaRotaFuga = new List<RotaFuga> { new RotaFuga { Id = 1 } };
            _mockRotaFuga.Setup(x => x.Listar()).ReturnsAsync(listaRotaFuga);

            // Act
            var result = await _mockRotaFuga.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestAdicionarRotaFuga()
        {
            // Arrange
            var rotaFuga = new RotaFuga { Id = 1 };
            _mockServicoRotaFuga.Setup(x => x.AdicionarRotaFuga(It.IsAny<RotaFuga>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoRotaFuga.Object.AdicionarRotaFuga(rotaFuga);

            // Assert
            _mockServicoRotaFuga.Verify(x => x.AdicionarRotaFuga(It.IsAny<RotaFuga>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaRotaFuga()
        {
            // Arrange
            var rotaFuga = new RotaFuga { Id = 1 };
            _mockServicoRotaFuga.Setup(x => x.AtualizaRotaFuga(It.IsAny<RotaFuga>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoRotaFuga.Object.AtualizaRotaFuga(rotaFuga);

            // Assert
            _mockServicoRotaFuga.Verify(x => x.AtualizaRotaFuga(It.IsAny<RotaFuga>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarListPorIdRotaFuga()
        {
            // Arrange
            var listaRotaFuga = new List<RotaFuga> { new RotaFuga { Id = 1 } };
            _mockRotaFuga.Setup(x => x.BuscarListPorIdRotaFuga(It.IsAny<int>())).ReturnsAsync(listaRotaFuga);

            // Act
            var result = await _mockRotaFuga.Object.BuscarListPorIdRotaFuga(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestListarRotaFuga()
        {
            // Arrange
            var listaRotaFuga = new List<RotaFuga> { new RotaFuga { Id = 1 } };
            _mockServicoRotaFuga.Setup(x => x.ListarRotaFuga()).ReturnsAsync(listaRotaFuga);

            // Act
            var result = await _mockServicoRotaFuga.Object.ListarRotaFuga();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
