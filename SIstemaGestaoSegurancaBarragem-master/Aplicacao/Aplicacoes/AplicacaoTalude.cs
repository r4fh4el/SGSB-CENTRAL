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
    public class AplicacaoTalude : IAplicacaoTalude
    {
        // INTERFACE DOMINIO
        ITalude _ITalude;

        // INTERFACE SERVICO
        IServicoTalude _IServicoTalude;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTalude(ITalude iTalude, IServicoTalude iServicoTalude)
        {
            _ITalude = iTalude;
            _IServicoTalude = iServicoTalude;
        }

        public async Task Adicionar(Talude Objeto)
        {
           await _ITalude.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTalude(Talude talude)
        {
            await _IServicoTalude.AdicionarTalude(talude);
        }

        public async Task Atualizar(Talude Objeto)
        {
            await _ITalude.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTalude(Talude talude)
        {
            await _IServicoTalude.AtualizaTalude(talude);
        }

        public async Task<Talude> BuscarPorId(int Id)
        {
            return await _ITalude.BuscarPorId(Id);
        }

        public async Task Excluir(Talude Objeto)
        {
            await _ITalude.Excluir(Objeto);
        }

        public async  Task<List<Talude>> Listar()
        {
            return await _ITalude.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Talude>> ListarTalude()
        {
            return await _IServicoTalude.ListarTalude();
        }

        public Task Adicionar(AplicacaoTalude Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoTalude Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoTalude Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Talude>> ListarTaludeBarragemId(int idBarragem)
        {
            return await _IServicoTalude.ListarTaludeBarragemId(idBarragem);
        }
    }
}
