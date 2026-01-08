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
    public class UnitTestHidrogramaParabolico
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IHidrogramaParabolico> _mockHidrogramaParabolico;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoHidrogramaParabolico> _mockServicoHidrogramaParabolico;

        public UnitTestHidrogramaParabolico()
        {
            _mockHidrogramaParabolico = new Mock<IHidrogramaParabolico>();
            _mockServicoHidrogramaParabolico = new Mock<IServicoHidrogramaParabolico>();
        }

        [Fact]
        public async Task TestAdicionarHidrogramaParabolico()
        {
            // Arrange
            var hidrograma = new HidrogramaParabolico { Id = 1 };
            _mockHidrogramaParabolico.Setup(x => x.Adicionar(It.IsAny<HidrogramaParabolico>())).Returns(Task.CompletedTask);

            // Act
            await _mockHidrogramaParabolico.Object.Adicionar(hidrograma);

            // Assert
            _mockHidrogramaParabolico.Verify(x => x.Adicionar(It.IsAny<HidrogramaParabolico>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarHidrogramaParabolico()
        {
            // Arrange
            var hidrograma = new HidrogramaParabolico { Id = 1 };
            _mockHidrogramaParabolico.Setup(x => x.Atualizar(It.IsAny<HidrogramaParabolico>())).Returns(Task.CompletedTask);

            // Act
            await _mockHidrogramaParabolico.Object.Atualizar(hidrograma);

            // Assert
            _mockHidrogramaParabolico.Verify(x => x.Atualizar(It.IsAny<HidrogramaParabolico>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var hidrograma = new HidrogramaParabolico { Id = 1 };
            _mockHidrogramaParabolico.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(hidrograma);

            // Act
            var result = await _mockHidrogramaParabolico.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarHidrogramaParabolico()
        {
            // Arrange
            var listaHidrograma = new List<HidrogramaParabolico> { new HidrogramaParabolico { Id = 1 } };
            _mockServicoHidrogramaParabolico.Setup(x => x.ListarHidrogramaParabolico()).ReturnsAsync(listaHidrograma);

            // Act
            var result = await _mockServicoHidrogramaParabolico.Object.ListarHidrogramaParabolico();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirHidrogramaParabolico()
        {
            // Arrange
            var hidrograma = new HidrogramaParabolico { Id = 1 };
            _mockHidrogramaParabolico.Setup(x => x.Excluir(It.IsAny<HidrogramaParabolico>())).Returns(Task.CompletedTask);

            // Act
            await _mockHidrogramaParabolico.Object.Excluir(hidrograma);

            // Assert
            _mockHidrogramaParabolico.Verify(x => x.Excluir(It.IsAny<HidrogramaParabolico>()), Times.Once);
        }

        [Fact]
        public async Task TestAdicionarHidrogramaParabolicoServico()
        {
            // Arrange
            var hidrograma = new HidrogramaParabolico { Id = 1 };
            _mockServicoHidrogramaParabolico.Setup(x => x.AdicionarHidrogramaParabolico(It.IsAny<HidrogramaParabolico>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoHidrogramaParabolico.Object.AdicionarHidrogramaParabolico(hidrograma);

            // Assert
            _mockServicoHidrogramaParabolico.Verify(x => x.AdicionarHidrogramaParabolico(It.IsAny<HidrogramaParabolico>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarHidrogramaParabolicoServico()
        {
            // Arrange
            var hidrograma = new HidrogramaParabolico { Id = 1 };
            _mockServicoHidrogramaParabolico.Setup(x => x.AtualizaHidrogramaParabolico(It.IsAny<HidrogramaParabolico>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoHidrogramaParabolico.Object.AtualizaHidrogramaParabolico(hidrograma);

            // Assert
            _mockServicoHidrogramaParabolico.Verify(x => x.AtualizaHidrogramaParabolico(It.IsAny<HidrogramaParabolico>()), Times.Once);
        }
    }
}
