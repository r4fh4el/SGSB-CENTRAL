using Aplicacao.Interfaces;
using Entidades.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IAplicacaoUsuario _IAplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UsuarioController(IAplicacaoUsuario IAplicacaoUsuario, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _IAplicacaoUsuario = IAplicacaoUsuario;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("/API/BuscarUsuarios")]
        public async Task<List<ApplicationUser>> BuscarUsuarios() 
        {
            return await _IAplicacaoUsuario.BuscarUsuarios();
        }

		[AllowAnonymous]
		[Produces("application/json")]
		[HttpPost("/API/Logar")]
		public async Task<ApplicationUser> Logar(string login, string senha)
        {
           
			return await _IAplicacaoUsuario.Logar(login, senha);
		}

		[AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/API/CriarToken")]
        public async Task<IActionResult> CriarToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }
            var resultado = await _IAplicacaoUsuario.ExisteUsuario(login.email, login.senha);

            if (!resultado)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-SGSB159357"))
                    .AddSubject("SGSB - Sistema de Gestão Barragem")
                    .AddIssuer("SGSB.Securiry.Bearer")
                    .AddAudience("SGSB.Securiry.Bearer")
                    .AddClaim("UsuarioApiNumero", "1")
                    .AddExpiry(60) //// 60 miinutos
                    .Builder();

                return Ok(token.value);

            }
            else
            {
                return Unauthorized();
            }
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/API/AdicionaUsuario")]
        public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Ok("E-mail ou senha incorretos");
            }
            var resultado = await _IAplicacaoUsuario.AdicionarUsuario(login.nome, login.login , login.email, login.senha, login.idade, login.celular);

            if (resultado)
            {
                return Ok("Usuário adicionado com sucesso!");
            }
            else
            {
                return Ok("Erro ao adicionar o usuário.");
            }


        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/API/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            // Tentar login com email primeiro, depois com UserName
            var resultado = await _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);
            
            // Se falhar, tentar com UserName (login)
            if (!resultado.Succeeded && !string.IsNullOrWhiteSpace(login.login))
            {
                resultado = await _signInManager.PasswordSignInAsync(login.login, login.senha, false, lockoutOnFailure: false);
            }

            if (resultado.Succeeded)
            {
                var idUsuario = await _IAplicacaoUsuario.RetornarIdUsuario(login.email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-SGSB159357"))
                    .AddSubject("SGSB - Sistema de Gestão Barragem")
                    .AddIssuer("SGSB.Securiry.Bearer")
                    .AddAudience("SGSB.Securiry.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(60) //// 60 miinutos
                    .Builder();

                return Ok(token.value);

            }
            else
            {
                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarUsuarioIdentity")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Ok("Falta algumas informações");

            var user = new ApplicationUser
            {
                Email = login.email,
                Nome = login.nome,
                UserName = login.login,
                Celular = login.celular,
                Tipo = Entidades.Enums.TipoUsuario.Comun

            };

            var resultado = await _userManager.CreateAsync(user, login.senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            //// Geração de Confirmação casp precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //// Retorno email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultadoEmail = await _userManager.ConfirmEmailAsync(user, code);
            var StatusMessage = resultadoEmail.Succeeded;

            if (resultadoEmail.Succeeded)
            {
                return Ok("Usuário adicionado com sucesso!");
            }
            else
            {
                return Ok("Erro ao confirmar o usuário");
            }
        }

    }
}
