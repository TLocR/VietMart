using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VietMark.Models
{
    public partial class DB_ThuongMaiDienTu : DbContext
    {
        public DB_ThuongMaiDienTu()
            : base("name=DB_ThuongMaiDienTu")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<ChiTiet_GiamGia_CuaHang> ChiTiet_GiamGia_CuaHang { get; set; }
        public virtual DbSet<ChiTiet_GiamGia_KhachHang> ChiTiet_GiamGia_KhachHang { get; set; }
        public virtual DbSet<ChiTietAnhSanPham> ChiTietAnhSanPhams { get; set; }
        public virtual DbSet<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        public virtual DbSet<CuaHang> CuaHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<MaGiamGia_CuaHang> MaGiamGia_CuaHang { get; set; }
        public virtual DbSet<MaGiamGia_KhachHang> MaGiamGia_KhachHang { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasOptional(e => e.CuaHang)
                .WithRequired(e => e.AspNetUser);

            modelBuilder.Entity<AspNetUser>()
                .HasOptional(e => e.KhachHang)
                .WithRequired(e => e.AspNetUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ChiTiet_GiamGia_CuaHang>()
                .Property(e => e.GiaBanSauGiamGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTiet_GiamGia_KhachHang>()
                .Property(e => e.GiaBanSauGiamGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CuaHang>()
                .HasMany(e => e.DonDatHangs)
                .WithOptional(e => e.CuaHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CuaHang>()
                .HasMany(e => e.MaGiamGia_CuaHang)
                .WithOptional(e => e.CuaHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.PhiGiaoHang)
                .HasPrecision(19, 4);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonDatHangs)
                .WithOptional(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.MaGiamGia_KhachHang)
                .WithOptional(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MaGiamGia_CuaHang>()
                .Property(e => e.PhanTramGiamGia)
                .HasPrecision(18, 4);

            modelBuilder.Entity<MaGiamGia_CuaHang>()
                .HasMany(e => e.ChiTiet_GiamGia_CuaHang)
                .WithRequired(e => e.MaGiamGia_CuaHang)
                .HasForeignKey(e => e.Id_GiamGia_CuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaGiamGia_KhachHang>()
                .Property(e => e.PhanTramGiamGia)
                .HasPrecision(18, 4);

            modelBuilder.Entity<MaGiamGia_KhachHang>()
                .HasMany(e => e.ChiTiet_GiamGia_KhachHang)
                .WithRequired(e => e.MaGiamGia_KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaBanBanDau)
                .HasPrecision(19, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietAnhSanPhams)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);
        }
    }
}
