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
    public class RepositorioTipoEstruturaBarragem : RepositorioGenerico<TipoEstruturaBarragem>, ITipoEstruturaBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTipoEstruturaBarragem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem(Expression<Func<TipoEstruturaBarragem, bool>> exTipoEstruturaBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TipoEstruturaBarragem.Where(exTipoEstruturaBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
