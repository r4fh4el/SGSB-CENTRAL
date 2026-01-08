using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testes
{
    public class UnitTestCondicaoFundacao : IAplicacaoCondicaoFundacao
    {
        // INTERFACE DOMINIO
        private readonly ICondicaoFundacao _iCondicaoFundacao;

        // INTERFACE SERVICO
        private readonly IServicoCondicaoFundacao _iServicoCondicaoFundacao;

        // CONSTRUTOR COM INJEÇÃO DE DEPENDÊNCIA
        public UnitTestCondicaoFundacao(ICondicaoFundacao iCondicaoFundacao, IServicoCondicaoFundacao iServicoCondicaoFundacao)
        {
            _iCondicaoFundacao = iCondicaoFundacao ?? throw new ArgumentNullException(nameof(iCondicaoFundacao));
            _iServicoCondicaoFundacao = iServicoCondicaoFundacao ?? throw new ArgumentNullException(nameof(iServicoCondicaoFundacao));
        }

        public async Task Adicionar(CondicaoFundacao objeto)
        {
            await _iCondicaoFundacao.Adicionar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AdicionarCondicaoFundacao(CondicaoFundacao condicaoFundacao)
        {
            await _iServicoCondicaoFundacao.AdicionarCondicaoFundacao(condicaoFundacao);
        }

        public async Task Atualizar(CondicaoFundacao objeto)
        {
            await _iCondicaoFundacao.Atualizar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AtualizaCondicaoFundacao(CondicaoFundacao condicaoFundacao)
        {
            await _iServicoCondicaoFundacao.AtualizaCondicaoFundacao(condicaoFundacao);
        }

        public async Task<CondicaoFundacao> BuscarPorId(int id)
        {
            return await _iCondicaoFundacao.BuscarPorId(id);
        }

        public async Task Excluir(CondicaoFundacao objeto)
        {
            await _iCondicaoFundacao.Excluir(objeto);
        }

        public async Task<List<CondicaoFundacao>> Listar()
        {
            return await _iCondicaoFundacao.Listar();
        }

        public Task<List<CondicaoFundacao>> ListarCondicaoFundacao()
        {
            throw new NotImplementedException();
        }
    }

}
