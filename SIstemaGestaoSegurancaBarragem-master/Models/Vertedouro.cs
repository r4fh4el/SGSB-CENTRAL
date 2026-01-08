
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Notificacoes;
namespace Model
{
    [Table("VERTEDOURO")]
    public class Vertedouro : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

  
        public int Id { get; set; }

        [Column("STR_TIPO_ESTRUTURA_EXTRAVASORA")]
        [MaxLength(255)]
        public string? TipoEstruturaExtravasoraPrincipal  { get; set; }
     
        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
      
        [Column("BOOL_VERTEDOURO_CONTROLE")]
        public bool VertedouroControle { get; set; }

        [Column("INT_QUANTIDADE_COMPORTA")]
        public int QuantidadeComportas { get; set; }

        [Column("BOOL_TIPO_ACIONAMENTO_COMPORTAS")]
        public bool TipoAcionamentoComportas { get; set; }

        [Column("STR_LOCALIZACAO")]
        [MaxLength(255)]
        public string? LocalizacaoVertedouro { get; set; }


        [Column("DB_COMPRIMENTO_TOTAL_VERTEDOURO")]
        public double ComprimentoTotalVertedouro { get; set; }

        [Column("DB_LARGURA_TOTAL_VERTEDOURO")]
        public double LarguraTotalVertedouro { get; set; }

        [Column("DB_ALTURA")]
        public double Altura { get; set; }

        [Column("DB_COTA_SOLEIRO")]
        public double CotaSoleiraVetedouro { get; set; }

        [Column("STR_MODALIDADE_DISSIPACAO_ENERGIA")]
        [MaxLength(255)]
        public string? ModalidadeDissipacaoEnergia { get; set; }

        [Column("BOOL_VETEDOURO_AUXILIAR")]
        public bool VetedouroAuxiliar { get; set; }

        [Column("DB_TEMPO_RETORNO_VAZAO")]
        public int TempoRetornoVazaoMaximaProjetoAnos { get; set; }

        [Column("DB_VAZAO")]
        public double VazaoMaximaProjetoVertedor { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
