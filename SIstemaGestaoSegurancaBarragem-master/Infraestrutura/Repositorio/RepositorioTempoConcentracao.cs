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
    public class RepositorioTempoConcentracao : RepositorioGenerico<TempoConcentracao>, ITempoConcentracao
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioTempoConcentracao() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<TempoConcentracao>> ListarTempoConcentracao(Expression<Func<TempoConcentracao, bool>> exTempoConcentracao)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.TempoConcentracao.Where(exTempoConcentracao).AsNoTracking().ToListAsync();
            }
        }
    }
}
