namespace WebAPI.Models
{
    public class CotaAreaVolumeModel
    {
        public int Id { get; set; }
        public int BarragemId { get; set; }
        public string Cota { get; set; }
        public string VolumeAcumulado { get; set; }
        public string AreaSuperficial { get; set; }
    }
}
