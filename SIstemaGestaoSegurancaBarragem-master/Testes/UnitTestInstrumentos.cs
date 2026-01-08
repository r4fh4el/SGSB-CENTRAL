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
    public class UnitTestInstrumentos
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IInstrumentos> _mockInstrumentos;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoInstrumentos> _mockServicoInstrumentos;

        public UnitTestInstrumentos()
        {
            _mockInstrumentos = new Mock<IInstrumentos>();
            _mockServicoInstrumentos = new Mock<IServicoInstrumentos>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var instrumento = new Instrumentos { Id = 1 };
            _mockInstrumentos.Setup(x => x.Adicionar(It.IsAny<Instrumentos>())).Returns(Task.CompletedTask);

            // Act
            await _mockInstrumentos.Object.Adicionar(instrumento);

            // Assert
            _mockInstrumentos.Verify(x => x.Adicionar(It.IsAny<Instrumentos>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var instrumento = new Instrumentos { Id = 1 };
            _mockInstrumentos.Setup(x => x.Atualizar(It.IsAny<Instrumentos>())).Returns(Task.CompletedTask);

            // Act
            await _mockInstrumentos.Object.Atualizar(instrumento);

            // Assert
            _mockInstrumentos.Verify(x => x.Atualizar(It.IsAny<Instrumentos>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var instrumento = new Instrumentos { Id = 1 };
            _mockInstrumentos.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(instrumento);

            // Act
            var result = await _mockInstrumentos.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaInstrumentos = new List<Instrumentos> { new Instrumentos { Id = 1 } };
            _mockInstrumentos.Setup(x => x.Listar()).ReturnsAsync(listaInstrumentos);

            // Act
            var result = await _mockInstrumentos.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var instrumento = new Instrumentos { Id = 1 };
            _mockInstrumentos.Setup(x => x.Excluir(It.IsAny<Instrumentos>())).Returns(Task.CompletedTask);

            // Act
            await _mockInstrumentos.Object.Excluir(instrumento);

            // Assert
            _mockInstrumentos.Verify(x => x.Excluir(It.IsAny<Instrumentos>()), Times.Once);
        }

        [Fact]
        public async Task TestAdicionarInstrumentos()
        {
            // Arrange
            var instrumento = new Instrumentos { Id = 1 };
            _mockServicoInstrumentos.Setup(x => x.AdicionarInstrumentos(It.IsAny<Instrumentos>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInstrumentos.Object.AdicionarInstrumentos(instrumento);

            // Assert
            _mockServicoInstrumentos.Verify(x => x.AdicionarInstrumentos(It.IsAny<Instrumentos>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaInstrumentos()
        {
            // Arrange
            var instrumento = new Instrumentos { Id = 1 };
            _mockServicoInstrumentos.Setup(x => x.AtualizaInstrumentos(It.IsAny<Instrumentos>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInstrumentos.Object.AtualizaInstrumentos(instrumento);

            // Assert
            _mockServicoInstrumentos.Verify(x => x.AtualizaInstrumentos(It.IsAny<Instrumentos>()), Times.Once);
        }

        [Fact]
        public async Task TestListarInstrumentos()
        {
            // Arrange
            var listaInstrumentos = new List<Instrumentos> { new Instrumentos { Id = 1 } };
            _mockServicoInstrumentos.Setup(x => x.ListarInstrumentos()).ReturnsAsync(listaInstrumentos);

            // Act
            var result = await _mockServicoInstrumentos.Object.ListarInstrumentos();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
