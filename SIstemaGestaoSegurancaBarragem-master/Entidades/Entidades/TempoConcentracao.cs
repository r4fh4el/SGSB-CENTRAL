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
    [Table("TEMPO_CONCENTRACAO")]
    public class TempoConcentracao: Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Comprimento do rio principal (L)
        /// </summary>
        [Column("DB_BARRAGEM_ID")]
        public int BarragemId { get; set; }

        /// <summary>
        /// Comprimento do rio principal (L)
        /// </summary>
        [Column("DB_COMPRIMENTO_RIO_PRINCIPAL")]
        public double ComprimentoRioPrincipal_L { get; set; }

        /// <summary>
        /// Declividade da bacia (S)
        /// </summary>
        [Column("DB_DECLIVIDADE_BACIA")]
        public double DeclividadeBacia_S { get; set; }


        /// <summary>
        /// Área de drenagem (A)
        /// </summary>
        [Column("DB_AREA_DRENAGEM")]
        public double AreaDrenagem_A { get; set; }

        /// <summary>
        /// Kirpich (1940)
        /// </summary>
        [Column("DB_RESULTADO_KIRPICH")]
        public double ResultadoKirpich { get; set; }

        /// <summary>
        /// Corps Engineers (1946)
        /// </summary>
        [Column("DB_RESULTADO_CORPS_ENGINEERS")]
        public double ResultadoCorpsEngineers { get; set; }

        /// <summary>
        /// Carter (1961)
        /// </summary>
        [Column("DB_RESULTADO_CARTER")]
        public double ResultadoCarter { get; set; }

        /// <summary>
        /// Dooge (1956)
        /// </summary>
        [Column("DB_RESULTADO_DOOGE")]
        public double ResultadoDooge { get; set; }

        /// <summary>
        /// Ven te Chow (1962)
        /// </summary>
        [Column("DB_RESULTADO_VEN_TE_CHOW")]
        public double ResultadoVenTeChow { get; set; }


        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
