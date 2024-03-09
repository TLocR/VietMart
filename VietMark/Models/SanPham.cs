namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTiet_GiamGia_CuaHang = new HashSet<ChiTiet_GiamGia_CuaHang>();
            ChiTiet_GiamGia_KhachHang = new HashSet<ChiTiet_GiamGia_KhachHang>();
            ChiTietAnhSanPhams = new HashSet<ChiTietAnhSanPham>();
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }

        [Key]
        public int Id_SanPham { get; set; }

        [StringLength(100)]
        public string TenSP { get; set; }

        [StringLength(250)]
        public string Mieuta { get; set; }

        public decimal? GiaBanBanDau { get; set; }

        public int? SoLuongTon { get; set; }

        [StringLength(128)]
        public string Id_CuaHang { get; set; }

        public int? Id_LoaiSanPham { get; set; }

        [StringLength(128)]
        public string Image { get; set; }

        public int? PhanTramGiamGia { get; set; }

        [StringLength(128)]
        public string code { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        public DateTime? ThoiGianSua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_GiamGia_CuaHang> ChiTiet_GiamGia_CuaHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_GiamGia_KhachHang> ChiTiet_GiamGia_KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietAnhSanPham> ChiTietAnhSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }

        public virtual CuaHang CuaHang { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
