namespace ExpedisiPaketAPI.DTOs
{
    public class CreatePaketDto
    {
        public string NomerResi { get; set; } = string.Empty;

        public string Pengirim { get; set; } = string.Empty;

        public string Penerima { get; set; } = string.Empty;

        public string AlamatAsal { get; set; } = string.Empty;

        public string AlamatTujuan { get; set; } = string.Empty;

        public double BeratKg { get; set; }

        public string Kota { get; set; } = string.Empty;

        public decimal Biaya { get; set; }
    }
}
