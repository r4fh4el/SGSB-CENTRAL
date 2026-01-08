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
    public class ServicoPontoEncontro : IServicoPontoEncontro
    {
        private readonly IPontoEncontro _IPontoEncontro;

        public ServicoPontoEncontro(IPontoEncontro pontoEncontro) 
        {
            _IPontoEncontro = pontoEncontro;
        }

        public async Task AdicionarPontoEncontro(PontoEncontro pontoEncontro)
        {
            var validarNome = pontoEncontro.ValidadePropriedadeSring(pontoEncontro.Nome , "Nome");

            if (validarNome)
            {
                pontoEncontro.DataAlteracao = DateTime.Now;
                pontoEncontro.DataCadastro = DateTime.Now;
               await _IPontoEncontro.Adicionar(pontoEncontro);
            }
        }

        public async Task AtualizaPontoEncontro(PontoEncontro pontoEncontro)
        {
            var validarNome = pontoEncontro.ValidadePropriedadeSring(pontoEncontro.Nome, "Nome");

            if (validarNome)
            {
                pontoEncontro.DataAlteracao = DateTime.Now;
                pontoEncontro.DataCadastro = pontoEncontro.DataCadastro;
                await _IPontoEncontro.Atualizar(pontoEncontro);
        }
        }

        public async Task<List<PontoEncontro>> ListarPontoEncontro()
        {
            return  await _IPontoEncontro.ListarPontoEncontro(n => n.Nome != null);
        }
    }
}
