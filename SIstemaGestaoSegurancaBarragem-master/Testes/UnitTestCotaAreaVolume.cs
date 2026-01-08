using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testes
{
    public class UnitTestCotaAreaVolume : IAplicacaoCotaAreaVolume
    {
        // INTERFACE DOMINIO
        private readonly ICotaAreaVolume _iCotaAreaVolume;

        // INTERFACE SERVICO
        private readonly IServicoCotaAreaVolume _iServicoCotaAreaVolume;

        // CONSTRUTOR COM INJEÇÃO DE DEPENDÊNCIA
        public UnitTestCotaAreaVolume(ICotaAreaVolume iCotaAreaVolume, IServicoCotaAreaVolume iServicoCotaAreaVolume)
        {
            _iCotaAreaVolume = iCotaAreaVolume ?? throw new ArgumentNullException(nameof(iCotaAreaVolume));
            _iServicoCotaAreaVolume = iServicoCotaAreaVolume ?? throw new ArgumentNullException(nameof(iServicoCotaAreaVolume));
        }

        public async Task Adicionar(CotaAreaVolume objeto)
        {
            await _iCotaAreaVolume.Adicionar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AdicionarCotaAreaVolume(CotaAreaVolume cotaAreaVolume)
        {
            await _iServicoCotaAreaVolume.AdicionarCotaAreaVolume(cotaAreaVolume);
        }

        public async Task Atualizar(CotaAreaVolume objeto)
        {
            await _iCotaAreaVolume.Atualizar(objeto);
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task AtualizaCotaAreaVolume(CotaAreaVolume cotaAreaVolume)
        {
            await _iServicoCotaAreaVolume.AtualizaCotaAreaVolume(cotaAreaVolume);
        }

        public async Task<CotaAreaVolume> BuscarPorId(int id)
        {
            return await _iCotaAreaVolume.BuscarPorId(id);
        }

        public async Task Excluir(CotaAreaVolume objeto)
        {
            await _iCotaAreaVolume.Excluir(objeto);
        }

        public async Task<List<CotaAreaVolume>> Listar()
        {
            return await _iCotaAreaVolume.Listar();
        }

        // CUSTOMIZÁVEL RETORNA DO SERVIÇO
        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolume()
        {
            return await _iServicoCotaAreaVolume.ListarCotaAreaVolume();
        }

        // Listar por Barragem ID
        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem)
        {
            return await _iServicoCotaAreaVolume.ListarCotaAreaVolumeBarragemId(idBarragem);
        }

        // Métodos a seguir não são necessários e lançam exceções. Remova se não forem utilizados.
    
    }
}
