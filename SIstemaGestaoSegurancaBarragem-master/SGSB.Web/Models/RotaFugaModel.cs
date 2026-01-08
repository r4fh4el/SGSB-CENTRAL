using System;

namespace SGSB.Web.Models
{
    public class RotaFugaModel
    {

        public int Id { get; set; }

        public string? Nome { get; set; }

        public int BarragemId { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }

    }
}
