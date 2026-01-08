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
    public class RepositorioSistemaDrenagemBarragem : RepositorioGenerico<SistemaDrenagemBarragem>, ISistemaDrenagemBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioSistemaDrenagemBarragem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragem(Expression<Func<SistemaDrenagemBarragem, bool>> exSistemaDrenagemBarragems)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.SistemaDrenagemBarragem.Where(exSistemaDrenagemBarragems).AsNoTracking().ToListAsync();
            }
        }


        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragemBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.SistemaDrenagemBarragem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
