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
    public class UnitTestZona : IAplicacaoZona
    {
        // INTERFACE DOMINIO
        IZona _IZona;

        // INTERFACE SERVIÇO
        IServicoZona _IServicoZona;

        //CONSTRUTOR COM INJEÇÃO DE INDEPENDÊNCIA
        public UnitTestZona(IZona iZona, IServicoZona iServicoZona)
        {
            _IZona = iZona;
            _IServicoZona = iServicoZona;
        }

        public async Task Adicionar(Zona Objeto)
        {
            await _IZona.Adicionar(Objeto);
        }

        public async Task AdicionarZona(Zona tipoMaterialBarragem)
        {
            await _IServicoZona.AdicionarZona(tipoMaterialBarragem);
        }

        public async Task Atualizar(Zona Objeto)
        {
            await _IZona.Atualizar(Objeto);
        }

        public async Task AtualizaZona(Zona tipoMaterialBarragem)
        {
            await _IServicoZona.AtualizaZona(tipoMaterialBarragem);
        }

        public async Task<Zona> BuscarPorId(int Id)
        {
            return await _IZona.BuscarPorId(Id);
        }

        public async Task Excluir(Zona Objeto)
        {
            await _IZona.Excluir(Objeto);
        }

        public async Task<List<Zona>> Listar()
        {
            return await _IZona.Listar();
        }

        public async Task<List<Zona>> ListarZona()
        {
            return await _IServicoZona.ListarZona();
        }

        public async Task<List<Zona>> ListarZonaId(int idBarragem)
        {
            return await _IServicoZona.ListarZonaId(idBarragem);
        }
    }

    public class UnitTestZonaTests
    {
        private readonly Mock<IZona> _mockIZona;
        private readonly Mock<IServicoZona> _mockIServicoZona;
        private readonly UnitTestZona _unitTestZona;

        public UnitTestZonaTests()
        {
            _mockIZona = new Mock<IZona>();
            _mockIServicoZona = new Mock<IServicoZona>();
            _unitTestZona = new UnitTestZona(_mockIZona.Object, _mockIServicoZona.Object);
        }

        [Fact]
        public async Task Adicionar_CallsAdicionarOnIZona()
        {
            var zona = new Zona();
            _mockIZona.Setup(m => m.Adicionar(zona)).Returns(Task.CompletedTask);

            await _unitTestZona.Adicionar(zona);

            _mockIZona.Verify(m => m.Adicionar(zona), Times.Once);
        }

        [Fact]
        public async Task Atualizar_CallsAtualizarOnIZona()
        {
            var zona = new Zona();
            _mockIZona.Setup(m => m.Atualizar(zona)).Returns(Task.CompletedTask);

            await _unitTestZona.Atualizar(zona);

            _mockIZona.Verify(m => m.Atualizar(zona), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_CallsBuscarPorIdOnIZona()
        {
            int id = 1;
            var expectedZona = new Zona();
            _mockIZona.Setup(m => m.BuscarPorId(id)).ReturnsAsync(expectedZona);

            var result = await _unitTestZona.BuscarPorId(id);

            Assert.Equal(expectedZona, result);
            _mockIZona.Verify(m => m.BuscarPorId(id), Times.Once);
        }

        [Fact]
        public async Task Excluir_CallsExcluirOnIZona()
        {
            var zona = new Zona();
            _mockIZona.Setup(m => m.Excluir(zona)).Returns(Task.CompletedTask);

            await _unitTestZona.Excluir(zona);

            _mockIZona.Verify(m => m.Excluir(zona), Times.Once);
        }

        [Fact]
        public async Task Listar_CallsListarOnIZona()
        {
            var expectedList = new List<Zona> { new Zona() };
            _mockIZona.Setup(m => m.Listar()).ReturnsAsync(expectedList);

            var result = await _unitTestZona.Listar();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task ListarZona_CallsListarZonaOnIServicoZona()
        {
            var expectedList = new List<Zona> { new Zona() };
            _mockIServicoZona.Setup(m => m.ListarZona()).ReturnsAsync(expectedList);

            var result = await _unitTestZona.ListarZona();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task ListarZonaId_CallsListarZonaIdOnIServicoZona()
        {
            int idBarragem = 1;
            var expectedList = new List<Zona> { new Zona() };
            _mockIServicoZona.Setup(m => m.ListarZonaId(idBarragem)).ReturnsAsync(expectedList);

            var result = await _unitTestZona.ListarZonaId(idBarragem);

            Assert.Equal(expectedList, result);
        }
    }
}
