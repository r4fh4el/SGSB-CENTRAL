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
    public class UnitTestSistemaDrenagem
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<ISistemaDrenagem> _mockSistemaDrenagem;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoSistemaDrenagem> _mockServicoSistemaDrenagem;

        public UnitTestSistemaDrenagem()
        {
            _mockSistemaDrenagem = new Mock<ISistemaDrenagem>();
            _mockServicoSistemaDrenagem = new Mock<IServicoSistemaDrenagem>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var sistemaDrenagem = new SistemaDrenagem { Id = 1 };
            _mockSistemaDrenagem.Setup(x => x.Adicionar(It.IsAny<SistemaDrenagem>())).Returns(Task.CompletedTask);

            // Act
            await _mockSistemaDrenagem.Object.Adicionar(sistemaDrenagem);

            // Assert
            _mockSistemaDrenagem.Verify(x => x.Adicionar(It.IsAny<SistemaDrenagem>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var sistemaDrenagem = new SistemaDrenagem { Id = 1 };
            _mockSistemaDrenagem.Setup(x => x.Atualizar(It.IsAny<SistemaDrenagem>())).Returns(Task.CompletedTask);

            // Act
            await _mockSistemaDrenagem.Object.Atualizar(sistemaDrenagem);

            // Assert
            _mockSistemaDrenagem.Verify(x => x.Atualizar(It.IsAny<SistemaDrenagem>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var sistemaDrenagem = new SistemaDrenagem { Id = 1 };
            _mockSistemaDrenagem.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(sistemaDrenagem);

            // Act
            var result = await _mockSistemaDrenagem.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var sistemaDrenagem = new SistemaDrenagem { Id = 1 };
            _mockSistemaDrenagem.Setup(x => x.Excluir(It.IsAny<SistemaDrenagem>())).Returns(Task.CompletedTask);

            // Act
            await _mockSistemaDrenagem.Object.Excluir(sistemaDrenagem);

            // Assert
            _mockSistemaDrenagem.Verify(x => x.Excluir(It.IsAny<SistemaDrenagem>()), Times.Once);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaSistemaDrenagem = new List<SistemaDrenagem> { new SistemaDrenagem { Id = 1 } };
            _mockSistemaDrenagem.Setup(x => x.Listar()).ReturnsAsync(listaSistemaDrenagem);

            // Act
            var result = await _mockSistemaDrenagem.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestAdicionarSistemaDrenagem()
        {
            // Arrange
            var sistemaDrenagem = new SistemaDrenagem { Id = 1 };
            _mockServicoSistemaDrenagem.Setup(x => x.AdicionarSistemaDrenagem(It.IsAny<SistemaDrenagem>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoSistemaDrenagem.Object.AdicionarSistemaDrenagem(sistemaDrenagem);

            // Assert
            _mockServicoSistemaDrenagem.Verify(x => x.AdicionarSistemaDrenagem(It.IsAny<SistemaDrenagem>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaSistemaDrenagem()
        {
            // Arrange
            var sistemaDrenagem = new SistemaDrenagem { Id = 1 };
            _mockServicoSistemaDrenagem.Setup(x => x.AtualizaSistemaDrenagem(It.IsAny<SistemaDrenagem>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoSistemaDrenagem.Object.AtualizaSistemaDrenagem(sistemaDrenagem);

            // Assert
            _mockServicoSistemaDrenagem.Verify(x => x.AtualizaSistemaDrenagem(It.IsAny<SistemaDrenagem>()), Times.Once);
        }

        [Fact]
        public async Task TestListarSistemaDrenagem()
        {
            // Arrange
            var listaSistemaDrenagem = new List<SistemaDrenagem> { new SistemaDrenagem { Id = 1 } };
            _mockServicoSistemaDrenagem.Setup(x => x.ListarSistemaDrenagem()).ReturnsAsync(listaSistemaDrenagem);

            // Act
            var result = await _mockServicoSistemaDrenagem.Object.ListarSistemaDrenagem();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
