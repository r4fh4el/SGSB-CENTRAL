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
    public class AplicacaoTempoRuptura : IAplicacaoTempoRuptura
    {
        // INTERFACE DOMINIO
        ITempoRuptura _ITempoRuptura;

        // INTERFACE SERVICO
        IServicoTempoRuptura _IServicoTempoRuptura;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoTempoRuptura(ITempoRuptura iTempoRuptura, IServicoTempoRuptura iServicoTempoRuptura)
        {
            _ITempoRuptura = iTempoRuptura;
            _IServicoTempoRuptura = iServicoTempoRuptura;
        }

        public async Task Adicionar(TempoRuptura Objeto)
        {
           await _ITempoRuptura.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(TempoRuptura TempoRuptura)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarTempoRuptura(TempoRuptura TempoRuptura)
        {
            await _IServicoTempoRuptura.AdicionarTempoRuptura(TempoRuptura);
        }

        public Task AtualizaBarragem(TempoRuptura TempoRuptura)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(TempoRuptura Objeto)
        {
            await _ITempoRuptura.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaTempoRuptura(TempoRuptura TempoRuptura)
        {
            await _IServicoTempoRuptura.AtualizaTempoRuptura(TempoRuptura);
        }

        public async Task<TempoRuptura> BuscarPorId(int Id)
        {
            return await _ITempoRuptura.BuscarPorId(Id);
        }

        public async Task Excluir(TempoRuptura Objeto)
        {
            await _ITempoRuptura.Excluir(Objeto);
        }

        public async  Task<List<TempoRuptura>> Listar()
        {
            return await _ITempoRuptura.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<TempoRuptura>> ListarTempoRuptura()
        {
            return await _IServicoTempoRuptura.ListarTempoRuptura();
        }
    }
}
