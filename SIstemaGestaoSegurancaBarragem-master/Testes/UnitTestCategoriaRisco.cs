using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testes
{
    public class UnitTestCategoriaRisco : IAplicacaoCategoriaRisco
    {
        // INTERFACE DOMINIO
        private readonly ICategoriaRisco _iCategoriaRisco;

        // INTERFACE SERVICO
        private readonly IServicoCategoriaRisco _iServicoCategoriaRisco;

        // CONSTRUTOR COM INJEÇÃO DE DEPENDÊNCIA
        public UnitTestCategoriaRisco(ICategoriaRisco iCategoriaRisco, IServicoCategoriaRisco iServicoCategoriaRisco)
        {
            _iCategoriaRisco = iCategoriaRisco ?? throw new ArgumentNullException(nameof(iCategoriaRisco));
            _iServicoCategoriaRisco = iServicoCategoriaRisco ?? throw new ArgumentNullException(nameof(iServicoCategoriaRisco));
        }

        public async Task Adicionar(CategoriaRisco objeto)
        {
            await _iCategoriaRisco.Adicionar(objeto);
        }

        public Task AdicionarBarragem(CategoriaRisco categoriaRisco)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AdicionarCategoriaRisco(CategoriaRisco categoriaRisco)
        {
            await _iServicoCategoriaRisco.AdicionarCategoriaRisco(categoriaRisco);
        }

        public Task AtualizaBarragem(CategoriaRisco categoriaRisco)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(CategoriaRisco objeto)
        {
            await _iCategoriaRisco.Atualizar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AtualizaCategoriaRisco(CategoriaRisco categoriaRisco)
        {
            await _iServicoCategoriaRisco.AtualizaCategoriaRisco(categoriaRisco);
        }

        public async Task<CategoriaRisco> BuscarPorId(int id)
        {
            return await _iCategoriaRisco.BuscarPorId(id);
        }

        public async Task Excluir(CategoriaRisco objeto)
        {
            await _iCategoriaRisco.Excluir(objeto);
        }

        public async Task<List<CategoriaRisco>> Listar()
        {
            return await _iCategoriaRisco.Listar();
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task<List<CategoriaRisco>> ListarCategoriaRisco()
        {
            return await _iServicoCategoriaRisco.ListarCategoriaRisco();
        }
    }
}
