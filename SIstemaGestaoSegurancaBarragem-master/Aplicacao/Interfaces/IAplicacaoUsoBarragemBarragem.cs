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
    public interface IAplicacaoUsoBarragemBarragem : IGenericaAplicacao<UsoBarragemBarragem>
    {
        Task AdicionarUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragem);
        Task AtualizaUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragem);
        Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem();
        Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId(int idBarragem);
    }
}
