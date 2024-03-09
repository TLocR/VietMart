namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonDatHang")]
    public partial class ChiTietDonDatHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_SanPham { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_DonDatHang { get; set; }

        public double? SoLuong { get; set; }

        public double? GiaTien { get; set; }

        [StringLength(100)]
        public string TinhTrangChiTietDonHang { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
