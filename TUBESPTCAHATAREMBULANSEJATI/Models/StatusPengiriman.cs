namespace EkspedisiPaketAPI.Models
{
    public class StatusPengiriman
    {
        public static class StatusPengirimanConstant
        {
            public const string Pending = "PENDING";
            public const string Diproses = "DIPROSES";
            public const string Dikirim = "DIKIRIM";
            public const string Selesai = "SELESAI";
            public const string Batal = "BATAL";
        }
    }
}