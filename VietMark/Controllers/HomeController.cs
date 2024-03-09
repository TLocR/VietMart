using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VietMark.Models;

namespace VietMark.Controllers
{
    public class HomeController : Controller
    {
        DB_ThuongMaiDienTu context = new DB_ThuongMaiDienTu();
        public ActionResult Index()
        {
            var listSanPham = context.SanPhams.ToList();
            return View(listSanPham);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendFeedBack(string name, string email, int number_phone, string subject, string message)
        {
            var newFeedback = new feedback();
            newFeedback.HoVaTen = name;
            newFeedback.Email = email;
            newFeedback.SDT = number_phone;
            newFeedback.TenChuDe = subject;
            newFeedback.NoiDung = message;
            context.feedbacks.Add(newFeedback);
            context.SaveChanges();
            return View();
        }
        public ActionResult Blog()
        {
            ViewBag.Message = "Your blog page.";

            return View();
        }

        public ActionResult Search(string poscats, string searchString, int? page)
        {            
            int cat_id = int.Parse(poscats);
            var listProduct = new List<SanPham>();
            
            if (searchString == "")
            {
                return RedirectToAction("list_product_by_cat_id", new { cat_id = cat_id });
            }
            else
            {
                if(cat_id == 0)
                {
                    listProduct = context.SanPhams.Where(p => p.TenSP.Contains(searchString)).ToList();
                }
                else
                {                    
                    listProduct = context.SanPhams.Where(p => p.TenSP.Contains(searchString) && p.Id_LoaiSanPham == cat_id).ToList();
                }                               
            }
            int pageSize = 6;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = listProduct.ToPagedList(pageIndex, pageSize);
            ViewBag.cat_id = cat_id;
            return View(result);
        }
        public ActionResult list_product_by_cat_id(string metatitle,int cat_id, int? page)
        {
            var context = new DB_ThuongMaiDienTu();
            var listProduct = new List<SanPham>();
            if (cat_id == 0)
            {
                ViewBag.Cat_Name = "Tất cả sản phẩm";
                listProduct = context.SanPhams.ToList();
            }
            else
            {
                var find = context.LoaiSanPhams.Where(p => p.Id_LoaiSanPham == cat_id).FirstOrDefault();
                ViewBag.Cat_Name = find.TenLoaiSP;
                listProduct = context.SanPhams.Where(p => p.Id_LoaiSanPham == cat_id).ToList();
            }
            int pageSize = 6;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = listProduct.ToPagedList(pageIndex, pageSize);
            ViewBag.cat_id = cat_id;
            return View(result);
        }
        public string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}