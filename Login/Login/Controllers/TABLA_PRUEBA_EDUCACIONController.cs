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
    public class TABLA_PRUEBA_EDUCACIONController : Controller
    {
        private prueba_educacion1Entities db = new prueba_educacion1Entities();

        // GET: TABLA_PRUEBA_EDUCACION
        public ActionResult Index()
        {
            return View(db.TABLA_PRUEBA_EDUCACION.ToList());
        }

        // GET: TABLA_PRUEBA_EDUCACION/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TABLA_PRUEBA_EDUCACION tABLA_PRUEBA_EDUCACION = db.TABLA_PRUEBA_EDUCACION.Find(id);
            if (tABLA_PRUEBA_EDUCACION == null)
            {
                return HttpNotFound();
            }
            return View(tABLA_PRUEBA_EDUCACION);
        }

        // GET: TABLA_PRUEBA_EDUCACION/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TABLA_PRUEBA_EDUCACION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,coleccion,sector,tema,contenido,pais,escala,territorio,temporalidad,unidad_medida,fuente,titulo,descripcion_larga,visualizacion,tag,url,filtro_url,filtro_integrado,muestra,suscripcion")] TABLA_PRUEBA_EDUCACION tABLA_PRUEBA_EDUCACION)
        {
            if (ModelState.IsValid)
            {
                db.TABLA_PRUEBA_EDUCACION.Add(tABLA_PRUEBA_EDUCACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tABLA_PRUEBA_EDUCACION);
        }

        // GET: TABLA_PRUEBA_EDUCACION/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TABLA_PRUEBA_EDUCACION tABLA_PRUEBA_EDUCACION = db.TABLA_PRUEBA_EDUCACION.Find(id);
            if (tABLA_PRUEBA_EDUCACION == null)
            {
                return HttpNotFound();
            }
            return View(tABLA_PRUEBA_EDUCACION);
        }

        // POST: TABLA_PRUEBA_EDUCACION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,coleccion,sector,tema,contenido,pais,escala,territorio,temporalidad,unidad_medida,fuente,titulo,descripcion_larga,visualizacion,tag,url,filtro_url,filtro_integrado,muestra,suscripcion")] TABLA_PRUEBA_EDUCACION tABLA_PRUEBA_EDUCACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tABLA_PRUEBA_EDUCACION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tABLA_PRUEBA_EDUCACION);
        }

        // GET: TABLA_PRUEBA_EDUCACION/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TABLA_PRUEBA_EDUCACION tABLA_PRUEBA_EDUCACION = db.TABLA_PRUEBA_EDUCACION.Find(id);
            if (tABLA_PRUEBA_EDUCACION == null)
            {
                return HttpNotFound();
            }
            return View(tABLA_PRUEBA_EDUCACION);
        }

        // POST: TABLA_PRUEBA_EDUCACION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TABLA_PRUEBA_EDUCACION tABLA_PRUEBA_EDUCACION = db.TABLA_PRUEBA_EDUCACION.Find(id);
            db.TABLA_PRUEBA_EDUCACION.Remove(tABLA_PRUEBA_EDUCACION);
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
