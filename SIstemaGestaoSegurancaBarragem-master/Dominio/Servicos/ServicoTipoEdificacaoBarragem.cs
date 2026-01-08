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
    public class ServicoTipoEdificacaoBarragem : IServicoTipoEdificacaoBarragem
    {
        private readonly ITipoEdificacaoBarragem _ITipoEdificacaoBarragem;

        public ServicoTipoEdificacaoBarragem(ITipoEdificacaoBarragem TipoEdificacaoBarragem) 
        {
            _ITipoEdificacaoBarragem = TipoEdificacaoBarragem;
        }

        public async Task AdicionarTipoEdificacaoBarragem(TipoEdificacaoBarragem TipoEdificacaoBarragem)
        {
            var validarNome = TipoEdificacaoBarragem.ValidadePropriedadeSring(TipoEdificacaoBarragem.NomePropriedade , "Nome");

            if (validarNome)
            {
                TipoEdificacaoBarragem.DataAlteracao = DateTime.Now;
                TipoEdificacaoBarragem.DataCadastro = DateTime.Now;
               await _ITipoEdificacaoBarragem.Adicionar(TipoEdificacaoBarragem);
            }
        }

        public async Task AtualizaTipoEdificacaoBarragem(TipoEdificacaoBarragem TipoEdificacaoBarragem)
        {
            var validarNome = TipoEdificacaoBarragem.ValidadePropriedadeSring(TipoEdificacaoBarragem.NomePropriedade, "Nome");

            if (validarNome)
            {
                TipoEdificacaoBarragem.DataAlteracao = DateTime.Now;
                TipoEdificacaoBarragem.DataCadastro = TipoEdificacaoBarragem.DataCadastro;
                await _ITipoEdificacaoBarragem.Atualizar(TipoEdificacaoBarragem);
        }
        }

        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragem()
        {
            return  await _ITipoEdificacaoBarragem.ListarTipoEdificacaoBarragem(n => n.NomePropriedade != null);
        }

        public async Task<List<TipoEdificacaoBarragem>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem)
        {
            return await _ITipoEdificacaoBarragem.ListarTipoEdificacaoBarragemBarragemId(idBarragem);
        }
    }
}
