using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoInspecoes : IGenericaAplicacao<Inspecoes>
    {
        Task AdicionarInspecoes(Inspecoes inspecoes);
        Task AtualizaInspecoes(Inspecoes inspecoes);
        Task<List<Inspecoes>> ListarInspecoes();
        Task<List<Inspecoes>> ListarInspecoesBarragemId(int idBarragem);

    }
    
}
