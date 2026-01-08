using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoHidrogramaTriangula : IServicoHidrogramaTriangula
    {
        private readonly IHidrogramaTriangula _IHidrogramaTriangula;

        public ServicoHidrogramaTriangula(IHidrogramaTriangula HidrogramaTriangula)
        {
            _IHidrogramaTriangula = HidrogramaTriangula;
        }

        public async Task AdicionarHidrogramaTriangula(HidrogramaTriangula HidrogramaTriangula)
        {

            HidrogramaTriangula.DataAlteracao = DateTime.Now;
            HidrogramaTriangula.DataCadastro = DateTime.Now;
            await _IHidrogramaTriangula.Adicionar(HidrogramaTriangula);

        }

        public async Task AtualizaHidrogramaTriangula(HidrogramaTriangula HidrogramaTriangula)
        {

            HidrogramaTriangula.DataAlteracao = DateTime.Now;
            HidrogramaTriangula.DataCadastro = HidrogramaTriangula.DataCadastro;
            await _IHidrogramaTriangula.Atualizar(HidrogramaTriangula);

        }

        public async Task<List<HidrogramaTriangula>> ListarHidrogramaTriangula()
        {
            return await _IHidrogramaTriangula.ListarHidrogramaTriangula(n => n.Id != null);
        }
    }
}
