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
    public class UnitTestTipoMaterialBarragem : IAplicacaoTipoMaterialBarragem
    {
        // INTERFACE DOMINIO
        ITipoMaterialBarragem _ITipoMaterialBarragem;

        // INTERFACE SERVICO
        IServicoTipoMaterialBarragem _IServicoTipoMaterialBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTipoMaterialBarragem(ITipoMaterialBarragem iTipoMaterialBarragem, IServicoTipoMaterialBarragem iServicoTipoMaterialBarragem)
        {
            _ITipoMaterialBarragem = iTipoMaterialBarragem;
            _IServicoTipoMaterialBarragem = iServicoTipoMaterialBarragem;
        }

        public async Task Adicionar(TipoMaterialBarragem objeto)
        {
            await _ITipoMaterialBarragem.Adicionar(objeto);
        }

        public Task AdicionarBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoMaterialBarragem.AdicionarTipoMaterialBarragem(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoMaterialBarragem objeto)
        {
            await _ITipoMaterialBarragem.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoMaterialBarragem.AtualizaTipoMaterialBarragem(tipoMaterialBarragem);
        }

        public async Task<TipoMaterialBarragem> BuscarPorId(int id)
        {
            return await _ITipoMaterialBarragem.BuscarPorId(id);
        }

        public async Task Excluir(TipoMaterialBarragem objeto)
        {
            await _ITipoMaterialBarragem.Excluir(objeto);
        }

        public async Task<List<TipoMaterialBarragem>> Listar()
        {
            return await _ITipoMaterialBarragem.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem()
        {
            return await _IServicoTipoMaterialBarragem.ListarTipoMaterialBarragem();
        }
    }

    public class UnitTestTipoMaterialBarragemTests
    {
        private readonly Mock<ITipoMaterialBarragem> _mockITipoMaterialBarragem;
        private readonly Mock<IServicoTipoMaterialBarragem> _mockIServicoTipoMaterialBarragem;
        private readonly UnitTestTipoMaterialBarragem _unitTestTipoMaterialBarragem;

        public UnitTestTipoMaterialBarragemTests()
        {
            _mockITipoMaterialBarragem = new Mock<ITipoMaterialBarragem>();
            _mockIServicoTipoMaterialBarragem = new Mock<IServicoTipoMaterialBarragem>();
            _unitTestTipoMaterialBarragem = new UnitTestTipoMaterialBarragem(_mockITipoMaterialBarragem.Object, _mockIServicoTipoMaterialBarragem.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITipoMaterialBarragem()
        {
            var tipoMaterialBarragem = new TipoMaterialBarragem();
            await _unitTestTipoMaterialBarragem.Adicionar(tipoMaterialBarragem);

            _mockITipoMaterialBarragem.Verify(m => m.Adicionar(tipoMaterialBarragem), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITipoMaterialBarragem()
        {
            var tipoMaterialBarragem = new TipoMaterialBarragem();
            await _unitTestTipoMaterialBarragem.Atualizar(tipoMaterialBarragem);

            _mockITipoMaterialBarragem.Verify(m => m.Atualizar(tipoMaterialBarragem), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITipoMaterialBarragem()
        {
            var tipoMaterialBarragem = new TipoMaterialBarragem();
            await _unitTestTipoMaterialBarragem.Excluir(tipoMaterialBarragem);

            _mockITipoMaterialBarragem.Verify(m => m.Excluir(tipoMaterialBarragem), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITipoMaterialBarragem()
        {
            var tiposMaterialBarragem = new List<TipoMaterialBarragem> { new TipoMaterialBarragem() };
            _mockITipoMaterialBarragem.Setup(m => m.Listar()).ReturnsAsync(tiposMaterialBarragem);

            var result = await _unitTestTipoMaterialBarragem.Listar();

            Assert.Equal(tiposMaterialBarragem, result);
        }

        [Fact]
        public async Task ListarTipoMaterialBarragem_CallsListarTipoMaterialBarragemOnIServicoTipoMaterialBarragem()
        {
            var tiposMaterialBarragem = new List<TipoMaterialBarragem> { new TipoMaterialBarragem() };
            _mockIServicoTipoMaterialBarragem.Setup(m => m.ListarTipoMaterialBarragem()).ReturnsAsync(tiposMaterialBarragem);

            var result = await _unitTestTipoMaterialBarragem.ListarTipoMaterialBarragem();

            Assert.Equal(tiposMaterialBarragem, result);
        }
    }
}
