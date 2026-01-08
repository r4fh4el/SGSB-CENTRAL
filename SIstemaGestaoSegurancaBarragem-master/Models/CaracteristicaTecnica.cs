
using Model.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("CARACTERISTICA_TECNICA")]
    public class CaracteristicaTecnica : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// a) Altura
        /// </summary>
   

        [Column("INT_ALT_Q1")]
        public int ALT_Q1 { get; set; }   
        
        [Column("INT_ALT_Q2")]
        public int ALT_Q2 { get; set; } 


        [Column("INT_ALT_Q3")]
        public int ALT_Q3 { get; set; }


        [Column("INT_ALT_Q4")]
        public int ALT_Q4 { get; set; }

        [Column("INT_ALT_RESPOSTA")]
        public int ALT_Resposta { get; set; }
        /// <summary>
        /// b) Comprimento
        /// </summary>


        [Column("INT_COMP_Q1")]
        public int COMP_Q1 { get; set; }

        [Column("INT_COMP_Q2")]
        public int COMP_Q2 { get; set; }


        [Column("INT_COMP_RESPOSTA")]
        public int COMP_Resposta { get; set; }

        /// <summary>
        /// c) Tipo de Barragem quano ao material de construção
        /// </summary>


        [Column("INT_TBMC_Q1")]
        public int TBMC_Q1 { get; set; }

        [Column("INT_TBMC_Q2")]
        public int TBMC_Q2 { get; set; }

        [Column("INT_TBMC_Q3")]
        public int TBMC_Q3 { get; set; }
        [Column("INT_TBMC_Q4")]
        public int TBMC_Q4 { get; set; }

        [Column("INT_TBMC_RESPOSTA")]
        public int TBMC_Resposta { get; set; }
        /// <summary>
        /// d) Tipo de Fundação
        /// </summary>


        [Column("INT_TF_Q1")]
        public int TF_Q1 { get; set; }

        [Column("INT_TF_Q2")]
        public int TF_Q2 { get; set; }


        [Column("INT_TF_Q3")]
        public int TF_Q3 { get; set; }

        [Column("INT_TF_Q4")]
        public int TF_Q4 { get; set; }


        [Column("INT_TF_Q5")]
        public int TF_Q5 { get; set; }

        [Column("INT_TF_RESPOSTA")]
        public int TF_Resposta { get; set; }

        /// <summary>
        /// e) Idade da Barragem
        /// </summary>


        [Column("INT_IB_Q1")]
        public int IB_Q1 { get; set; }

        [Column("INT_IB_Q2")]
        public int IB_Q2 { get; set; }


        [Column("INT_IB_Q3")]
        public int IB_Q3 { get; set; }

        [Column("INT_IB_Q4")]
        public int IB_Q4 { get; set; }

        [Column("INT_IB_RESPOSTA")]
        public int IB_Resposta { get; set; }
        /// <summary>
        /// f) Vazão de Projeto
        /// </summary>


        [Column("INT_VP_Q1")]
        public int VP_Q1 { get; set; }

        [Column("INT_VP_Q2")]
        public int VP_Q2 { get; set; }


        [Column("INT_VP_Q3")]
        public int VP_Q3 { get; set; }

        [Column("INT_VP_Q4")]
        public int VP_Q4 { get; set; }

        [Column("INT_VP_RESPOSTA")]
        public int VP_Resposta { get; set; }


        [Column("INT_CT_TOTAL")]
        public int CTTotal { get; set; }

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
