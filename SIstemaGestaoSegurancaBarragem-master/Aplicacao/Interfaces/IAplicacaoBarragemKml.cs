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
    public interface IAplicacaoBarragemKml : IGenericaAplicacao<BarragemKml>
    {
        Task AdicionarBarragemKml(BarragemKml BarragemKml);
        Task AtualizaBarragemKml(BarragemKml BarragemKml);
        Task<List<BarragemKml>> ListarBarragemKml();
    }
}
