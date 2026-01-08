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
    [Table("TIPO_EDIFICACAO_BARRAGEM")]
    public class TipoEdificacaoBarragem : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("SISTEMA_DRENAGEM_ID")]
      
        public int TipoEdificacaoId { get; set; }
        public virtual TipoEdificacao? TipoEdificacao { get; set; }

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
