using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VietMark.Models;

namespace VietMark.Areas.OwnerStore.Controllers
{
    public class SanPhamsController : Controller
    {
        private DB_ThuongMaiDienTu db = new DB_ThuongMaiDienTu();

        // GET: OwnerStore/SanPhams
        public ActionResult Index()
        {        
            string id_store = User.Identity.GetUserId();
            var sanPhams = db.SanPhams.Include(s => s.CuaHang).Include(s => s.LoaiSanPham).Where(s => s.Id_CuaHang == id_store);
            return View(sanPhams.ToList());
        }

        // GET: OwnerStore/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: OwnerStore/SanPhams/Create
        public ActionResult Create()
        {
            /*ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang");*/
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id_LoaiSanPham", "TenLoaiSP");
            return View();
        }

        // POST: OwnerStore/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_SanPham,TenSP,code,Mieuta,GiaBanBanDau,SoLuongTon,Id_LoaiSanPham,PhanTramGiamGia")] SanPham sanPham, HttpPostedFileBase image)
        {
            if(image != null && image.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(image.FileName);
                string urlImage = Server.MapPath("~/Content/img/products/" + fileName);
                image.SaveAs(urlImage);

                sanPham.Image = "/Content/img/products/" + fileName;    

            }

            if (ModelState.IsValid)
            {
                sanPham.ThoiGianTao = DateTime.Now;
                sanPham.ThoiGianSua = DateTime.Now;
                sanPham.Id_CuaHang = User.Identity.GetUserId();
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           /* ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang", sanPham.Id_CuaHang);*/
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id_LoaiSanPham", "TenLoaiSP", sanPham.Id_LoaiSanPham);
            return View(sanPham);
        }

        // GET: OwnerStore/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            /*ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang", sanPham.Id_CuaHang);*/
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id_LoaiSanPham", "TenLoaiSP", sanPham.Id_LoaiSanPham);
            return View(sanPham);
        }

        // POST: OwnerStore/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_SanPham,TenSP,code,Mieuta,GiaBanBanDau,SoLuongTon,Id_LoaiSanPham,PhanTramGiamGia")] SanPham sanPham, HttpPostedFileBase editImage)
        {
            sanPham.Id_CuaHang = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {                              
                SanPham modifySanPham = db.SanPhams.Find(sanPham.Id_SanPham);                
                if (modifySanPham != null)
                {
                    if (editImage != null && editImage.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(editImage.FileName);
                        string urlImage = Server.MapPath("~/Content/img/products/" + fileName);
                        editImage.SaveAs(urlImage);

                        modifySanPham.Image = "/Content/img/products/" + fileName;
                        
                    }
                }
               
                db.Entry(modifySanPham).State = EntityState.Modified;
                modifySanPham.TenSP = sanPham.TenSP;
                modifySanPham.code = sanPham.code;
                modifySanPham.Mieuta = sanPham.Mieuta;
                modifySanPham.GiaBanBanDau = sanPham.GiaBanBanDau;
                modifySanPham.SoLuongTon = sanPham.SoLuongTon;
                modifySanPham.Id_LoaiSanPham = sanPham.Id_LoaiSanPham;
                modifySanPham.PhanTramGiamGia = sanPham.PhanTramGiamGia;
                modifySanPham.ThoiGianSua = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            /*ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang", sanPham.Id_CuaHang);*/
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id_LoaiSanPham", "TenLoaiSP", sanPham.Id_LoaiSanPham);
            return View(sanPham);
        }

        // GET: OwnerStore/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: OwnerStore/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
