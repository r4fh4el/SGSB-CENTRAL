
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
    public class UnitTestTicket
    {
        private readonly Mock<ITicket> _mockTicketRepository;
        private readonly Mock<IServicoTicket> _mockServicoTicket;
        private readonly AplicacaoTicket _aplicacaoTicket;

        public UnitTestTicket()
        {
            _mockTicketRepository = new Mock<ITicket>();
            _mockServicoTicket = new Mock<IServicoTicket>();
            _aplicacaoTicket = new AplicacaoTicket(_mockTicketRepository.Object, _mockServicoTicket.Object);
        }

        [Fact]
        public async Task Adicionar_ShouldAddTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Titulo = "Test Ticket" };

            // Act
            await _aplicacaoTicket.Adicionar(ticket);

            // Assert
            _mockTicketRepository.Verify(x => x.Adicionar(ticket), Times.Once);
        }

        [Fact]
        public async Task AdicionarTicket_ShouldCallServicoTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Titulo = "Test Ticket" };

            // Act
            await _aplicacaoTicket.AdicionarTicket(ticket);

            // Assert
            _mockServicoTicket.Verify(x => x.AdicionarTicket(ticket), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ShouldUpdateTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Titulo = "Updated Ticket" };

            // Act
            await _aplicacaoTicket.Atualizar(ticket);

            // Assert
            _mockTicketRepository.Verify(x => x.Atualizar(ticket), Times.Once);
        }

        [Fact]
        public async Task AtualizaTicket_ShouldCallServicoAtualizaTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Titulo = "Updated Ticket" };

            // Act
            await _aplicacaoTicket.AtualizaTicket(ticket);

            // Assert
            _mockServicoTicket.Verify(x => x.AtualizaTicket(ticket), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_ShouldReturnCorrectTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Titulo = "Test Ticket" };
            _mockTicketRepository.Setup(x => x.BuscarPorId(1)).ReturnsAsync(ticket);

            // Act
            var result = await _aplicacaoTicket.BuscarPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Ticket", result.Titulo);
        }

        [Fact]
        public async Task Excluir_ShouldDeleteTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Titulo = "Ticket to Delete" };

            // Act
            await _aplicacaoTicket.Excluir(ticket);

            // Assert
            _mockTicketRepository.Verify(x => x.Excluir(ticket), Times.Once);
        }

        [Fact]
        public async Task Listar_ShouldReturnListOfTickets()
        {
            // Arrange
            var ticketList = new List<Ticket>
            {
                new Ticket { Id = 1, Titulo = "Ticket 1" },
                new Ticket { Id = 2, Titulo = "Ticket 2" }
            };
            _mockTicketRepository.Setup(x => x.Listar()).ReturnsAsync(ticketList);

            // Act
            var result = await _aplicacaoTicket.Listar();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListarTicket_ShouldCallServicoTicketList()
        {
            // Arrange
            var ticketList = new List<Ticket>
            {
                new Ticket { Id = 1, Titulo = "Ticket 1" },
                new Ticket { Id = 2, Titulo = "Ticket 2" }
            };
            _mockServicoTicket.Setup(x => x.ListarTicket()).ReturnsAsync(ticketList);

            // Act
            var result = await _aplicacaoTicket.ListarTicket();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockServicoTicket.Verify(x => x.ListarTicket(), Times.Once);
        }
    }
}
