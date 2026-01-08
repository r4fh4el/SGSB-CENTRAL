namespace SGSB.AppPopulacao.MVVM.Models
{
    public class PontoEncontroModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int BarragemId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}
