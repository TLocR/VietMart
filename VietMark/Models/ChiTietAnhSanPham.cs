namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietAnhSanPham")]
    public partial class ChiTietAnhSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_image { get; set; }

        public int Id_SanPham { get; set; }

        [Column("Tên ảnh")]
        [Required]
        [StringLength(50)]
        public string Tên_ảnh { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
