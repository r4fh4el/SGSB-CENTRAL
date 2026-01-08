namespace WebAPI.Models
{
    public class CaracterizacaoAreaJusanteBarragemModel
    {
        
      public int Id { get; set; }
        public int BarragemId { get; set; }

        public int DistantciaKm { get; set; }

      
        public int TipoEdificacaoId { get; set; }
        public virtual TipoEdificacaoModel? TipoEdificacao { get; set; }
     

    

    }
}
