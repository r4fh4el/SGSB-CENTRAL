using Aplicacao.Interfaces;
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
    public class AplicacaoCategoriaRisco : IAplicacaoCategoriaRisco
    {
        // INTERFACE DOMINIO
        ICategoriaRisco _ICategoriaRisco;

        // INTERFACE SERVICO
        IServicoCategoriaRisco _IServicoCategoriaRisco;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoCategoriaRisco(ICategoriaRisco iCategoriaRisco, IServicoCategoriaRisco iServicoCategoriaRisco)
        {
            _ICategoriaRisco = iCategoriaRisco;
            _IServicoCategoriaRisco = iServicoCategoriaRisco;
        }

        public async Task Adicionar(CategoriaRisco Objeto)
        {
           await _ICategoriaRisco.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(CategoriaRisco CategoriaRisco)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarCategoriaRisco(CategoriaRisco CategoriaRisco)
        {
            await _IServicoCategoriaRisco.AdicionarCategoriaRisco(CategoriaRisco);
        }

        public Task AtualizaBarragem(CategoriaRisco CategoriaRisco)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(CategoriaRisco Objeto)
        {
            await _ICategoriaRisco.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaCategoriaRisco(CategoriaRisco CategoriaRisco)
        {
            await _IServicoCategoriaRisco.AtualizaCategoriaRisco(CategoriaRisco);
        }

        public async Task<CategoriaRisco> BuscarPorId(int Id)
        {
            return await _ICategoriaRisco.BuscarPorId(Id);
        }

        public async Task Excluir(CategoriaRisco Objeto)
        {
            await _ICategoriaRisco.Excluir(Objeto);
        }

        public async  Task<List<CategoriaRisco>> Listar()
        {
            return await _ICategoriaRisco.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<CategoriaRisco>> ListarCategoriaRisco()
        {
            return await _IServicoCategoriaRisco.ListarCategoriaRisco();
        }
    }
}
