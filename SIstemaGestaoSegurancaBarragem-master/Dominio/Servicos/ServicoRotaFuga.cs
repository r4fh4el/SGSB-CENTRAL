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
    public class ServicoRotaFuga : IServicoRotaFuga
    {
        private readonly IRotaFuga _IRotaFuga;

        public ServicoRotaFuga(IRotaFuga RotaFuga) 
        {
            _IRotaFuga = RotaFuga;
        }

        public async Task AdicionarRotaFuga(RotaFuga RotaFuga)
        {
            var validarNome = RotaFuga.ValidadePropriedadeSring(RotaFuga.Nome , "Nome");

            if (validarNome)
            {
                RotaFuga.DataAlteracao = DateTime.Now;
                RotaFuga.DataCadastro = DateTime.Now;
               await _IRotaFuga.Adicionar(RotaFuga);
            }
        }

        public async Task AtualizaRotaFuga(RotaFuga RotaFuga)
        {
            var validarNome = RotaFuga.ValidadePropriedadeSring(RotaFuga.Nome, "Nome");

            if (validarNome)
            {
                RotaFuga.DataAlteracao = DateTime.Now;
                RotaFuga.DataCadastro = RotaFuga.DataCadastro;
                await _IRotaFuga.Atualizar(RotaFuga);
        }
        }

        public async Task<List<RotaFuga>> BuscarListPorIdRotaFuga(int id)
        {
            return  await _IRotaFuga.BuscarListPorIdRotaFuga(id);
        }
        public async Task<List<RotaFuga>> ListarRotaFuga()
        {
            return  await _IRotaFuga.ListarRotaFuga(n => n.Nome != null);
        }
    }
}
