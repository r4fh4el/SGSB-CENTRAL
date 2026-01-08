 using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
    using Dominio.Interfaces.InterfaceServicos;
    using Entidades.Entidades;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

namespace Testes
{
    public class UnitTestDanoPotencialAssociado : IAplicacaoDanoPotencialAssociado
        {
            private readonly IDanoPotencialAssociado _danoPotencialAssociadoRepository;
            private readonly IServicoDanoPotencialAssociado _servicoDanoPotencialAssociado;

            // Construtor com injeção de dependência
            public UnitTestDanoPotencialAssociado(IDanoPotencialAssociado danoPotencialAssociadoRepository, IServicoDanoPotencialAssociado servicoDanoPotencialAssociado)
            {
                _danoPotencialAssociadoRepository = danoPotencialAssociadoRepository;
                _servicoDanoPotencialAssociado = servicoDanoPotencialAssociado;
            }

            public async Task Adicionar(DanoPotencialAssociado danoPotencialAssociado)
            {
                if (danoPotencialAssociado == null) throw new ArgumentNullException(nameof(danoPotencialAssociado));
                await _danoPotencialAssociadoRepository.Adicionar(danoPotencialAssociado);
            }

            public async Task Atualizar(DanoPotencialAssociado danoPotencialAssociado)
            {
                if (danoPotencialAssociado == null) throw new ArgumentNullException(nameof(danoPotencialAssociado));
                await _danoPotencialAssociadoRepository.Atualizar(danoPotencialAssociado);
            }

            public async Task<DanoPotencialAssociado> BuscarPorId(int id)
            {
                return await _danoPotencialAssociadoRepository.BuscarPorId(id);
            }

            public async Task Excluir(DanoPotencialAssociado danoPotencialAssociado)
            {
                if (danoPotencialAssociado == null) throw new ArgumentNullException(nameof(danoPotencialAssociado));
                await _danoPotencialAssociadoRepository.Excluir(danoPotencialAssociado);
            }

            public async Task<List<DanoPotencialAssociado>> Listar()
            {
                return await _danoPotencialAssociadoRepository.Listar();
            }

            // Customizável - retorna do serviço
            public async Task AdicionarDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociado)
            {
                if (danoPotencialAssociado == null) throw new ArgumentNullException(nameof(danoPotencialAssociado));
                await _servicoDanoPotencialAssociado.AdicionarDanoPotencialAssociado(danoPotencialAssociado);
            }

            public async Task AtualizaDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociado)
            {
                if (danoPotencialAssociado == null) throw new ArgumentNullException(nameof(danoPotencialAssociado));
                await _servicoDanoPotencialAssociado.AtualizaDanoPotencialAssociado(danoPotencialAssociado);
            }

            public async Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado()
            {
                return await _servicoDanoPotencialAssociado.ListarDanoPotencialAssociado();
            }

            public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int idBarragem)
            {
                return await _danoPotencialAssociadoRepository.GetDanoPotencialAssociadoBarragemId(idBarragem);
            }

        Task IAplicacaoDanoPotencialAssociado.AdicionarBarragem(DanoPotencialAssociado danoPotencialAssociado)
        {
            throw new NotImplementedException();
        }

        Task IAplicacaoDanoPotencialAssociado.AtualizaBarragem(DanoPotencialAssociado danoPotencialAssociado)
        {
            throw new NotImplementedException();
        }

        Task<List<DanoPotencialAssociado>> IAplicacaoDanoPotencialAssociado.ListarDanoPotencialAssociado()
        {
            throw new NotImplementedException();
        }

        Task<DanoPotencialAssociado> IAplicacaoDanoPotencialAssociado.GetDanoPotencialAssociadoBarragemId(int id)
        {
            throw new NotImplementedException();
        }

        Task IGenericaAplicacao<DanoPotencialAssociado>.Adicionar(DanoPotencialAssociado Objeto)
        {
            throw new NotImplementedException();
        }

        Task IGenericaAplicacao<DanoPotencialAssociado>.Atualizar(DanoPotencialAssociado Objeto)
        {
            throw new NotImplementedException();
        }

        Task IGenericaAplicacao<DanoPotencialAssociado>.Excluir(DanoPotencialAssociado Objeto)
        {
            throw new NotImplementedException();
        }

        Task<DanoPotencialAssociado> IGenericaAplicacao<DanoPotencialAssociado>.BuscarPorId(int Id)
        {
            throw new NotImplementedException();
        }

        Task<List<DanoPotencialAssociado>> IGenericaAplicacao<DanoPotencialAssociado>.Listar()
        {
            throw new NotImplementedException();
        }
    }
    }
