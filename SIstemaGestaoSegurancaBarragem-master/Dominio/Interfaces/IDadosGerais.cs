using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IDadosGerais : IGenericos<DadosGerais>
    {
        Task<List<DadosGerais>> ListarDadosGerais(Expression<Func<DadosGerais, bool>> exDadosGerais);
        Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem);
    }
}
