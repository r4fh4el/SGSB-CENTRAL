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
    public class UnitTestTempoRuptura : IAplicacaoTempoRuptura
    {
        // INTERFACE DOMINIO
        ITempoRuptura _ITempoRuptura;

        // INTERFACE SERVICO
        IServicoTempoRuptura _IServicoTempoRuptura;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTempoRuptura(ITempoRuptura iTempoRuptura, IServicoTempoRuptura iServicoTempoRuptura)
        {
            _ITempoRuptura = iTempoRuptura;
            _IServicoTempoRuptura = iServicoTempoRuptura;
        }

        public async Task Adicionar(TempoRuptura Objeto)
        {
            await _ITempoRuptura.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TempoRuptura tempoRuptura)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTempoRuptura(TempoRuptura tempoRuptura)
        {
            await _IServicoTempoRuptura.AdicionarTempoRuptura(tempoRuptura);
        }

        public Task AtualizaBarragem(TempoRuptura tempoRuptura)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TempoRuptura Objeto)
        {
            await _ITempoRuptura.Atualizar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTempoRuptura(TempoRuptura tempoRuptura)
        {
            await _IServicoTempoRuptura.AtualizaTempoRuptura(tempoRuptura);
        }

        public async Task<TempoRuptura> BuscarPorId(int Id)
        {
            return await _ITempoRuptura.BuscarPorId(Id);
        }

        public async Task Excluir(TempoRuptura Objeto)
        {
            await _ITempoRuptura.Excluir(Objeto);
        }

        public async Task<List<TempoRuptura>> Listar()
        {
            return await _ITempoRuptura.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TempoRuptura>> ListarTempoRuptura()
        {
            return await _IServicoTempoRuptura.ListarTempoRuptura();
        }
    }

    public class UnitTestTempoRupturaTests
    {
        private readonly Mock<ITempoRuptura> _mockITempoRuptura;
        private readonly Mock<IServicoTempoRuptura> _mockIServicoTempoRuptura;
        private readonly UnitTestTempoRuptura _unitTestTempoRuptura;

        public UnitTestTempoRupturaTests()
        {
            _mockITempoRuptura = new Mock<ITempoRuptura>();
            _mockIServicoTempoRuptura = new Mock<IServicoTempoRuptura>();
            _unitTestTempoRuptura = new UnitTestTempoRuptura(_mockITempoRuptura.Object, _mockIServicoTempoRuptura.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITempoRuptura()
        {
            var tempoRuptura = new TempoRuptura();
            await _unitTestTempoRuptura.Adicionar(tempoRuptura);

            _mockITempoRuptura.Verify(m => m.Adicionar(tempoRuptura), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITempoRuptura()
        {
            var tempoRuptura = new TempoRuptura();
            await _unitTestTempoRuptura.Atualizar(tempoRuptura);

            _mockITempoRuptura.Verify(m => m.Atualizar(tempoRuptura), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITempoRuptura()
        {
            var tempoRuptura = new TempoRuptura();
            await _unitTestTempoRuptura.Excluir(tempoRuptura);

            _mockITempoRuptura.Verify(m => m.Excluir(tempoRuptura), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITempoRuptura()
        {
            var temposRuptura = new List<TempoRuptura> { new TempoRuptura() };
            _mockITempoRuptura.Setup(m => m.Listar()).ReturnsAsync(temposRuptura);

            var result = await _unitTestTempoRuptura.Listar();

            Assert.Equal(temposRuptura, result);
        }

        [Fact]
        public async Task ListarTempoRuptura_CallsListarTempoRupturaOnIServicoTempoRuptura()
        {
            var temposRuptura = new List<TempoRuptura> { new TempoRuptura() };
            _mockIServicoTempoRuptura.Setup(m => m.ListarTempoRuptura()).ReturnsAsync(temposRuptura);

            var result = await _unitTestTempoRuptura.ListarTempoRuptura();

            Assert.Equal(temposRuptura, result);
        }
    }
}
