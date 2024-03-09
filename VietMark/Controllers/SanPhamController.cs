using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VietMark.Models;

namespace VietMark.Controllers
{
    public class SanPhamController : Controller
    {
        DB_ThuongMaiDienTu context = new DB_ThuongMaiDienTu();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string title, int maSP)
        {
            var findSanPham = context.SanPhams.Where(p => p.Id_SanPham == maSP).FirstOrDefault();
            
            return View(findSanPham);
        }
        

    }
}