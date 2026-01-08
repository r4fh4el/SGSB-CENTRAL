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
    public class RepositorioIndiceCaracterizacaoBH : RepositorioGenerico<IndiceCaracterizacaoBH>, IIndiceCaracterizacaoBH
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioIndiceCaracterizacaoBH() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<IndiceCaracterizacaoBH>> ListarIndiceCaracterizacaoBH(Expression<Func<IndiceCaracterizacaoBH, bool>> exIndiceCaracterizacaoBH)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.IndiceCaracterizacaoBH.Where(exIndiceCaracterizacaoBH).AsNoTracking().ToListAsync();
            }
        }
    }
}
