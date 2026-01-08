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
    [Table("RESERVATORIO")]
    public class Reservatorio : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///  Capacidade total do reservatório (hm³) 
        /// </summary>
        [Column("DB_CAPACIDADE_TOTAL_RESERVATORIO")]
        public double CapacidadeTotalReservatorio { get; set; }

        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
        /// <summary>
        ///  Volume útil do reservatório (hm³)
        /// </summary>
        [Column("DB_VOLUME_UTIL_RESERVATORIO")]
        public double VolumeUtilReservatorio { get; set; }

        /// <summary>
        ///  Borda Livre (m)
        /// </summary>
        [Column("DB_BORDA_LIVRE")]
        public double BordaLivre { get; set; }

        /// <summary>
        ///  NA Máximo Maximorum (m)
        /// </summary>
        [Column("DB_MAXIMO_MAXIMORUM")]
        public double MaximoMaximorum { get; set; }


        /// <summary>
        ///  NA Máximo Normal (m)
        /// </summary>
        [Column("DB_MAXIMO_NORMAL")]
        public double MaximoNormal{ get; set; }


        /// <summary>
        ///  NA Mínimo Operacional (m)
        /// </summary>
        [Column("DB_MINIMO_OPERACIONAL")]
        public double MinimoOperacional { get; set; }

        /// <summary>
        ///  . Volume morto do reservatório  (hm³)
        /// </summary>
        [Column("DB_VOLUME_MORTO")]
        public double VolumeMorto { get; set; }


        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
