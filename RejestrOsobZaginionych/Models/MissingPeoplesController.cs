using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RejestrOsobZaginionych.Models
{
    public class MissingPeoplesController : Controller
    {
        private BazaOsobEntities db = new BazaOsobEntities();

        // GET: MissingPeoples
        public ActionResult Index(string sortOrder, string szukanie)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nazwisko" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Wiek_desc" : "Wiek";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "MZam_desc" : "Mzam";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "MZni_desc" : "Mzni";
            var osoby = from o in db.MissingPeople
                           select o;
            if (!String.IsNullOrEmpty(szukanie))
            {
                osoby = osoby.Where(s => s.Nazwisko.Contains(szukanie)
                                       || s.Imie.Contains(szukanie));
            }
            switch (sortOrder)
            {
                case "Nazwisko":
                    osoby = osoby.OrderByDescending(s => s.Nazwisko);
                    break;
                case "Wiek":
                    osoby = osoby.OrderBy(s => s.Wiek);
                    break;
                case "MZam_desc":
                    osoby = osoby.OrderByDescending(s => s.MiejsceZamieszkania);
                    break;
                case "MZam":
                    osoby = osoby.OrderBy(s => s.MiejsceZamieszkania);
                    break;
                case "MZni_desc":
                    osoby = osoby.OrderByDescending(s => s.MiejsceZaginiecia);
                    break;
                case "Mzni":
                    osoby = osoby.OrderBy(s => s.MiejsceZaginiecia);
                    break;
                case "Wiek_desc":
                    osoby = osoby.OrderByDescending(s => s.Wiek);
                    break;
                default:
                    osoby = osoby.OrderBy(s => s.Nazwisko);
                    break;
            }
            ViewBag.type = 0;
            return View(osoby.ToList());
        }
        public ActionResult Details2(int id)
        {
            ViewBag.type = 1;
            MissingPeople missingpeople = db.MissingPeople.Find(id);
            if (missingpeople == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details2", missingpeople);
        }
        // GET: MissingPeoples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissingPeople missingPeople = db.MissingPeople.Find(id);
            if (missingPeople == null)
            {
                return HttpNotFound();
            }
            return View(missingPeople);
        }

        // GET: MissingPeoples/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MissingPeoples/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imie,Nazwisko,MiejsceZamieszkania,MiejsceZaginiecia,Wiek,Plec,Opis")] MissingPeople missingPeople,HttpPostedFileBase Zdjecie)
        {
            if (ModelState.IsValid)
            {
                if (Zdjecie != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        Zdjecie.InputStream.CopyTo(memoryStream);
                        missingPeople.Zdjecie = memoryStream.ToArray();
                    }
                }
                else
                {
                    missingPeople.Zdjecie = null;
                }
                db.MissingPeople.Add(missingPeople);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(missingPeople);
        }

        // GET: MissingPeoples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissingPeople missingPeople = db.MissingPeople.Find(id);
            if (missingPeople == null)
            {
                return HttpNotFound();
            }
            return View(missingPeople);
        }

        // POST: MissingPeoples/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,MiejsceZamieszkania,MiejsceZaginiecia,Wiek,Plec,Opis")] MissingPeople missingPeople, HttpPostedFileBase Zdjecie)
        {
            if (ModelState.IsValid)
            {
                if (Zdjecie != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        Zdjecie.InputStream.CopyTo(memoryStream);
                        missingPeople.Zdjecie = memoryStream.ToArray();
                    }
                }
                else
                {
                    missingPeople.Zdjecie = null;
                }
                db.Entry(missingPeople).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(missingPeople);
        }

        // GET: MissingPeoples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissingPeople missingPeople = db.MissingPeople.Find(id);
            if (missingPeople == null)
            {
                return HttpNotFound();
            }
            return View(missingPeople);
        }

        // POST: MissingPeoples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MissingPeople missingPeople = db.MissingPeople.Find(id);
            db.MissingPeople.Remove(missingPeople);
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
