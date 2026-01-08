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
    public class RepositorioUsoSoloPredominante : RepositorioGenerico<UsoSoloPredominante>, IUsoSoloPredominante
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioUsoSoloPredominante() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante(Expression<Func<UsoSoloPredominante, bool>> exUsoSoloPredominante)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.UsoSoloPredominante.Where(exUsoSoloPredominante).AsNoTracking().ToListAsync();
            }
        }
    }
}
