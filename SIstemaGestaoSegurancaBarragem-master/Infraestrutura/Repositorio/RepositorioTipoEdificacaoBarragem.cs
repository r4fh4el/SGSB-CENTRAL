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
    public class RepositorioTipoEdificacaoBarragem : RepositorioGenerico<TipoEdificacaoBarragem>, ITipoEdificacaoBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTipoEdificacaoBarragem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem(Expression<Func<TipoEdificacaoBarragem, bool>> exTipoEdificacaoBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TipoEdificacaoBarragem.Where(exTipoEdificacaoBarragem).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TipoEdificacaoBarragem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
