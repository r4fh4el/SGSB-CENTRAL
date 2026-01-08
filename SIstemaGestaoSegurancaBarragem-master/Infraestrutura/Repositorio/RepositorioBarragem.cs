using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioBarragem : RepositorioGenerico<Barragem>, IBarragem
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;
        public RepositorioBarragem()
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Barragem>> ListarBarragem(Expression<Func<Barragem, bool>> exBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Barragem.Where(exBarragem).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Barragem>> BuscarListaPorIdBarragem(int id)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                return await banco.Barragem.Where(x => x.Id == id).AsNoTracking().ToListAsync();
            }
        }


        public async Task<bool> DeletarBarragemRelacionais(int idBarragem)
        {
            using (var banco = new Contexto(_optionsbuilder))
            {
                var lstDadosGerais = await banco.DadosGerais.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstDadosGerais)
                {
                    await new RepositorioDadosGerais().Excluir(item);
                }

                var lstTalude = await banco.Talude.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstTalude)
                {
                    await new RepositorioTalude().Excluir(item);
                }

                var lstReservatorio = await banco.Reservatorio.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstReservatorio)
                {
                    await new RepositorioReservatorio().Excluir(item);
                }

                var lstVertedouro = await banco.Vertedouro.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstVertedouro)
                {
                    await new RepositorioVertedouro().Excluir(item);
                }

                var lstDescarregadorFundo = await banco.DescarregadorFundo.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstDescarregadorFundo)
                {
                    await new RepositorioDescarregadorFundo().Excluir(item);
                }


                var lstInformacoesComplementares = await banco.InformacoesComplementares.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstInformacoesComplementares)
                {
                    await new RepositorioInformacoesComplementares().Excluir(item);
                }



                var lstInspecoes = await banco.Inspecoes.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstInspecoes)
                {
                    await new RepositorioInspecoes().Excluir(item);
                }


                var lstUsoBarragem = await banco.UsoBarragem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstUsoBarragem)
                {
                    await new RepositorioUsoBarragem().Excluir(item);
                }


                var lstCaracterizacaoAreaJusanteBarragem = await banco.CaracterizacaoAreaJusanteBarragem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstCaracterizacaoAreaJusanteBarragem)
                {
                    await new RepositorioCaracterizacaoAreaJusanteBarragem().Excluir(item);
                }

                
                var lstCotaAreaVolume = await banco.CotaAreaVolume.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstCotaAreaVolume)
                {
                    await new RepositorioCotaAreaVolume().Excluir(item);
                }

                
                var lstTomadaDagua = await banco.TomadaDagua.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstTomadaDagua)
                {
                    await new RepositorioTomadaDagua().Excluir(item);
                }
                
                var lstSistemaDrenagem = await banco.SistemaDrenagem.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstSistemaDrenagem)
                {
                    await new RepositorioSistemaDrenagem().Excluir(item);
                }

                    var lstInstrumentos = await banco.Instrumentos.Where(x => x.BarragemId == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstInstrumentos)
                {
                    await new RepositorioInstrumentos().Excluir(item);
                }
                var lstBarragem = await banco.Barragem.Where(x => x.Id == idBarragem).AsNoTracking().ToListAsync();

                foreach (var item in lstBarragem)
                {
                    await new RepositorioBarragem().Excluir(item);
                }

                return true;
            }
        }
    }
}
