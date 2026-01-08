
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
    [Table("CARACTERIZACAO_AREA_JUSANTE_BARRAGEM")]
    public class CaracterizacaoAreaJusanteParaBarragens : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("CARACTERIZACAO_AREA_JUSANTE_ID")]
        public int CaracterizacaoAreaJusanteBarragemId { get; set; }
        public virtual CaracterizacaoAreaJusanteBarragem? CaracterizacaoAreaJusanteBarragem { get; set; }
        
        [ForeignKey("BARRAGEM_ID")]
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
