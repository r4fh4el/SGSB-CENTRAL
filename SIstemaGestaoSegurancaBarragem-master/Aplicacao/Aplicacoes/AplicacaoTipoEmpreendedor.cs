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
    public class AplicacaoTipoEmpreendedor : IAplicacaoTipoEmpreendedor
    {
        // INTERFACE DOMINIO
        ITipoEmpreendedor _ITipoEmpreendedor;

        // INTERFACE SERVICO
        IServicoTipoEmpreendedor _IServicoTipoEmpreendedor;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTipoEmpreendedor(ITipoEmpreendedor iTipoEmpreendedor, IServicoTipoEmpreendedor iServicoTipoEmpreendedor)
        {
            _ITipoEmpreendedor = iTipoEmpreendedor;
            _IServicoTipoEmpreendedor = iServicoTipoEmpreendedor;
        }

        public async Task Adicionar(TipoEmpreendedor Objeto)
        {
           await _ITipoEmpreendedor.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TipoEmpreendedor tipoEmpreendedor)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor)
        {
            await _IServicoTipoEmpreendedor.AdicionarTipoEmpreendedor(tipoEmpreendedor);
        }

        public Task AtualizaBarragem(TipoEmpreendedor tipoEmpreendedor)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TipoEmpreendedor Objeto)
        {
            await _ITipoEmpreendedor.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor)
        {
            await _IServicoTipoEmpreendedor.AtualizaTipoEmpreendedor(tipoEmpreendedor);
        }

        public async Task<TipoEmpreendedor> BuscarPorId(int Id)
        {
            return await _ITipoEmpreendedor.BuscarPorId(Id);
        }

        public async Task Excluir(TipoEmpreendedor Objeto)
        {
            await _ITipoEmpreendedor.Excluir(Objeto);
        }

        public async  Task<List<TipoEmpreendedor>> Listar()
        {
            return await _ITipoEmpreendedor.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor()
        {
            return await _IServicoTipoEmpreendedor.ListarTipoEmpreendedor();
        }
    }
}
