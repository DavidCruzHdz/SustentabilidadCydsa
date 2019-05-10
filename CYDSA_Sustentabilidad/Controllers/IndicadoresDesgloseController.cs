using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class IndicadoresDesgloseController : Controller
    {

        // GET: IndicadoresDesglose
        public ActionResult IndicadoresDesglose(int IdIndicador, int IdAnio = 0, int IdPlanta = 0)
        {
            Entities db = new Entities();
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }


            if (Session["idUsuario"] == null)
            {

                return Redirect("/Usuarios/Login");
            }
            else
            {
                IdPlanta = (int)Session["IdPlanta"];
                IdAnio = (int)Session["IdAnio"];

                var IdsUsuario = (int)Session["idUsuario"];

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

                var a = db.IdsDesgloseIndicadores.Where(x => x.IdsIdIndicador == IdIndicador & x.IdsIdEmpresa == IdPlanta);
                a.FirstOrDefault().IdsAnio = IdPlanta;
                a.FirstOrDefault().IdsIdEmpresa = IdAnio;
                return View(a);
            }
        }

        // GET: IndicadoresDesglose/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IndicadoresDesglose/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndicadoresDesglose/Create
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

        // GET: IndicadoresDesglose/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IndicadoresDesglose/Edit/5
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

        // GET: IndicadoresDesglose/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndicadoresDesglose/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
