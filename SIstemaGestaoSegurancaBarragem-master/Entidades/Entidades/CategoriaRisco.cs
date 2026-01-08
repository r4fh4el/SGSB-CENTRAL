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
    [Table("CATEGORIA_RISCO")]
    public class CategoriaRisco : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("INT_CT_A")]
        public int CT_A { get; set; }
              
        [Column("INT_CT_B")]
        public int CT_B { get; set; }

        [Column("INT_CT_C")]
        public int CT_C { get; set; }

        [Column("INT_CT_D")]
        public int CT_D { get; set; }

        [Column("INT_CT_E")]
        public int CT_E { get; set; }

        [Column("INT_EC_H")]
        public int EC_H { get; set; }

        [Column("INT_EC_I")]
        public int EC_I { get; set; }


        [Column("INT_EC_J")]
        public int EC_J{ get; set; }


        [Column("INT_EC_L")]
        public int EC_L { get; set; }


        [Column("INT_PS_N")]
        public int PS_N { get; set; }

        [Column("INT_PS_O")]
        public int PS_O { get; set; }

        [Column("INT_PS_P")]
        public int PS_P { get; set; }

        [Column("INT_PS_Q")]
        public int PS_Q{ get; set; }

        [Column("INT_PS_R")]
        public int PS_R{ get; set; }

        [Column("INT_VALOR_TOTAL")]
        public int ValorTotal { get; set; }

        [Column("INT_VALOR_TOTAL_EC")]
        public int ValorTotalEC { get; set; }
     

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
