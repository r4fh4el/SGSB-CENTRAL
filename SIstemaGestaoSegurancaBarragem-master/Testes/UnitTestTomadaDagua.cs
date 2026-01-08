using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Testes
{
    public class UnitTestTomadaDagua : IAplicacaoTomadaDagua
    {
        // INTERFACE DOMINIO
        ITomadaDagua _ITomadaDagua;

        // INTERFACE SERVICO
        IServicoTomadaDagua _IServicoTomadaDagua;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestTomadaDagua(ITomadaDagua iTomadaDagua, IServicoTomadaDagua iServicoTomadaDagua)
        {
            _ITomadaDagua = iTomadaDagua;
            _IServicoTomadaDagua = iServicoTomadaDagua;
        }

        public async Task Adicionar(TomadaDagua objeto)
        {
            await _ITomadaDagua.Adicionar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTomadaDagua(TomadaDagua tomadaDagua)
        {
            await _IServicoTomadaDagua.AdicionarTomadaDagua(tomadaDagua);
        }

        public async Task Atualizar(TomadaDagua objeto)
        {
            await _ITomadaDagua.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTomadaDagua(TomadaDagua tomadaDagua)
        {
            await _IServicoTomadaDagua.AtualizaTomadaDagua(tomadaDagua);
        }

        public async Task<TomadaDagua> BuscarPorId(int id)
        {
            return await _ITomadaDagua.BuscarPorId(id);
        }

        public async Task Excluir(TomadaDagua objeto)
        {
            await _ITomadaDagua.Excluir(objeto);
        }

        public async Task<List<TomadaDagua>> Listar()
        {
            return await _ITomadaDagua.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TomadaDagua>> ListarTomadaDagua()
        {
            return await _IServicoTomadaDagua.ListarTomadaDagua();
        }

        public Task Adicionar(AplicacaoTomadaDagua objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoTomadaDagua objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoTomadaDagua objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem)
        {
            return await _IServicoTomadaDagua.ListarTomadaDaguaBarragemId(idBarragem);
        }
    }

    public class UnitTestTomadaDaguaTests
    {
        private readonly Mock<ITomadaDagua> _mockITomadaDagua;
        private readonly Mock<IServicoTomadaDagua> _mockIServicoTomadaDagua;
        private readonly UnitTestTomadaDagua _unitTestTomadaDagua;

        public UnitTestTomadaDaguaTests()
        {
            _mockITomadaDagua = new Mock<ITomadaDagua>();
            _mockIServicoTomadaDagua = new Mock<IServicoTomadaDagua>();
            _unitTestTomadaDagua = new UnitTestTomadaDagua(_mockITomadaDagua.Object, _mockIServicoTomadaDagua.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnITomadaDagua()
        {
            var tomadaDagua = new TomadaDagua();
            await _unitTestTomadaDagua.Adicionar(tomadaDagua);

            _mockITomadaDagua.Verify(m => m.Adicionar(tomadaDagua), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnITomadaDagua()
        {
            var tomadaDagua = new TomadaDagua();
            await _unitTestTomadaDagua.Atualizar(tomadaDagua);

            _mockITomadaDagua.Verify(m => m.Atualizar(tomadaDagua), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnITomadaDagua()
        {
            var tomadaDagua = new TomadaDagua();
            await _unitTestTomadaDagua.Excluir(tomadaDagua);

            _mockITomadaDagua.Verify(m => m.Excluir(tomadaDagua), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnITomadaDagua()
        {
            var tomadasDagua = new List<TomadaDagua> { new TomadaDagua() };
            _mockITomadaDagua.Setup(m => m.Listar()).ReturnsAsync(tomadasDagua);

            var result = await _unitTestTomadaDagua.Listar();

            Assert.Equal(tomadasDagua, result);
        }

        [Fact]
        public async Task ListarTomadaDagua_CallsListarTomadaDaguaOnIServicoTomadaDagua()
        {
            var tomadasDagua = new List<TomadaDagua> { new TomadaDagua() };
            _mockIServicoTomadaDagua.Setup(m => m.ListarTomadaDagua()).ReturnsAsync(tomadasDagua);

            var result = await _unitTestTomadaDagua.ListarTomadaDagua();

            Assert.Equal(tomadasDagua, result);
        }

        [Fact]
        public async Task ListarTomadaDaguaBarragemId_CallsListarTomadaDaguaBarragemIdOnIServicoTomadaDagua()
        {
            int idBarragem = 1;
            var tomadasDagua = new List<TomadaDagua> { new TomadaDagua() };
            _mockIServicoTomadaDagua.Setup(m => m.ListarTomadaDaguaBarragemId(idBarragem)).ReturnsAsync(tomadasDagua);

            var result = await _unitTestTomadaDagua.ListarTomadaDaguaBarragemId(idBarragem);

            Assert.Equal(tomadasDagua, result);
        }
    }
}
