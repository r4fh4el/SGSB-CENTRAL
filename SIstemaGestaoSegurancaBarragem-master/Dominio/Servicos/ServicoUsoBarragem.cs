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
    public class ServicoUsoBarragem : IServicoUsoBarragem
    {
        private readonly IUsoBarragem _IUsoBarragem;

        public ServicoUsoBarragem(IUsoBarragem usoBarragem) 
        {
            _IUsoBarragem = usoBarragem;
        }

        public async Task AdicionarUsoBarragem(UsoBarragem usoBarragem)
        {
            var validarNome = usoBarragem.ValidadePropriedadeSring(usoBarragem.Nome , "Nome");

            if (validarNome)
            {
                usoBarragem.DataAlteracao = DateTime.Now;
                usoBarragem.DataCadastro = DateTime.Now;
               await _IUsoBarragem.Adicionar(usoBarragem);
            }
        }

        public async Task AtualizaUsoBarragem(UsoBarragem usoBarragem)
        {
            var validarNome = usoBarragem.ValidadePropriedadeSring(usoBarragem.Nome, "Nome");

            if (validarNome)
            {
                usoBarragem.DataAlteracao = DateTime.Now;
                usoBarragem.DataCadastro = usoBarragem.DataCadastro;
                await _IUsoBarragem.Atualizar(usoBarragem);
        }
        }

        public async Task<List<UsoBarragem>> ListarUsoBarragem()
        {
            return  await _IUsoBarragem.ListarUsoBarragem(n => n.Nome != null);
        }
    }
}
