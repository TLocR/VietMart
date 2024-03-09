using Microsoft.AspNet.Identity;
using System;
using System.Collections;
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
    public class KhachHangsController : Controller
    {
        private DB_ThuongMaiDienTu db = new DB_ThuongMaiDienTu();

        // GET: OwnerStore/KhachHangs
        public ActionResult Index()
        {
            string id_store = User.Identity.GetUserId();
            var list_order = db.DonDatHangs.Where(p => p.CuaHang.Id_CuaHang == id_store).ToList(); // Lấy danh sách đơn hàng từ cửa hàng hiện hành
            
            //lấy ra danh sách id khách hàng từ danh sách đơn hàng
            var array_id_order = new List<String>();
            for(int i = 0; i < list_order.Count; i++)
            {
                array_id_order.Add(list_order[i].Id_KhachHang);
            }
            array_id_order.Distinct().ToList();//Lọc các giá trị trùng lập

            var listKH = new List<KhachHang>();
            for(int i = 0; i< array_id_order.Count; i++)
            {
                var order_id = array_id_order[i];
                var item = db.KhachHangs.Include(k => k.AspNetUser).Where(p => p.Id_KhachHang == order_id).FirstOrDefault();
                listKH.Add(item);
            }        
            var result = listKH.GroupBy(x => x.Id_KhachHang).Select(x => x.First()).ToList();
            return View(result.ToList());
        }

        // GET: OwnerStore/KhachHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: OwnerStore/KhachHangs/Create
        public ActionResult Create()
        {
            ViewBag.Id_KhachHang = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: OwnerStore/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_KhachHang,TenKh,DiaChi,SDT")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_KhachHang = new SelectList(db.AspNetUsers, "Id", "Email", khachHang.Id_KhachHang);
            return View(khachHang);
        }

        // GET: OwnerStore/KhachHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_KhachHang = new SelectList(db.AspNetUsers, "Id", "Email", khachHang.Id_KhachHang);
            return View(khachHang);
        }

        // POST: OwnerStore/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_KhachHang,TenKh,DiaChi,SDT")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_KhachHang = new SelectList(db.AspNetUsers, "Id", "Email", khachHang.Id_KhachHang);
            return View(khachHang);
        }

        // GET: OwnerStore/KhachHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: OwnerStore/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
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
