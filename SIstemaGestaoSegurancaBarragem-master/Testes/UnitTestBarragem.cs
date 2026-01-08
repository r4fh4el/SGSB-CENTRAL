using Aplicacao.Aplicacoes;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Testes
{
    public class UnitTestBarragem
    {
        private readonly Mock<IBarragem> _mockBarragemRepository;
        private readonly Mock<IServicoBarragem> _mockServicoBarragem;
        private readonly AplicacaoBarragem _aplicacaoBarragem;

        public UnitTestBarragem()
        {
            _mockBarragemRepository = new Mock<IBarragem>();
            _mockServicoBarragem = new Mock<IServicoBarragem>();
            _aplicacaoBarragem = new AplicacaoBarragem(_mockBarragemRepository.Object, _mockServicoBarragem.Object);
        }

        [Fact]
        public async Task Adicionar_ShouldAddBarragem()
        {
            // Arrange
            var barragem = new Barragem { Id = 1, Nome = "Barragem Teste" };

            // Act
            await _aplicacaoBarragem.Adicionar(barragem);

            // Assert
            _mockBarragemRepository.Verify(x => x.Adicionar(barragem), Times.Once);
        }

        [Fact]
        public async Task AdicionarBarragem_ShouldCallServicoAdicionarBarragem()
        {
            // Arrange
            var barragem = new Barragem { Id = 1, Nome = "Barragem Teste" };

            // Act
            await _aplicacaoBarragem.AdicionarBarragem(barragem);

            // Assert
            _mockServicoBarragem.Verify(x => x.AdicionarBarragem(barragem), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ShouldUpdateBarragem()
        {
            // Arrange
            var barragem = new Barragem { Id = 1, Nome = "Barragem Atualizada" };

            // Act
            await _aplicacaoBarragem.Atualizar(barragem);

            // Assert
            _mockBarragemRepository.Verify(x => x.Atualizar(barragem), Times.Once);
        }

        [Fact]
        public async Task AtualizaBarragem_ShouldCallServicoAtualizaBarragem()
        {
            // Arrange
            var barragem = new Barragem { Id = 1, Nome = "Barragem Atualizada" };

            // Act
            await _aplicacaoBarragem.AtualizaBarragem(barragem);

            // Assert
            _mockServicoBarragem.Verify(x => x.AtualizaBarragem(barragem), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_ShouldReturnCorrectBarragem()
        {
            // Arrange
            var barragem = new Barragem { Id = 1, Nome = "Barragem Teste" };
            _mockBarragemRepository.Setup(x => x.BuscarPorId(1)).ReturnsAsync(barragem);

            // Act
            var result = await _aplicacaoBarragem.BuscarPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Barragem Teste", result.Nome);
        }

        [Fact]
        public async Task Excluir_ShouldDeleteBarragem()
        {
            // Arrange
            var barragem = new Barragem { Id = 1, Nome = "Barragem to Delete" };

            // Act
            await _aplicacaoBarragem.Excluir(barragem);

            // Assert
            _mockBarragemRepository.Verify(x => x.Excluir(barragem), Times.Once);
        }

        [Fact]
        public async Task Listar_ShouldReturnListOfBarragens()
        {
            // Arrange
            var barragemList = new List<Barragem>
            {
                new Barragem { Id = 1, Nome = "Barragem 1" },
                new Barragem { Id = 2, Nome = "Barragem 2" }
            };
            _mockBarragemRepository.Setup(x => x.Listar()).ReturnsAsync(barragemList);

            // Act
            var result = await _aplicacaoBarragem.Listar();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListarBarragem_ShouldCallServicoListarBarragem()
        {
            // Arrange
            var barragemList = new List<Barragem>
            {
                new Barragem { Id = 1, Nome = "Barragem 1" },
                new Barragem { Id = 2, Nome = "Barragem 2" }
            };
            _mockServicoBarragem.Setup(x => x.ListarBarragem()).ReturnsAsync(barragemList);

            // Act
            var result = await _aplicacaoBarragem.ListarBarragem();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockServicoBarragem.Verify(x => x.ListarBarragem(), Times.Once);
        }
    }
}
