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
    public class RepositorioReservatorio : RepositorioGenerico<Reservatorio>, IReservatorio
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioReservatorio() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Reservatorio>> ListarReservatorio(Expression<Func<Reservatorio, bool>> exReservatorios)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Reservatorio.Where(exReservatorios).AsNoTracking().ToListAsync();
            }
        }
        public async Task<List<Reservatorio>> ListarReservatorioBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Reservatorio.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }

    }
}
