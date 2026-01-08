using System;

namespace SGSB.Web.Models
{
    public class UsoBarragemBarragemModel
    {
        public int Id { get; set; }

        public int UsoBarragemId { get; set; }
        public virtual UsoBarragemModel? UsoBarragem { get; set; }

        public int BarragemId { get; set; }
        public virtual BarragemModel? Barragem { get; set; }


        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }

    }
}
