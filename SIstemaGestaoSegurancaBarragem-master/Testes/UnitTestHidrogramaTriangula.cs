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
    public class UnitTestHidrogramaTriangula
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IHidrogramaTriangula> _mockHidrogramaTriangula;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoHidrogramaTriangula> _mockServicoHidrogramaTriangula;

        public UnitTestHidrogramaTriangula()
        {
            _mockHidrogramaTriangula = new Mock<IHidrogramaTriangula>();
            _mockServicoHidrogramaTriangula = new Mock<IServicoHidrogramaTriangula>();
        }

        [Fact]
        public async Task TestAdicionarHidrogramaTriangula()
        {
            // Arrange
            var hidrograma = new HidrogramaTriangula { Id = 1 };
            _mockHidrogramaTriangula.Setup(x => x.Adicionar(It.IsAny<HidrogramaTriangula>())).Returns(Task.CompletedTask);

            // Act
            await _mockHidrogramaTriangula.Object.Adicionar(hidrograma);

            // Assert
            _mockHidrogramaTriangula.Verify(x => x.Adicionar(It.IsAny<HidrogramaTriangula>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarHidrogramaTriangula()
        {
            // Arrange
            var hidrograma = new HidrogramaTriangula { Id = 1 };
            _mockHidrogramaTriangula.Setup(x => x.Atualizar(It.IsAny<HidrogramaTriangula>())).Returns(Task.CompletedTask);

            // Act
            await _mockHidrogramaTriangula.Object.Atualizar(hidrograma);

            // Assert
            _mockHidrogramaTriangula.Verify(x => x.Atualizar(It.IsAny<HidrogramaTriangula>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var hidrograma = new HidrogramaTriangula { Id = 1 };
            _mockHidrogramaTriangula.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(hidrograma);

            // Act
            var result = await _mockHidrogramaTriangula.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarHidrogramaTriangula()
        {
            // Arrange
            var listaHidrograma = new List<HidrogramaTriangula> { new HidrogramaTriangula { Id = 1 } };
            _mockServicoHidrogramaTriangula.Setup(x => x.ListarHidrogramaTriangula()).ReturnsAsync(listaHidrograma);

            // Act
            var result = await _mockServicoHidrogramaTriangula.Object.ListarHidrogramaTriangula();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirHidrogramaTriangula()
        {
            // Arrange
            var hidrograma = new HidrogramaTriangula { Id = 1 };
            _mockHidrogramaTriangula.Setup(x => x.Excluir(It.IsAny<HidrogramaTriangula>())).Returns(Task.CompletedTask);

            // Act
            await _mockHidrogramaTriangula.Object.Excluir(hidrograma);

            // Assert
            _mockHidrogramaTriangula.Verify(x => x.Excluir(It.IsAny<HidrogramaTriangula>()), Times.Once);
        }

        [Fact]
        public async Task TestAdicionarHidrogramaTriangulaServico()
        {
            // Arrange
            var hidrograma = new HidrogramaTriangula { Id = 1 };
            _mockServicoHidrogramaTriangula.Setup(x => x.AdicionarHidrogramaTriangula(It.IsAny<HidrogramaTriangula>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoHidrogramaTriangula.Object.AdicionarHidrogramaTriangula(hidrograma);

            // Assert
            _mockServicoHidrogramaTriangula.Verify(x => x.AdicionarHidrogramaTriangula(It.IsAny<HidrogramaTriangula>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarHidrogramaTriangulaServico()
        {
            // Arrange
            var hidrograma = new HidrogramaTriangula { Id = 1 };
            _mockServicoHidrogramaTriangula.Setup(x => x.AtualizaHidrogramaTriangula(It.IsAny<HidrogramaTriangula>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoHidrogramaTriangula.Object.AtualizaHidrogramaTriangula(hidrograma);

            // Assert
            _mockServicoHidrogramaTriangula.Verify(x => x.AtualizaHidrogramaTriangula(It.IsAny<HidrogramaTriangula>()), Times.Once);
        }
    }
}
