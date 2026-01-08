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
    public class AplicacaoHidrogramaTriangula : IAplicacaoHidrogramaTriangula
    {
        // INTERFACE DOMINIO
        IHidrogramaTriangula _IHidrogramaTriangula;

        // INTERFACE SERVICO
        IServicoHidrogramaTriangula _IServicoHidrogramaTriangula;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoHidrogramaTriangula(IHidrogramaTriangula iHidrogramaTriangula, IServicoHidrogramaTriangula iServicoHidrogramaTriangula)
        {
            _IHidrogramaTriangula = iHidrogramaTriangula;
            _IServicoHidrogramaTriangula = iServicoHidrogramaTriangula;
        }

        public async Task Adicionar(HidrogramaTriangula Objeto)
        {
           await _IHidrogramaTriangula.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(HidrogramaTriangula HidrogramaTriangula)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarHidrogramaTriangula(HidrogramaTriangula HidrogramaTriangula)
        {
            await _IServicoHidrogramaTriangula.AdicionarHidrogramaTriangula(HidrogramaTriangula);
        }

        public Task AtualizaBarragem(HidrogramaTriangula HidrogramaTriangula)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(HidrogramaTriangula Objeto)
        {
            await _IHidrogramaTriangula.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaHidrogramaTriangula(HidrogramaTriangula HidrogramaTriangula)
        {
            await _IServicoHidrogramaTriangula.AtualizaHidrogramaTriangula(HidrogramaTriangula);
        }

        public async Task<HidrogramaTriangula> BuscarPorId(int Id)
        {
            return await _IHidrogramaTriangula.BuscarPorId(Id);
        }

        public async Task Excluir(HidrogramaTriangula Objeto)
        {
            await _IHidrogramaTriangula.Excluir(Objeto);
        }

        public async  Task<List<HidrogramaTriangula>> Listar()
        {
            return await _IHidrogramaTriangula.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<HidrogramaTriangula>> ListarHidrogramaTriangula()
        {
            return await _IServicoHidrogramaTriangula.ListarHidrogramaTriangula();
        }
    }
}
