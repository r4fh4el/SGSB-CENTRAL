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
    public class AplicacaoRotaFuga : IAplicacaoRotaFuga
    {
        // INTERFACE DOMINIO
        IRotaFuga _IRotaFuga;

        // INTERFACE SERVICO
        IServicoRotaFuga _IServicoRotaFuga;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoRotaFuga(IRotaFuga iRotaFuga, IServicoRotaFuga iServicoRotaFuga)
        {
            _IRotaFuga = iRotaFuga;
            _IServicoRotaFuga = iServicoRotaFuga;
        }

        public async Task Adicionar(RotaFuga Objeto)
        {
           await _IRotaFuga.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(RotaFuga RotaFuga)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarRotaFuga(RotaFuga RotaFuga)
        {
            await _IServicoRotaFuga.AdicionarRotaFuga(RotaFuga);
        }

        public Task AtualizaBarragem(RotaFuga RotaFuga)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(RotaFuga Objeto)
        {
            await _IRotaFuga.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaRotaFuga(RotaFuga RotaFuga)
        {
            await _IServicoRotaFuga.AtualizaRotaFuga(RotaFuga);
        }

        public async Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id)
        {
            return await _IRotaFuga.BuscarListPorIdRotaFuga(id);
        }

        public async Task<RotaFuga> BuscarPorId(int Id)
        {
            return await _IRotaFuga.BuscarPorId(Id);
        }

        public async Task Excluir(RotaFuga Objeto)
        {
            await _IRotaFuga.Excluir(Objeto);
        }

        public async  Task<List<RotaFuga>> Listar()
        {
            return await _IRotaFuga.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<RotaFuga>> ListarRotaFuga()
        {
            return await _IServicoRotaFuga.ListarRotaFuga();
        }
    }
}
