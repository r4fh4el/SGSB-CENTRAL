using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
   
    public interface IServicoCaracterizacaoAreaJusanteBarragem
    {
        Task AdicionarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem);
        Task AtualizaCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem);
        Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragem();
        Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragemBarragemId(int idBarragem);
    }
}
