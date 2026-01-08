using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoCategoriaRisco
    {
        Task AdicionarCategoriaRisco(CategoriaRisco CategoriaRisco);
        Task AtualizaCategoriaRisco(CategoriaRisco CategoriaRisco);
        Task<List<CategoriaRisco>> ListarCategoriaRisco();
    }
}
