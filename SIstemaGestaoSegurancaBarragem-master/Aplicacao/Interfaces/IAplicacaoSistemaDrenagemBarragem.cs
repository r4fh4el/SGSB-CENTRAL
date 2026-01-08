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
    public interface IAplicacaoSistemaDrenagemBarragem : IGenericaAplicacao<SistemaDrenagemBarragem>
    {
        Task AdicionarSistemaDrenagemBarragem(SistemaDrenagemBarragem SistemaDrenagemBarragem);
        Task AtualizaSistemaDrenagemBarragem(SistemaDrenagemBarragem SistemaDrenagemBarragem);
        Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragem();

        Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragemBarragemId(int idBarragem);
    }
    
}
