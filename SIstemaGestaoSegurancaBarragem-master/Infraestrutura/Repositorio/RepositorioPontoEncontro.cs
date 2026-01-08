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
    public class RepositorioPontoEncontro : RepositorioGenerico<PontoEncontro>, IPontoEncontro
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioPontoEncontro() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<PontoEncontro>> ListarPontoEncontro(Expression<Func<PontoEncontro, bool>> exPontoEncontro)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.PontoEncontro.Where(exPontoEncontro).AsNoTracking().ToListAsync();
            }
        }
    }
}
