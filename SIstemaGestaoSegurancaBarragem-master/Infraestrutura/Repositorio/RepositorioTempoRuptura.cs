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
    public class RepositorioTempoRuptura : RepositorioGenerico<TempoRuptura>, ITempoRuptura
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTempoRuptura() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TempoRuptura>> ListarTempoRuptura(Expression<Func<TempoRuptura, bool>> exTempoRuptura)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TempoRuptura.Where(exTempoRuptura).AsNoTracking().ToListAsync();
            }
        }
    }
}
