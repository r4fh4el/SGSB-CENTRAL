using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoTipoEdificacaoBarragem : IAplicacaoTipoEdificacaoBarragem
    {
        // INTERFACE DOMINIO
        ITipoEdificacaoBarragem _ITipoEdificacaoBarragem;

        // INTERFACE SERVICO
        IServicoTipoEdificacaoBarragem _IServicoTipoEdificacaoBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTipoEdificacaoBarragem(ITipoEdificacaoBarragem iTipoEdificacaoBarragem, IServicoTipoEdificacaoBarragem iServicoTipoEdificacaoBarragem)
        {
            _ITipoEdificacaoBarragem = iTipoEdificacaoBarragem;
            _IServicoTipoEdificacaoBarragem = iServicoTipoEdificacaoBarragem;
        }

        public async Task Adicionar(TipoEdificacaoBarragem Objeto)
        {
           await _ITipoEdificacaoBarragem.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEdificacaoBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacaoBarragem.AdicionarTipoEdificacaoBarragem(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoEdificacaoBarragem Objeto)
        {
            await _ITipoEdificacaoBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoEdificacaoBarragem(TipoEdificacaoBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEdificacaoBarragem.AtualizaTipoEdificacaoBarragem(tipoMaterialBarragem);
        }

        public async Task<TipoEdificacaoBarragem> BuscarPorId(int Id)
        {
            return await _ITipoEdificacaoBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(TipoEdificacaoBarragem Objeto)
        {
            await _ITipoEdificacaoBarragem.Excluir(Objeto);
        }

        public async  Task<List<TipoEdificacaoBarragem>> Listar()
        {
            return await _ITipoEdificacaoBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem()
        {
            return await _IServicoTipoEdificacaoBarragem.ListarTipoEdificacaoBarragem();
        }

        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem)
        {
            return await _IServicoTipoEdificacaoBarragem.ListarTipoEdificacaoBarragemBarragemId(idBarragem);
        }
    }
}
