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
    public class RepositorioHidrogramaTriangula : RepositorioGenerico<HidrogramaTriangula>, IHidrogramaTriangula
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioHidrogramaTriangula() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<HidrogramaTriangula>> ListarHidrogramaTriangula(Expression<Func<HidrogramaTriangula, bool>> exHidrogramaTriangula)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.HidrogramaTriangula.Where(exHidrogramaTriangula).AsNoTracking().ToListAsync();
            }
        }
    }
}
