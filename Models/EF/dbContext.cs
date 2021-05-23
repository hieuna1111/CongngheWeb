using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication.Models.EF
{
    public partial class dbContext : DbContext
    {
        public dbContext()
            : base("name=dbContext")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<ThamGia> ThamGias { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChuDe>()
                .HasMany(e => e.Saches)
                .WithOptional(e => e.ChuDe)
                .HasForeignKey(e => e.MaCD)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .HasForeignKey(e => e.MaDH);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonHangs)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.MaKH)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhaXuatBan>()
                .HasMany(e => e.Saches)
                .WithOptional(e => e.NhaXuatBan)
                .HasForeignKey(e => e.MaNXB)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PhieuNhap>()
                .HasMany(e => e.ChiTietPhieuNhaps)
                .WithRequired(e => e.PhieuNhap)
                .HasForeignKey(e => e.MaPhieuNhap);

            modelBuilder.Entity<Sach>()
                .Property(e => e.AnhBia)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.Sach)
                .HasForeignKey(e => e.MaSach);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietPhieuNhaps)
                .WithRequired(e => e.Sach)
                .HasForeignKey(e => e.MaSach);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ThamGias)
                .WithRequired(e => e.Sach)
                .HasForeignKey(e => e.MaSach);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.ThamGias)
                .WithRequired(e => e.TacGia)
                .HasForeignKey(e => e.MaTG);
        }
    }
}
