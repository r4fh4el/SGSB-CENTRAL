using Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGSB.Web.Models
{
    public class BarragemModel
    {
        public BarragemModel() 
        {
            this.DadosGerais = new DadosGeraisViewModel();
            this.DescarregadorFundo = new DescarregadorFundoModel();
            this.TipoEdificacao = new TipoEdificacao();
            this.Reservatorio = new ReservatorioModel();
            this.Talude = new TaludeModel();
            this.TipoEstruturaBarragemModel = new TipoEstruturaBarragemModel();
            this.TipoMaterialBarragemModel = new TipoMaterialBarragemModel();
            this.TomadaDagua = new TomadaDaguaModel();
            this.Instrumentos = new Instrumentos();
            this.SistemaDrenagem = new SistemaDrenagemModel();
            this.CotaAreaVolume = new CotaAreaVolumeModel();
            this.DocumentacaoProjetoConstrucaoOperacao = new DocumentacaoProjetoConstrucaoOperacaoModel();
            this.DocumentacaoPerguntasModel = new DocumentacaoPerguntasModel();
            this.CaracterizacaoAreaJusanteBarragem = new CaracterizacaoAreaJusanteBarragemModel();
            this.UsoBarragem = new UsoBarragemModel();
            this.Inspecoes = new InspecoesModel();
            this.Vertedouro = new VertedouroModel();
            this.Instrumentos = new Instrumentos();
            this.InformacoesComplementares = new InformacoesComplementaresModel();
            this.CondicaoFundacao = new CondicaoFundacao();
           
        }
   
        public string? Latitude { get; set; }

        public string? Longitude { get; set; }
        public int Id { get; set; }

        public string? BaciaHidrograficaAbrangencia { get; set; }
        public string? Nome { get; set; }




        public string? CursoDaguaBarrado { get; set; }


        public string? AnoConclusaoObra { get; set; }


        public string? IdadeBarragem { get; set; }


        public string? TipoFundacao { get; set; }


        public double AlturaMAxima { get; set; }


        public double ComprimentoCoroamento { get; set; }


        public double LarguraCoroamentoBarragem { get; set; }

        public double CotaCoroamentoBarragem { get; set; }

        public int Tipo_Material_Id { get; set; }
        public TipoMaterialBarragemModel TipoMaterialBarragemModel { get; set; }
        public int Tipo_Empreendedor_Id { get; set; }
        public TipoEmpreendedorModel TipoEmpreendedorModel { get; set; }

        public int Tipo_Estrutura_Id { get; set; }
        public TipoEstruturaBarragemModel TipoEstruturaBarragemModel { get; set; }

        public int CondicaoFundacaoId { get; set; }
        public CondicaoFundacao CondicaoFundacao { get; set; }

        public int DadosGeraisId { get; set; }
        public DadosGeraisViewModel DadosGerais { get; set; }

        public int DescarregadorFundoId { get; set; }
        public DescarregadorFundoModel DescarregadorFundo { get; set; }  
        
        public int DocumentacaoPerguntasModelId { get; set; }
        public DocumentacaoPerguntasModel DocumentacaoPerguntasModel { get; set; } 
        
        public int ReservatorioId { get; set; }
        public ReservatorioModel Reservatorio { get; set; }  
        
        public int TaludeId { get; set; }
        public TaludeModel Talude { get; set; }
        
        public int VertedouroId { get; set; }
        public VertedouroModel Vertedouro { get; set; }  
        
        public int TomadaDaguaId { get; set; }
        public TomadaDaguaModel TomadaDagua { get; set; }

        public int InstrumentosId { get; set; }
        public Instrumentos Instrumentos { get; set; }

        public int SistemaDrenagemId { get; set; }
        public SistemaDrenagemModel SistemaDrenagem { get; set; }  
        public int CotaAreaVolumeId { get; set; }
        public CotaAreaVolumeModel CotaAreaVolume { get; set; } 
        public int DocumentacaoProjetoConstrucaoOperacaoId { get; set; }
        public DocumentacaoProjetoConstrucaoOperacaoModel DocumentacaoProjetoConstrucaoOperacao { get; set; }
        public int CaracterizacaoAreaJusanteBarragemId { get; set; }
        public CaracterizacaoAreaJusanteBarragemModel CaracterizacaoAreaJusanteBarragem { get; set; }
        public int UsoBarragemId { get; set; }
        public UsoBarragemModel UsoBarragem { get; set; }

        public int InspecoesId { get; set; }
        public InspecoesModel Inspecoes { get; set; }

        public int InformacoesComplementaresId { get; set; }
        public InformacoesComplementaresModel InformacoesComplementares { get; set; }

        public int TipoEdificacaoId { get; set; }
        public TipoEdificacao TipoEdificacao { get; set; }
    }
}
