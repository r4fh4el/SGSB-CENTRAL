using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testes
{
    public class UnitTestCaracterizacaoAreaJusanteBarragem : IAplicacaoCaracterizacaoAreaJusanteBarragem
    {
        // INTERFACE DOMINIO
        private readonly ICaracterizacaoAreaJusanteBarragem _ICaracterizacaoAreaJusanteBarragem;

        // INTERFACE SERVICO
        private readonly IServicoCaracterizacaoAreaJusanteBarragem _IServicoCaracterizacaoAreaJusanteBarragem;

        // CONSTRUTOR COM INJEÇÃO DE DEPENDÊNCIA
        public UnitTestCaracterizacaoAreaJusanteBarragem(
            ICaracterizacaoAreaJusanteBarragem iCaracterizacaoAreaJusanteBarragem,
            IServicoCaracterizacaoAreaJusanteBarragem iServicoCaracterizacaoAreaJusanteBarragem)
        {
            _ICaracterizacaoAreaJusanteBarragem = iCaracterizacaoAreaJusanteBarragem ?? throw new ArgumentNullException(nameof(iCaracterizacaoAreaJusanteBarragem));
            _IServicoCaracterizacaoAreaJusanteBarragem = iServicoCaracterizacaoAreaJusanteBarragem ?? throw new ArgumentNullException(nameof(iServicoCaracterizacaoAreaJusanteBarragem));
        }

        public async Task Adicionar(CaracterizacaoAreaJusanteBarragem objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            await _ICaracterizacaoAreaJusanteBarragem.Adicionar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AdicionarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem)
        {
            if (caracterizacaoAreaJusanteBarragem == null) throw new ArgumentNullException(nameof(caracterizacaoAreaJusanteBarragem));
            await _IServicoCaracterizacaoAreaJusanteBarragem.AdicionarCaracterizacaoAreaJusanteBarragem(caracterizacaoAreaJusanteBarragem);
        }

        public async Task Atualizar(CaracterizacaoAreaJusanteBarragem objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            await _ICaracterizacaoAreaJusanteBarragem.Atualizar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AtualizaCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem)
        {
            if (caracterizacaoAreaJusanteBarragem == null) throw new ArgumentNullException(nameof(caracterizacaoAreaJusanteBarragem));
            await _IServicoCaracterizacaoAreaJusanteBarragem.AtualizaCaracterizacaoAreaJusanteBarragem(caracterizacaoAreaJusanteBarragem);
        }

        public async Task<CaracterizacaoAreaJusanteBarragem> BuscarPorId(int id)
        {
            return await _ICaracterizacaoAreaJusanteBarragem.BuscarPorId(id);
        }

        public async Task Excluir(CaracterizacaoAreaJusanteBarragem objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            await _ICaracterizacaoAreaJusanteBarragem.Excluir(objeto);
        }

        public async Task<List<CaracterizacaoAreaJusanteBarragem>> Listar()
        {
            return await _ICaracterizacaoAreaJusanteBarragem.Listar();
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragem()
        {
            return await _IServicoCaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragem();
        }

        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragemBarragemId(int idBarragem)
        {
            return await _IServicoCaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragemBarragemId(idBarragem);
        }
    }
}
