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
    public interface IAplicacaoUsoBarragem : IGenericaAplicacao<UsoBarragem>
    {
        Task AdicionarUsoBarragem(UsoBarragem usoBarragem);
        Task AtualizaUsoBarragem(UsoBarragem usoBarragem);
        Task<List<UsoBarragem>> ListarUsoBarragem();
    }
    
}
