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
    [Table("VAZAO_PICO")]
    public class VazaPico: Notifica
    {


        public VazaPico() 
        { 
        this.HidrogramaRupturaTriangular = new HidrogramaRupturaTriangular();
        }

        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Column("DB_VHID")]
        public double valorVhid { get; set; }
        
        [Column("DB_HHID")]
        public double valorHhid { get; set; }

        [Column("DB_YMED")]
        public double valorYmed { get; set; }


        [Column("DB_AS")]
        public double valorAS{ get; set; }

        [Column("DB_HBARR")]
        public double valorHbarr { get; set; }

    
        [Column("DB_LARGURA_BARRAGEM")]
        public double valorLarguraBarragem{ get; set; }  
    
        [Column("DB_TEMPO_RUPTURA")]
        public double valorTempoRuptura{ get; set; }

        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
        public virtual Barragem? Barragem { get; set; }

        [ForeignKey("HIDROGRAMA_RUPTURA_TRIANGULAR_ID")]
        [Column(Order = 2)]
        public int HidrogramaRupturaTriangularId { get; set; }
        public virtual HidrogramaRupturaTriangular? HidrogramaRupturaTriangular { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
