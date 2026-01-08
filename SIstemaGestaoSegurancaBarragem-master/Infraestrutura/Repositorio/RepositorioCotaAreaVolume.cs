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
    public class RepositorioCotaAreaVolume : RepositorioGenerico<CotaAreaVolume>, ICotaAreaVolume
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioCotaAreaVolume() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolume(Expression<Func<CotaAreaVolume, bool>> exCotaAreaVolumes)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.CotaAreaVolume.Where(exCotaAreaVolumes).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.CotaAreaVolume.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
