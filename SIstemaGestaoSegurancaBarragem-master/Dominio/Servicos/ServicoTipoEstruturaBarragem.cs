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
    public class ServicoTipoEstruturaBarragem : IServicoTipoEstruturaBarragem
    {
        private readonly ITipoEstruturaBarragem _ITipoEstruturaBarragem;

        public ServicoTipoEstruturaBarragem(ITipoEstruturaBarragem tipoMaterialBarragem) 
        {
            _ITipoEstruturaBarragem = tipoMaterialBarragem;
        }

        public async Task AdicionarTipoEstruturaBarragem(TipoEstruturaBarragem tipoMaterialBarragem)
        {
            var validarNome = tipoMaterialBarragem.ValidadePropriedadeSring(tipoMaterialBarragem.Nome , "Nome");

            if (validarNome)
            {
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = DateTime.Now;
               await _ITipoEstruturaBarragem.Adicionar(tipoMaterialBarragem);
            }
        }


        public async Task AtualizarTipoEstruturaBarragem(TipoEstruturaBarragem tipoMaterialBarragem)
        {
            var validarNome = tipoMaterialBarragem.ValidadePropriedadeSring(tipoMaterialBarragem.Nome, "Nome");

            if (validarNome)
            {
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = tipoMaterialBarragem.DataCadastro;
                await _ITipoEstruturaBarragem.Atualizar(tipoMaterialBarragem);
        }
        }

        public async Task<List<TipoEstruturaBarragem>> ListarTipoEstruturaBarragem()
        {
            return  await _ITipoEstruturaBarragem.ListarTipoEstruturaBarragem(n => n.Nome != null);
        }
    }
}
