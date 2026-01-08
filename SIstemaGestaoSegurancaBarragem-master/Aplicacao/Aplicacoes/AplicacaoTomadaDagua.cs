using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
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
    public class AplicacaoTomadaDagua : IAplicacaoTomadaDagua
    {
        // INTERFACE DOMINIO
        ITomadaDagua _ITomadaDagua;

        // INTERFACE SERVICO
        IServicoTomadaDagua _IServicoTomadaDagua;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTomadaDagua(ITomadaDagua iTomadaDagua, IServicoTomadaDagua iServicoTomadaDagua)
        {
            _ITomadaDagua = iTomadaDagua;
            _IServicoTomadaDagua = iServicoTomadaDagua;
        }

        public async Task Adicionar(TomadaDagua Objeto)
        {
           await _ITomadaDagua.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTomadaDagua(TomadaDagua tomadaDagua)
        {
            await _IServicoTomadaDagua.AdicionarTomadaDagua(tomadaDagua);
        }

        public async Task Atualizar(TomadaDagua Objeto)
        {
            await _ITomadaDagua.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTomadaDagua(TomadaDagua tomadaDagua)
        {
            await _IServicoTomadaDagua.AtualizaTomadaDagua(tomadaDagua);
        }

        public async Task<TomadaDagua> BuscarPorId(int Id)
        {
            return await _ITomadaDagua.BuscarPorId(Id);
        }

        public async Task Excluir(TomadaDagua Objeto)
        {
            await _ITomadaDagua.Excluir(Objeto);
        }

        public async  Task<List<TomadaDagua>> Listar()
        {
            return await _ITomadaDagua.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TomadaDagua>> ListarTomadaDagua()
        {
            return await _IServicoTomadaDagua.ListarTomadaDagua();
        }

        public Task Adicionar(AplicacaoTomadaDagua Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoTomadaDagua Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoTomadaDagua Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem)
        {
            return await _IServicoTomadaDagua.ListarTomadaDaguaBarragemId(idBarragem);
        }

    }
}
