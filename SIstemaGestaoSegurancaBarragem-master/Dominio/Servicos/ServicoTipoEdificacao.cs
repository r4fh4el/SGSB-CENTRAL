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
    public class ServicoTipoEdificacao : IServicoTipoEdificacao
    {
        private readonly ITipoEdificacao _ITipoEdificacao;

        public ServicoTipoEdificacao(ITipoEdificacao tipoEdificacao) 
        {
            _ITipoEdificacao = tipoEdificacao;
        }

        public async Task AdicionarTipoEdificacao(TipoEdificacao tipoEdificacao)
        {
            var validarNome = tipoEdificacao.ValidadePropriedadeSring(tipoEdificacao.Nome , "Nome");

            if (validarNome)
            {
                tipoEdificacao.DataAlteracao = DateTime.Now;
                tipoEdificacao.DataCadastro = DateTime.Now;
               await _ITipoEdificacao.Adicionar(tipoEdificacao);
            }
        }

        public async Task AtualizaTipoEdificacao(TipoEdificacao tipoEdificacao)
        {
            var validarNome = tipoEdificacao.ValidadePropriedadeSring(tipoEdificacao.Nome, "Nome");

            if (validarNome)
            {
                tipoEdificacao.DataAlteracao = DateTime.Now;
                tipoEdificacao.DataCadastro = tipoEdificacao.DataCadastro;
                await _ITipoEdificacao.Atualizar(tipoEdificacao);
        }
        }

        public async Task<List<TipoEdificacao>> ListarTipoEdificacao()
        {
            return  await _ITipoEdificacao.ListarTipoEdificacao(n => n.Nome != null);
        }



     
    }
}
