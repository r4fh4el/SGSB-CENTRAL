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
    public class UnitTestTempoConcentracao : IAplicacaoTempoConcentracao
    {
        // INTERFACE DOMINIO
        ITempoConcentracao _ITempoConcentracao;

        // INTERFACE SERVICO
        IServicoTempoConcentracao _IServicoTempoConcentracao;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTempoConcentracao(ITempoConcentracao iTempoConcentracao, IServicoTempoConcentracao iServicoTempoConcentracao)
        {
            _ITempoConcentracao = iTempoConcentracao;
            _IServicoTempoConcentracao = iServicoTempoConcentracao;
        }

        public async Task Adicionar(TempoConcentracao Objeto)
        {
            await _ITempoConcentracao.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TempoConcentracao tempoConcentracao)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTempoConcentracao(TempoConcentracao tempoConcentracao)
        {
            await _IServicoTempoConcentracao.AdicionarTempoConcentracao(tempoConcentracao);
        }

        public Task AtualizaBarragem(TempoConcentracao tempoConcentracao)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TempoConcentracao Objeto)
        {
            await _ITempoConcentracao.Atualizar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTempoConcentracao(TempoConcentracao tempoConcentracao)
        {
            await _IServicoTempoConcentracao.AtualizaTempoConcentracao(tempoConcentracao);
        }

        public async Task<TempoConcentracao> BuscarPorId(int Id)
        {
            return await _ITempoConcentracao.BuscarPorId(Id);
        }

        public async Task Excluir(TempoConcentracao Objeto)
        {
            await _ITempoConcentracao.Excluir(Objeto);
        }

        public async Task<List<TempoConcentracao>> Listar()
        {
            return await _ITempoConcentracao.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TempoConcentracao>> ListarTempoConcentracao()
        {
            return await _IServicoTempoConcentracao.ListarTempoConcentracao();
        }
    }

    public class UnitTestTempoConcentracaoTests
    {
        private readonly Mock<ITempoConcentracao> _mockITempoConcentracao;
        private readonly Mock<IServicoTempoConcentracao> _mockIServicoTempoConcentracao;
        private readonly UnitTestTempoConcentracao _unitTestTempoConcentracao;

        public UnitTestTempoConcentracaoTests()
        {
            _mockITempoConcentracao = new Mock<ITempoConcentracao>();
            _mockIServicoTempoConcentracao = new Mock<IServicoTempoConcentracao>();
            _unitTestTempoConcentracao = new UnitTestTempoConcentracao(_mockITempoConcentracao.Object, _mockIServicoTempoConcentracao.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITempoConcentracao()
        {
            var tempoConcentracao = new TempoConcentracao();
            await _unitTestTempoConcentracao.Adicionar(tempoConcentracao);

            _mockITempoConcentracao.Verify(m => m.Adicionar(tempoConcentracao), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITempoConcentracao()
        {
            var tempoConcentracao = new TempoConcentracao();
            await _unitTestTempoConcentracao.Atualizar(tempoConcentracao);

            _mockITempoConcentracao.Verify(m => m.Atualizar(tempoConcentracao), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITempoConcentracao()
        {
            var tempoConcentracao = new TempoConcentracao();
            await _unitTestTempoConcentracao.Excluir(tempoConcentracao);

            _mockITempoConcentracao.Verify(m => m.Excluir(tempoConcentracao), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITempoConcentracao()
        {
            var temposConcentracao = new List<TempoConcentracao> { new TempoConcentracao() };
            _mockITempoConcentracao.Setup(m => m.Listar()).ReturnsAsync(temposConcentracao);

            var result = await _unitTestTempoConcentracao.Listar();

            Assert.Equal(temposConcentracao, result);
        }

        [Fact]
        public async Task ListarTempoConcentracao_CallsListarTempoConcentracaoOnIServicoTempoConcentracao()
        {
            var temposConcentracao = new List<TempoConcentracao> { new TempoConcentracao() };
            _mockIServicoTempoConcentracao.Setup(m => m.ListarTempoConcentracao()).ReturnsAsync(temposConcentracao);

            var result = await _unitTestTempoConcentracao.ListarTempoConcentracao();

            Assert.Equal(temposConcentracao, result);
        }
    }
}
