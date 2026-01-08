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
    public class RepositorioUsoBarragemBarragem : RepositorioGenerico<UsoBarragemBarragem>, IUsoBarragemBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioUsoBarragemBarragem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem(Expression<Func<UsoBarragemBarragem, bool>> exUsoBarragemBarragems)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.UsoBarragemBarragem.Where(exUsoBarragemBarragems).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.UsoBarragemBarragem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
