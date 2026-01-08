using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoUsuario
    {
        Task<bool> AdicionarUsuario(string nome,string login,string email, string senha, int idade, string celular);
        Task<List<ApplicationUser>> BuscarUsuarios();
        Task<bool> ExisteUsuario(string email, string senha);
		Task<ApplicationUser> Logar(string usuario, string senha);
		Task<string> RetornarIdUsuario(string email);
    }
}
