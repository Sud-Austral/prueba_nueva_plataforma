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
    public class GRAFICOesController : Controller
    {
        private graficosEntities db = new graficosEntities();

        // GET: GRAFICOes
        public ActionResult Index()
        {
            var gRAFICO = db.GRAFICO.Include(g => g.CATEGORIA).Include(g => g.DETALLE).Include(g => g.FUENTE).Include(g => g.PARAMETRO).Include(g => g.RESPONSABLE1).Include(g => g.TEMPORALIDAD).Include(g => g.TERRITORIO).Include(g => g.TIPO_GRAFICO).Include(g => g.UNIDAD_MEDIDA1);
            return View(gRAFICO.ToList());
        }

        // GET: GRAFICOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GRAFICO gRAFICO = db.GRAFICO.Find(id);
            if (gRAFICO == null)
            {
                return HttpNotFound();
            }
            return View(gRAFICO);
        }

        // GET: GRAFICOes/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORIA_id = new SelectList(db.CATEGORIA, "id", "nombre");
            ViewBag.DETALLE_id = new SelectList(db.DETALLE, "id", "nombre");
            ViewBag.FUENTE_id = new SelectList(db.FUENTE, "id", "nombre");
            ViewBag.PARAMETRO_id = new SelectList(db.PARAMETRO, "id", "nombre");
            ViewBag.RESPONSABLE_id = new SelectList(db.RESPONSABLE, "id", "nombre");
            ViewBag.TEMPORALIDAD_id = new SelectList(db.TEMPORALIDAD, "id", "nombre");
            ViewBag.TERRITORIO_id = new SelectList(db.TERRITORIO, "id", "nombre");
            ViewBag.TIPO_GRAFICO_id = new SelectList(db.TIPO_GRAFICO, "id", "nombre");
            ViewBag.UNIDAD_MEDIDA_id = new SelectList(db.UNIDAD_MEDIDA, "id", "nombre");
            return View();
        }

        // POST: GRAFICOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,titulo,subtitulo,tags,url,iso_pais,nivel_administrativo,descripcion_larga,fecha_publicacion,idioma,responsable,shopify,auxiliar,rango_edad,CATEGORIA_id,PARAMETRO_id,DETALLE_id,TERRITORIO_id,TEMPORALIDAD_id,TIPO_GRAFICO_id,FUENTE_id,unidad_medida,tamanio_muestra,caracteristica_especial,auxiliar_1,RESPONSABLE_id,UNIDAD_MEDIDA_id")] GRAFICO gRAFICO)
        {
            if (ModelState.IsValid)
            {
                db.GRAFICO.Add(gRAFICO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CATEGORIA_id = new SelectList(db.CATEGORIA, "id", "nombre", gRAFICO.CATEGORIA_id);
            ViewBag.DETALLE_id = new SelectList(db.DETALLE, "id", "nombre", gRAFICO.DETALLE_id);
            ViewBag.FUENTE_id = new SelectList(db.FUENTE, "id", "nombre", gRAFICO.FUENTE_id);
            ViewBag.PARAMETRO_id = new SelectList(db.PARAMETRO, "id", "nombre", gRAFICO.PARAMETRO_id);
            ViewBag.RESPONSABLE_id = new SelectList(db.RESPONSABLE, "id", "nombre", gRAFICO.RESPONSABLE_id);
            ViewBag.TEMPORALIDAD_id = new SelectList(db.TEMPORALIDAD, "id", "nombre", gRAFICO.TEMPORALIDAD_id);
            ViewBag.TERRITORIO_id = new SelectList(db.TERRITORIO, "id", "nombre", gRAFICO.TERRITORIO_id);
            ViewBag.TIPO_GRAFICO_id = new SelectList(db.TIPO_GRAFICO, "id", "nombre", gRAFICO.TIPO_GRAFICO_id);
            ViewBag.UNIDAD_MEDIDA_id = new SelectList(db.UNIDAD_MEDIDA, "id", "nombre", gRAFICO.UNIDAD_MEDIDA_id);
            return View(gRAFICO);
        }

        // GET: GRAFICOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GRAFICO gRAFICO = db.GRAFICO.Find(id);
            if (gRAFICO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORIA_id = new SelectList(db.CATEGORIA, "id", "nombre", gRAFICO.CATEGORIA_id);
            ViewBag.DETALLE_id = new SelectList(db.DETALLE, "id", "nombre", gRAFICO.DETALLE_id);
            ViewBag.FUENTE_id = new SelectList(db.FUENTE, "id", "nombre", gRAFICO.FUENTE_id);
            ViewBag.PARAMETRO_id = new SelectList(db.PARAMETRO, "id", "nombre", gRAFICO.PARAMETRO_id);
            ViewBag.RESPONSABLE_id = new SelectList(db.RESPONSABLE, "id", "nombre", gRAFICO.RESPONSABLE_id);
            ViewBag.TEMPORALIDAD_id = new SelectList(db.TEMPORALIDAD, "id", "nombre", gRAFICO.TEMPORALIDAD_id);
            ViewBag.TERRITORIO_id = new SelectList(db.TERRITORIO, "id", "nombre", gRAFICO.TERRITORIO_id);
            ViewBag.TIPO_GRAFICO_id = new SelectList(db.TIPO_GRAFICO, "id", "nombre", gRAFICO.TIPO_GRAFICO_id);
            ViewBag.UNIDAD_MEDIDA_id = new SelectList(db.UNIDAD_MEDIDA, "id", "nombre", gRAFICO.UNIDAD_MEDIDA_id);
            return View(gRAFICO);
        }

        // POST: GRAFICOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,titulo,subtitulo,tags,url,iso_pais,nivel_administrativo,descripcion_larga,fecha_publicacion,idioma,responsable,shopify,auxiliar,rango_edad,CATEGORIA_id,PARAMETRO_id,DETALLE_id,TERRITORIO_id,TEMPORALIDAD_id,TIPO_GRAFICO_id,FUENTE_id,unidad_medida,tamanio_muestra,caracteristica_especial,auxiliar_1,RESPONSABLE_id,UNIDAD_MEDIDA_id")] GRAFICO gRAFICO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gRAFICO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORIA_id = new SelectList(db.CATEGORIA, "id", "nombre", gRAFICO.CATEGORIA_id);
            ViewBag.DETALLE_id = new SelectList(db.DETALLE, "id", "nombre", gRAFICO.DETALLE_id);
            ViewBag.FUENTE_id = new SelectList(db.FUENTE, "id", "nombre", gRAFICO.FUENTE_id);
            ViewBag.PARAMETRO_id = new SelectList(db.PARAMETRO, "id", "nombre", gRAFICO.PARAMETRO_id);
            ViewBag.RESPONSABLE_id = new SelectList(db.RESPONSABLE, "id", "nombre", gRAFICO.RESPONSABLE_id);
            ViewBag.TEMPORALIDAD_id = new SelectList(db.TEMPORALIDAD, "id", "nombre", gRAFICO.TEMPORALIDAD_id);
            ViewBag.TERRITORIO_id = new SelectList(db.TERRITORIO, "id", "nombre", gRAFICO.TERRITORIO_id);
            ViewBag.TIPO_GRAFICO_id = new SelectList(db.TIPO_GRAFICO, "id", "nombre", gRAFICO.TIPO_GRAFICO_id);
            ViewBag.UNIDAD_MEDIDA_id = new SelectList(db.UNIDAD_MEDIDA, "id", "nombre", gRAFICO.UNIDAD_MEDIDA_id);
            return View(gRAFICO);
        }

        // GET: GRAFICOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GRAFICO gRAFICO = db.GRAFICO.Find(id);
            if (gRAFICO == null)
            {
                return HttpNotFound();
            }
            return View(gRAFICO);
        }

        // POST: GRAFICOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GRAFICO gRAFICO = db.GRAFICO.Find(id);
            db.GRAFICO.Remove(gRAFICO);
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
