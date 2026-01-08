using Aplicacao.Interfaces;
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
    public class AplicacaoTempoConcentracao : IAplicacaoTempoConcentracao
    {
        // INTERFACE DOMINIO
        ITempoConcentracao _ITempoConcentracao;

        // INTERFACE SERVICO
        IServicoTempoConcentracao _IServicoTempoConcentracao;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTempoConcentracao(ITempoConcentracao iTempoConcentracao, IServicoTempoConcentracao iServicoTempoConcentracao)
        {
            _ITempoConcentracao = iTempoConcentracao;
            _IServicoTempoConcentracao = iServicoTempoConcentracao;
        }

        public async Task Adicionar(TempoConcentracao Objeto)
        {
           await _ITempoConcentracao.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TempoConcentracao tempoConcentracao)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTempoConcentracao(TempoConcentracao tempoConcentracao)
        {
            await _IServicoTempoConcentracao.AdicionarTempoConcentracao(tempoConcentracao);
        }

        public Task AtualizaBarragem(TempoConcentracao tempoConcentracao)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TempoConcentracao Objeto)
        {
            await _ITempoConcentracao.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTempoConcentracao(TempoConcentracao tempoConcentracao)
        {
            await _IServicoTempoConcentracao.AtualizaTempoConcentracao(tempoConcentracao);
        }

        public async Task<TempoConcentracao> BuscarPorId(int Id)
        {
            return await _ITempoConcentracao.BuscarPorId(Id);
        }

        public async Task Excluir(TempoConcentracao Objeto)
        {
            await _ITempoConcentracao.Excluir(Objeto);
        }

        public async  Task<List<TempoConcentracao>> Listar()
        {
            return await _ITempoConcentracao.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TempoConcentracao>> ListarTempoConcentracao()
        {
            return await _IServicoTempoConcentracao.ListarTempoConcentracao();
        }
    }
}
