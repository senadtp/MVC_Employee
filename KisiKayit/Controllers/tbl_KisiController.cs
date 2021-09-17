using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KisiKayit.Models;

namespace KisiKayit.Controllers
{
    public class tbl_KisiController : Controller
    {
        private KisiKayitEntities db = new KisiKayitEntities();

        // GET: tbl_Kisi
        public ActionResult Index()
        {
            return View(db.tbl_Kisi.ToList());
        }

        // GET: tbl_Kisi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kisi tbl_Kisi = db.tbl_Kisi.Find(id);
            if (tbl_Kisi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kisi);
        }

        // GET: tbl_Kisi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Kisi/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KisiID,KisiAdi,KisiSoyadi,KisiEmail,KisiFotograf")] tbl_Kisi tbl_Kisi)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", " ");
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/Images/KullaniciResimleri/" + DosyaAdi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    tbl_Kisi.KisiFotograf = DosyaAdi + uzanti;
                }
                db.tbl_Kisi.Add(tbl_Kisi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Kisi);
        }

        // GET: tbl_Kisi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kisi tbl_Kisi = db.tbl_Kisi.Find(id);
            if (tbl_Kisi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kisi);
        }

        // POST: tbl_Kisi/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KisiID,KisiAdi,KisiSoyadi,KisiEmail,KisiFotograf")] tbl_Kisi tbl_Kisi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Kisi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Kisi);
        }

        // GET: tbl_Kisi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kisi tbl_Kisi = db.tbl_Kisi.Find(id);
            if (tbl_Kisi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kisi);
        }

        // POST: tbl_Kisi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Kisi tbl_Kisi = db.tbl_Kisi.Find(id);
            db.tbl_Kisi.Remove(tbl_Kisi);
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
