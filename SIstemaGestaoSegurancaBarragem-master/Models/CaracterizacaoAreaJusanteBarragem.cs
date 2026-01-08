
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
    [Table("CARACTERIZACAO_AREA_JUSANTE")]
    public class CaracterizacaoAreaJusanteBarragem : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("INT_DISTANCIA")]
        public int? DistantciaKm { get; set; }

        [ForeignKey("TIPO_EDIFICACAO")]
        [Column(Order = 2)]
        public int? TipoEdificacaoId { get; set; }
        public virtual TipoEdificacao? TipoEdificacao { get; set; }
        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int? BarragemId { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime? DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime? DataAlteracao { get; set; }

    }
}
