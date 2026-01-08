using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUsuario 
    {
        Task<bool> AdicionarUsuario(string nome,string login, string email, string senha, int idade, string celular);
        Task<bool> ExisteUsuario(string email, string senha);
        Task<List<ApplicationUser>> BuscarUsuario();

        Task<string> RetornarIdUsuario(string Email);
		Task<ApplicationUser> Logar(string usuario, string senha);
	}
}
