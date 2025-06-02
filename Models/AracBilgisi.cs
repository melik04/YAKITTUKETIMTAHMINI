using System.ComponentModel.DataAnnotations;

namespace YakitTuketimTahmini.Models
{
    public class AracBilgisi
    {
        public int Id { get; set; }

        [Required]
        public string Marka { get; set; }

        [Required]
        public string Model { get; set; }

        public int Yil { get; set; }

        public string MotorTipi { get; set; }

        public string VitesTipi { get; set; }

        public float Agirlik { get; set; }

        public string YakitTipi { get; set; }

        public float? TahminiTuketim { get; set; }
    }
}
