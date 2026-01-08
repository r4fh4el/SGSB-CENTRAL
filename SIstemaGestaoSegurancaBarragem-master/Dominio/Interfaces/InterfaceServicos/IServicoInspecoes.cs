using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
   
    public interface IServicoInspecoes
    {
        Task AdicionarInspecoes(Inspecoes inspecoes);
        Task AtualizaInspecoes(Inspecoes inspecoes);
        Task<List<Inspecoes>> ListarInspecoes();
        Task<List<Inspecoes>> ListarInspecoesBarragemId(int idBarragem);
    }
}
