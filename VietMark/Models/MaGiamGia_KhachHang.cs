namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaGiamGia_KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaGiamGia_KhachHang()
        {
            ChiTiet_GiamGia_KhachHang = new HashSet<ChiTiet_GiamGia_KhachHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_GiamGia_KhachHang { get; set; }

        [StringLength(128)]
        public string Id_KhachHang { get; set; }

        [StringLength(128)]
        public string TenGiamGia { get; set; }

        public decimal? PhanTramGiamGia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDauGiamGia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThucGiamGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_GiamGia_KhachHang> ChiTiet_GiamGia_KhachHang { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
