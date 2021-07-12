using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class AGENCIA_INFORMACIONController : Controller
    {
        private agenciaEntities1 db = new agenciaEntities1();

        // GET: AGENCIA_INFORMACION
        public ActionResult Index()
        {
            return View(db.AGENCIA_INFORMACION.ToList());
        }

        // GET: AGENCIA_INFORMACION/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENCIA_INFORMACION aGENCIA_INFORMACION = db.AGENCIA_INFORMACION.Find(id);
            if (aGENCIA_INFORMACION == null)
            {
                return HttpNotFound();
            }
            return View(aGENCIA_INFORMACION);
        }

        // GET: AGENCIA_INFORMACION/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AGENCIA_INFORMACION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,coleccion,sector,tema,contenido,pais,escala,territorio,temporalidad,unidad_medida,fuente,titulo,descripcion_larga,visualizacion,tag,url")] AGENCIA_INFORMACION aGENCIA_INFORMACION)
        {
            if (ModelState.IsValid)
            {
                db.AGENCIA_INFORMACION.Add(aGENCIA_INFORMACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aGENCIA_INFORMACION);
        }

        // GET: AGENCIA_INFORMACION/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENCIA_INFORMACION aGENCIA_INFORMACION = db.AGENCIA_INFORMACION.Find(id);
            if (aGENCIA_INFORMACION == null)
            {
                return HttpNotFound();
            }
            return View(aGENCIA_INFORMACION);
        }

        // POST: AGENCIA_INFORMACION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,coleccion,sector,tema,contenido,pais,escala,territorio,temporalidad,unidad_medida,fuente,titulo,descripcion_larga,visualizacion,tag,url")] AGENCIA_INFORMACION aGENCIA_INFORMACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aGENCIA_INFORMACION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aGENCIA_INFORMACION);
        }

        // GET: AGENCIA_INFORMACION/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENCIA_INFORMACION aGENCIA_INFORMACION = db.AGENCIA_INFORMACION.Find(id);
            if (aGENCIA_INFORMACION == null)
            {
                return HttpNotFound();
            }
            return View(aGENCIA_INFORMACION);
        }

        // POST: AGENCIA_INFORMACION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AGENCIA_INFORMACION aGENCIA_INFORMACION = db.AGENCIA_INFORMACION.Find(id);
            db.AGENCIA_INFORMACION.Remove(aGENCIA_INFORMACION);
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
