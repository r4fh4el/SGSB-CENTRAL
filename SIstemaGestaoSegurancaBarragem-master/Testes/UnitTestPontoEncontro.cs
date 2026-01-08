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
    public class UnitTestPontoEncontro
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IPontoEncontro> _mockPontoEncontro;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoPontoEncontro> _mockServicoPontoEncontro;

        public UnitTestPontoEncontro()
        {
            _mockPontoEncontro = new Mock<IPontoEncontro>();
            _mockServicoPontoEncontro = new Mock<IServicoPontoEncontro>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var pontoEncontro = new PontoEncontro { Id = 1 };
            _mockPontoEncontro.Setup(x => x.Adicionar(It.IsAny<PontoEncontro>())).Returns(Task.CompletedTask);

            // Act
            await _mockPontoEncontro.Object.Adicionar(pontoEncontro);

            // Assert
            _mockPontoEncontro.Verify(x => x.Adicionar(It.IsAny<PontoEncontro>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var pontoEncontro = new PontoEncontro { Id = 1 };
            _mockPontoEncontro.Setup(x => x.Atualizar(It.IsAny<PontoEncontro>())).Returns(Task.CompletedTask);

            // Act
            await _mockPontoEncontro.Object.Atualizar(pontoEncontro);

            // Assert
            _mockPontoEncontro.Verify(x => x.Atualizar(It.IsAny<PontoEncontro>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var pontoEncontro = new PontoEncontro { Id = 1 };
            _mockPontoEncontro.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(pontoEncontro);

            // Act
            var result = await _mockPontoEncontro.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var pontoEncontro = new PontoEncontro { Id = 1 };
            _mockPontoEncontro.Setup(x => x.Excluir(It.IsAny<PontoEncontro>())).Returns(Task.CompletedTask);

            // Act
            await _mockPontoEncontro.Object.Excluir(pontoEncontro);

            // Assert
            _mockPontoEncontro.Verify(x => x.Excluir(It.IsAny<PontoEncontro>()), Times.Once);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaPontoEncontro = new List<PontoEncontro> { new PontoEncontro { Id = 1 } };
            _mockPontoEncontro.Setup(x => x.Listar()).ReturnsAsync(listaPontoEncontro);

            // Act
            var result = await _mockPontoEncontro.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestAdicionarPontoEncontro()
        {
            // Arrange
            var pontoEncontro = new PontoEncontro { Id = 1 };
            _mockServicoPontoEncontro.Setup(x => x.AdicionarPontoEncontro(It.IsAny<PontoEncontro>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoPontoEncontro.Object.AdicionarPontoEncontro(pontoEncontro);

            // Assert
            _mockServicoPontoEncontro.Verify(x => x.AdicionarPontoEncontro(It.IsAny<PontoEncontro>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaPontoEncontro()
        {
            // Arrange
            var pontoEncontro = new PontoEncontro { Id = 1 };
            _mockServicoPontoEncontro.Setup(x => x.AtualizaPontoEncontro(It.IsAny<PontoEncontro>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoPontoEncontro.Object.AtualizaPontoEncontro(pontoEncontro);

            // Assert
            _mockServicoPontoEncontro.Verify(x => x.AtualizaPontoEncontro(It.IsAny<PontoEncontro>()), Times.Once);
        }

        [Fact]
        public async Task TestListarPontoEncontro()
        {
            // Arrange
            var listaPontoEncontro = new List<PontoEncontro> { new PontoEncontro { Id = 1 } };
            _mockServicoPontoEncontro.Setup(x => x.ListarPontoEncontro()).ReturnsAsync(listaPontoEncontro);

            // Act
            var result = await _mockServicoPontoEncontro.Object.ListarPontoEncontro();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
