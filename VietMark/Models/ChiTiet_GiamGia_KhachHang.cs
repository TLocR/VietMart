namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChiTiet_GiamGia_KhachHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_SanPham { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_GiamGia_KhachHang { get; set; }

        public decimal? GiaBanSauGiamGia { get; set; }

        public virtual MaGiamGia_KhachHang MaGiamGia_KhachHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
