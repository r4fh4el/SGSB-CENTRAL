using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoInformacoesComplementares
    {
        Task AdicionarInformacoesComplementares(InformacoesComplementares informacoesComplementares);
        Task AtualizaInformacoesComplementares(InformacoesComplementares informacoesComplementares);
        Task<List<InformacoesComplementares>> ListarInformacoesComplementares();
        Task<List<InformacoesComplementares>> ListarInformacoesComplementaresBarragemId(int idBarragem);
    }
}
