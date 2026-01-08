using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class UnitTestDadosGerais : IAplicacaoDadosGerais
    {
        private readonly IDadosGerais _dadosGeraisRepository;
        private readonly IServicoDadosGerais _servicoDadosGerais;

        public UnitTestDadosGerais(IDadosGerais dadosGeraisRepository, IServicoDadosGerais servicoDadosGerais)
        {
            _dadosGeraisRepository = dadosGeraisRepository;
            _servicoDadosGerais = servicoDadosGerais;
        }

        public async Task Adicionar(DadosGerais dadosGerais)
        {
            if (dadosGerais == null) throw new ArgumentNullException(nameof(dadosGerais));
            await _dadosGeraisRepository.Adicionar(dadosGerais);
        }

        public Task AdicionarDadosGerais(DadosGerais dadosGerais)
        {
            throw new NotImplementedException();
        }

        public Task AtualizaDadosGerais(DadosGerais dadosGerais)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(DadosGerais dadosGerais)
        {
            if (dadosGerais == null) throw new ArgumentNullException(nameof(dadosGerais));
            await _dadosGeraisRepository.Atualizar(dadosGerais);
        }

        public async Task<DadosGerais> BuscarPorId(int id)
        {
            return await _dadosGeraisRepository.BuscarPorId(id);
        }

        public async Task Excluir(DadosGerais dadosGerais)
        {
            if (dadosGerais == null) throw new ArgumentNullException(nameof(dadosGerais));
            await _dadosGeraisRepository.Excluir(dadosGerais);
        }

        public async Task<List<DadosGerais>> Listar()
        {
            return await _dadosGeraisRepository.Listar();
        }

        public Task<List<DadosGerais>> ListarDadosGerais()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DadosGerais>> ListarDadosGeraisBarragemId(int idBarragem)
        {
            return await _servicoDadosGerais.ListarDadosGeraisBarragemId(idBarragem);
        }
    }
}
