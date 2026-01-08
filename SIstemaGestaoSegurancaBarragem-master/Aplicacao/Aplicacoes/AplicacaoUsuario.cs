using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoUsuario : IAplicacaoUsuario
    {

        // INTERFACE DOMINIOCriarTokenIdentity
        IUsuario _IUsuario;


        public AplicacaoUsuario(IUsuario usuario) 
        { 
            _IUsuario = usuario;
         }

        public async Task<bool> AdicionarUsuario(string nome, string login,  string email, string senha, int idade, string celular)
        {
           return await _IUsuario.AdicionarUsuario(nome, login , email,  senha,  idade,  celular);
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
}
