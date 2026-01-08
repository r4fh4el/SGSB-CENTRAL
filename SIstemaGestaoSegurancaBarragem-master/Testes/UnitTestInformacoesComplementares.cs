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
    public class UnitTestInformacoesComplementares
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IInformacoesComplementares> _mockInformacoesComplementares;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoInformacoesComplementares> _mockServicoInformacoesComplementares;

        public UnitTestInformacoesComplementares()
        {
            _mockInformacoesComplementares = new Mock<IInformacoesComplementares>();
            _mockServicoInformacoesComplementares = new Mock<IServicoInformacoesComplementares>();
        }

        [Fact]
        public async Task TestAdicionarInformacoesComplementares()
        {
            // Arrange
            var informacao = new InformacoesComplementares { Id = 1 };
            _mockInformacoesComplementares.Setup(x => x.Adicionar(It.IsAny<InformacoesComplementares>())).Returns(Task.CompletedTask);

            // Act
            await _mockInformacoesComplementares.Object.Adicionar(informacao);

            // Assert
            _mockInformacoesComplementares.Verify(x => x.Adicionar(It.IsAny<InformacoesComplementares>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarInformacoesComplementares()
        {
            // Arrange
            var informacao = new InformacoesComplementares { Id = 1 };
            _mockInformacoesComplementares.Setup(x => x.Atualizar(It.IsAny<InformacoesComplementares>())).Returns(Task.CompletedTask);

            // Act
            await _mockInformacoesComplementares.Object.Atualizar(informacao);

            // Assert
            _mockInformacoesComplementares.Verify(x => x.Atualizar(It.IsAny<InformacoesComplementares>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var informacao = new InformacoesComplementares { Id = 1 };
            _mockInformacoesComplementares.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(informacao);

            // Act
            var result = await _mockInformacoesComplementares.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarInformacoesComplementares()
        {
            // Arrange
            var listaInformacoes = new List<InformacoesComplementares> { new InformacoesComplementares { Id = 1 } };
            _mockServicoInformacoesComplementares.Setup(x => x.ListarInformacoesComplementares()).ReturnsAsync(listaInformacoes);

            // Act
            var result = await _mockServicoInformacoesComplementares.Object.ListarInformacoesComplementares();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirInformacoesComplementares()
        {
            // Arrange
            var informacao = new InformacoesComplementares { Id = 1 };
            _mockInformacoesComplementares.Setup(x => x.Excluir(It.IsAny<InformacoesComplementares>())).Returns(Task.CompletedTask);

            // Act
            await _mockInformacoesComplementares.Object.Excluir(informacao);

            // Assert
            _mockInformacoesComplementares.Verify(x => x.Excluir(It.IsAny<InformacoesComplementares>()), Times.Once);
        }

        [Fact]
        public async Task TestAdicionarInformacoesComplementaresServico()
        {
            // Arrange
            var informacao = new InformacoesComplementares { Id = 1 };
            _mockServicoInformacoesComplementares.Setup(x => x.AdicionarInformacoesComplementares(It.IsAny<InformacoesComplementares>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInformacoesComplementares.Object.AdicionarInformacoesComplementares(informacao);

            // Assert
            _mockServicoInformacoesComplementares.Verify(x => x.AdicionarInformacoesComplementares(It.IsAny<InformacoesComplementares>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarInformacoesComplementaresServico()
        {
            // Arrange
            var informacao = new InformacoesComplementares { Id = 1 };
            _mockServicoInformacoesComplementares.Setup(x => x.AtualizaInformacoesComplementares(It.IsAny<InformacoesComplementares>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInformacoesComplementares.Object.AtualizaInformacoesComplementares(informacao);

            // Assert
            _mockServicoInformacoesComplementares.Verify(x => x.AtualizaInformacoesComplementares(It.IsAny<InformacoesComplementares>()), Times.Once);
        }

        [Fact]
        public async Task TestListarInformacoesComplementaresBarragemId()
        {
            // Arrange
            var listaInformacoes = new List<InformacoesComplementares> { new InformacoesComplementares { Id = 1 } };
            _mockServicoInformacoesComplementares.Setup(x => x.ListarInformacoesComplementaresBarragemId(It.IsAny<int>())).ReturnsAsync(listaInformacoes);

            // Act
            var result = await _mockServicoInformacoesComplementares.Object.ListarInformacoesComplementaresBarragemId(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
