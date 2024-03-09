namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }

        [Key]
        public int Id_DonDatHang { get; set; }

        [StringLength(128)]
        public string Id_KhachHang { get; set; }

        public DateTime? NgayDatHang { get; set; }

        public DateTime? NgayGiaoHang { get; set; }

        [StringLength(500)]
        public string DiaChiGiaoHang { get; set; }

        public decimal? PhiGiaoHang { get; set; }

        [StringLength(100)]
        public string TinhTrangDonDatHang { get; set; }

        [StringLength(128)]
        public string Id_CuaHang { get; set; }

        [StringLength(128)]
        public string HinhThucThanhToan { get; set; }

        [StringLength(128)]
        public string TenNguoiNhan { get; set; }

        [StringLength(128)]
        public string SDTNguoiNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }

        public virtual CuaHang CuaHang { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
