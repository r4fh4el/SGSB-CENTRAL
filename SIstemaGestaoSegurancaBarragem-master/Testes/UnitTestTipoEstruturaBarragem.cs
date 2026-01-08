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
    public class UnitTestTipoEstruturaBarragem : IAplicacaoTipoEstruturaBarragem
    {
        // INTERFACE DOMINIO
        ITipoEstruturaBarragem _ITipoEstruturaBarragem;

        // INTERFACE SERVICO
        IServicoTipoEstruturaBarragem _IServicoTipoEstruturaBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTipoEstruturaBarragem(ITipoEstruturaBarragem iTipoEstruturaBarragem, IServicoTipoEstruturaBarragem iServicoTipoEstruturaBarragem)
        {
            _ITipoEstruturaBarragem = iTipoEstruturaBarragem;
            _IServicoTipoEstruturaBarragem = iServicoTipoEstruturaBarragem;
        }

        public async Task Adicionar(TipoEstruturaBarragem objeto)
        {
            await _ITipoEstruturaBarragem.Adicionar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEstruturaBarragem(TipoEstruturaBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEstruturaBarragem.AdicionarTipoEstruturaBarragem(tipoMaterialBarragem);
        }

        public async Task Atualizar(TipoEstruturaBarragem objeto)
        {
            await _ITipoEstruturaBarragem.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizarTipoEstruturaBarragem(TipoEstruturaBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEstruturaBarragem.AtualizarTipoEstruturaBarragem(tipoMaterialBarragem);
        }

        public async Task<TipoEstruturaBarragem> BuscarPorId(int id)
        {
            return await _ITipoEstruturaBarragem.BuscarPorId(id);
        }

        public async Task Excluir(TipoEstruturaBarragem objeto)
        {
            await _ITipoEstruturaBarragem.Excluir(objeto);
        }

        public async Task<List<TipoEstruturaBarragem>> Listar()
        {
            return await _ITipoEstruturaBarragem.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem()
        {
            return await _IServicoTipoEstruturaBarragem.ListarTipoEstruturaBarragem();
        }
    }

    public class UnitTestTipoEstruturaBarragemTests
    {
        private readonly Mock<ITipoEstruturaBarragem> _mockITipoEstruturaBarragem;
        private readonly Mock<IServicoTipoEstruturaBarragem> _mockIServicoTipoEstruturaBarragem;
        private readonly UnitTestTipoEstruturaBarragem _unitTestTipoEstruturaBarragem;

        public UnitTestTipoEstruturaBarragemTests()
        {
            _mockITipoEstruturaBarragem = new Mock<ITipoEstruturaBarragem>();
            _mockIServicoTipoEstruturaBarragem = new Mock<IServicoTipoEstruturaBarragem>();
            _unitTestTipoEstruturaBarragem = new UnitTestTipoEstruturaBarragem(_mockITipoEstruturaBarragem.Object, _mockIServicoTipoEstruturaBarragem.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITipoEstruturaBarragem()
        {
            var tipoEstruturaBarragem = new TipoEstruturaBarragem();
            await _unitTestTipoEstruturaBarragem.Adicionar(tipoEstruturaBarragem);

            _mockITipoEstruturaBarragem.Verify(m => m.Adicionar(tipoEstruturaBarragem), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITipoEstruturaBarragem()
        {
            var tipoEstruturaBarragem = new TipoEstruturaBarragem();
            await _unitTestTipoEstruturaBarragem.Atualizar(tipoEstruturaBarragem);

            _mockITipoEstruturaBarragem.Verify(m => m.Atualizar(tipoEstruturaBarragem), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITipoEstruturaBarragem()
        {
            var tipoEstruturaBarragem = new TipoEstruturaBarragem();
            await _unitTestTipoEstruturaBarragem.Excluir(tipoEstruturaBarragem);

            _mockITipoEstruturaBarragem.Verify(m => m.Excluir(tipoEstruturaBarragem), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITipoEstruturaBarragem()
        {
            var tiposEstruturaBarragem = new List<TipoEstruturaBarragem> { new TipoEstruturaBarragem() };
            _mockITipoEstruturaBarragem.Setup(m => m.Listar()).ReturnsAsync(tiposEstruturaBarragem);

            var result = await _unitTestTipoEstruturaBarragem.Listar();

            Assert.Equal(tiposEstruturaBarragem, result);
        }

        [Fact]
        public async Task ListarTipoEstruturaBarragem_CallsListarTipoEstruturaBarragemOnIServicoTipoEstruturaBarragem()
        {
            var tiposEstruturaBarragem = new List<TipoEstruturaBarragem> { new TipoEstruturaBarragem() };
            _mockIServicoTipoEstruturaBarragem.Setup(m => m.ListarTipoEstruturaBarragem()).ReturnsAsync(tiposEstruturaBarragem);

            var result = await _unitTestTipoEstruturaBarragem.ListarTipoEstruturaBarragem();

            Assert.Equal(tiposEstruturaBarragem, result);
        }
    }
}
