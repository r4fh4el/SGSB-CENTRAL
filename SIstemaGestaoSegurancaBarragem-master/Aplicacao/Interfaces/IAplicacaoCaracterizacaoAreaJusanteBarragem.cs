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
    public interface IAplicacaoCaracterizacaoAreaJusanteBarragem : IGenericaAplicacao<CaracterizacaoAreaJusanteBarragem>
    {
        Task AdicionarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem);
        Task AtualizaCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem);
        Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragem();
        Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragemBarragemId(int idBarragem);
    }
    
}
