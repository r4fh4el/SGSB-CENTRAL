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
    public class AplicacaoVazaPico : IAplicacaoVazaPico
    {
        // INTERFACE DOMINIO
        IVazaPico _IVazaPico;

        // INTERFACE SERVICO
        IServicoVazaPico _IServicoVazaPico;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoVazaPico(IVazaPico iVazaPico, IServicoVazaPico iServicoVazaPico)
        {
            _IVazaPico = iVazaPico;
            _IServicoVazaPico = iServicoVazaPico;
        }

        public async Task Adicionar(VazaPico Objeto)
        {
           await _IVazaPico.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(VazaPico vazaPico)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarVazaPico(VazaPico vazaPico)
        {
            await _IServicoVazaPico.AdicionarVazaPico(vazaPico);
        }

        public Task AtualizaBarragem(VazaPico vazaPico)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(VazaPico Objeto)
        {
            await _IVazaPico.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaVazaPico(VazaPico vazaPico)
        {
            await _IServicoVazaPico.AtualizaVazaPico(vazaPico);
        }

        public async Task<VazaPico> BuscarPorId(int Id)
        {
            return await _IVazaPico.BuscarPorId(Id);
        }

        public async Task Excluir(VazaPico Objeto)
        {
            await _IVazaPico.Excluir(Objeto);
        }

        public async  Task<List<VazaPico>> Listar()
        {
            return await _IVazaPico.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<VazaPico>> ListarVazaPico()
        {
            return await _IServicoVazaPico.ListarVazaPico();
        }
        
        public async Task<VazaPico> VerificarBarragemId(int BarragemId)
        { 
            return await _IServicoVazaPico.VerificarBarragemId(BarragemId);
        }
    }
}
