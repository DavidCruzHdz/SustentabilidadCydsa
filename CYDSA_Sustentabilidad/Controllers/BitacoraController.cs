using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class BitacoraController : Controller
    {
        Entities db = new Entities();
        // GET: Bitacora
        public ActionResult Bitacora(int IdMes, int IdIndicador)
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 6 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            if (Session["idUsuario"] == null)
            {

                return Redirect("Usuarios/Login");
           }
            else
            {
                var IdPlanta = (int)Session["IdPlanta"];
                var IdAnio = (int)Session["IdAnio"];
                var IdsUsuario = Session["idUsuario"];

                ViewBag.Anios = IdAnio;
                ViewBag.Planta = IdPlanta;

                Entities context = new Entities();
                ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
                ViewBag.dropdownAnio = new SelectList(context.IdsIndicadores.ToList(), "IdsIdIndicadores", "IdsAnio");

                List<SelectListItem> IdsAnio = new List<SelectListItem>();
                var contar = 1;

                foreach (int item in context.IdsMediciones.ToList().Select(x => x.IdsAnio).Distinct().ToList())
                {

                    IdsAnio.Add(new SelectListItem() { Text = item.ToString(), Value = contar.ToString() });
                    contar++;
                }

                ViewBag.dropdownAnio = IdsAnio;

                Entities db = new Entities();
                var idplanta = (int)Session["IdPlanta"];
                var idanio = (int)Session["IdAnio"];
                var a = db.IdsBitacora.Where(x => x.IdsIdIndicador == IdIndicador & x.IdsIdEmpresa == idplanta & x.IdsAnio == idanio & x.IdsMes == IdMes);
                if (a.Count() == 0)
                {
                    ViewBag.indicador = db.IdsCatIndicadores.Find(IdIndicador).IdsDescripcionIndicador;
                    ViewBag.mes = IdMes;
                }

                return View(a);
            }
        }

        // GET: Bitacora/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bitacora/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bitacora/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bitacora/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bitacora/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bitacora/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bitacora/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
