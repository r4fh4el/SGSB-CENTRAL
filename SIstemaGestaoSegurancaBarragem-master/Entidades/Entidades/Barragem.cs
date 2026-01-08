using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Barragem : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Nome
        /// </summary>
        [Column("STR_Nome")]
        [MaxLength(255)]
        public string? Nome { get; set; }
        [Column("STR_LATITUDE")]
        [MaxLength(50)]
        public string? Latitude { get; set; }

        [Column("STR_LONGITUDE")]
        [MaxLength(50)]
        public string? Longitude { get; set; }
        /// <summary>
        ///  Bacia Hidrográfica de abrangência
        /// </summary>
        [Column("STR_BACIA_HIDROGRAFICA_ABRANGENCIA")]
        [MaxLength(255)]
        public string? BaciaHidrograficaAbrangencia { get; set; }

        /// <summary>
        ///  Curso d’água barrado 
        /// </summary>
        [Column("STR_CURSO_DAGUA_BARRADO")]
        [MaxLength(255)]
        public string? CursoDaguaBarrado { get; set; }

        /// <summary>
        ///  Ano de conclusão da obra 
        /// </summary>
        [Column("STR_ANO_CONCLUSAO")]
        [MaxLength(255)]
        public string? AnoConclusaoObra { get; set; }

        /// <summary>
        ///  Idade da Barragem  
        /// </summary>
        [Column("STR_IDADE_BARRAGEM")]
        [MaxLength(255)]
        public string? IdadeBarragem { get; set; }

        /// <summary>
        ///  Idade da Barragem  
        /// </summary>
        [Column("STR_TIPO_FUNDACAO")]
        [MaxLength(255)]
        public string? TipoFundacao { get; set; }

        /// <summary>
        ///  Altura máxima da barragem medida do encontro do pé do talude de jusante com o nível do solo até a crista de coroamento do barramento (m) 
        /// </summary>
        [Column("DB_ALTURA_MAXIMA")]
        public double AlturaMAxima { get; set; }

        /// <summary>
        ///  Comprimento do coroamento (m) 
        /// </summary>
        [Column("DB_COMPRIMENTO_COROAMENTO")]
        public double ComprimentoCoroamento { get; set; }

        /// <summary>
        ///   Largura do coroamento da barragem principal (m) 
        /// </summary>
        [Column("DB_LARGURA_COROAMENTO_BARRAGEM")]
        public double LarguraCoroamentoBarragem { get; set; }
        /// <summary>
        ///   Cota do coroamento da barragem principal (m) 
        /// </summary>
        [Column("DB_COTA_COROAMENTO_BARRAGEM")]
        public double CotaCoroamentoBarragem{ get; set; }


        [ForeignKey("TIPO_MATERIAL_ID")]
        [Column(Order = 1)]
        public int Tipo_Material_Id { get; set; }
        [NotMapped]
        public virtual TipoMaterialBarragem? TipoMaterialBarragem { get; set; }


        [ForeignKey("TIPO_ESTRUTURA_ID")]
        [Column(Order = 2)]
        public int Tipo_Estrutura_Id { get; set; }

        [NotMapped]
        public virtual TipoEstruturaBarragem? TipoEstruturaBarragem { get; set; }


        [ForeignKey("CONDICAO_FUNDACAO_ID")]
        [Column(Order = 3)]
        public int CondicaoFundacaoId { get; set; }

        [NotMapped]
        public virtual CondicaoFundacao? CondicaoFundacao { get; set; }
        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }

    }
}
