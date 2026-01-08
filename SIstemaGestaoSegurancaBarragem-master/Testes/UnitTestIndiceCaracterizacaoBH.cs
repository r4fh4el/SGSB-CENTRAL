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
    public class UnitTestIndiceCaracterizacaoBH
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IIndiceCaracterizacaoBH> _mockIndiceCaracterizacaoBH;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoIndiceCaracterizacaoBH> _mockServicoIndiceCaracterizacaoBH;

        public UnitTestIndiceCaracterizacaoBH()
        {
            _mockIndiceCaracterizacaoBH = new Mock<IIndiceCaracterizacaoBH>();
            _mockServicoIndiceCaracterizacaoBH = new Mock<IServicoIndiceCaracterizacaoBH>();
        }

        [Fact]
        public async Task TestAdicionarIndiceCaracterizacaoBH()
        {
            // Arrange
            var indice = new IndiceCaracterizacaoBH { Id = 1 };
            _mockIndiceCaracterizacaoBH.Setup(x => x.Adicionar(It.IsAny<IndiceCaracterizacaoBH>())).Returns(Task.CompletedTask);

            // Act
            await _mockIndiceCaracterizacaoBH.Object.Adicionar(indice);

            // Assert
            _mockIndiceCaracterizacaoBH.Verify(x => x.Adicionar(It.IsAny<IndiceCaracterizacaoBH>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarIndiceCaracterizacaoBH()
        {
            // Arrange
            var indice = new IndiceCaracterizacaoBH { Id = 1 };
            _mockIndiceCaracterizacaoBH.Setup(x => x.Atualizar(It.IsAny<IndiceCaracterizacaoBH>())).Returns(Task.CompletedTask);

            // Act
            await _mockIndiceCaracterizacaoBH.Object.Atualizar(indice);

            // Assert
            _mockIndiceCaracterizacaoBH.Verify(x => x.Atualizar(It.IsAny<IndiceCaracterizacaoBH>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var indice = new IndiceCaracterizacaoBH { Id = 1 };
            _mockIndiceCaracterizacaoBH.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(indice);

            // Act
            var result = await _mockIndiceCaracterizacaoBH.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarIndiceCaracterizacaoBH()
        {
            // Arrange
            var listaIndice = new List<IndiceCaracterizacaoBH> { new IndiceCaracterizacaoBH { Id = 1 } };
            _mockServicoIndiceCaracterizacaoBH.Setup(x => x.ListarIndiceCaracterizacaoBH()).ReturnsAsync(listaIndice);

            // Act
            var result = await _mockServicoIndiceCaracterizacaoBH.Object.ListarIndiceCaracterizacaoBH();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirIndiceCaracterizacaoBH()
        {
            // Arrange
            var indice = new IndiceCaracterizacaoBH { Id = 1 };
            _mockIndiceCaracterizacaoBH.Setup(x => x.Excluir(It.IsAny<IndiceCaracterizacaoBH>())).Returns(Task.CompletedTask);

            // Act
            await _mockIndiceCaracterizacaoBH.Object.Excluir(indice);

            // Assert
            _mockIndiceCaracterizacaoBH.Verify(x => x.Excluir(It.IsAny<IndiceCaracterizacaoBH>()), Times.Once);
        }

        [Fact]
        public async Task TestAdicionarIndiceCaracterizacaoBHServico()
        {
            // Arrange
            var indice = new IndiceCaracterizacaoBH { Id = 1 };
            _mockServicoIndiceCaracterizacaoBH.Setup(x => x.AdicionarIndiceCaracterizacaoBH(It.IsAny<IndiceCaracterizacaoBH>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoIndiceCaracterizacaoBH.Object.AdicionarIndiceCaracterizacaoBH(indice);

            // Assert
            _mockServicoIndiceCaracterizacaoBH.Verify(x => x.AdicionarIndiceCaracterizacaoBH(It.IsAny<IndiceCaracterizacaoBH>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarIndiceCaracterizacaoBHServico()
        {
            // Arrange
            var indice = new IndiceCaracterizacaoBH { Id = 1 };
            _mockServicoIndiceCaracterizacaoBH.Setup(x => x.AtualizaIndiceCaracterizacaoBH(It.IsAny<IndiceCaracterizacaoBH>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoIndiceCaracterizacaoBH.Object.AtualizaIndiceCaracterizacaoBH(indice);

            // Assert
            _mockServicoIndiceCaracterizacaoBH.Verify(x => x.AtualizaIndiceCaracterizacaoBH(It.IsAny<IndiceCaracterizacaoBH>()), Times.Once);
        }
    }
}
