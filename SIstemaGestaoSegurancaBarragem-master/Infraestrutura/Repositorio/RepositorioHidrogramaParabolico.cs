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
    public class RepositorioHidrogramaParabolico : RepositorioGenerico<HidrogramaParabolico>, IHidrogramaParabolico
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioHidrogramaParabolico() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<HidrogramaParabolico>> ListarHidrogramaParabolico(Expression<Func<HidrogramaParabolico, bool>> exHidrogramaParabolico)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.HidrogramaParabolico.Where(exHidrogramaParabolico).AsNoTracking().ToListAsync();
            }
        }
    }
}
