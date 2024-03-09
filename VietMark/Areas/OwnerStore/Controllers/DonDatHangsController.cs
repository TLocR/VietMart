using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VietMark.Models;

namespace VietMark.Areas.OwnerStore.Controllers
{
    public class DonDatHangsController : Controller
    {
        private DB_ThuongMaiDienTu db = new DB_ThuongMaiDienTu();

        // GET: OwnerStore/DonDatHangs
        public ActionResult Index()
        {
            string id_store = User.Identity.GetUserId();
            var donDatHangs = db.DonDatHangs.Include(d => d.CuaHang).Include(d => d.KhachHang).Where(p => p.Id_CuaHang == id_store);
            return View(donDatHangs.ToList());
        }

        // GET: OwnerStore/DonDatHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // GET: OwnerStore/DonDatHangs/Create
        public ActionResult Create()
        {
            ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang");
            ViewBag.Id_KhachHang = new SelectList(db.KhachHangs, "Id_KhachHang", "TenKh");
            return View();
        }

        // POST: OwnerStore/DonDatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_DonDatHang,Id_KhachHang,NgayDatHang,NgayGiaoHang,DiaChiGiaoHang,PhiGiaoHang,NgayThanhToan,TinhTrangDonDatHang,Id_CuaHang")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang", donDatHang.Id_CuaHang);
            ViewBag.Id_KhachHang = new SelectList(db.KhachHangs, "Id_KhachHang", "TenKh", donDatHang.Id_KhachHang);
            return View(donDatHang);
        }

        // GET: OwnerStore/DonDatHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang", donDatHang.Id_CuaHang);
            ViewBag.Id_KhachHang = new SelectList(db.KhachHangs, "Id_KhachHang", "TenKh", donDatHang.Id_KhachHang);
            return View(donDatHang);
        }

        // POST: OwnerStore/DonDatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_DonDatHang,Id_KhachHang,NgayDatHang,NgayGiaoHang,DiaChiGiaoHang,PhiGiaoHang,NgayThanhToan,TinhTrangDonDatHang,Id_CuaHang")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_CuaHang = new SelectList(db.CuaHangs, "Id_CuaHang", "Ten_CuaHang", donDatHang.Id_CuaHang);
            ViewBag.Id_KhachHang = new SelectList(db.KhachHangs, "Id_KhachHang", "TenKh", donDatHang.Id_KhachHang);
            return View(donDatHang);
        }

        // GET: OwnerStore/DonDatHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // POST: OwnerStore/DonDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           /* var cat = db.DonDatHangs.Include(c => c.ChiTietDonDatHangs).SingleOrDefault(c => c.Id_DonDatHang == id);
            db.ChiTietDonDatHangs.RemoveRange(cat.ChiTietDonDatHangs);
            db.DonDatHangs.Remove(cat);
            db.SaveChanges();*/
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(donDatHang);
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
