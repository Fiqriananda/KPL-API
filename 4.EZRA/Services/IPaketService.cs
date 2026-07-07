using ExpedisiPaketAPI.Models;
using ExpedisiPaketAPI.DTOs;

namespace ExpedisiPaketAPI.Services
{
    public interface IPaketService
    {
        Task<List<Paket>> GetAllPaketsAsync();
        Task<Paket?> GetPaketByIdAsync(int id);
        Task<Paket?> GetPaketByNomerResiAsync(string nomerResi);
        Task<Paket> CreatePaketAsync(CreatePaketDto dto);
        Task<Paket?> UpdatePaketAsync(int id, UpdatePaketDto dto);
        Task<bool> DeletePaketAsync(int id);
        Task<List<Paket>> GetPaketByStatusAsync(string status);
        Task<List<Paket>> GetPaketByKotaAsync(string kota);
    }
}
