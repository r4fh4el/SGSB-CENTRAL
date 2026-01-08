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
    public class ServicoDanoPotencialAssociado : IServicoDanoPotencialAssociado
    {
        private readonly IDanoPotencialAssociado _IDanoPotencialAssociado;

        public ServicoDanoPotencialAssociado(IDanoPotencialAssociado tipoMaterialBarragem) 
        {
            _IDanoPotencialAssociado = tipoMaterialBarragem;
        }

        public async Task AdicionarDanoPotencialAssociado(DanoPotencialAssociado tipoMaterialBarragem)
        {
            
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = DateTime.Now;
               await _IDanoPotencialAssociado.Adicionar(tipoMaterialBarragem);
           
        }

        public async Task AtualizaDanoPotencialAssociado(DanoPotencialAssociado tipoMaterialBarragem)
        {
          
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = tipoMaterialBarragem.DataCadastro;
                await _IDanoPotencialAssociado.Atualizar(tipoMaterialBarragem);
        
        }

        public async Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado()
        {
            return  await _IDanoPotencialAssociado.ListarDanoPotencialAssociado(n => n.DpaTotal != null);
        }
          public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int id)
        {
            return  await _IDanoPotencialAssociado.GetDanoPotencialAssociadoBarragemId(id);
        }

        
    }
}
