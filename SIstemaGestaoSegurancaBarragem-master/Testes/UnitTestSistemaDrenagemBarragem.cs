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
    public class UnitTestSistemaDrenagemBarragem
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<ISistemaDrenagemBarragem> _mockSistemaDrenagemBarragem;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoSistemaDrenagemBarragem> _mockServicoSistemaDrenagemBarragem;

        public UnitTestSistemaDrenagemBarragem()
        {
            _mockSistemaDrenagemBarragem = new Mock<ISistemaDrenagemBarragem>();
            _mockServicoSistemaDrenagemBarragem = new Mock<IServicoSistemaDrenagemBarragem>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var sistemaDrenagemBarragem = new SistemaDrenagemBarragem { Id = 1 };
            _mockSistemaDrenagemBarragem.Setup(x => x.Adicionar(It.IsAny<SistemaDrenagemBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockSistemaDrenagemBarragem.Object.Adicionar(sistemaDrenagemBarragem);

            // Assert
            _mockSistemaDrenagemBarragem.Verify(x => x.Adicionar(It.IsAny<SistemaDrenagemBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var sistemaDrenagemBarragem = new SistemaDrenagemBarragem { Id = 1 };
            _mockSistemaDrenagemBarragem.Setup(x => x.Atualizar(It.IsAny<SistemaDrenagemBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockSistemaDrenagemBarragem.Object.Atualizar(sistemaDrenagemBarragem);

            // Assert
            _mockSistemaDrenagemBarragem.Verify(x => x.Atualizar(It.IsAny<SistemaDrenagemBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var sistemaDrenagemBarragem = new SistemaDrenagemBarragem { Id = 1 };
            _mockSistemaDrenagemBarragem.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(sistemaDrenagemBarragem);

            // Act
            var result = await _mockSistemaDrenagemBarragem.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var sistemaDrenagemBarragem = new SistemaDrenagemBarragem { Id = 1 };
            _mockSistemaDrenagemBarragem.Setup(x => x.Excluir(It.IsAny<SistemaDrenagemBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockSistemaDrenagemBarragem.Object.Excluir(sistemaDrenagemBarragem);

            // Assert
            _mockSistemaDrenagemBarragem.Verify(x => x.Excluir(It.IsAny<SistemaDrenagemBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaSistemaDrenagemBarragem = new List<SistemaDrenagemBarragem> { new SistemaDrenagemBarragem { Id = 1 } };
            _mockSistemaDrenagemBarragem.Setup(x => x.Listar()).ReturnsAsync(listaSistemaDrenagemBarragem);

            // Act
            var result = await _mockSistemaDrenagemBarragem.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestAdicionarSistemaDrenagemBarragem()
        {
            // Arrange
            var sistemaDrenagemBarragem = new SistemaDrenagemBarragem { Id = 1 };
            _mockServicoSistemaDrenagemBarragem.Setup(x => x.AdicionarSistemaDrenagemBarragem(It.IsAny<SistemaDrenagemBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoSistemaDrenagemBarragem.Object.AdicionarSistemaDrenagemBarragem(sistemaDrenagemBarragem);

            // Assert
            _mockServicoSistemaDrenagemBarragem.Verify(x => x.AdicionarSistemaDrenagemBarragem(It.IsAny<SistemaDrenagemBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaSistemaDrenagemBarragem()
        {
            // Arrange
            var sistemaDrenagemBarragem = new SistemaDrenagemBarragem { Id = 1 };
            _mockServicoSistemaDrenagemBarragem.Setup(x => x.AtualizaSistemaDrenagemBarragem(It.IsAny<SistemaDrenagemBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoSistemaDrenagemBarragem.Object.AtualizaSistemaDrenagemBarragem(sistemaDrenagemBarragem);

            // Assert
            _mockServicoSistemaDrenagemBarragem.Verify(x => x.AtualizaSistemaDrenagemBarragem(It.IsAny<SistemaDrenagemBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestListarSistemaDrenagemBarragem()
        {
            // Arrange
            var listaSistemaDrenagemBarragem = new List<SistemaDrenagemBarragem> { new SistemaDrenagemBarragem { Id = 1 } };
            _mockServicoSistemaDrenagemBarragem.Setup(x => x.ListarSistemaDrenagemBarragem()).ReturnsAsync(listaSistemaDrenagemBarragem);

            // Act
            var result = await _mockServicoSistemaDrenagemBarragem.Object.ListarSistemaDrenagemBarragem();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestListarSistemaDrenagemBarragemBarragemId()
        {
            // Arrange
            var listaSistemaDrenagemBarragem = new List<SistemaDrenagemBarragem> { new SistemaDrenagemBarragem { Id = 1 } };
            _mockServicoSistemaDrenagemBarragem.Setup(x => x.ListarSistemaDrenagemBarragemBarragemId(It.IsAny<int>())).ReturnsAsync(listaSistemaDrenagemBarragem);

            // Act
            var result = await _mockServicoSistemaDrenagemBarragem.Object.ListarSistemaDrenagemBarragemBarragemId(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
