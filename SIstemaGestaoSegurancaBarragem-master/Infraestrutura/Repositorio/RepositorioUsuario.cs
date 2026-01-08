using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
	public class RepositorioUsuario : RepositorioGenerico<ApplicationUser>, IUsuario
	{
		private readonly DbContextOptions<Contexto> _optionsbuilder;
		public RepositorioUsuario()
		{
			_optionsbuilder = new DbContextOptions<Contexto>();
		}
		public async Task<List<ApplicationUser>> BuscarUsuario()
		{

			using (var data = new Contexto(_optionsbuilder))
			{
				return await data.ApplicationUser.AsNoTracking().ToListAsync();

			}

		}
		public async Task<bool> AdicionarUsuario(string nome, string login, string email, string senha, int idade, string celular)
		{
			try
			{
				using (var data = new Contexto(_optionsbuilder))
				{
					await data.ApplicationUser.AddAsync(new ApplicationUser
					{
						Nome = nome,
						UserName = login,
						Email = email,
						Idade = idade,
						PasswordHash = senha,
						Celular = celular,
						Tipo = Entidades.Enums.TipoUsuario.Comun
					});

					await data.SaveChangesAsync();
				}
			}
			catch (Exception)
			{

				return false;
			}
			return true;
		}

		public async Task<bool> ExisteUsuario(string email, string senha)
		{
			try
			{
				using (var data = new Contexto(_optionsbuilder))
				{
					return await data.ApplicationUser.Where(x => x.Email.Equals(email) && x.PasswordHash.Equals(senha)).AsNoTracking().AnyAsync();
				}
			}
			catch (Exception)
			{

				return false;
			}

		}


		public async Task<ApplicationUser> Logar(string login, string senha)
		{
		


			using (var data = new Contexto(_optionsbuilder))
			{
				var User  = await data.ApplicationUser.Where(x => x.Email.Equals(login)).AsNoTracking().FirstOrDefaultAsync();

                /////
                ///

                // Example registration process
                var passwordHasher = new PasswordHasher<ApplicationUser>();

                // Assuming 'login' is the user's email or username and 'senha' is the user's password
                string hashedPassword = passwordHasher.HashPassword(new ApplicationUser(), senha);

                // Store 'hashedPassword' in the database along with other user details

					// Example login process
					// Retrieve the hashed password from the database based on the user's login (email or username)
                string storedHashedPassword = "hashedPasswordFromDatabase";

                // Assuming 'providedPassword' is the password entered by the user during login
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(new ApplicationUser(), User.PasswordHash, senha);

                switch (passwordVerificationResult)
                {
                    case PasswordVerificationResult.Success:
                        // Password is correct
     
						return User;
                        break;

                    case PasswordVerificationResult.Failed:
                        // Password is incorrect
                        Console.WriteLine("Login failed. Invalid password.");
                        return null;
                        break;

                    case PasswordVerificationResult.SuccessRehashNeeded:
                        // This result indicates that the password was hashed using an older hashing algorithm
                        // It might be a good idea to rehash and update the stored hash in the database
                        Console.WriteLine("Login successful! Rehash needed.");
                        break;

                    default:
                        // Handle other cases if necessary
                        break;
                }
                return null;
            }
		}

		public async Task<string> RetornarIdUsuario(string Email)
		{
			try
			{
				using (var data = new Contexto(_optionsbuilder))
				{
					var usuario = await data.ApplicationUser.Where(x => x.Email.Equals(Email))
						.AsNoTracking()
						.FirstOrDefaultAsync();

					return usuario.Id;
				}
			}
			catch (Exception)
			{

				return string.Empty;
			}

		}
	}
}
