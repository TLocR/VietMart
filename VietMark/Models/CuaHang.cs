namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CuaHang")]
    public partial class CuaHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CuaHang()
        {
            DonDatHangs = new HashSet<DonDatHang>();
            MaGiamGia_CuaHang = new HashSet<MaGiamGia_CuaHang>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public string Id_CuaHang { get; set; }

        [StringLength(128)]
        public string Ten_CuaHang { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string SoDienThoai { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaGiamGia_CuaHang> MaGiamGia_CuaHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
