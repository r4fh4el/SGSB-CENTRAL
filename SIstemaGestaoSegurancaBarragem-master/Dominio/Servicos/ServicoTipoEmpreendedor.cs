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
    public class ServicoTipoEmpreendedor : IServicoTipoEmpreendedor
    {
        private readonly ITipoEmpreendedor _ITipoEmpreendedor;

        public ServicoTipoEmpreendedor(ITipoEmpreendedor tipoEmpreendedor) 
        {
            _ITipoEmpreendedor = tipoEmpreendedor;
        }

        public async Task AdicionarTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor)
        {
            var validarNome = tipoEmpreendedor.ValidadePropriedadeSring(tipoEmpreendedor.Nome , "Nome");

            if (validarNome)
            {
                tipoEmpreendedor.DataAlteracao = DateTime.Now;
                tipoEmpreendedor.DataCadastro = DateTime.Now;
               await _ITipoEmpreendedor.Adicionar(tipoEmpreendedor);
            }
        }

        public async Task AtualizaTipoEmpreendedor(TipoEmpreendedor tipoEmpreendedor)
        {
            var validarNome = tipoEmpreendedor.ValidadePropriedadeSring(tipoEmpreendedor.Nome, "Nome");

            if (validarNome)
            {
                tipoEmpreendedor.DataAlteracao = DateTime.Now;
                tipoEmpreendedor.DataCadastro = tipoEmpreendedor.DataCadastro;
                await _ITipoEmpreendedor.Atualizar(tipoEmpreendedor);
        }
        }

        public async Task<List<TipoEmpreendedor>> ListarTipoEmpreendedor()
        {
            return  await _ITipoEmpreendedor.ListarTipoEmpreendedor(n => n.Nome != null);
        }
    }
}
