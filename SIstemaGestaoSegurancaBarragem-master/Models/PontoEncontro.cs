
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Notificacoes;
namespace Model
{
    [Table("PONTO_ENCONTRO")]
    public class PontoEncontro : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }

        [Column("STR_NOME")]
        [MaxLength(255)]
        public string? Nome { get; set; }

        [Column("STR_LATITUDE")]
        public double Latitude { get; set; }

        [Column("STR_LONGITUDE")]
        public double Longitude { get; set; }


        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
