using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VietMark.Models;

namespace VietMark.Areas.Admin.Controllers
{
    public class CuaHangsController : Controller
    {
        private DB_ThuongMaiDienTu db = new DB_ThuongMaiDienTu();

        // GET: Admin/CuaHangs
        public ActionResult Index()
        {
            var cuaHangs = db.CuaHangs.Include(c => c.AspNetUser);
            return View(cuaHangs.ToList());
        }

        // GET: Admin/CuaHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = db.CuaHangs.Find(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            return View(cuaHang);
        }

        // GET: Admin/CuaHangs/Create
        public ActionResult Create()
        {
            ViewBag.Id_CuaHang = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Admin/CuaHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_CuaHang,Ten_CuaHang,DiaChi,SoDienThoai")] CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                db.CuaHangs.Add(cuaHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_CuaHang = new SelectList(db.AspNetUsers, "Id", "Email", cuaHang.Id_CuaHang);
            return View(cuaHang);
        }

        // GET: Admin/CuaHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = db.CuaHangs.Find(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_CuaHang = new SelectList(db.AspNetUsers, "Id", "Email", cuaHang.Id_CuaHang);
            return View(cuaHang);
        }

        // POST: Admin/CuaHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_CuaHang,Ten_CuaHang,DiaChi,SoDienThoai")] CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuaHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_CuaHang = new SelectList(db.AspNetUsers, "Id", "Email", cuaHang.Id_CuaHang);
            return View(cuaHang);
        }

        // GET: Admin/CuaHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = db.CuaHangs.Find(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            return View(cuaHang);
        }

        // POST: Admin/CuaHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var cat = db.CuaHangs.Include(c => c.SanPhams).SingleOrDefault(c => c.Id_CuaHang == id);
            db.SanPhams.RemoveRange(cat.SanPhams);
            db.CuaHangs.Remove(cat);
            db.SaveChanges();
            /*CuaHang cuaHang = db.CuaHangs.Find(id);
            db.CuaHangs.Remove(cuaHang);
            db.SaveChanges();*/
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
