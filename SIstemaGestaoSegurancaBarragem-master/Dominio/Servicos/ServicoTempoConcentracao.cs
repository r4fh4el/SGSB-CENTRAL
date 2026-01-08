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
    public class ServicoTempoConcentracao : IServicoTempoConcentracao
    {
        private readonly ITempoConcentracao _ITempoConcentracao;

        public ServicoTempoConcentracao(ITempoConcentracao tempoConcentracao) 
        {
            _ITempoConcentracao = tempoConcentracao;
        }

        public async Task AdicionarTempoConcentracao(TempoConcentracao tempoConcentracao)
        {
          
                tempoConcentracao.DataAlteracao = DateTime.Now;
                tempoConcentracao.DataCadastro = DateTime.Now;
               await _ITempoConcentracao.Adicionar(tempoConcentracao);
         
        }

        public async Task AtualizaTempoConcentracao(TempoConcentracao tempoConcentracao)
        {
         
           
                tempoConcentracao.DataAlteracao = DateTime.Now;
                tempoConcentracao.DataCadastro = tempoConcentracao.DataCadastro;
                await _ITempoConcentracao.Atualizar(tempoConcentracao);
       
        }

        public async Task<List<TempoConcentracao>> ListarTempoConcentracao()
        {
            return  await _ITempoConcentracao.Listar();
        }
    }
}
