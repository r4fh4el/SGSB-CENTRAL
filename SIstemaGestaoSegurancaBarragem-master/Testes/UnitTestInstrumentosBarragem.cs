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
    public class UnitTestInstrumentosBarragem
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IInstrumentosBarragem> _mockInstrumentosBarragem;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoInstrumentosBarragem> _mockServicoInstrumentosBarragem;

        public UnitTestInstrumentosBarragem()
        {
            _mockInstrumentosBarragem = new Mock<IInstrumentosBarragem>();
            _mockServicoInstrumentosBarragem = new Mock<IServicoInstrumentosBarragem>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var instrumentosBarragem = new InstrumentosBarragem { Id = 1 };
            _mockInstrumentosBarragem.Setup(x => x.Adicionar(It.IsAny<InstrumentosBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockInstrumentosBarragem.Object.Adicionar(instrumentosBarragem);

            // Assert
            _mockInstrumentosBarragem.Verify(x => x.Adicionar(It.IsAny<InstrumentosBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var instrumentosBarragem = new InstrumentosBarragem { Id = 1 };
            _mockInstrumentosBarragem.Setup(x => x.Atualizar(It.IsAny<InstrumentosBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockInstrumentosBarragem.Object.Atualizar(instrumentosBarragem);

            // Assert
            _mockInstrumentosBarragem.Verify(x => x.Atualizar(It.IsAny<InstrumentosBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var instrumentosBarragem = new InstrumentosBarragem { Id = 1 };
            _mockInstrumentosBarragem.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(instrumentosBarragem);

            // Act
            var result = await _mockInstrumentosBarragem.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var instrumentosBarragem = new InstrumentosBarragem { Id = 1 };
            _mockInstrumentosBarragem.Setup(x => x.Excluir(It.IsAny<InstrumentosBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockInstrumentosBarragem.Object.Excluir(instrumentosBarragem);

            // Assert
            _mockInstrumentosBarragem.Verify(x => x.Excluir(It.IsAny<InstrumentosBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaInstrumentosBarragem = new List<InstrumentosBarragem> { new InstrumentosBarragem { Id = 1 } };
            _mockInstrumentosBarragem.Setup(x => x.Listar()).ReturnsAsync(listaInstrumentosBarragem);

            // Act
            var result = await _mockInstrumentosBarragem.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestAdicionarInstrumentosBarragem()
        {
            // Arrange
            var instrumentosBarragem = new InstrumentosBarragem { Id = 1 };
            _mockServicoInstrumentosBarragem.Setup(x => x.AdicionarInstrumentosBarragem(It.IsAny<InstrumentosBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInstrumentosBarragem.Object.AdicionarInstrumentosBarragem(instrumentosBarragem);

            // Assert
            _mockServicoInstrumentosBarragem.Verify(x => x.AdicionarInstrumentosBarragem(It.IsAny<InstrumentosBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaInstrumentosBarragem()
        {
            // Arrange
            var instrumentosBarragem = new InstrumentosBarragem { Id = 1 };
            _mockServicoInstrumentosBarragem.Setup(x => x.AtualizaInstrumentosBarragem(It.IsAny<InstrumentosBarragem>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoInstrumentosBarragem.Object.AtualizaInstrumentosBarragem(instrumentosBarragem);

            // Assert
            _mockServicoInstrumentosBarragem.Verify(x => x.AtualizaInstrumentosBarragem(It.IsAny<InstrumentosBarragem>()), Times.Once);
        }

        [Fact]
        public async Task TestListarInstrumentosBarragem()
        {
            // Arrange
            var listaInstrumentosBarragem = new List<InstrumentosBarragem> { new InstrumentosBarragem { Id = 1 } };
            _mockServicoInstrumentosBarragem.Setup(x => x.ListarInstrumentosBarragem()).ReturnsAsync(listaInstrumentosBarragem);

            // Act
            var result = await _mockServicoInstrumentosBarragem.Object.ListarInstrumentosBarragem();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestListarInstrumentosBarragemBarragemId()
        {
            // Arrange
            var listaInstrumentosBarragem = new List<InstrumentosBarragem> { new InstrumentosBarragem { Id = 1 } };
            int idBarragem = 1;
            _mockServicoInstrumentosBarragem.Setup(x => x.ListarInstrumentosBarragemBarragemId(It.IsAny<int>())).ReturnsAsync(listaInstrumentosBarragem);

            // Act
            var result = await _mockServicoInstrumentosBarragem.Object.ListarInstrumentosBarragemBarragemId(idBarragem);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
