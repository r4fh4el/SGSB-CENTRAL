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
    [Table("HIDROGRAMA_RUPTURA_TRIANGULAR")]
    public class HidrogramaRupturaTriangular: Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Column("DB_VOLUME_RESERVATORIOS")]
        public double valorVolumeReservatorio { get; set; }
        
        [Column("DB_QP")]
        public double valorQP { get; set; }

        [Column("DB_TEMPO_PICO")]
        public double valorTempoPico{ get; set; }
       
        [Column("DB_TEMPO_BASE")]
        public double valorTempoBase { get; set; }


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
