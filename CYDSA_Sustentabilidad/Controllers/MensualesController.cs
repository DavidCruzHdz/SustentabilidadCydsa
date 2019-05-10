using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class MensualesController : Controller
    {
        // GET: Mensuales
        // GET: IndicadorAnual
        Entities db = new Entities();

        public ActionResult Mensuales(int IdPlanta = 0, int IdAnio = 0)
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 1 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }


            if (Session["idUsuario"] == null)
            {
                return Redirect("/Usuarios/Login");
                //ViewBag.Message = "Mensuales";

                //return View();
            }
            else
            {

                if (Session["IdPlanta"] == null)
                    Session["IdPlanta"] = Session["idPlantaDF"];
                if (Session["IdAnio"] == null)
                    Session["IdAnio"] = System.DateTime.Now.Year;
                Session["modelo"] = null;

                if (IdPlanta > 0)
                {
                    Session["IdPlanta"] = IdPlanta;
                }
                if (IdAnio > 0)
                {
                    Session["IdAnio"] = IdAnio;
                }

                IdPlanta = (int)Session["IdPlanta"];
                IdAnio = (int)Session["IdAnio"];

                //var IdsIdCia = 1;
                var IdsUsuario = (int)Session["idUsuario"];

                Entities context = new Entities();

                if (IdPlanta == 0)
                    IdPlanta = context.IdsPermisosPlantas.Where(x => x.IdsIdUsuario == IdsUsuario && x.IdsPlantaDefault == true).FirstOrDefault().IdsIdEmpresa;
                if (IdAnio == 0)
                    IdAnio = System.DateTime.Now.Year;

                if (db.IdsIndicadores.Where(x => x.IdsIdEmpresa == IdPlanta & x.IdsAnio == IdAnio).Count() > 0)
                    context.SpIndicadoresMonitoreoPorMes3(IdPlanta, IdAnio, IdsUsuario);
                int idUsuario = (int)Session["idUsuario"];
                var b = context.IdsPermisosPlantas.ToList().Where(x => x.IdsIdUsuario == idUsuario);
                foreach (var item in b)
                {
                    item.Empresa = item.IdsCatEmpresas.IdsDescripcionEmpresa;
                }

                ViewBag.dropdownPlanta = new SelectList(b, "IdsIdEmpresa", "Empresa");

                /// ciclo que va contar años
                List<SelectListItem> IdsAnio = new List<SelectListItem>();

                //var contar = 1;
                int AnioActual = DateTime.Now.Year;
                int AnioInicial = AnioActual - 20;

                for (int i = AnioActual; i >= AnioInicial; i--)
                {

                    IdsAnio.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.Anios = IdsAnio;

                ViewBag.dropdownAnio = IdsAnio;
                ViewBag.planta = IdPlanta;
                return View(db.IdsIndicadorAnual.ToList().Where(x => x.IdsIdEmpresa == IdPlanta & x.IdsAnio == IdAnio));
            }
        }
        [HttpPost]
        public ActionResult Mensuales()
        {

            return View();
        }




        [HttpPost]
        public JsonResult Index3(int IdPlanta = 0, int IdAnio = 0, int IdIndicador = 0)
        {


            Session["hola"] = 5;
            Session["idcia"] = 1;
            //Session["IdUsuario"] = 1;

            if (IdPlanta > 0)
            {
                Session["IdPlanta"] = IdPlanta;
            }
            if (IdAnio > 0)
            {
                Session["IdAnio"] = IdAnio;
            }



            IdPlanta = (int)Session["IdPlanta"];
            IdAnio = (int)Session["IdAnio"];
            //IdIndicador = (int)Session["IdIndicador"];

            Entities db = new Entities();

            //int IdsIdCia = 1;
            int IdsUsuario = (int)Session["idUsuario"];

            Entities context = new Entities();


            if (db.IdsMediciones.Where(x => x.IdsIdEmpresa == IdPlanta & x.IdsAnio == IdAnio).Count() > 0)
            {
                //if (db.IdsMediciones.Where(x => x.IdsIdPlanta == IdPlanta & x.IdsAnio == IdAnio & x.IdsMes == 1).Count() > 0)
                //{
                //    db.SpSelIndicadoresCYDSA(IdsIdCia, IdPlanta, 1, IdAnio);
                //}


            }

            if (db.IdsIndicadores.Where(x => x.IdsIdEmpresa == IdPlanta & x.IdsAnio == IdAnio).Count() > 0)
                context.SpIndicadoresMonitoreoPorMes3(IdPlanta, IdAnio, IdsUsuario);

            //var Restdata = db.SpIndicadoresMonitoreoPorMes(IdsIdCia, IdPlanta, IdAnio, IdsUsuario);

            var IdsIdPlanta = int.Parse(Request["IdPlanta"].ToString());
            int IdsAnio2 = int.Parse(Request["IdAnio"].ToString());

            context.SpIndicadoresMonitoreoPorMes3(IdsIdPlanta, IdsAnio2, IdsUsuario);

            ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
            ViewBag.dropdownAnio = new SelectList(context.IdsMediciones.ToList(), "IdsMediciones", "IdsAnio");
            List<SelectListItem> IdsAnio = new List<SelectListItem>();
            var contar = 1;

            foreach (int item in context.IdsMediciones.ToList().Select(x => x.IdsAnio).Distinct().ToList())
            {

                IdsAnio.Add(new SelectListItem() { Text = item.ToString(), Value = contar.ToString() });
                contar++;
            }

            ViewBag.dropdownAnio = IdsAnio;




            //var idPlanta = 0;
            //if(idPlanta!= null)
            //{
            //    idPlanta = ViewBag.Planta;
            //}




            //List<IdsIndicadores> lista = db.IdsIndicadores.Where(a => a.IdsMes > 8).ToList();
            //int? id = 2;  
            // return View(db.IdsIndicadores.ToList().Where(x=>x.IdsIdCia ==3) ) ;/* .Where(x=>x.IdsIdPlanta == idPlanta || idPlanta ==0));*/
            //var a = db.IdsIndicadorAnual.ToList().Where(x => x.IdsIdPlanta == IdPlanta); & (x.IdsAnio == IdAnio || IdAnio == 0));
            return Json(db.IdsIndicadorAnual.ToList().Where(x => (x.IdsIdEmpresa == IdPlanta || IdPlanta == 0) & (x.IdsAnio == IdAnio || IdAnio == 0)).Select(x => new { IdsIdIndicador = x.IdsIdIndicador, IdsMesEnero = x.IdsMesEnero, IdsMesFebrero = x.IdsMesFebrero, IdsMesMarzo = x.IdsMesMarzo, IdsMesAbril = x.IdsMesAbril, IdsMesMayo = x.IdsMesMayo, IdsMesJunio = x.IdsMesJunio, IdsMesJulio = x.IdsMesJulio, IdsMesAgosto = x.IdsMesAgosto, IdsMesSeptiembre = x.IdsMesSeptiembre, IdsMesOctubre = x.IdsMesOctubre, IdsMesNoviembre = x.IdsMesNoviembre, IdsMesDiciembre = x.IdsMesDiciembre, IdPlanta = x.IdsIdEmpresa, IdAnio = x.IdsAnio, IdDescripcion = x.IdsCatIndicadores.IdsDescripcionIndicador }), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult FiltraMedicion(int IdPlanta = 0, int IdAnio = 0)
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

            //List<IdsIndicadores> lista = db.IdsIndicadores.Where(a => a.IdsMes > 8).ToList();

            // return View(db.IdsIndicadores.ToList().Where(x=>x.IdsIdCia ==3) ) ;/* .Where(x=>x.IdsIdPlanta == idPlanta || idPlanta ==0));*/
            var a = db.IdsIndicadorAnual.ToList().Where(x => (x.IdsIdEmpresa == IdPlanta || IdPlanta == 0) & (x.IdsAnio == IdAnio || IdAnio == 0));
            //return Json(db.IdsIndicadorAnual.ToList().Where(x => (x.IdsIdPlanta == IdPlanta || IdPlanta == 0) & (x.IdsAnio == IdAnio || IdAnio == 0)).Select(x => new { IdsIdIndicador = x.IdsIdIndicador, IdsMesEnero = x.IdsMesEnero, IdsMesFebrero = x.IdsMesFebrero, IdsMesMarzo = x.IdsMesMarzo, IdsMesAbril = x.IdsMesAbril, IdsMesMayo = x.IdsMesMayo, IdsMesJunio = x.IdsMesJunio, IdsMesJulio = x.IdsMesJulio, IdsMesAgosto = x.IdsMesAgosto, IdsMesSeptiembre = x.IdsMesSeptiembre, IdsMesOctubre = x.IdsMesOctubre, IdsMesNoviembre = x.IdsMesNoviembre, IdsMesDiciembre = x.IdsMesDiciembre }), JsonRequestBehavior.AllowGet);

            return Json(Redirect(Url.Action("Index", "Home")));

            //return Json(new { NewUrl = @Url.Action("DarkSide") });
        }

    }
}