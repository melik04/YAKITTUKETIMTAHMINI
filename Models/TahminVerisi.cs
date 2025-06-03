namespace YakitTuketimTahmini.Models

{
    public class TahminVerisi
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string KullaniciIP { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public string MotorTipi { get; set; }
        public float? TahminiTuketim { get; set; }
    }
}