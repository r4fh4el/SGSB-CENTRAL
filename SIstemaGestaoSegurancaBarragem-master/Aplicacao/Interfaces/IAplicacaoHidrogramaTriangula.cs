using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoHidrogramaTriangula : IGenericaAplicacao<HidrogramaTriangula>
    {
        Task AdicionarBarragem(HidrogramaTriangula HidrogramaTriangula);
        Task AtualizaBarragem(HidrogramaTriangula HidrogramaTriangula);
        Task<List<HidrogramaTriangula>> ListarHidrogramaTriangula();
    }
}
