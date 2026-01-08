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
    public class AplicacaoHidrogramaParabolico : IAplicacaoHidrogramaParabolico
    {
        // INTERFACE DOMINIO
        IHidrogramaParabolico _IHidrogramaParabolico;

        // INTERFACE SERVICO
        IServicoHidrogramaParabolico _IServicoHidrogramaParabolico;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoHidrogramaParabolico(IHidrogramaParabolico iHidrogramaParabolico, IServicoHidrogramaParabolico iServicoHidrogramaParabolico)
        {
            _IHidrogramaParabolico = iHidrogramaParabolico;
            _IServicoHidrogramaParabolico = iServicoHidrogramaParabolico;
        }

        public async Task Adicionar(HidrogramaParabolico Objeto)
        {
           await _IHidrogramaParabolico.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(HidrogramaParabolico HidrogramaParabolico)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarHidrogramaParabolico(HidrogramaParabolico HidrogramaParabolico)
        {
            await _IServicoHidrogramaParabolico.AdicionarHidrogramaParabolico(HidrogramaParabolico);
        }

        public Task AtualizaBarragem(HidrogramaParabolico HidrogramaParabolico)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(HidrogramaParabolico Objeto)
        {
            await _IHidrogramaParabolico.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaHidrogramaParabolico(HidrogramaParabolico HidrogramaParabolico)
        {
            await _IServicoHidrogramaParabolico.AtualizaHidrogramaParabolico(HidrogramaParabolico);
        }

        public async Task<HidrogramaParabolico> BuscarPorId(int Id)
        {
            return await _IHidrogramaParabolico.BuscarPorId(Id);
        }

        public async Task Excluir(HidrogramaParabolico Objeto)
        {
            await _IHidrogramaParabolico.Excluir(Objeto);
        }

        public async  Task<List<HidrogramaParabolico>> Listar()
        {
            return await _IHidrogramaParabolico.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<HidrogramaParabolico>> ListarHidrogramaParabolico()
        {
            return await _IServicoHidrogramaParabolico.ListarHidrogramaParabolico();
        }
    }
}
