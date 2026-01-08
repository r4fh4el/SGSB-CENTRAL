using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoPontoEncontro
    {
        Task AdicionarPontoEncontro(PontoEncontro pontoEncontro);
        Task AtualizaPontoEncontro(PontoEncontro pontoEncontro);
        Task<List<PontoEncontro>> ListarPontoEncontro();
    }
}
