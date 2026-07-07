using ExpedisiPaketAPI.Services;
using ExpedisiPaketAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpedisiPaketAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaketsController : ControllerBase
    {
        private readonly IPaketService _paketService;

        public PaketsController(IPaketService paketService)
        {
            _paketService = paketService;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllPakets()
        {
            try
            {
                var pakets = await _paketService.GetAllPaketsAsync();
                return Ok(new { success = true, data = pakets, count = pakets.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaketById(int id)
        {
            try
            {
                var paket = await _paketService.GetPaketByIdAsync(id);
                if (paket == null)
                    return NotFound(new { success = false, message = "Paket tidak ditemukan" });

                return Ok(new { success = true, data = paket });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("resi/{nomerResi}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPaketByNomerResi(string nomerResi)
        {
            try
            {
                var paket = await _paketService.GetPaketByNomerResiAsync(nomerResi);
                if (paket == null)
                    return NotFound(new { success = false, message = "Paket dengan nomor resi ini tidak ditemukan" });

                return Ok(new { success = true, data = paket });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult> GetPaketByStatus(string status)
        {
            try
            {
                var pakets = await _paketService.GetPaketByStatusAsync(status);
                return Ok(new { success = true, data = pakets, count = pakets.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("kota/{kota}")]
        public async Task<ActionResult> GetPaketByKota(string kota)
        {
            try
            {
                var pakets = await _paketService.GetPaketByKotaAsync(kota);
                return Ok(new { success = true, data = pakets, count = pakets.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreatePaket([FromBody] CreatePaketDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Data tidak valid", errors = ModelState.Values.SelectMany(v => v.Errors) });

                var paket = await _paketService.CreatePaketAsync(dto);
                return CreatedAtAction(nameof(GetPaketById), new { id = paket.Id }, new { success = true, data = paket });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Kurir")]
        public async Task<ActionResult> UpdatePaket(int id, [FromBody] UpdatePaketDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Data tidak valid", errors = ModelState.Values.SelectMany(v => v.Errors) });

                var paket = await _paketService.UpdatePaketAsync(id, dto);
                if (paket == null)
                    return NotFound(new { success = false, message = "Paket tidak ditemukan" });

                return Ok(new { success = true, data = paket });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeletePaket(int id)
        {
            try
            {
                var success = await _paketService.DeletePaketAsync(id);
                if (!success)
                    return NotFound(new { success = false, message = "Paket tidak ditemukan" });

                return Ok(new { success = true, message = "Paket berhasil dihapus" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
