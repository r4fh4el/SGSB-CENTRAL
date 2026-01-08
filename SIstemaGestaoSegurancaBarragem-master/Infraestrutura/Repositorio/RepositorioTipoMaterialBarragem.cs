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
    public class RepositorioTipoMaterialBarragem : RepositorioGenerico<TipoMaterialBarragem>, ITipoMaterialBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTipoMaterialBarragem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem(Expression<Func<TipoMaterialBarragem, bool>> exTipoMaterialBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TipoMaterialBarragem.Where(exTipoMaterialBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
