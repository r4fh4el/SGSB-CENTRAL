using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
   
    public interface IServicoSistemaDrenagem
    {
        Task AdicionarSistemaDrenagem(SistemaDrenagem sistemaDrenagem);
        Task AtualizaSistemaDrenagem(SistemaDrenagem sistemaDrenagem);
        Task<List<SistemaDrenagem>> ListarSistemaDrenagem();
    }
}
