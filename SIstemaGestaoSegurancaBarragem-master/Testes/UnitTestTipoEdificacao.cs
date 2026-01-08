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
    public class UnitTestTipoEdificacao : IAplicacaoTipoEdificacao
    {
        // INTERFACE DOMINIO
        ITipoEdificacao _ITipoEdificacao;

        // INTERFACE SERVICO
        IServicoTipoEdificacao _IServicoTipoEdificacao;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTipoEdificacao(ITipoEdificacao iTipoEdificacao, IServicoTipoEdificacao iServicoTipoEdificacao)
        {
            _ITipoEdificacao = iTipoEdificacao;
            _IServicoTipoEdificacao = iServicoTipoEdificacao;
        }

        public async Task Adicionar(TipoEdificacao objeto)
        {
            await _ITipoEdificacao.Adicionar(objeto);
        }

        public Task AdicionarBarragem(TipoEdificacao tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEdificacao(TipoEdificacao tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacao.AdicionarTipoEdificacao(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(TipoEdificacao tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoEdificacao objeto)
        {
            await _ITipoEdificacao.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoEdificacao(TipoEdificacao tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacao.AtualizaTipoEdificacao(tipoMaterialBarragem);
        }

        public async Task<TipoEdificacao> BuscarPorId(int id)
        {
            return await _ITipoEdificacao.BuscarPorId(id);
        }

        public async Task Excluir(TipoEdificacao objeto)
        {
            await _ITipoEdificacao.Excluir(objeto);
        }

        public async Task<List<TipoEdificacao>> Listar()
        {
            return await _ITipoEdificacao.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEdificacao>> ListarTipoEdificacao()
        {
            return await _IServicoTipoEdificacao.ListarTipoEdificacao();
        }
    }

    public class UnitTestTipoEdificacaoTests
    {
        private readonly Mock<ITipoEdificacao> _mockITipoEdificacao;
        private readonly Mock<IServicoTipoEdificacao> _mockIServicoTipoEdificacao;
        private readonly UnitTestTipoEdificacao _unitTestTipoEdificacao;

        public UnitTestTipoEdificacaoTests()
        {
            _mockITipoEdificacao = new Mock<ITipoEdificacao>();
            _mockIServicoTipoEdificacao = new Mock<IServicoTipoEdificacao>();
            _unitTestTipoEdificacao = new UnitTestTipoEdificacao(_mockITipoEdificacao.Object, _mockIServicoTipoEdificacao.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITipoEdificacao()
        {
            var tipoEdificacao = new TipoEdificacao();
            await _unitTestTipoEdificacao.Adicionar(tipoEdificacao);

            _mockITipoEdificacao.Verify(m => m.Adicionar(tipoEdificacao), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITipoEdificacao()
        {
            var tipoEdificacao = new TipoEdificacao();
            await _unitTestTipoEdificacao.Atualizar(tipoEdificacao);

            _mockITipoEdificacao.Verify(m => m.Atualizar(tipoEdificacao), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITipoEdificacao()
        {
            var tipoEdificacao = new TipoEdificacao();
            await _unitTestTipoEdificacao.Excluir(tipoEdificacao);

            _mockITipoEdificacao.Verify(m => m.Excluir(tipoEdificacao), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITipoEdificacao()
        {
            var tiposEdificacao = new List<TipoEdificacao> { new TipoEdificacao() };
            _mockITipoEdificacao.Setup(m => m.Listar()).ReturnsAsync(tiposEdificacao);

            var result = await _unitTestTipoEdificacao.Listar();

            Assert.Equal(tiposEdificacao, result);
        }

        [Fact]
        public async Task ListarTipoEdificacao_CallsListarTipoEdificacaoOnIServicoTipoEdificacao()
        {
            var tiposEdificacao = new List<TipoEdificacao> { new TipoEdificacao() };
            _mockIServicoTipoEdificacao.Setup(m => m.ListarTipoEdificacao()).ReturnsAsync(tiposEdificacao);

            var result = await _unitTestTipoEdificacao.ListarTipoEdificacao();

            Assert.Equal(tiposEdificacao, result);
        }
    }
}
