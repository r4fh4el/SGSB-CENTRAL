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
    public class UnitTestDocumentacaoPerguntas
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IDocumentacaoPerguntas> _mockDocumentacaoPerguntas;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoDocumentacaoPerguntas> _mockServicoDocumentacaoPerguntas;

        public UnitTestDocumentacaoPerguntas()
        {
            _mockDocumentacaoPerguntas = new Mock<IDocumentacaoPerguntas>();
            _mockServicoDocumentacaoPerguntas = new Mock<IServicoDocumentacaoPerguntas>();
        }

        [Fact]
        public async Task TestAdicionarDocumentacaoPerguntas()
        {
            // Arrange
            var documentacaoPerguntas = new DocumentacaoPerguntas { Id = 1 };
            _mockDocumentacaoPerguntas.Setup(x => x.Adicionar(It.IsAny<DocumentacaoPerguntas>())).Returns(Task.CompletedTask);

            // Act
            await _mockDocumentacaoPerguntas.Object.Adicionar(documentacaoPerguntas);

            // Assert
            _mockDocumentacaoPerguntas.Verify(x => x.Adicionar(It.IsAny<DocumentacaoPerguntas>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarDocumentacaoPerguntas()
        {
            // Arrange
            var documentacaoPerguntas = new DocumentacaoPerguntas { Id = 1 };
            _mockDocumentacaoPerguntas.Setup(x => x.Atualizar(It.IsAny<DocumentacaoPerguntas>())).Returns(Task.CompletedTask);

            // Act
            await _mockDocumentacaoPerguntas.Object.Atualizar(documentacaoPerguntas);

            // Assert
            _mockDocumentacaoPerguntas.Verify(x => x.Atualizar(It.IsAny<DocumentacaoPerguntas>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var documentacaoPerguntas = new DocumentacaoPerguntas { Id = 1 };
            _mockDocumentacaoPerguntas.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(documentacaoPerguntas);

            // Act
            var result = await _mockDocumentacaoPerguntas.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarDocumentacaoPerguntas()
        {
            // Arrange
            var listaDocumentacaoPerguntas = new List<DocumentacaoPerguntas> { new DocumentacaoPerguntas { Id = 1 } };
            _mockServicoDocumentacaoPerguntas.Setup(x => x.ListarDocumentacaoPerguntas()).ReturnsAsync(listaDocumentacaoPerguntas);

            // Act
            var result = await _mockServicoDocumentacaoPerguntas.Object.ListarDocumentacaoPerguntas();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirDocumentacaoPerguntas()
        {
            // Arrange
            var documentacaoPerguntas = new DocumentacaoPerguntas { Id = 1 };
            _mockDocumentacaoPerguntas.Setup(x => x.Excluir(It.IsAny<DocumentacaoPerguntas>())).Returns(Task.CompletedTask);

            // Act
            await _mockDocumentacaoPerguntas.Object.Excluir(documentacaoPerguntas);

            // Assert
            _mockDocumentacaoPerguntas.Verify(x => x.Excluir(It.IsAny<DocumentacaoPerguntas>()), Times.Once);
        }
    }
}
