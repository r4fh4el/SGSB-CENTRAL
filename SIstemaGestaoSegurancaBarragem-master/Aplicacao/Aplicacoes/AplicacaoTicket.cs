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
    public class AplicacaoTicket : IAplicacaoTicket
    {
        // INTERFACE DOMINIO
        ITicket _ITicket;

        // INTERFACE SERVICO
        IServicoTicket _IServicoTicket;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTicket(ITicket iTicket, IServicoTicket iServicoTicket)
        {
            _ITicket = iTicket;
            _IServicoTicket = iServicoTicket;
        }

        public async Task Adicionar(Ticket Objeto)
        {
           await _ITicket.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(Ticket tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTicket(Ticket tipoMaterialBarragem)
        {
            await _IServicoTicket.AdicionarTicket(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(Ticket tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(Ticket Objeto)
        {
            await _ITicket.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTicket(Ticket tipoMaterialBarragem)
        {
            await _IServicoTicket.AtualizaTicket(tipoMaterialBarragem);
        }

        public async Task<Ticket> BuscarPorId(int Id)
        {
            return await _ITicket.BuscarPorId(Id);
        }

        public async Task Excluir(Ticket Objeto)
        {
            await _ITicket.Excluir(Objeto);
        }

        public async  Task<List<Ticket>> Listar()
        {
            return await _ITicket.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Ticket>> ListarTicket()
        {
            return await _IServicoTicket.ListarTicket();
        }
    }
}
