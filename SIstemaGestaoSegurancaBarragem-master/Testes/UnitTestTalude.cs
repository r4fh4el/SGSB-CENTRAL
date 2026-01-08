using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;

    using Aplicacao.Interfaces;
    using Aplicacao.Interfaces.Genericos;
    using Dominio.Interfaces;
    using Dominio.Interfaces.InterfaceServicos;
    using Dominio.Servicos;
    using Entidades.Entidades;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace Testes
{
        public class UnitestTalude : IAplicacaoTalude
        {
            // INTERFACE DOMINIO
            private readonly ITalude _iTalude;

            // INTERFACE SERVICO
            private readonly IServicoTalude _iServicoTalude;

            // CONSTRUTOR COM INJEÇÃO DE DEPENDÊNCIA
            public UnitestTalude(ITalude iTalude, IServicoTalude iServicoTalude)
            {
                _iTalude = iTalude;
                _iServicoTalude = iServicoTalude;
            }

            public async Task Adicionar(Talude objeto)
            {
                await _iTalude.Adicionar(objeto);
            }

            // RETORNA DO SERVIÇO
            public async Task AdicionarTalude(Talude talude)
            {
                await _iServicoTalude.AdicionarTalude(talude);
            }

            public async Task Atualizar(Talude objeto)
            {
                await _iTalude.Atualizar(objeto);
            }

            // RETORNA DO SERVIÇO
            public async Task AtualizarTalude(Talude talude)
            {
                await _iServicoTalude.AtualizaTalude(talude);
            }

        public Task AtualizaTalude(Talude talude)
        {
            throw new NotImplementedException();
        }

        public async Task<Talude> BuscarPorId(int id)
            {
                return await _iTalude.BuscarPorId(id);
            }

            public async Task Excluir(Talude objeto)
            {
                await _iTalude.Excluir(objeto);
            }

            public async Task<List<Talude>> Listar()
            {
                return await _iTalude.Listar();
            }

            // RETORNA DO SERVIÇO
            public async Task<List<Talude>> ListarTalude()
            {
                return await _iServicoTalude.ListarTalude();
            }

            public async Task<List<Talude>> ListarTaludeBarragemId(int idBarragem)
            {
                return await _iServicoTalude.ListarTaludeBarragemId(idBarragem);
            }
        }
    }
