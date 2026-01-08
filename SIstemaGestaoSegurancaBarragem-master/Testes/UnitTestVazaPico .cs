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
    public class UnitTestVazaPico : IAplicacaoVazaPico
    {
        // INTERFACE DOMINIO
        IVazaPico _IVazaPico;

        // INTERFACE SERVICO
        IServicoVazaPico _IServicoVazaPico;

        //CONSTRUTOR COM INJEÇÃO DE INDEPENDÊNCIA
        public UnitTestVazaPico(IVazaPico iVazaPico, IServicoVazaPico iServicoVazaPico)
        {
            _IVazaPico = iVazaPico;
            _IServicoVazaPico = iServicoVazaPico;
        }

        public async Task Adicionar(VazaPico Objeto)
        {
            await _IVazaPico.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(VazaPico vazaPico)
        {
            throw new NotImplementedException();
        }

        public async Task AdicionarVazaPico(VazaPico vazaPico)
        {
            await _IServicoVazaPico.AdicionarVazaPico(vazaPico);
        }

        public Task AtualizaBarragem(VazaPico vazaPico)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(VazaPico Objeto)
        {
            await _IVazaPico.Atualizar(Objeto);
        }

        public async Task AtualizaVazaPico(VazaPico vazaPico)
        {
            await _IServicoVazaPico.AtualizaVazaPico(vazaPico);
        }

        public async Task<VazaPico> BuscarPorId(int Id)
        {
            return await _IVazaPico.BuscarPorId(Id);
        }

        public async Task Excluir(VazaPico Objeto)
        {
            await _IVazaPico.Excluir(Objeto);
        }

        public async Task<List<VazaPico>> Listar()
        {
            return await _IVazaPico.Listar();
        }

        public async Task<List<VazaPico>> ListarVazaPico()
        {
            return await _IServicoVazaPico.ListarVazaPico();
        }

        public async Task<VazaPico> VerificarBarragemId(int BarragemId)
        {
            return await _IServicoVazaPico.VerificarBarragemId(BarragemId);
        }
    }

    public class UnitTestVazaPicoTests
    {
        private readonly Mock<IVazaPico> _mockIVazaPico;
        private readonly Mock<IServicoVazaPico> _mockIServicoVazaPico;
        private readonly UnitTestVazaPico _unitTestVazaPico;

        public UnitTestVazaPicoTests()
        {
            _mockIVazaPico = new Mock<IVazaPico>();
            _mockIServicoVazaPico = new Mock<IServicoVazaPico>();
            _unitTestVazaPico = new UnitTestVazaPico(_mockIVazaPico.Object, _mockIServicoVazaPico.Object);
        }

        [Fact]
        public async Task Adicionar_CallsAdicionarOnIVazaPico()
        {
            var vazaPico = new VazaPico();
            _mockIVazaPico.Setup(m => m.Adicionar(vazaPico)).Returns(Task.CompletedTask);

            await _unitTestVazaPico.Adicionar(vazaPico);

            _mockIVazaPico.Verify(m => m.Adicionar(vazaPico), Times.Once);
        }

        [Fact]
        public async Task Atualizar_CallsAtualizarOnIVazaPico()
        {
            var vazaPico = new VazaPico();
            _mockIVazaPico.Setup(m => m.Atualizar(vazaPico)).Returns(Task.CompletedTask);

            await _unitTestVazaPico.Atualizar(vazaPico);

            _mockIVazaPico.Verify(m => m.Atualizar(vazaPico), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_CallsBuscarPorIdOnIVazaPico()
        {
            int id = 1;
            var expectedVazaPico = new VazaPico();
            _mockIVazaPico.Setup(m => m.BuscarPorId(id)).ReturnsAsync(expectedVazaPico);

            var result = await _unitTestVazaPico.BuscarPorId(id);

            Assert.Equal(expectedVazaPico, result);
            _mockIVazaPico.Verify(m => m.BuscarPorId(id), Times.Once);
        }

        [Fact]
        public async Task Excluir_CallsExcluirOnIVazaPico()
        {
            var vazaPico = new VazaPico();
            _mockIVazaPico.Setup(m => m.Excluir(vazaPico)).Returns(Task.CompletedTask);

            await _unitTestVazaPico.Excluir(vazaPico);

            _mockIVazaPico.Verify(m => m.Excluir(vazaPico), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnIVazaPico()
        {
            var expectedList = new List<VazaPico> { new VazaPico() };
            _mockIVazaPico.Setup(m => m.Listar()).ReturnsAsync(expectedList);

            var result = await _unitTestVazaPico.Listar();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task ListarVazaPico_CallsListarVazaPicoOnIServicoVazaPico()
        {
            var expectedList = new List<VazaPico> { new VazaPico() };
            _mockIServicoVazaPico.Setup(m => m.ListarVazaPico()).ReturnsAsync(expectedList);

            var result = await _unitTestVazaPico.ListarVazaPico();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task VerificarBarragemId_CallsVerificarBarragemIdOnIServicoVazaPico()
        {
            int barragemId = 1;
            var expectedVazaPico = new VazaPico();
            _mockIServicoVazaPico.Setup(m => m.VerificarBarragemId(barragemId)).ReturnsAsync(expectedVazaPico);

            var result = await _unitTestVazaPico.VerificarBarragemId(barragemId);

            Assert.Equal(expectedVazaPico, result);
        }
    }
}
