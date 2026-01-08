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
    public class RepositorioCategoriaRisco : RepositorioGenerico<CategoriaRisco>, ICategoriaRisco
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioCategoriaRisco() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<CategoriaRisco>> ListarCategoriaRisco(Expression<Func<CategoriaRisco, bool>> exCategoriaRisco)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.CategoriaRisco.Where(exCategoriaRisco).AsNoTracking().ToListAsync();
            }
        }
    }
}
