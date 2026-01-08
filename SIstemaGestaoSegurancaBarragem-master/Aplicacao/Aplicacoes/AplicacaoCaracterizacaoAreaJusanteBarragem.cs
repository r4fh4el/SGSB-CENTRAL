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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoCaracterizacaoAreaJusanteBarragem : IAplicacaoCaracterizacaoAreaJusanteBarragem
    {
        // INTERFACE DOMINIO
        ICaracterizacaoAreaJusanteBarragem _ICaracterizacaoAreaJusanteBarragem;

        // INTERFACE SERVICO
        IServicoCaracterizacaoAreaJusanteBarragem _IServicoCaracterizacaoAreaJusanteBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoCaracterizacaoAreaJusanteBarragem(ICaracterizacaoAreaJusanteBarragem iCaracterizacaoAreaJusanteBarragem, IServicoCaracterizacaoAreaJusanteBarragem iServicoCaracterizacaoAreaJusanteBarragem)
        {
            _ICaracterizacaoAreaJusanteBarragem = iCaracterizacaoAreaJusanteBarragem;
            _IServicoCaracterizacaoAreaJusanteBarragem = iServicoCaracterizacaoAreaJusanteBarragem;
        }

        public async Task Adicionar(CaracterizacaoAreaJusanteBarragem Objeto)
        {
           await _ICaracterizacaoAreaJusanteBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem)
        {
            await _IServicoCaracterizacaoAreaJusanteBarragem.AdicionarCaracterizacaoAreaJusanteBarragem(caracterizacaoAreaJusanteBarragem);
        }

        public async Task Atualizar(CaracterizacaoAreaJusanteBarragem Objeto)
        {
            await _ICaracterizacaoAreaJusanteBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem)
        {
            await _IServicoCaracterizacaoAreaJusanteBarragem.AtualizaCaracterizacaoAreaJusanteBarragem(caracterizacaoAreaJusanteBarragem);
        }

        public async Task<CaracterizacaoAreaJusanteBarragem> BuscarPorId(int Id)
        {
            return await _ICaracterizacaoAreaJusanteBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(CaracterizacaoAreaJusanteBarragem Objeto)
        {
            await _ICaracterizacaoAreaJusanteBarragem.Excluir(Objeto);
        }

        public async  Task<List<CaracterizacaoAreaJusanteBarragem>> Listar()
        {
            return await _ICaracterizacaoAreaJusanteBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragem()
        {
            return await _IServicoCaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragem();
        }

        public Task Adicionar(AplicacaoCaracterizacaoAreaJusanteBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoCaracterizacaoAreaJusanteBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoCaracterizacaoAreaJusanteBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragemBarragemId(int idBarragem)
        {
            return await _IServicoCaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragemBarragemId(idBarragem);
        }
    }
}
