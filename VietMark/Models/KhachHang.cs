namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonDatHangs = new HashSet<DonDatHang>();
            MaGiamGia_KhachHang = new HashSet<MaGiamGia_KhachHang>();
        }

        [Key]
        public string Id_KhachHang { get; set; }

        [StringLength(500)]
        public string TenKh { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaGiamGia_KhachHang> MaGiamGia_KhachHang { get; set; }
    }
}
