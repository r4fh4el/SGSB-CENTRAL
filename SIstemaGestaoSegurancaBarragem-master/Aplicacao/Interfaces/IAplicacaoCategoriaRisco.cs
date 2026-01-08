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
    public interface IAplicacaoCategoriaRisco : IGenericaAplicacao<CategoriaRisco>
    {
        Task AdicionarBarragem(CategoriaRisco CategoriaRisco);
        Task AtualizaBarragem(CategoriaRisco CategoriaRisco);
        Task<List<CategoriaRisco>> ListarCategoriaRisco();
    }
}
