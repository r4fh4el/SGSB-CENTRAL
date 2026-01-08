using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoTalude : IServicoTalude
    {
        private readonly ITalude _ITalude;

        public ServicoTalude(ITalude talude) 
        {
            _ITalude = talude;
        }

        public async Task AdicionarTalude(Talude talude)
        {
                talude.DataAlteracao = DateTime.Now;
                talude.DataCadastro = DateTime.Now;
               await _ITalude.Adicionar(talude);
           
        }

        public async Task AtualizaTalude(Talude talude)
        {
            
                talude.DataAlteracao = DateTime.Now;
                talude.DataCadastro = talude.DataCadastro;
                await _ITalude.Atualizar(talude);
        
        }

        public async Task<List<Talude>> ListarTalude()
        {
            return  await _ITalude.ListarTalude(n => n.TipoProtecaoSuperficieJusante != null);
        }

        public async Task<List<Talude>> ListarTaludeBarragemId(int idBarragem)
        {
            return await _ITalude.ListarTaludeBarragemId(idBarragem);
        }
    }
}
