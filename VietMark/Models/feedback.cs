namespace VietMark.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feedback")]
    public partial class feedback
    {
        public int id { get; set; }

        [StringLength(128)]
        public string HoVaTen { get; set; }

        [StringLength(128)]
        public string Email { get; set; }

        public int? SDT { get; set; }

        [StringLength(128)]
        public string TenChuDe { get; set; }

        [StringLength(500)]
        public string NoiDung { get; set; }
    }
}
