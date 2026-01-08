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
    [Table("PLANO_SEGURANCA_BARRAGEM")]
    public class PlanoSegurancaBarragem : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// N) Existência De Documentação De Projeto
        /// </summary>


        [Column("INT_EDP_Q1")]
        public int EDP_Q1 { get; set; }   
        
        [Column("INT_EDP_Q2")]
        public int EDP_Q2 { get; set; } 


        [Column("INT_EDP_Q3")]
        public int EDP_Q3 { get; set; }


        [Column("INT_EDP_Q4")]
        public int EDP_Q4 { get; set; }

        [Column("INT_EDP_Q5")]
        public int EDP_Q5 { get; set; }

        [Column("INT_EDP_RESPOSTA")]
        public int EDP_Resposta { get; set; }
        /// <summary>
        /// o) Estrutura organizacional e qualificação técnica dos profissionais da equipe de Segurança de Barragem
        /// </summary>


        [Column("INT_EOQ_Q1")]
        public int EOQ_Q1 { get; set; }

        [Column("INT_EOQ_Q2")]
        public int EOQ_Q2 { get; set; }

        [Column("INT_EOQ_Q3")]
        public int EOQ_Q3 { get; set; }

        [Column("INT_EOQ_RESPOSTA")]
        public int EOQ_Resposta { get; set; }
        /// <summary>
        /// p) Procedimentos de roteiros de inspeções de segurança e de monitoramento
        /// </summary>


        [Column("INT_PRISM_Q1")]
        public int PRISM_Q1 { get; set; }

        [Column("INT_PRISM_Q2")]
        public int PRISM_Q2 { get; set; }

        [Column("INT_PRISM_Q3")]
        public int PRISM_Q3 { get; set; }
        [Column("INT_PRISM_Q4")]
        public int PRISM_Q4 { get; set; }

        [Column("INT_PRISM_RESPOSTA")]
        public int PRISM_Resposta { get; set; }
        /// <summary>
        /// q) Regra operacional dos dispositivos de descarga da barragem
        /// </summary>


        [Column("INT_RODD_Q1")]
        public int RODD_Q1 { get; set; }

        [Column("INT_RODD_Q2")]
        public int RODD_Q2 { get; set; }


        [Column("INT_RODD_RESPOSTA")]
        public int RODD_Resposta { get; set; }


        /// <summary>
        /// r) Relatórios de inspeção de segurança com análise e interpretação
        /// </summary>


        [Column("INT_RISA_Q1")]
        public int RISA_Q1 { get; set; }

        [Column("INT_RISA_Q2")]
        public int RISA_Q2 { get; set; }


        [Column("INT_RISA_Q3")]
        public int RISA_Q3 { get; set; }


        [Column("INT_RISA_RESPOSTA")]
        public int RISA_Resposta { get; set; }


        [Column("INT_PS_TOTAL")]
        public int PSTotal { get; set; }

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
