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
    public class ServicoVertedouro : IServicoVertedouro
    {
        private readonly IVertedouro _IVertedouro;

        public ServicoVertedouro(IVertedouro tipoMaterialBarragem)
        {
            _IVertedouro = tipoMaterialBarragem;
        }

        public async Task AdicionarVertedouro(Vertedouro tipoMaterialBarragem)
        {

            tipoMaterialBarragem.DataAlteracao = DateTime.Now;
            tipoMaterialBarragem.DataCadastro = DateTime.Now;
            await _IVertedouro.Adicionar(tipoMaterialBarragem);

        }

        public async Task AtualizaVertedouro(Vertedouro tipoMaterialBarragem)
        {

            tipoMaterialBarragem.DataAlteracao = DateTime.Now;
            tipoMaterialBarragem.DataCadastro = tipoMaterialBarragem.DataCadastro;
            await _IVertedouro.Atualizar(tipoMaterialBarragem);

        }

        public async Task<List<Vertedouro>> ListarVertedouro()
        {
            return await _IVertedouro.ListarVertedouro(n => n.LocalizacaoVertedouro != null);
        }

        public async Task<List<Vertedouro>> ListarVertedouroBarragemId(int idBarragem)
        {
            return await _IVertedouro.ListarVertedouroBarragemId(idBarragem);
        }
    }
}
