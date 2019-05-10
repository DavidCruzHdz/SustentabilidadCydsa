using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class MontosPorMesController : Controller
    {
        Entities db = new Entities();
        // GET: MontosPorMes
        public ActionResult MontosPorMes(int IdAnio = 0, int IdPlanta = 0)
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 6 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            if (Session["idUsuario"] == null)
            {
                return Redirect("/Usuarios/Login");
                //ViewBag.Message = "Montos";

                //return View();
            }
            else
            {
                IdPlanta = (int)Session["IdPlanta"];
                IdAnio = (int)Session["IdAnio"];

                Entities db = new Entities();
                //var idsplanta = Request["IdPlanta"];
                //var idsanio = Request["IdAnio"];

                //var IdsIdCia = 1;
                var IdsUsuario = (int)Session["idUsuario"];

                ViewBag.Anios = IdAnio;
                ViewBag.Planta = IdPlanta;

                Entities context = new Entities();
                context.SpMensualesPorMontos(IdPlanta, IdAnio, IdsUsuario);
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

                // ViewBag.SelectedItem = IdAnio;
                //ViewBag.SearchStatusList = IdsAnio;
                //return View();

                // var Restdata = db.SpMensualesPorMontos(IdsIdCia, IdPlanta, IdAnio, IdsUsuario); //es el mismo de arriba

                var a = db.IdsMensualesPorMontos.Where(x => x.IdsIdEmpresa == IdPlanta & x.IdsAnio == IdAnio);

                return View(a);

                //return View(db.IdsMensualesPorMontos.ToList());

            }
        }

        // GET: MontosPorMes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MontosPorMes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MontosPorMes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MontosPorMes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MontosPorMes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MontosPorMes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MontosPorMes/Delete/5
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



        [HttpPost]
        public JsonResult Index3(int id = 0, int IdAnio = 0)
        {
            Entities db = new Entities();



            Entities context = new Entities();

            ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
            //ViewBag.dropdownAnio = new SelectList(context.IdsIndicadores.ToList(), "IdsIdIndicadores", "IdsAnio");
            List<SelectListItem> IdsAnio = new List<SelectListItem>();
            var contar = 1;

            foreach (int item in context.IdsMediciones.ToList().Select(x => x.IdsAnio).Distinct().ToList())
            {

                IdsAnio.Add(new SelectListItem() { Text = item.ToString(), Value = contar.ToString() });
                contar++;
            }

            ViewBag.dropdownAnio = IdsAnio;

            //int idcia = 1;
            var a = db.SpMensualesPorMontos(id, IdAnio, (int)Session["idUsuario"]);


            return Json(db.IdsMensualesPorMontos.ToList().Where(x => (x.IdsIdEmpresa == id || id == 0) & (x.IdsAnio == IdAnio || IdAnio == 0)).Select(x => new { IdsIdIndicador = x.IdsIdIndicador, IdsMesEnero = x.IdsMesEnero, IdsMesFebrero = x.IdsMesFebrero, IdsMesMarzo = x.IdsMesMarzo, IdsMesAbril = x.IdsMesAbril, IdsMesMayo = x.IdsMesMayo, IdsMesJunio = x.IdsMesJunio, IdsMesJulio = x.IdsMesJulio, IdsMesAgosto = x.IdsMesAgosto, IdsMesSeptiembre = x.IdsMesSeptiembre, IdsMesOctubre = x.IdsMesOctubre, IdsMesNoviembre = x.IdsMesNoviembre, IdsMesDiciembre = x.IdsMesDiciembre, IdsAnual= x.IdsAnual, descripcion = x.IdsCatIndicadores.IdsDescripcionIndicador}), JsonRequestBehavior.AllowGet);
        }

    }
}
