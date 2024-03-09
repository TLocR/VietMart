using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VietMark.Models
{
    public class CartItem
    {
        public int MaSP { get; set; }

        [DisplayName("Tên Sản Phẩm")]
        public string TenSP { get; set; }
        public int SoLuongTon { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public string Id_CuaHang { get; set; }
        public decimal Money
        {
            get
            {
                return Quantity * Price;
            }
        }
    }
}