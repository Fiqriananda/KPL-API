using ExpedisiPaketAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpedisiPaketAPI.Data //expedisi
{
    public class ExpedisiDbContext : IdentityDbContext<IdentityUser>
    {
        public ExpedisiDbContext(DbContextOptions<ExpedisiDbContext> options) : base(options)
        {
        }

        public DbSet<Paket> Pakets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paket>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.NomerResi).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Pengirim).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Penerima).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AlamatAsal).IsRequired().HasMaxLength(255);
                entity.Property(e => e.AlamatTujuan).IsRequired().HasMaxLength(255);
                entity.Property(e => e.BeratKg).HasPrecision(10, 2);
                entity.Property(e => e.StatusPengiriman).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TanggalDikirm).IsRequired();
                entity.Property(e => e.TanggalDiterima);
                entity.Property(e => e.Kota).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Biaya).HasPrecision(12, 2);

                entity.HasIndex(e => e.NomerResi).IsUnique();
            });
        }
    }
}
