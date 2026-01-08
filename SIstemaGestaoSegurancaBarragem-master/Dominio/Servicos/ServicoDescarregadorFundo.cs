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
    public class ServicoDescarregadorFundo : IServicoDescarregadorFundo
    {
        private readonly IDescarregadorFundo _IDescarregadorFundo;

        public ServicoDescarregadorFundo(IDescarregadorFundo descarregadorFundo) 
        {
            _IDescarregadorFundo = descarregadorFundo;
        }

        public async Task AdicionarDescarregadorFundo(DescarregadorFundo descarregadorFundo)
        {
            var validarNome = descarregadorFundo.ValidadePropriedadeSring(descarregadorFundo.Localizacao , "Localizacao");

            if (validarNome)
            {
                descarregadorFundo.DataAlteracao = DateTime.Now;
                descarregadorFundo.DataCadastro = DateTime.Now;
               await _IDescarregadorFundo.Adicionar(descarregadorFundo);
            }
        }

        public async Task AtualizaDescarregadorFundo(DescarregadorFundo descarregadorFundo)
        {
            var validarNome = descarregadorFundo.ValidadePropriedadeSring(descarregadorFundo.Localizacao, "Localizacao");

            if (validarNome)
            {
                descarregadorFundo.DataAlteracao = DateTime.Now;
                descarregadorFundo.DataCadastro = descarregadorFundo.DataCadastro;
                await _IDescarregadorFundo.Atualizar(descarregadorFundo);
        }
        }

        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundo()
        {
            return  await _IDescarregadorFundo.ListarDescarregadorFundo(n => n.Localizacao != null);
        }


        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId(int idBarragem)
        {
            return await _IDescarregadorFundo.ListarDescarregadorFundoBarragemId(idBarragem);
        }
    }
}
