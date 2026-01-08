using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("DADOS_TECNICOS_BARRAGEM")]
    public class DadosTecnicosBarragem : Notifica
    {

        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("BARRAGEM_ID")]
      
        public int BarragemId { get; set; }
        public virtual Barragem? Barragem { get; set; }

        [ForeignKey("TALUDE_ID")]
   
        public int TaludeId { get; set; }
        public virtual Talude? DadosGerais { get; set; }

        [ForeignKey("RESERVATORIO_ID")]
 
        public int ReservatorioId { get; set; }
        public virtual Reservatorio? DadReservatorioosGerais { get; set; }

        [ForeignKey("TOMADA_DAGUA_ID")]
      
        public int TomadaDaguaId { get; set; }
        public virtual TomadaDagua? TomadaDagua { get; set; }

        [ForeignKey("INSTRUMENTOS_ID")]
     
        public int InstrumentosId { get; set; }
        public virtual Instrumentos? Instrumentos { get; set; }

        [ForeignKey("SISTEMA_DRENAGEM_ID")]
     
        public int SistemaDrenagemId { get; set; }
        public virtual SistemaDrenagem? SistemaDrenagem { get; set; }


        [ForeignKey("COTA_AREA_VOLUME_ID")]
    
        public int CotaAreaVolumeId { get; set; }
        public virtual CotaAreaVolume? CotaAreaVolume { get; set; }


        [ForeignKey("DOCUMENTACAO_PROJETO_ID")]
  
        public int DocumentacaoProjetoConstrucaoOperacaoId { get; set; }
        public virtual DocumentacaoProjetoConstrucaoOperacao? DocumentacaoProjetoConstrucaoOperacao { get; set; }



        [ForeignKey("CARACTERIZACAO_AREA_JUSANTE_ID")]

        public int CaracterizacaoAreaJusanteParaBarragensId { get; set; }
        public virtual CaracterizacaoAreaJusanteParaBarragens? CaracterizacaoAreaJusanteParaBarragens { get; set; }


        [ForeignKey("USO_BARRAGEM_ID")]
    
        public int UsoBarragemId { get; set; }
        public virtual UsoBarragem? UsoBarragem { get; set; }

        [ForeignKey("INSPECAO_ID")]
 
        public int InspecoesId { get; set; }
        public virtual Inspecoes? Inspecoes { get; set; }

        [ForeignKey("INFORMACOES_COMPLEMENTARES_ID")]

        public int InformacoesComplementaresId { get; set; }
        public virtual InformacoesComplementares? InformacoesComplementares { get; set; }


    }

}

