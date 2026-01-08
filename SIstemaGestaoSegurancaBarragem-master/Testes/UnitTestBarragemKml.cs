using Aplicacao.Aplicacoes;
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
    public class UnitTestBarragemKml
    {
        private readonly Mock<IBarragemKml> _mockBarragemKmlRepository;
        private readonly Mock<IServicoBarragemKml> _mockServicoBarragemKml;
        private readonly AplicacaoBarragemKml _aplicacaoBarragemKml;

        public UnitTestBarragemKml()
        {
            _mockBarragemKmlRepository = new Mock<IBarragemKml>();
            _mockServicoBarragemKml = new Mock<IServicoBarragemKml>();
            _aplicacaoBarragemKml = new AplicacaoBarragemKml(_mockBarragemKmlRepository.Object, _mockServicoBarragemKml.Object);
        }

        [Fact]
        public async Task Adicionar_ShouldAddBarragemKml()
        {
            // Arrange
            var barragemKml = new BarragemKml
            {
                Id = 1,
                BarragemId = 10,
                Coordenadas = "12.34,56.78",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            // Act
            await _aplicacaoBarragemKml.Adicionar(barragemKml);

            // Assert
            _mockBarragemKmlRepository.Verify(x => x.Adicionar(barragemKml), Times.Once);
        }

        [Fact]
        public async Task AdicionarBarragemKml_ShouldCallServicoAdicionarBarragemKml()
        {
            // Arrange
            var barragemKml = new BarragemKml
            {
                Id = 1,
                BarragemId = 10,
                Coordenadas = "12.34,56.78",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            // Act
            await _aplicacaoBarragemKml.AdicionarBarragemKml(barragemKml);

            // Assert
            _mockServicoBarragemKml.Verify(x => x.AdicionarBarragemKml(barragemKml), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ShouldUpdateBarragemKml()
        {
            // Arrange
            var barragemKml = new BarragemKml
            {
                Id = 1,
                BarragemId = 10,
                Coordenadas = "12.34,56.78",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            // Act
            await _aplicacaoBarragemKml.Atualizar(barragemKml);

            // Assert
            _mockBarragemKmlRepository.Verify(x => x.Atualizar(barragemKml), Times.Once);
        }

        [Fact]
        public async Task AtualizaBarragemKml_ShouldCallServicoAtualizarBarragemKml()
        {
            // Arrange
            var barragemKml = new BarragemKml
            {
                Id = 1,
                BarragemId = 10,
                Coordenadas = "12.34,56.78",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            // Act
            await _aplicacaoBarragemKml.AtualizaBarragemKml(barragemKml);

            // Assert
            _mockServicoBarragemKml.Verify(x => x.AtualizaBarragemKml(barragemKml), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_ShouldReturnCorrectBarragemKml()
        {
            // Arrange
            var barragemKml = new BarragemKml
            {
                Id = 1,
                BarragemId = 10,
                Coordenadas = "12.34,56.78",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };
            _mockBarragemKmlRepository.Setup(x => x.BuscarPorId(1)).ReturnsAsync(barragemKml);

            // Act
            var result = await _aplicacaoBarragemKml.BuscarPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("12.34,56.78", result.Coordenadas);
        }

        [Fact]
        public async Task Excluir_ShouldDeleteBarragemKml()
        {
            // Arrange
            var barragemKml = new BarragemKml
            {
                Id = 1,
                BarragemId = 10,
                Coordenadas = "12.34,56.78",
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            // Act
            await _aplicacaoBarragemKml.Excluir(barragemKml);

            // Assert
            _mockBarragemKmlRepository.Verify(x => x.Excluir(barragemKml), Times.Once);
        }

        [Fact]
        public async Task Listar_ShouldReturnListOfBarragemKml()
        {
            // Arrange
            var barragemKmlList = new List<BarragemKml>
            {
                new BarragemKml { Id = 1, BarragemId = 10, Coordenadas = "12.34,56.78", DataCadastro = DateTime.Now, DataAlteracao = DateTime.Now },
                new BarragemKml { Id = 2, BarragemId = 20, Coordenadas = "34.56,78.90", DataCadastro = DateTime.Now, DataAlteracao = DateTime.Now }
            };
            _mockBarragemKmlRepository.Setup(x => x.Listar()).ReturnsAsync(barragemKmlList);

            // Act
            var result = await _aplicacaoBarragemKml.Listar();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListarBarragemKml_ShouldCallServicoListarBarragemKml()
        {
            // Arrange
            var barragemKmlList = new List<BarragemKml>
            {
                new BarragemKml { Id = 1, BarragemId = 10, Coordenadas = "12.34,56.78", DataCadastro = DateTime.Now, DataAlteracao = DateTime.Now },
                new BarragemKml { Id = 2, BarragemId = 20, Coordenadas = "34.56,78.90", DataCadastro = DateTime.Now, DataAlteracao = DateTime.Now }
            };
            _mockServicoBarragemKml.Setup(x => x.ListarBarragemKml()).ReturnsAsync(barragemKmlList);

            // Act
            var result = await _aplicacaoBarragemKml.ListarBarragemKml();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockServicoBarragemKml.Verify(x => x.ListarBarragemKml(), Times.Once);
        }
    }
}
