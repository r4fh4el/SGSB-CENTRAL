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
    public class ServicoDadosGerais : IServicoDadosGerais
    {
        private readonly IDadosGerais _IDadosGerais;

        public ServicoDadosGerais(IDadosGerais dadosGerais)
        {
            _IDadosGerais = dadosGerais;
        }

        public async Task AdicionarDadosGerais(DadosGerais dadosGerais)
        {
            var validarNome = dadosGerais.ValidadePropriedadeSring(dadosGerais.NomeEmpreendedor, "NomeEmpreendedor");

            if (validarNome)
            {
                dadosGerais.DataAlteracao = DateTime.Now;
                dadosGerais.DataCadastro = DateTime.Now;
                await _IDadosGerais.Adicionar(dadosGerais);
            }
        }

        public async Task AtualizaDadosGerais(DadosGerais dadosGerais)
        {
            var validarNome = dadosGerais.ValidadePropriedadeSring(dadosGerais.NomeEmpreendedor, "NomeEmpreendedor");

            if (validarNome)
            {
                dadosGerais.DataAlteracao = DateTime.Now;
                dadosGerais.DataCadastro = dadosGerais.DataCadastro;
                await _IDadosGerais.Atualizar(dadosGerais);
            }
        }

        public async Task<List<DadosGerais>> ListarDadosGerais()
        {
            return await _IDadosGerais.ListarDadosGerais(n => n.NomeEmpreendedor != null);
        }

        public async Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem)
        {
            return await _IDadosGerais.ListarDadosGeraisBarragemId(idBarragem);
        }

    }
}
