namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaGiamGia_CuaHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaGiamGia_CuaHang()
        {
            ChiTiet_GiamGia_CuaHang = new HashSet<ChiTiet_GiamGia_CuaHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_GiamGiaCuaHang { get; set; }

        [StringLength(128)]
        public string TenGiamGia { get; set; }

        public decimal? PhanTramGiamGia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDauGiamGia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThucGiamGia { get; set; }

        [StringLength(128)]
        public string Id_CuaHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_GiamGia_CuaHang> ChiTiet_GiamGia_CuaHang { get; set; }

        public virtual CuaHang CuaHang { get; set; }
    }
}
