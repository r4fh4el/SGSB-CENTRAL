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
    public class UnitTestReservatorio
    {
        // MOCK INTERFACE DOMINIO
        private readonly Mock<IReservatorio> _mockReservatorio;

        // MOCK INTERFACE SERVICO
        private readonly Mock<IServicoReservatorio> _mockServicoReservatorio;

        public UnitTestReservatorio()
        {
            _mockReservatorio = new Mock<IReservatorio>();
            _mockServicoReservatorio = new Mock<IServicoReservatorio>();
        }

        [Fact]
        public async Task TestAdicionar()
        {
            // Arrange
            var reservatorio = new Reservatorio { Id = 1 };
            _mockReservatorio.Setup(x => x.Adicionar(It.IsAny<Reservatorio>())).Returns(Task.CompletedTask);

            // Act
            await _mockReservatorio.Object.Adicionar(reservatorio);

            // Assert
            _mockReservatorio.Verify(x => x.Adicionar(It.IsAny<Reservatorio>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizar()
        {
            // Arrange
            var reservatorio = new Reservatorio { Id = 1 };
            _mockReservatorio.Setup(x => x.Atualizar(It.IsAny<Reservatorio>())).Returns(Task.CompletedTask);

            // Act
            await _mockReservatorio.Object.Atualizar(reservatorio);

            // Assert
            _mockReservatorio.Verify(x => x.Atualizar(It.IsAny<Reservatorio>()), Times.Once);
        }

        [Fact]
        public async Task TestBuscarPorId()
        {
            // Arrange
            var reservatorio = new Reservatorio { Id = 1 };
            _mockReservatorio.Setup(x => x.BuscarPorId(It.IsAny<int>())).ReturnsAsync(reservatorio);

            // Act
            var result = await _mockReservatorio.Object.BuscarPorId(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task TestExcluir()
        {
            // Arrange
            var reservatorio = new Reservatorio { Id = 1 };
            _mockReservatorio.Setup(x => x.Excluir(It.IsAny<Reservatorio>())).Returns(Task.CompletedTask);

            // Act
            await _mockReservatorio.Object.Excluir(reservatorio);

            // Assert
            _mockReservatorio.Verify(x => x.Excluir(It.IsAny<Reservatorio>()), Times.Once);
        }

        [Fact]
        public async Task TestListar()
        {
            // Arrange
            var listaReservatorio = new List<Reservatorio> { new Reservatorio { Id = 1 } };
            _mockReservatorio.Setup(x => x.Listar()).ReturnsAsync(listaReservatorio);

            // Act
            var result = await _mockReservatorio.Object.Listar();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestAdicionarReservatorio()
        {
            // Arrange
            var reservatorio = new Reservatorio { Id = 1 };
            _mockServicoReservatorio.Setup(x => x.AdicionarReservatorio(It.IsAny<Reservatorio>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoReservatorio.Object.AdicionarReservatorio(reservatorio);

            // Assert
            _mockServicoReservatorio.Verify(x => x.AdicionarReservatorio(It.IsAny<Reservatorio>()), Times.Once);
        }

        [Fact]
        public async Task TestAtualizaReservatorio()
        {
            // Arrange
            var reservatorio = new Reservatorio { Id = 1 };
            _mockServicoReservatorio.Setup(x => x.AtualizaReservatorio(It.IsAny<Reservatorio>())).Returns(Task.CompletedTask);

            // Act
            await _mockServicoReservatorio.Object.AtualizaReservatorio(reservatorio);

            // Assert
            _mockServicoReservatorio.Verify(x => x.AtualizaReservatorio(It.IsAny<Reservatorio>()), Times.Once);
        }

        [Fact]
        public async Task TestListarReservatorio()
        {
            // Arrange
            var listaReservatorio = new List<Reservatorio> { new Reservatorio { Id = 1 } };
            _mockServicoReservatorio.Setup(x => x.ListarReservatorio()).ReturnsAsync(listaReservatorio);

            // Act
            var result = await _mockServicoReservatorio.Object.ListarReservatorio();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async Task TestListarReservatorioBarragemId()
        {
            // Arrange
            var listaReservatorio = new List<Reservatorio> { new Reservatorio { Id = 1 } };
            _mockServicoReservatorio.Setup(x => x.ListarReservatorioBarragemId(It.IsAny<int>())).ReturnsAsync(listaReservatorio);

            // Act
            var result = await _mockServicoReservatorio.Object.ListarReservatorioBarragemId(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }
    }
}
