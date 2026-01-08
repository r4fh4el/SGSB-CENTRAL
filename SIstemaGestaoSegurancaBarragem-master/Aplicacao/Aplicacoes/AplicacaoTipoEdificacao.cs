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
    public class AplicacaoTipoEdificacao : IAplicacaoTipoEdificacao
    {
        // INTERFACE DOMINIO
        ITipoEdificacao _ITipoEdificacao;

        // INTERFACE SERVICO
        IServicoTipoEdificacao _IServicoTipoEdificacao;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTipoEdificacao(ITipoEdificacao iTipoEdificacao, IServicoTipoEdificacao iServicoTipoEdificacao)
        {
            _ITipoEdificacao = iTipoEdificacao;
            _IServicoTipoEdificacao = iServicoTipoEdificacao;
        }

        public async Task Adicionar(TipoEdificacao Objeto)
        {
           await _ITipoEdificacao.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TipoEdificacao tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEdificacao(TipoEdificacao tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacao.AdicionarTipoEdificacao(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(TipoEdificacao tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoEdificacao Objeto)
        {
            await _ITipoEdificacao.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoEdificacao(TipoEdificacao tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacao.AtualizaTipoEdificacao(tipoMaterialBarragem);
        }

        public async Task<TipoEdificacao> BuscarPorId(int Id)
        {
            return await _ITipoEdificacao.BuscarPorId(Id);
        }

        public async Task Excluir(TipoEdificacao Objeto)
        {
            await _ITipoEdificacao.Excluir(Objeto);
        }

        public async  Task<List<TipoEdificacao>> Listar()
        {
            return await _ITipoEdificacao.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEdificacao>> ListarTipoEdificacao()
        {
            return await _IServicoTipoEdificacao.ListarTipoEdificacao();
        }
    }
}
