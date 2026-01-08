using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Configuracoes
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {


        }
        
        public DbSet<DanoPotencialAssociado> DanoPotencialAssociado { get; set; }
        public DbSet<TipoEstruturaBarragem> TipoEstruturaBarragem { get; set; }
        public DbSet<DocumentacaoPerguntas> DocumentacaoPerguntas { get; set; }
        public DbSet<TipoEmpreendedor> TipoEmpreendedor { get; set; }
        public DbSet<CondicaoFundacao> CondicaoFundacao { get; set; }
        public DbSet<TipoMaterialBarragem> TipoMaterialBarragem { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<TipoEdificacao> TipoEdificacao { get; set; }
        public DbSet<Barragem> Barragem { get; set; }
        public DbSet<CaracterizacaoAreaJusanteBarragem> CaracterizacaoAreaJusanteBarragem { get; set; }
        public DbSet<CaracterizacaoAreaJusanteParaBarragens> CaracterizacaoAreaJusanteParaBarragens { get; set; }
        public DbSet<CotaAreaVolume> CotaAreaVolume { get; set; }
         public DbSet<DadosGerais> DadosGerais { get; set; }

         public DbSet<DescarregadorFundo> DescarregadorFundo { get; set; }
         public DbSet<DocumentacaoProjetoConstrucaoOperacao> DocumentacaoProjetoConstrucaoOperacao { get; set; }
         public DbSet<InformacoesComplementares> InformacoesComplementares { get; set; }
         public DbSet<Inspecoes> Inspecoes { get; set; }
         public DbSet<Instrumentos> Instrumentos { get; set; }
         public DbSet<InstrumentosBarragem> InstrumentosBarragem { get; set; }
         public DbSet<Reservatorio> Reservatorio { get; set; }
         public DbSet<SistemaDrenagem> SistemaDrenagem { get; set; }
         public DbSet<SistemaDrenagemBarragem> SistemaDrenagemBarragem { get; set; }
         public DbSet<Talude> Talude { get; set; }
         public DbSet<TomadaDagua> TomadaDagua { get; set; }
         public DbSet<UsoBarragem> UsoBarragem { get; set; }
         public DbSet<UsoBarragemBarragem> UsoBarragemBarragem { get; set; }
         public DbSet<Vertedouro> Vertedouro { get; set; }
         public DbSet<UsoSoloPredominante> UsoSoloPredominante { get; set; }
         public DbSet<CaracteristicaBacia> CaracteristicaBacia { get; set; }
         public DbSet<TempoConcentracao> TempoConcentracao { get; set; }
         public DbSet<IndiceCaracterizacaoBH> IndiceCaracterizacaoBH { get; set; }
         public DbSet<VazaPico> VazaPico { get; set; }
         public DbSet<CategoriaRisco> CategoriaRisco { get; set; }
          public DbSet<TempoRuptura> TempoRuptura { get; set; }
          public DbSet<HidrogramaTriangula> HidrogramaTriangula { get; set; }
          public DbSet<HidrogramaParabolico> HidrogramaParabolico { get; set; }
          public DbSet<RotaFuga> RotaFuga { get; set; }
          public DbSet<Ticket> Ticket { get; set; }
          public DbSet<PontoEncontro> PontoEncontro { get; set; }
          public DbSet<TipoEdificacaoBarragem> TipoEdificacaoBarragem { get; set; }
          public DbSet<BarragemKml> BarragemKml { get; set; }
          public DbSet<Zona> Zona { get; set; }

        public string ObterStringConexao()
        {
            // Esta string de conexão é usada apenas como fallback quando não configurada via DI
            // A configuração principal deve vir do appsettings.json através do DbContextOptions
            string strCon = "Data Source=108.181.193.92,15000;Initial Catalog=SGSB_2;Persist Security Info=True;User ID=admin;Password=Rl031139@;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework";
            return strCon;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder); 
            }
        }

        public class ContextoFactory : IDesignTimeDbContextFactory<Contexto>
        {
            public Contexto CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
                // String de conexão para design-time (migrations)
                // Pode ser sobrescrita via variável de ambiente: ConnectionStrings__DefaultConnection
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                    ?? "Data Source=108.181.193.92,15000;Initial Catalog=SGSB_2;Persist Security Info=True;User ID=admin;Password=Rl031139@;TrustServerCertificate=Yes;MultipleActiveResultSets=True;Application Name=EntityFramework";
                
                optionsBuilder.UseSqlServer(connectionString);

                return new Contexto(optionsBuilder.Options);
            }
        }

    }
}
