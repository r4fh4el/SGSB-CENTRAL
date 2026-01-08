using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoDadosGerais : IAplicacaoDadosGerais
    {
        // INTERFACE DOMINIO
        IDadosGerais _IDadosGerais;

        // INTERFACE SERVICO
        IServicoDadosGerais _IServicoDadosGerais;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoDadosGerais(IDadosGerais iDadosGerais, IServicoDadosGerais iServicoDadosGerais)
        {
            _IDadosGerais = iDadosGerais;
            _IServicoDadosGerais = iServicoDadosGerais;
        }

        public async Task Adicionar(DadosGerais Objeto)
        {
            await _IDadosGerais.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarDadosGerais(DadosGerais dadosGerais)
        {
            await _IServicoDadosGerais.AdicionarDadosGerais(dadosGerais);
        }

        public async Task Atualizar(DadosGerais Objeto)
        {
            await _IDadosGerais.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaDadosGerais(DadosGerais dadosGerais)
        {
            await _IServicoDadosGerais.AtualizaDadosGerais(dadosGerais);
        }

        public async Task<DadosGerais> BuscarPorId(int Id)
        {
            return await _IDadosGerais.BuscarPorId(Id);
        }

        public async Task Excluir(DadosGerais Objeto)
        {
            await _IDadosGerais.Excluir(Objeto);
        }

        public async Task<List<DadosGerais>> Listar()
        {
            return await _IDadosGerais.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<DadosGerais>> ListarDadosGerais()
        {
            return await _IServicoDadosGerais.ListarDadosGerais();
        }
        public async Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem)
        {
            return await _IServicoDadosGerais.ListarDadosGeraisBarragemId(idBarragem);
        }

        public Task Adicionar(AplicacaoDadosGerais Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoDadosGerais Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoDadosGerais Objeto)
        {
            throw new NotImplementedException();
        }


    }
}
