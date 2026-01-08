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
    public class ServicoTipoMaterialBarragem : IServicoTipoMaterialBarragem
    {
        private readonly ITipoMaterialBarragem _ITipoMaterialBarragem;

        public ServicoTipoMaterialBarragem(ITipoMaterialBarragem tipoMaterialBarragem) 
        {
            _ITipoMaterialBarragem = tipoMaterialBarragem;
        }

        public async Task AdicionarTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            var validarNome = tipoMaterialBarragem.ValidadePropriedadeSring(tipoMaterialBarragem.Nome , "Nome");

            if (validarNome)
            {
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = DateTime.Now;
               await _ITipoMaterialBarragem.Adicionar(tipoMaterialBarragem);
            }
        }

        public async Task AtualizaTipoMaterialBarragem(TipoMaterialBarragem tipoMaterialBarragem)
        {
            var validarNome = tipoMaterialBarragem.ValidadePropriedadeSring(tipoMaterialBarragem.Nome, "Nome");

            if (validarNome)
            {
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = tipoMaterialBarragem.DataCadastro;
                await _ITipoMaterialBarragem.Atualizar(tipoMaterialBarragem);
        }
        }

        public async Task<List<TipoMaterialBarragem>> ListarTipoMaterialBarragem()
        {
            return  await _ITipoMaterialBarragem.ListarTipoMaterialBarragem(n => n.Nome != null);
        }
    }
}
