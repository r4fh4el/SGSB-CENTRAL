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
    public class AplicacaoTipoEstruturaBarragem : IAplicacaoTipoEstruturaBarragem
    {
        // INTERFACE DOMINIO
        ITipoEstruturaBarragem _ITipoEstruturaBarragem;

        // INTERFACE SERVICO
        IServicoTipoEstruturaBarragem _IServicoTipoEstruturaBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTipoEstruturaBarragem(ITipoEstruturaBarragem iTipoEstruturaBarragem, IServicoTipoEstruturaBarragem iServicoTipoEstruturaBarragem)
        {
            _ITipoEstruturaBarragem = iTipoEstruturaBarragem;
            _IServicoTipoEstruturaBarragem = iServicoTipoEstruturaBarragem;
        }

        public async Task Adicionar(TipoEstruturaBarragem Objeto)
        {
           await _ITipoEstruturaBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEstruturaBarragem(TipoEstruturaBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEstruturaBarragem.AdicionarTipoEstruturaBarragem(tipoMaterialBarragem);
        }

        public async Task Atualizar(TipoEstruturaBarragem Objeto)
        {
            await _ITipoEstruturaBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizarTipoEstruturaBarragem(TipoEstruturaBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoEstruturaBarragem.AtualizarTipoEstruturaBarragem(tipoMaterialBarragem);
        }

        public async Task<TipoEstruturaBarragem> BuscarPorId(int Id)
        {
            return await _ITipoEstruturaBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(TipoEstruturaBarragem Objeto)
        {
            await _ITipoEstruturaBarragem.Excluir(Objeto);
        }

        public async  Task<List<TipoEstruturaBarragem>> Listar()
        {
            return await _ITipoEstruturaBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem()
        {
            return await _IServicoTipoEstruturaBarragem.ListarTipoEstruturaBarragem();
        }
    }
}
