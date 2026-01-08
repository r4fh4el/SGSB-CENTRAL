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
    public class RepositorioUsoBarragem : RepositorioGenerico<UsoBarragem>, IUsoBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioUsoBarragem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<UsoBarragem>> ListarUsoBarragem(Expression<Func<UsoBarragem, bool>> exUsoBarragems)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.UsoBarragem.Where(exUsoBarragems).AsNoTracking().ToListAsync();
            }
        }
    }
}
