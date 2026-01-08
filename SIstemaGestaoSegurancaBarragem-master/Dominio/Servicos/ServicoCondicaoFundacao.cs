using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoCondicaoFundacao : IServicoCondicaoFundacao
    {
        private readonly ICondicaoFundacao _ICondicaoFundacao;

        public ServicoCondicaoFundacao(ICondicaoFundacao condicaoFundacao) 
        {
            _ICondicaoFundacao = condicaoFundacao;
        }

        public async Task AdicionarCondicaoFundacao(CondicaoFundacao condicaoFundacao)
        {
            var validarNome = condicaoFundacao.ValidadePropriedadeSring(condicaoFundacao.Nome , "Nome");

            if (validarNome)
            {
                condicaoFundacao.DataAlteracao = DateTime.Now;
                condicaoFundacao.DataCadastro = DateTime.Now;
               await _ICondicaoFundacao.Adicionar(condicaoFundacao);
            }
        }

        public async Task AtualizaCondicaoFundacao(CondicaoFundacao condicaoFundacao)
        {
            var validarNome = condicaoFundacao.ValidadePropriedadeSring(condicaoFundacao.Nome, "Nome");

            if (validarNome)
            {
                condicaoFundacao.DataAlteracao = DateTime.Now;
                condicaoFundacao.DataCadastro = condicaoFundacao.DataCadastro;
                await _ICondicaoFundacao.Atualizar(condicaoFundacao);
        }
        }

        public async Task<List<CondicaoFundacao>> ListarCondicaoFundacao()
        {
            return  await _ICondicaoFundacao.ListarCondicaoFundacao(n => n.Nome != null);
        }
    }
}
