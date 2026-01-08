using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoSistemaDrenagem : IGenericaAplicacao<SistemaDrenagem>
    {
        Task AdicionarSistemaDrenagem(SistemaDrenagem sistemaDrenagem);
        Task AtualizaSistemaDrenagem(SistemaDrenagem sistemaDrenagem);
        Task<List<SistemaDrenagem>> ListarSistemaDrenagem();
    }
    
}
