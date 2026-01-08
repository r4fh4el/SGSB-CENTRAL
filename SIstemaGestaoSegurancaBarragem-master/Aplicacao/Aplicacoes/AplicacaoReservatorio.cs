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
    public class AplicacaoReservatorio : IAplicacaoReservatorio
    {
        // INTERFACE DOMINIO
        IReservatorio _IReservatorio;

        // INTERFACE SERVICO
        IServicoReservatorio _IServicoReservatorio;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoReservatorio(IReservatorio iReservatorio, IServicoReservatorio iServicoReservatorio)
        {
            _IReservatorio = iReservatorio;
            _IServicoReservatorio = iServicoReservatorio;
        }

        public async Task Adicionar(Reservatorio Objeto)
        {
           await _IReservatorio.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarReservatorio(Reservatorio reservatorio)
        {
            await _IServicoReservatorio.AdicionarReservatorio(reservatorio);
        }

        public async Task Atualizar(Reservatorio Objeto)
        {
            await _IReservatorio.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaReservatorio(Reservatorio reservatorio)
        {
            await _IServicoReservatorio.AtualizaReservatorio(reservatorio);
        }

        public async Task<Reservatorio> BuscarPorId(int Id)
        {
            return await _IReservatorio.BuscarPorId(Id);
        }

        public async Task Excluir(Reservatorio Objeto)
        {
            await _IReservatorio.Excluir(Objeto);
        }

        public async  Task<List<Reservatorio>> Listar()
        {
            return await _IReservatorio.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Reservatorio>> ListarReservatorio()
        {
            return await _IServicoReservatorio.ListarReservatorio();
        }

        public Task Adicionar(AplicacaoReservatorio Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoReservatorio Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoReservatorio Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reservatorio>> ListarReservatorioBarragemId(int idBarragem)
        {
            return await _IServicoReservatorio.ListarReservatorioBarragemId(idBarragem);
        }
    }
}
