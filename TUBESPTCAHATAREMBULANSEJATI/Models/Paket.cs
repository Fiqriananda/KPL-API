using System;
using static EkspedisiPaketAPI.Models.StatusPengiriman.StatusPengirimanConstant;
namespace ExpedisiPaketAPI.Models
{
    public class Paket //paket
    {
        public int Id { get; set; }
        public string NomerResi { get; set; } = string.Empty;
        public string Pengirim { get; set; } = string.Empty;
        public string Penerima { get; set; } = string.Empty;
        public string AlamatAsal { get; set; } = string.Empty;
        public string AlamatTujuan { get; set; } = string.Empty;
        public double BeratKg { get; set; }
        public string StatusPengiriman { get; set; } = Pending;
        public DateTime TanggalDikirm { get; set; }
        public DateTime? TanggalDiterima { get; set; }
        public string Kota { get; set; } = string.Empty;
        public decimal Biaya { get; set; }
    }
}