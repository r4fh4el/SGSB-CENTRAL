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
    public class UnitTestUsuario : IAplicacaoUsuario
    {
        // INTERFACE DOMINIO
        IUsuario _IUsuario;

        public UnitTestUsuario(IUsuario usuario)
        {
            _IUsuario = usuario;
        }

        public async Task<bool> AdicionarUsuario(string nome, string login, string email, string senha, int idade, string celular)
        {
            return await _IUsuario.AdicionarUsuario(nome, login, email, senha, idade, celular);
        }

        public async Task<List<ApplicationUser>> BuscarUsuarios()
        {
            return await _IUsuario.BuscarUsuario();
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            return await _IUsuario.ExisteUsuario(email, senha);
        }

        public async Task<ApplicationUser> Logar(string login, string senha)
        {
            return await _IUsuario.Logar(login, senha);
        }

        public async Task<string> RetornarIdUsuario(string email)
        {
            return await _IUsuario.RetornarIdUsuario(email);
        }
    }

    public class UnitTestUsuarioTests
    {
        private readonly Mock<IUsuario> _mockIUsuario;
        private readonly UnitTestUsuario _unitTestUsuario;

        public UnitTestUsuarioTests()
        {
            _mockIUsuario = new Mock<IUsuario>();
            _unitTestUsuario = new UnitTestUsuario(_mockIUsuario.Object);
        }

        [Fact]
        public async Task AdicionarUsuario_ValidInput_CallsAdicionarUsuarioOnIUsuario()
        {
            string nome = "Teste";
            string login = "teste.login";
            string email = "teste@email.com";
            string senha = "senha123";
            int idade = 30;
            string celular = "1234567890";

            _mockIUsuario.Setup(m => m.AdicionarUsuario(nome, login, email, senha, idade, celular)).ReturnsAsync(true);

            var result = await _unitTestUsuario.AdicionarUsuario(nome, login, email, senha, idade, celular);

            Assert.True(result);
            _mockIUsuario.Verify(m => m.AdicionarUsuario(nome, login, email, senha, idade, celular), Times.Once);
        }

        [Fact]
        public async Task BuscarUsuarios_CallsBuscarUsuarioOnIUsuario()
        {
            var usuarios = new List<ApplicationUser> { new ApplicationUser() };
            _mockIUsuario.Setup(m => m.BuscarUsuario()).ReturnsAsync(usuarios);

            var result = await _unitTestUsuario.BuscarUsuarios();

            Assert.Equal(usuarios, result);
        }

        [Fact]
        public async Task ExisteUsuario_ValidCredentials_CallsExisteUsuarioOnIUsuario()
        {
            string email = "teste@email.com";
            string senha = "senha123";
            _mockIUsuario.Setup(m => m.ExisteUsuario(email, senha)).ReturnsAsync(true);

            var result = await _unitTestUsuario.ExisteUsuario(email, senha);

            Assert.True(result);
            _mockIUsuario.Verify(m => m.ExisteUsuario(email, senha), Times.Once);
        }

        [Fact]
        public async Task Logar_ValidCredentials_CallsLogarOnIUsuario()
        {
            string login = "teste.login";
            string senha = "senha123";
            var user = new ApplicationUser();
            _mockIUsuario.Setup(m => m.Logar(login, senha)).ReturnsAsync(user);

            var result = await _unitTestUsuario.Logar(login, senha);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task RetornarIdUsuario_ValidEmail_CallsRetornarIdUsuarioOnIUsuario()
        {
            string email = "teste@email.com";
            string expectedId = "user123";
            _mockIUsuario.Setup(m => m.RetornarIdUsuario(email)).ReturnsAsync(expectedId);

            var result = await _unitTestUsuario.RetornarIdUsuario(email);

            Assert.Equal(expectedId, result);
        }
    }
}
