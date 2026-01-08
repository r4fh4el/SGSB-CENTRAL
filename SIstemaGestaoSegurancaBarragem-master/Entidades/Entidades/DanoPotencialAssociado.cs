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
    [Table("DANO_POTENCIAL_ASSOCIADO")]
    public class DanoPotencialAssociado : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// a) Volume Total do Reservatório
        /// </summary>
       

        [Column("INT_VTR_Q1")]
        public int VTR_Q1 { get; set; }   
        
        [Column("INT_VTR_Q2")]
        public int VTR_Q2 { get; set; } 


        [Column("INT_VTR_Q3")]
        public int VTR_Q3 { get; set; }


        [Column("INT_VTR_Q4")]
        public int VTR_Q4 { get; set; }



        [Column("INT_VTR_RESPOSTA")]
        public int VTR_Resposta{ get; set; }


        /// <summary>
        /// b) Potencial de perdas de vidas humanas
        /// </summary>


        [Column("INT_PPV_Q1")]
        public int PPV_Q1 { get; set; }

        [Column("INT_PPV_Q2")]
        public int PPV_Q2 { get; set; }


        [Column("INT_PPV_Q3")]
        public int PPV_Q3 { get; set; }


        [Column("INT_PPV_Q4")]
        public int PPV_Q4 { get; set; }


        [Column("INT_PPV_RESPOSTA")]
        public int PPV_Resposta { get; set; }
        /// <summary>
        /// c) Impacto ambiental
        /// </summary>


        [Column("INT_IA_Q1")]
        public int IA_Q1 { get; set; }

        [Column("INT_IA_Q2")]
        public int IA_Q2 { get; set; }

        [Column("INT_IA_RESPOSTA")]
        public int IA_Resposta { get; set; }

        /// <summary>
        /// d) Impacto sócio-econômico
        /// </summary>


        [Column("INT_ISE_Q1")]
        public int ISE_Q1 { get; set; }

        [Column("INT_ISE_Q2")]
        public int ISE_Q2 { get; set; }


        [Column("INT_ISE_Q3")]
        public int ISE_Q3 { get; set; }

        [Column("INT_ISE_RESPOSTA")]
        public int ISE_Resposta { get; set; }


        [Column("INT_DPA_TOTAL")]
        public int DpaTotal { get; set; }

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
