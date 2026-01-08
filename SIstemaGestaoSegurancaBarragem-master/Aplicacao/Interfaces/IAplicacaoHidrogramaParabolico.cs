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
    public interface IAplicacaoHidrogramaParabolico : IGenericaAplicacao<HidrogramaParabolico>
    {
        Task AdicionarBarragem(HidrogramaParabolico HidrogramaParabolico);
        Task AtualizaBarragem(HidrogramaParabolico HidrogramaParabolico);
        Task<List<HidrogramaParabolico>> ListarHidrogramaParabolico();
    }
}
