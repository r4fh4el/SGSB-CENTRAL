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
    public class RepositorioInstrumentos : RepositorioGenerico<Instrumentos>, IInstrumentos
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioInstrumentos() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Instrumentos>> ListarInstrumentos(Expression<Func<Instrumentos, bool>> exInstrumentos)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Instrumentos.Where(exInstrumentos).AsNoTracking().ToListAsync();
            }
        }
    }
}
