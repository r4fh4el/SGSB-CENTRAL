
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
    [Table("DADOS_GERAl")]
    public class DadosGerais : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("STR_NOME_EMPREENDEDOR")]
        [MaxLength(255)]
        public string? NomeEmpreendedor { get; set; }

        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }

        [Column("INT_TIPO_EMPREENDEDOR")]
        public int TipoEmpreendedor { get; set; }

        [Column("STR_ENDERECO_EMPREENDEDOR")]
        [MaxLength(255)]
        public string? EnderecoEmpreendedor { get; set; }
       
        [Column("STR_RESPONSAVEL_EMPREENDEDOR")]
        [MaxLength(255)]
        public string? ResponsavelEmpreendedor { get; set; }
        
        [Column("STR_EMAIL_EMPREENDEDOR")]
        [MaxLength(255)]
        public string? EmailEmpreendedor { get; set; }

        [Column("STR_TELEFONE_EMPREENDEDOR")]
        [MaxLength(50)]
        public string? TelefoneEmpreendedor { get; set; }
      
        [Column("INT_QUANTIDADE_BARRAGEM")]
        public int QuantidadeBarragem { get; set; }

        [Column("STR_LATITUDE_EMPREENDEDOR")]
        [MaxLength(50)]
        public string? Latitude { get; set; }

        [Column("STR_LONGITUDE_EMPREENDEDOR")]
        [MaxLength(50)]
        public string? Longitude { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
