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
    public class AplicacaoTipoMaterialBarragem : IAplicacaoTipoMaterialBarragem
    {
        // INTERFACE DOMINIO
        ITipoMaterialBarragem _ITipoMaterialBarragem;

        // INTERFACE SERVICO
        IServicoTipoMaterialBarragem _IServicoTipoMaterialBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTipoMaterialBarragem(ITipoMaterialBarragem iTipoMaterialBarragem, IServicoTipoMaterialBarragem iServicoTipoMaterialBarragem)
        {
            _ITipoMaterialBarragem = iTipoMaterialBarragem;
            _IServicoTipoMaterialBarragem = iServicoTipoMaterialBarragem;
        }

        public async Task Adicionar(TipoMaterialBarragem Objeto)
        {
           await _ITipoMaterialBarragem.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoMaterialBarragem.AdicionarTipoMaterialBarragem(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoMaterialBarragem Objeto)
        {
            await _ITipoMaterialBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            await _IServicoTipoMaterialBarragem.AtualizaTipoMaterialBarragem(tipoMaterialBarragem);
        }

        public async Task<TipoMaterialBarragem> BuscarPorId(int Id)
        {
            return await _ITipoMaterialBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(TipoMaterialBarragem Objeto)
        {
            await _ITipoMaterialBarragem.Excluir(Objeto);
        }

        public async  Task<List<TipoMaterialBarragem>> Listar()
        {
            return await _ITipoMaterialBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem()
        {
            return await _IServicoTipoMaterialBarragem.ListarTipoMaterialBarragem();
        }
    }
}
