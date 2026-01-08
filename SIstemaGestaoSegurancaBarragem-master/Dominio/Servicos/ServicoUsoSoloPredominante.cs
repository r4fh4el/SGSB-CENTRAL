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
    public class ServicoUsoSoloPredominante : IServicoUsoSoloPredominante
    {
        private readonly IUsoSoloPredominante _IUsoSoloPredominante;

        public ServicoUsoSoloPredominante(IUsoSoloPredominante usoSoloPredominante) 
        {
            _IUsoSoloPredominante = usoSoloPredominante;
        }

        public async Task AdicionarUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante)
        {
            var validarNome = usoSoloPredominante.ValidadePropriedadeSring(usoSoloPredominante.Nome , "Nome");

            if (validarNome)
            {
                usoSoloPredominante.DataAlteracao = DateTime.Now;
                usoSoloPredominante.DataCadastro = DateTime.Now;
               await _IUsoSoloPredominante.Adicionar(usoSoloPredominante);
            }
        }

        public async Task AtualizaUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante)
        {
            var validarNome = usoSoloPredominante.ValidadePropriedadeSring(usoSoloPredominante.Nome, "Nome");

            if (validarNome)
            {
                usoSoloPredominante.DataAlteracao = DateTime.Now;
                usoSoloPredominante.DataCadastro = usoSoloPredominante.DataCadastro;
                await _IUsoSoloPredominante.Atualizar(usoSoloPredominante);
        }
        }

        public async Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante()
        {
            return  await _IUsoSoloPredominante.ListarUsoSoloPredominante(n => n.Nome != null);
        }
    }
}
