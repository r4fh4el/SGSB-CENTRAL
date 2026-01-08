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
    [Table("ESTADO_CONVERVACAO")]
    public class EstadoConservacao : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// G) Confiabilidade Das Estruturas Extravasoras 
        /// </summary>


        [Column("INT_CEE_Q1")]
        public int CEE_Q1 { get; set; }   
        
        [Column("INT_CEE_Q2")]
        public int CEE_Q2 { get; set; } 


        [Column("INT_CEE_Q3")]
        public int CEE_Q3 { get; set; }


        [Column("INT_CEE_Q4")]
        public int CEE_Q4 { get; set; }


        [Column("INT_CEE_RESPOSTA")]
        public int CEE_Resposta { get; set; }

        /// <summary>
        /// h) Confiabilidade das Estruturas de Adução
        /// </summary>


        [Column("INT_CEA_Q1")]
        public int CEA_Q1 { get; set; }

        [Column("INT_CEA_Q2")]
        public int CEA_Q2 { get; set; }

        [Column("INT_CEA_Q3")]
        public int CEA_Q3 { get; set; }

        [Column("INT_CEA_RESPOSTA")]
        public int CEA_Resposta { get; set; }


        /// <summary>
        /// i) Percolação
        /// </summary>


        [Column("INT_PERC_Q1")]
        public int PERC_Q1 { get; set; }

        [Column("INT_PERC_Q2")]
        public int PERC_Q2 { get; set; }

        [Column("INT_PERC_Q3")]
        public int PERC_Q3 { get; set; }
        [Column("INT_PERC_Q4")]
        public int PERC_Q4 { get; set; }

        [Column("INT_PERC_RESPOSTA")]
        public int PERC_Resposta { get; set; }

        /// <summary>
        /// j) Deformações e recalques
        /// </summary>


        [Column("INT_DERE_Q1")]
        public int DERE_Q1 { get; set; }

        [Column("INT_DERE_Q2")]
        public int DERE_Q2 { get; set; }


        [Column("INT_DERE_Q3")]
        public int DERE_Q3 { get; set; }

        [Column("INT_DERE_Q4")]
        public int DERE_Q4 { get; set; }


        [Column("INT_DERE_RESPOSTA")]
        public int DERE_Resposta { get; set; }


        /// <summary>
        /// k) Deterioração dos Taludes / Parâmetros (k)
        /// </summary>


        [Column("INT_DETA_Q1")]
        public int DETA_Q1 { get; set; }

        [Column("INT_DETA_Q2")]
        public int DETA_Q2 { get; set; }


        [Column("INT_DETA_Q3")]
        public int DETA_Q3 { get; set; }

        [Column("INT_DETA_Q4")]
        public int DETA_Q4 { get; set; }


        [Column("INT_DETA_RESPOSTA")]
        public int DETA_Resposta { get; set; }

        /// <summary>
        /// l) Eclusa
        /// </summary>


        [Column("INT_ECLU_Q1")]
        public int ECLU_Q1 { get; set; }

        [Column("INT_ECLU_Q2")]
        public int ECLU_Q2 { get; set; }


        [Column("INT_ECLU_Q3")]
        public int ECLU_Q3 { get; set; }

        [Column("INT_ECLU_Q4")]
        public int ECLU_Q4 { get; set; }

        [Column("INT_ECLU_RESPOSTA")]
        public int ECLU_Resposta { get; set; }

        [Column("INT_EC_TOTAL")]
        public int ECTotal { get; set; }

        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
        public virtual Barragem? Barragem { get; set; }


        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
