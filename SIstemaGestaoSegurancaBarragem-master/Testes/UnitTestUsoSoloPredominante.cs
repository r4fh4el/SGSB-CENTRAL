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
    public class UnitTestUsoSoloPredominante : IAplicacaoUsoSoloPredominante
    {
        // INTERFACE DOMINIO
        IUsoSoloPredominante _IUsoSoloPredominante;

        // INTERFACE SERVICO
        IServicoUsoSoloPredominante _IServicoUsoSoloPredominante;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestUsoSoloPredominante(IUsoSoloPredominante iUsoSoloPredominante, IServicoUsoSoloPredominante iServicoUsoSoloPredominante)
        {
            _IUsoSoloPredominante = iUsoSoloPredominante;
            _IServicoUsoSoloPredominante = iServicoUsoSoloPredominante;
        }

        public async Task Adicionar(UsoSoloPredominante objeto)
        {
            await _IUsoSoloPredominante.Adicionar(objeto);
        }

        public Task AdicionarBarragem(UsoSoloPredominante usoSoloPredominante)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante)
        {
            await _IServicoUsoSoloPredominante.AdicionarUsoSoloPredominante(usoSoloPredominante);
        }

        public Task AtualizaBarragem(UsoSoloPredominante usoSoloPredominante)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(UsoSoloPredominante objeto)
        {
            await _IUsoSoloPredominante.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante)
        {
            await _IServicoUsoSoloPredominante.AtualizaUsoSoloPredominante(usoSoloPredominante);
        }

        public async Task<UsoSoloPredominante> BuscarPorId(int id)
        {
            return await _IUsoSoloPredominante.BuscarPorId(id);
        }

        public async Task Excluir(UsoSoloPredominante objeto)
        {
            await _IUsoSoloPredominante.Excluir(objeto);
        }

        public async Task<List<UsoSoloPredominante>> Listar()
        {
            return await _IUsoSoloPredominante.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante()
        {
            return await _IServicoUsoSoloPredominante.ListarUsoSoloPredominante();
        }
    }

    public class UnitTestUsoSoloPredominanteTests
    {
        private readonly Mock<IUsoSoloPredominante> _mockIUsoSoloPredominante;
        private readonly Mock<IServicoUsoSoloPredominante> _mockIServicoUsoSoloPredominante;
        private readonly UnitTestUsoSoloPredominante _unitTestUsoSoloPredominante;

        public UnitTestUsoSoloPredominanteTests()
        {
            _mockIUsoSoloPredominante = new Mock<IUsoSoloPredominante>();
            _mockIServicoUsoSoloPredominante = new Mock<IServicoUsoSoloPredominante>();
            _unitTestUsoSoloPredominante = new UnitTestUsoSoloPredominante(_mockIUsoSoloPredominante.Object, _mockIServicoUsoSoloPredominante.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnIUsoSoloPredominante()
        {
            var usoSoloPredominante = new UsoSoloPredominante();
            await _unitTestUsoSoloPredominante.Adicionar(usoSoloPredominante);

            _mockIUsoSoloPredominante.Verify(m => m.Adicionar(usoSoloPredominante), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnIUsoSoloPredominante()
        {
            var usoSoloPredominante = new UsoSoloPredominante();
            await _unitTestUsoSoloPredominante.Atualizar(usoSoloPredominante);

            _mockIUsoSoloPredominante.Verify(m => m.Atualizar(usoSoloPredominante), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnIUsoSoloPredominante()
        {
            var usoSoloPredominante = new UsoSoloPredominante();
            await _unitTestUsoSoloPredominante.Excluir(usoSoloPredominante);

            _mockIUsoSoloPredominante.Verify(m => m.Excluir(usoSoloPredominante), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnIUsoSoloPredominante()
        {
            var usosSoloPredominante = new List<UsoSoloPredominante> { new UsoSoloPredominante() };
            _mockIUsoSoloPredominante.Setup(m => m.Listar()).ReturnsAsync(usosSoloPredominante);

            var result = await _unitTestUsoSoloPredominante.Listar();

            Assert.Equal(usosSoloPredominante, result);
        }

        [Fact]
        public async Task ListarUsoSoloPredominante_CallsListarUsoSoloPredominanteOnIServicoUsoSoloPredominante()
        {
            var usosSoloPredominante = new List<UsoSoloPredominante> { new UsoSoloPredominante() };
            _mockIServicoUsoSoloPredominante.Setup(m => m.ListarUsoSoloPredominante()).ReturnsAsync(usosSoloPredominante);

            var result = await _unitTestUsoSoloPredominante.ListarUsoSoloPredominante();

            Assert.Equal(usosSoloPredominante, result);
        }

        [Fact]
        public async Task BuscarPorId_ValidId_CallsBuscarPorIdOnIUsoSoloPredominante()
        {
            int id = 1;
            var usoSoloPredominante = new UsoSoloPredominante();
            _mockIUsoSoloPredominante.Setup(m => m.BuscarPorId(id)).ReturnsAsync(usoSoloPredominante);

            var result = await _unitTestUsoSoloPredominante.BuscarPorId(id);

            Assert.Equal(usoSoloPredominante, result);
        }
    }
}
