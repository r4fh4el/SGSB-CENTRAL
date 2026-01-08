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
    public class UnitTestTipoEdificacaoBarragem : IAplicacaoTipoEdificacaoBarragem
    {
        // INTERFACE DOMINIO
        ITipoEdificacaoBarragem _ITipoEdificacaoBarragem;

        // INTERFACE SERVICO
        IServicoTipoEdificacaoBarragem _IServicoTipoEdificacaoBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTipoEdificacaoBarragem(ITipoEdificacaoBarragem iTipoEdificacaoBarragem, IServicoTipoEdificacaoBarragem iServicoTipoEdificacaoBarragem)
        {
            _ITipoEdificacaoBarragem = iTipoEdificacaoBarragem;
            _IServicoTipoEdificacaoBarragem = iServicoTipoEdificacaoBarragem;
        }

        public async Task Adicionar(TipoEdificacaoBarragem objeto)
        {
            await _ITipoEdificacaoBarragem.Adicionar(objeto);
        }

        public Task AdicionarBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEdificacaoBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacaoBarragem.AdicionarTipoEdificacaoBarragem(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoEdificacaoBarragem objeto)
        {
            await _ITipoEdificacaoBarragem.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoEdificacaoBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacaoBarragem.AtualizaTipoEdificacaoBarragem(tipoMaterialBarragem);
        }

        public async Task<TipoEdificacaoBarragem> BuscarPorId(int id)
        {
            return await _ITipoEdificacaoBarragem.BuscarPorId(id);
        }

        public async Task Excluir(TipoEdificacaoBarragem objeto)
        {
            await _ITipoEdificacaoBarragem.Excluir(objeto);
        }

        public async Task<List<TipoEdificacaoBarragem>> Listar()
        {
            return await _ITipoEdificacaoBarragem.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem()
        {
            return await _IServicoTipoEdificacaoBarragem.ListarTipoEdificacaoBarragem();
        }

        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem)
        {
            return await _IServicoTipoEdificacaoBarragem.ListarTipoEdificacaoBarragemBarragemId(idBarragem);
        }
    }

    public class UnitTestTipoEdificacaoBarragemTests
    {
        private readonly Mock<ITipoEdificacaoBarragem> _mockITipoEdificacaoBarragem;
        private readonly Mock<IServicoTipoEdificacaoBarragem> _mockIServicoTipoEdificacaoBarragem;
        private readonly UnitTestTipoEdificacaoBarragem _unitTestTipoEdificacaoBarragem;

        public UnitTestTipoEdificacaoBarragemTests()
        {
            _mockITipoEdificacaoBarragem = new Mock<ITipoEdificacaoBarragem>();
            _mockIServicoTipoEdificacaoBarragem = new Mock<IServicoTipoEdificacaoBarragem>();
            _unitTestTipoEdificacaoBarragem = new UnitTestTipoEdificacaoBarragem(_mockITipoEdificacaoBarragem.Object, _mockIServicoTipoEdificacaoBarragem.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITipoEdificacaoBarragem()
        {
            var tipoEdificacaoBarragem = new TipoEdificacaoBarragem();
            await _unitTestTipoEdificacaoBarragem.Adicionar(tipoEdificacaoBarragem);

            _mockITipoEdificacaoBarragem.Verify(m => m.Adicionar(tipoEdificacaoBarragem), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITipoEdificacaoBarragem()
        {
            var tipoEdificacaoBarragem = new TipoEdificacaoBarragem();
            await _unitTestTipoEdificacaoBarragem.Atualizar(tipoEdificacaoBarragem);

            _mockITipoEdificacaoBarragem.Verify(m => m.Atualizar(tipoEdificacaoBarragem), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITipoEdificacaoBarragem()
        {
            var tipoEdificacaoBarragem = new TipoEdificacaoBarragem();
            await _unitTestTipoEdificacaoBarragem.Excluir(tipoEdificacaoBarragem);

            _mockITipoEdificacaoBarragem.Verify(m => m.Excluir(tipoEdificacaoBarragem), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITipoEdificacaoBarragem()
        {
            var tiposEdificacaoBarragem = new List<TipoEdificacaoBarragem> { new TipoEdificacaoBarragem() };
            _mockITipoEdificacaoBarragem.Setup(m => m.Listar()).ReturnsAsync(tiposEdificacaoBarragem);

            var result = await _unitTestTipoEdificacaoBarragem.Listar();

            Assert.Equal(tiposEdificacaoBarragem, result);
        }

        [Fact]
        public async Task ListarTipoEdificacaoBarragem_CallsListarTipoEdificacaoBarragemOnIServicoTipoEdificacaoBarragem()
        {
            var tiposEdificacaoBarragem = new List<TipoEdificacaoBarragem> { new TipoEdificacaoBarragem() };
            _mockIServicoTipoEdificacaoBarragem.Setup(m => m.ListarTipoEdificacaoBarragem()).ReturnsAsync(tiposEdificacaoBarragem);

            var result = await _unitTestTipoEdificacaoBarragem.ListarTipoEdificacaoBarragem();

            Assert.Equal(tiposEdificacaoBarragem, result);
        }

        [Fact]
        public async Task ListarTipoEdificacaoBarragemBarragemId_CallsListarTipoEdificacaoBarragemBarragemIdOnIServicoTipoEdificacaoBarragem()
        {
            int idBarragem = 1;
            var tiposEdificacaoBarragem = new List<TipoEdificacaoBarragem> { new TipoEdificacaoBarragem() };
            _mockIServicoTipoEdificacaoBarragem.Setup(m => m.ListarTipoEdificacaoBarragemBarragemId(idBarragem)).ReturnsAsync(tiposEdificacaoBarragem);

            var result = await _unitTestTipoEdificacaoBarragem.ListarTipoEdificacaoBarragemBarragemId(idBarragem);

            Assert.Equal(tiposEdificacaoBarragem, result);
        }
    }
}
