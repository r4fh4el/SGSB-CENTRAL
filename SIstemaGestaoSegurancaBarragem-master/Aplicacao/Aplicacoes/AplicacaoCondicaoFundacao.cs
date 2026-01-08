using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoCondicaoFundacao : IAplicacaoCondicaoFundacao
    {
        // INTERFACE DOMINIO
        ICondicaoFundacao _ICondicaoFundacao;

        // INTERFACE SERVICO
        IServicoCondicaoFundacao _IServicoCondicaoFundacao;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoCondicaoFundacao(ICondicaoFundacao iCondicaoFundacao, IServicoCondicaoFundacao iServicoCondicaoFundacao)
        {
            _ICondicaoFundacao = iCondicaoFundacao;
            _IServicoCondicaoFundacao = iServicoCondicaoFundacao;
        }

        public async Task Adicionar(CondicaoFundacao Objeto)
        {
           await _ICondicaoFundacao.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarCondicaoFundacao(CondicaoFundacao condicaoFundacao)
        {
            await _IServicoCondicaoFundacao.AdicionarCondicaoFundacao(condicaoFundacao);
        }

        public async Task Atualizar(CondicaoFundacao Objeto)
        {
            await _ICondicaoFundacao.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaCondicaoFundacao(CondicaoFundacao condicaoFundacao)
        {
            await _IServicoCondicaoFundacao.AtualizaCondicaoFundacao(condicaoFundacao);
        }

        public async Task<CondicaoFundacao> BuscarPorId(int Id)
        {
            return await _ICondicaoFundacao.BuscarPorId(Id);
        }

        public async Task Excluir(CondicaoFundacao Objeto)
        {
            await _ICondicaoFundacao.Excluir(Objeto);
        }

        public async  Task<List<CondicaoFundacao>> Listar()
        {
            return await _ICondicaoFundacao.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<CondicaoFundacao>> ListarCondicaoFundacao()
        {
            return await _IServicoCondicaoFundacao.ListarCondicaoFundacao();
        }

        public Task Adicionar(AplicacaoCondicaoFundacao Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoCondicaoFundacao Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoCondicaoFundacao Objeto)
        {
            throw new NotImplementedException();
        }

     
    }
}
