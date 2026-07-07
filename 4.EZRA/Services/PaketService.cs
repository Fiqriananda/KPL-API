using ExpedisiPaketAPI.Data;
using ExpedisiPaketAPI.Models;
using ExpedisiPaketAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ExpedisiPaketAPI.Services
{
    public class PaketService : IPaketService
    {
        private readonly ExpedisiDbContext _context;

        public PaketService(ExpedisiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paket>> GetAllPaketsAsync()
        {
            return await _context.Pakets.ToListAsync();
        }

        public async Task<Paket?> GetPaketByIdAsync(int id)
        {
            return await _context.Pakets.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paket?> GetPaketByNomerResiAsync(string nomerResi)
        {
            return await _context.Pakets.FirstOrDefaultAsync(p => p.NomerResi == nomerResi);
        }

        public async Task<Paket> CreatePaketAsync(CreatePaketDto dto)
        {
            var paket = new Paket
            {
                NomerResi = dto.NomerResi,
                Pengirim = dto.Pengirim,
                Penerima = dto.Penerima,
                AlamatAsal = dto.AlamatAsal,
                AlamatTujuan = dto.AlamatTujuan,
                BeratKg = dto.BeratKg,
                StatusPengiriman = "PENDING",
                TanggalDikirm = DateTime.UtcNow,
                Kota = dto.Kota,
                Biaya = dto.Biaya
            };

            _context.Pakets.Add(paket);
            await _context.SaveChangesAsync();
            return paket;
        }

        public async Task<Paket?> UpdatePaketAsync(int id, UpdatePaketDto dto)
        {
            var paket = await _context.Pakets.FirstOrDefaultAsync(p => p.Id == id);
            if (paket == null)
                return null;

            paket.Pengirim = dto.Pengirim;
            paket.Penerima = dto.Penerima;
            paket.AlamatAsal = dto.AlamatAsal;
            paket.AlamatTujuan = dto.AlamatTujuan;
            paket.BeratKg = dto.BeratKg;
            paket.StatusPengiriman = dto.StatusPengiriman;
            paket.Kota = dto.Kota;
            paket.Biaya = dto.Biaya;

            if (dto.StatusPengiriman == "DELIVERED")
                paket.TanggalDiterima = DateTime.UtcNow;

            _context.Pakets.Update(paket);
            await _context.SaveChangesAsync();
            return paket;
        }

        public async Task<bool> DeletePaketAsync(int id)
        {
            var paket = await _context.Pakets.FirstOrDefaultAsync(p => p.Id == id);
            if (paket == null)
                return false;

            _context.Pakets.Remove(paket);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Paket>> GetPaketByStatusAsync(string status)
        {
            return await _context.Pakets.Where(p => p.StatusPengiriman == status).ToListAsync();
        }

        public async Task<List<Paket>> GetPaketByKotaAsync(string kota)
        {
            return await _context.Pakets.Where(p => p.Kota == kota).ToListAsync();
        }
    }
}
