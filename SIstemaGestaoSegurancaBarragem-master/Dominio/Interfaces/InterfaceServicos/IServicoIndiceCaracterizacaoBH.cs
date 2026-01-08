using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoIndiceCaracterizacaoBH
    {
        Task AdicionarIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBH);
        Task AtualizaIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBH);
        Task<List<IndiceCaracterizacaoBH>> ListarIndiceCaracterizacaoBH();
    }
}
