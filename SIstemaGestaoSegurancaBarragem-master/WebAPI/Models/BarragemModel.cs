namespace WebAPI.Models
{
    public class BarragemModel
    {
        public int Id { get; set; }

        public string? BaciaHidrograficaAbrangencia { get; set; }
        public string? Nome { get; set; }

   
        public string? CursoDaguaBarrado { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? AnoConclusaoObra { get; set; }

       
        public string? IdadeBarragem { get; set; }

       
        //public string? TipoFundacao { get; set; }

      
        public double AlturaMAxima { get; set; }

      
        public double ComprimentoCoroamento { get; set; }

      
        public double LarguraCoroamentoBarragem { get; set; }
       
        public double CotaCoroamentoBarragem { get; set; }

        public int Tipo_Material_Id { get; set; }
        //public TipoMaterialBarragemModel lstTipoMaterialBarragem { get; set; }

        public int Tipo_Estrutura_Id { get; set; }
        //public  TipoEstruturaBarragemModel TipoEstruturaBarragem { get; set; }

        public int CondicaoFundacaoId { get; set; }
        //public  CondicaoFundacaoModel CondicaoFundacao { get; set; }
    }
}
