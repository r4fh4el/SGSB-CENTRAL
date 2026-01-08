using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebAPI.Models
{
    public class SistemaDrenagemBarragemModel
    {

        public int Id { get; set; }
        public int SistemaDrenagemId { get; set; }
        public virtual SistemaDrenagemModel? SistemaDrenagem { get; set; }

        public int BarragemId { get; set; }
        public virtual BarragemModel? Barragem { get; set; }

        public DateTime DataCadastro { get; set; }


        public DateTime DataAlteracao { get; set; }

    }
}
