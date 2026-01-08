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
    public interface IInformacoesComplementares : IGenericos<InformacoesComplementares>
    {
        Task<List<InformacoesComplementares>> ListarInformacoesComplementares(Expression<Func<InformacoesComplementares, bool>> exInformacoesComplementares);

        Task<List<InformacoesComplementares>> ListarInformacoesComplementaresBarragemId(int idBarragem);
    }
}
