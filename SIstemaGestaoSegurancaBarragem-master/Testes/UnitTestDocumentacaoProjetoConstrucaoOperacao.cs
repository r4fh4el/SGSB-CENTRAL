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
    public class UnitTestDocumentacaoProjetoConstrucaoOperacao
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IDocumentacaoProjetoConstrucaoOperacao> _mockDocumentacaoProjetoConstrucaoOperacao;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoDocumentacaoProjetoConstrucaoOperacao> _mockServicoDocumentacaoProjetoConstrucaoOperacao;

        public UnitTestDocumentacaoProjetoConstrucaoOperacao()
        {
            _mockDocumentacaoProjetoConstrucaoOperacao = new Mock<IDocumentacaoProjetoConstrucaoOperacao>();
            _mockServicoDocumentacaoProjetoConstrucaoOperacao = new Mock<IServicoDocumentacaoProjetoConstrucaoOperacao>();
        }

        [Fact]
        public async Task TestAdicionarDocumentacaoProjetoConstrucaoOperacao()
        {
            // Arrange
            var documentacao = new DocumentacaoProjetoConstrucaoOperacao { Id = 1 };
            _mockDocumentacaoProjetoConstrucaoOperacao.Setup(x => x.Adicionar(It.IsAny<DocumentacaoProjetoConstrucaoOperacao>())).Returns(Task.CompletedTask);

            // Act
            await _mockDocumentacaoProjetoConstrucaoOperacao.Object.Adicionar(documentacao);

            // Assert
            _mockDocumentacaoProjetoConstrucaoOperacao.Verify(x => x.Adicionar(It.IsAny<DocumentacaoProjetoConstrucaoOperacao>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizarDocumentacaoProjetoConstrucaoOperacao()
        {
            // Arrange
            var documentacao = new DocumentacaoProjetoConstrucaoOperacao { Id = 1 };
            _mockDocumentacaoProjetoConstrucaoOperacao.Setup(x => x.Atualizar(It.IsAny<DocumentacaoProjetoConstrucaoOperacao>())).Returns(Task.CompletedTask);

            // Act
            await _mockDocumentacaoProjetoConstrucaoOperacao.Object.Atualizar(documentacao);

            // Assert
            _mockDocumentacaoProjetoConstrucaoOperacao.Verify(x => x.Atualizar(It.IsAny<DocumentacaoProjetoConstrucaoOperacao>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var documentacao = new DocumentacaoProjetoConstrucaoOperacao { Id = 1 };
            _mockDocumentacaoProjetoConstrucaoOperacao.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(documentacao);

            // Act
            var result = await _mockDocumentacaoProjetoConstrucaoOperacao.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestListarDocumentacaoProjetoConstrucaoOperacao()
        {
            // Arrange
            var listaDocumentacao = new List<DocumentacaoProjetoConstrucaoOperacao> { new DocumentacaoProjetoConstrucaoOperacao { Id = 1 } };
            _mockServicoDocumentacaoProjetoConstrucaoOperacao.Setup(x => x.ListarDocumentacaoProjetoConstrucaoOperacao()).ReturnsAsync(listaDocumentacao);

            // Act
            var result = await _mockServicoDocumentacaoProjetoConstrucaoOperacao.Object.ListarDocumentacaoProjetoConstrucaoOperacao();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestExcluirDocumentacaoProjetoConstrucaoOperacao()
        {
            // Arrange
            var documentacao = new DocumentacaoProjetoConstrucaoOperacao { Id = 1 };
            _mockDocumentacaoProjetoConstrucaoOperacao.Setup(x => x.Excluir(It.IsAny<DocumentacaoProjetoConstrucaoOperacao>())).Returns(Task.CompletedTask);

            // Act
            await _mockDocumentacaoProjetoConstrucaoOperacao.Object.Excluir(documentacao);

            // Assert
            _mockDocumentacaoProjetoConstrucaoOperacao.Verify(x => x.Excluir(It.IsAny<DocumentacaoProjetoConstrucaoOperacao>()), Times.Once);
        }

        [Fact]
        public async Task TestListarDocumentacaoProjetoConstrucaoOperacaoBarragemId()
        {
            // Arrange
            var listaDocumentacao = new List<DocumentacaoProjetoConstrucaoOperacao> { new DocumentacaoProjetoConstrucaoOperacao { Id = 1 } };
            _mockServicoDocumentacaoProjetoConstrucaoOperacao.Setup(x => x.ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(It.IsAny<int>())).ReturnsAsync(listaDocumentacao);

            // Act
            var result = await _mockServicoDocumentacaoProjetoConstrucaoOperacao.Object.ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
