using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoUsoBarragem
    {
        Task AdicionarUsoBarragem(UsoBarragem usoBarragem);
        Task AtualizaUsoBarragem(UsoBarragem usoBarragem);
        Task<List<UsoBarragem>> ListarUsoBarragem();
    }
}
