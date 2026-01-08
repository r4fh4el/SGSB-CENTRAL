using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoCondicaoFundacao
    {
        Task AdicionarCondicaoFundacao(CondicaoFundacao condicaoFundacao);
        Task AtualizaCondicaoFundacao(CondicaoFundacao condicaoFundacao);
        Task<List<CondicaoFundacao>> ListarCondicaoFundacao();
    }
}
