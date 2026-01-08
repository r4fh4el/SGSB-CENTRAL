using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class TipoEdificacaoBarragemModel
    {
         public int Id { get; set; }

        public int TipoEdificacaoId { get; set; }
        public virtual TipoEdificacaoModel? TipoEdificacao { get; set; }

        public int BarragemId { get; set; }
        public virtual BarragemModel? Barragem { get; set; }
   public DateTime DataCadastro { get; set; }

         public DateTime DataAlteracao { get; set; }
    }
}
