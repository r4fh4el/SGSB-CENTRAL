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
    public class RepositorioSistemaDrenagem : RepositorioGenerico<SistemaDrenagem>, ISistemaDrenagem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioSistemaDrenagem() 
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<SistemaDrenagem>> ListarSistemaDrenagem(Expression<Func<SistemaDrenagem, bool>> exSistemaDrenagems)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.SistemaDrenagem.Where(exSistemaDrenagems).AsNoTracking().ToListAsync();
            }
        }
    }
}
