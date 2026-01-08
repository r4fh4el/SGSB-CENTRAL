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
    public interface IAplicacaoReservatorio : IGenericaAplicacao<Reservatorio>
    {
        Task AdicionarReservatorio(Reservatorio reservatorio);
        Task AtualizaReservatorio(Reservatorio reservatorio);
        Task<List<Reservatorio>> ListarReservatorio();

        Task<List<Reservatorio>> ListarReservatorioBarragemId(int idBarragem);

    }
}
