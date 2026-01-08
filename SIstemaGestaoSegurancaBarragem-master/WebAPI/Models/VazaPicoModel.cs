using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class VazaPicoModel
    {
        public int Id { get; set; }

        public double? valorVhid { get; set; }

        public double? valorHhid { get; set; }

        public double? valorYmed { get; set; }


        public double? valorAS { get; set; }


        public int BarragemId { get; set; }
    }
}
