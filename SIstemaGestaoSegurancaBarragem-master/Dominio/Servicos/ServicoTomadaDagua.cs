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
    public class ServicoTomadaDagua : IServicoTomadaDagua
    {
        private readonly ITomadaDagua _ITomadaDagua;

        public ServicoTomadaDagua(ITomadaDagua tomadaDagua) 
        {
            _ITomadaDagua = tomadaDagua;
        }

        public async Task AdicionarTomadaDagua(TomadaDagua tomadaDagua)
        {
            var validarNome = tomadaDagua.ValidadePropriedadeSring(tomadaDagua.Localizacao , "Nome");

            if (validarNome)
            {
                tomadaDagua.DataAlteracao = DateTime.Now;
                tomadaDagua.DataCadastro = DateTime.Now;
               await _ITomadaDagua.Adicionar(tomadaDagua);
            }
        }

        public async Task AtualizaTomadaDagua(TomadaDagua tomadaDagua)
        {
            var validarNome = tomadaDagua.ValidadePropriedadeSring(tomadaDagua.Localizacao, "Nome");

            if (validarNome)
            {
                tomadaDagua.DataAlteracao = DateTime.Now;
                tomadaDagua.DataCadastro = tomadaDagua.DataCadastro;
                await _ITomadaDagua.Atualizar(tomadaDagua);
        }
        }

        public async Task<List<TomadaDagua>> ListarTomadaDagua()
        {
            return  await _ITomadaDagua.ListarTomadaDagua(n => n.Localizacao != null);
        }

        public async Task<List<TomadaDagua>> ListarTomadaDaguaBarragemId(int idBarragem)
        {
            return await _ITomadaDagua.ListarTomadaDaguaBarragemId(idBarragem);
        }
    }
}
