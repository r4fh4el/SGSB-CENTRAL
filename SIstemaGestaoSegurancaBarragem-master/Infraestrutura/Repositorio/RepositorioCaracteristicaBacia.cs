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
    public class RepositorioCaracteristicaBacia : RepositorioGenerico<CaracteristicaBacia>, ICaracteristicaBacia
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioCaracteristicaBacia() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia(Expression<Func<CaracteristicaBacia, bool>> exCaracteristicaBacia)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.CaracteristicaBacia.Where(exCaracteristicaBacia).AsNoTracking().ToListAsync();
            }
        }
    }
}
