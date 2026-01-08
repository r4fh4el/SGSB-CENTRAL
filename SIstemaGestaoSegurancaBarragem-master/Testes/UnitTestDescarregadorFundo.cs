using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Testes
{
    public class UnitTestDescarregadorFundo
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IDescarregadorFundo> _mockDescarregadorFundo;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoDescarregadorFundo> _mockServicoDescarregadorFundo;

        public UnitTestDescarregadorFundo()
        {
            _mockDescarregadorFundo = new Mock<IDescarregadorFundo>();
            _mockServicoDescarregadorFundo = new Mock<IServicoDescarregadorFundo>();
        }

        [Fact]
        public async Task TestAdicionarDescarregadorFundo()
        {
            // Arrange
            var descarregadorFundo = new DescarregadorFundo { Id = 1 };
            _mockDescarregadorFundo.Setup(x => x.Adicionar(It.IsAny<DescarregadorFundo>())).Returns(Task.CompletedTask);

            // Act
            await _mockDescarregadorFundo.Object.Adicionar(descarregadorFundo);

            // Assert
            _mockDescarregadorFundo.Verify(x => x.Adicionar(It.IsAny<DescarregadorFundo>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarDescarregadorFundo()
        {
            // Arrange
            var descarregadorFundo = new DescarregadorFundo { Id = 1 };
            _mockDescarregadorFundo.Setup(x => x.Atualizar(It.IsAny<DescarregadorFundo>())).Returns(Task.CompletedTask);

            // Act
            await _mockDescarregadorFundo.Object.Atualizar(descarregadorFundo);

            // Assert
            _mockDescarregadorFundo.Verify(x => x.Atualizar(It.IsAny<DescarregadorFundo>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var descarregadorFundo = new DescarregadorFundo { Id = 1 };
            _mockDescarregadorFundo.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(descarregadorFundo);

            // Act
            var result = await _mockDescarregadorFundo.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarDescarregadorFundo()
        {
            // Arrange
            var listaDescarregadorFundo = new List<DescarregadorFundo> { new DescarregadorFundo { Id = 1 } };
            _mockServicoDescarregadorFundo.Setup(x => x.ListarDescarregadorFundo()).ReturnsAsync(listaDescarregadorFundo);

            // Act
            var result = await _mockServicoDescarregadorFundo.Object.ListarDescarregadorFundo();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestListarDescarregadorFundoBarragemId()
        {
            // Arrange
            var listaDescarregadorFundo = new List<DescarregadorFundo> { new DescarregadorFundo { Id = 1 } };
            _mockServicoDescarregadorFundo.Setup(x => x.ListarDescarregadorFundoBarragemId(It.IsAny<int>())).ReturnsAsync(listaDescarregadorFundo);

            // Act
            var result = await _mockServicoDescarregadorFundo.Object.ListarDescarregadorFundoBarragemId(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
