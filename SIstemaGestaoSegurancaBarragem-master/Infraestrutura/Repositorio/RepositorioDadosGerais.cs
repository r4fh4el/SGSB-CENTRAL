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
    public class RepositorioDadosGerais : RepositorioGenerico<DadosGerais>, IDadosGerais
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioDadosGerais() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<DadosGerais>> ListarDadosGerais(Expression<Func<DadosGerais, bool>> exDadosGeraiss)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DadosGerais.Where(exDadosGeraiss).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.DadosGerais.Where(x=>x.BarragemId == idBarragem).AsNoTracking().ToListAsync();
            }
        }
    }
}
