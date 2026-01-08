using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioInformacoesComplementares : RepositorioGenerico<InformacoesComplementares>, IInformacoesComplementares
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioInformacoesComplementares() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementares(Expression<Func<InformacoesComplementares, bool>> exInformacoesComplementaress)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.InformacoesComplementares.Where(exInformacoesComplementaress).AsNoTracking().ToListAsync();
            }
        }
        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementaresBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.InformacoesComplementares.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
