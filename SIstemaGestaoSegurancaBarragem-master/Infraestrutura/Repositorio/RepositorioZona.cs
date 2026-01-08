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
    public class RepositorioZona : RepositorioGenerico<Zona>, IZona
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioZona() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Zona>> ListarZona(Expression<Func<Zona, bool>> exZona)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Zona.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Zona>> ListarZonaId(int id)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Zona.Where(x => x.Id == id).AsNoTracking().ToListAsync();
            }
        }
    }
}
