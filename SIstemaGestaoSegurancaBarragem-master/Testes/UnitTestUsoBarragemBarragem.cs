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
    public class UnitTestUsoBarragemBarragem : IAplicacaoUsoBarragemBarragem
    {
        // INTERFACE DOMINIO
        IUsoBarragemBarragem _IUsoBarragemBarragem;

        // INTERFACE SERVICO
        IServicoUsoBarragemBarragem _IServicoUsoBarragemBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public UnitTestUsoBarragemBarragem(IUsoBarragemBarragem iUsoBarragemBarragem, IServicoUsoBarragemBarragem iServicoUsoBarragemBarragem)
        {
            _IUsoBarragemBarragem = iUsoBarragemBarragem;
            _IServicoUsoBarragemBarragem = iServicoUsoBarragemBarragem;
        }

        public async Task Adicionar(UsoBarragemBarragem objeto)
        {
            await _IUsoBarragemBarragem.Adicionar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarUsoBarragemBarragem(UsoBarragemBarragem usoBarragemBarragem)
        {
            await _IServicoUsoBarragemBarragem.AdicionarUsoBarragemBarragem(usoBarragemBarragem);
        }

        public async Task Atualizar(UsoBarragemBarragem objeto)
        {
            await _IUsoBarragemBarragem.Atualizar(objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaUsoBarragemBarragem(UsoBarragemBarragem usoBarragemBarragem)
        {
            await _IServicoUsoBarragemBarragem.AtualizaUsoBarragemBarragem(usoBarragemBarragem);
        }

        public async Task<UsoBarragemBarragem> BuscarPorId(int id)
        {
            return await _IUsoBarragemBarragem.BuscarPorId(id);
        }

        public async Task Excluir(UsoBarragemBarragem objeto)
        {
            await _IUsoBarragemBarragem.Excluir(objeto);
        }

        public async Task<List<UsoBarragemBarragem>> Listar()
        {
            return await _IUsoBarragemBarragem.Listar();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem()
        {
            return await _IServicoUsoBarragemBarragem.ListarUsoBarragemBarragem();
        }

        public Task Adicionar(AplicacaoUsoBarragemBarragem objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoUsoBarragemBarragem objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoUsoBarragemBarragem objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId(int idBarragem)
        {
            return await _IServicoUsoBarragemBarragem.ListarUsoBarragemBarragemBarragemId(idBarragem);
        }
    }

    public class UnitTestUsoBarragemBarragemTests
    {
        private readonly Mock<IUsoBarragemBarragem> _mockIUsoBarragemBarragem;
        private readonly Mock<IServicoUsoBarragemBarragem> _mockIServicoUsoBarragemBarragem;
        private readonly UnitTestUsoBarragemBarragem _unitTestUsoBarragemBarragem;

        public UnitTestUsoBarragemBarragemTests()
        {
            _mockIUsoBarragemBarragem = new Mock<IUsoBarragemBarragem>();
            _mockIServicoUsoBarragemBarragem = new Mock<IServicoUsoBarragemBarragem>();
            _unitTestUsoBarragemBarragem = new UnitTestUsoBarragemBarragem(_mockIUsoBarragemBarragem.Object, _mockIServicoUsoBarragemBarragem.Object);
        }

        [Fact]
        public async Task Adicionar_ValidObject_CallsAdicionarOnIUsoBarragemBarragem()
        {
            var usoBarragemBarragem = new UsoBarragemBarragem();
            await _unitTestUsoBarragemBarragem.Adicionar(usoBarragemBarragem);

            _mockIUsoBarragemBarragem.Verify(m => m.Adicionar(usoBarragemBarragem), Times.Once);
        }

        [Fact]
        public async Task Atualizar_ValidObject_CallsAtualizarOnIUsoBarragemBarragem()
        {
            var usoBarragemBarragem = new UsoBarragemBarragem();
            await _unitTestUsoBarragemBarragem.Atualizar(usoBarragemBarragem);

            _mockIUsoBarragemBarragem.Verify(m => m.Atualizar(usoBarragemBarragem), Times.Once);
        }

        [Fact]
        public async Task Excluir_ValidObject_CallsExcluirOnIUsoBarragemBarragem()
        {
            var usoBarragemBarragem = new UsoBarragemBarragem();
            await _unitTestUsoBarragemBarragem.Excluir(usoBarragemBarragem);

            _mockIUsoBarragemBarragem.Verify(m => m.Excluir(usoBarragemBarragem), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnIUsoBarragemBarragem()
        {
            var usosBarragemBarragem = new List<UsoBarragemBarragem> { new UsoBarragemBarragem() };
            _mockIUsoBarragemBarragem.Setup(m => m.Listar()).ReturnsAsync(usosBarragemBarragem);

            var result = await _unitTestUsoBarragemBarragem.Listar();

            Assert.Equal(usosBarragemBarragem, result);
        }

        [Fact]
        public async Task ListarUsoBarragemBarragem_CallsListarUsoBarragemBarragemOnIServicoUsoBarragemBarragem()
        {
            var usosBarragemBarragem = new List<UsoBarragemBarragem> { new UsoBarragemBarragem() };
            _mockIServicoUsoBarragemBarragem.Setup(m => m.ListarUsoBarragemBarragem()).ReturnsAsync(usosBarragemBarragem);

            var result = await _unitTestUsoBarragemBarragem.ListarUsoBarragemBarragem();

            Assert.Equal(usosBarragemBarragem, result);
        }

        [Fact]
        public async Task ListarUsoBarragemBarragemBarragemId_CallsListarUsoBarragemBarragemBarragemIdOnIServicoUsoBarragemBarragem()
        {
            int idBarragem = 1;
            var usosBarragemBarragem = new List<UsoBarragemBarragem> { new UsoBarragemBarragem() };
            _mockIServicoUsoBarragemBarragem.Setup(m => m.ListarUsoBarragemBarragemBarragemId(idBarragem)).ReturnsAsync(usosBarragemBarragem);

            var result = await _unitTestUsoBarragemBarragem.ListarUsoBarragemBarragemBarragemId(idBarragem);

            Assert.Equal(usosBarragemBarragem, result);
        }
    }
}
