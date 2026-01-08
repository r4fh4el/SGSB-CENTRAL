using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Testes
{
    public class UnitTestUsoBarragem : IAplicacaoUsoBarragem
    {
        // INTERFACE DOMINIO
        IUsoBarragem _IUsoBarragem;

        // INTERFACE SERVICO
        IServicoUsoBarragem _IServicoUsoBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestUsoBarragem(IUsoBarragem iUsoBarragem, IServicoUsoBarragem iServicoUsoBarragem)
        {
            _IUsoBarragem = iUsoBarragem;
            _IServicoUsoBarragem = iServicoUsoBarragem;
        }

        public async Task Adicionar(UsoBarragem objeto)
        {
            await _IUsoBarragem.Adicionar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarUsoBarragem(UsoBarragem usoBarragem)
        {
            await _IServicoUsoBarragem.AdicionarUsoBarragem(usoBarragem);
        }

        public async Task Atualizar(UsoBarragem objeto)
        {
            await _IUsoBarragem.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaUsoBarragem(UsoBarragem usoBarragem)
        {
            await _IServicoUsoBarragem.AtualizaUsoBarragem(usoBarragem);
        }

        public async Task<UsoBarragem> BuscarPorId(int id)
        {
            return await _IUsoBarragem.BuscarPorId(id);
        }

        public async Task Excluir(UsoBarragem objeto)
        {
            await _IUsoBarragem.Excluir(objeto);
        }

        public async Task<List<UsoBarragem>> Listar()
        {
            return await _IUsoBarragem.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<UsoBarragem>> ListarUsoBarragem()
        {
            return await _IServicoUsoBarragem.ListarUsoBarragem();
        }

        public Task Adicionar(AplicacaoUsoBarragem objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoUsoBarragem objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoUsoBarragem objeto)
        {
            throw new NotImplementedException();
        }
    }

    public class UnitTestUsoBarragemTests
    {
        private readonly Mock<IUsoBarragem> _mockIUsoBarragem;
        private readonly Mock<IServicoUsoBarragem> _mockIServicoUsoBarragem;
        private readonly UnitTestUsoBarragem _unitTestUsoBarragem;

        public UnitTestUsoBarragemTests()
        {
            _mockIUsoBarragem = new Mock<IUsoBarragem>();
            _mockIServicoUsoBarragem = new Mock<IServicoUsoBarragem>();
            _unitTestUsoBarragem = new UnitTestUsoBarragem(_mockIUsoBarragem.Object, _mockIServicoUsoBarragem.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnIUsoBarragem()
        {
            var usoBarragem = new UsoBarragem();
            await _unitTestUsoBarragem.Adicionar(usoBarragem);

            _mockIUsoBarragem.Verify(m => m.Adicionar(usoBarragem), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnIUsoBarragem()
        {
            var usoBarragem = new UsoBarragem();
            await _unitTestUsoBarragem.Atualizar(usoBarragem);

            _mockIUsoBarragem.Verify(m => m.Atualizar(usoBarragem), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnIUsoBarragem()
        {
            var usoBarragem = new UsoBarragem();
            await _unitTestUsoBarragem.Excluir(usoBarragem);

            _mockIUsoBarragem.Verify(m => m.Excluir(usoBarragem), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnIUsoBarragem()
        {
            var usosBarragem = new List<UsoBarragem> { new UsoBarragem() };
            _mockIUsoBarragem.Setup(m => m.Listar()).ReturnsAsync(usosBarragem);

            var result = await _unitTestUsoBarragem.Listar();

            Assert.Equal(usosBarragem, result);
        }

        [Fact]
        public async Task ListarUsoBarragem_CallsListarUsoBarragemOnIServicoUsoBarragem()
        {
            var usosBarragem = new List<UsoBarragem> { new UsoBarragem() };
            _mockIServicoUsoBarragem.Setup(m => m.ListarUsoBarragem()).ReturnsAsync(usosBarragem);

            var result = await _unitTestUsoBarragem.ListarUsoBarragem();

            Assert.Equal(usosBarragem, result);
        }
    }
}
