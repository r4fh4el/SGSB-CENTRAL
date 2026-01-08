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
    public class RepositorioBarragemKml : RepositorioGenerico<BarragemKml>, IBarragemKml
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioBarragemKml() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<BarragemKml>> ListarBarragemKml(Expression<Func<BarragemKml, bool>> exBarragemKml)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.BarragemKml.Where(exBarragemKml).AsNoTracking().ToListAsync();
            }
        }
    }
}
