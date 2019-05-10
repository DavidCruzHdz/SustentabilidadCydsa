using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class IndicadorAnualController : Controller
    {
        // GET: IndicadorAnual
        Entities db = new Entities();

        public ActionResult index()
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            var a = db.IdsIndicadorAnual.ToList();
            return View(a);
        }
        public ActionResult IndicadorAnual()
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }
            var IdsIdPlanta = int.Parse(Request["IdPlanta"].ToString());
                int IdsAnio = int.Parse(Request["Anio"].ToString());
            var IdsUsuario = (int)Session["idUsuario"];

            var Restdata = db.SpIndicadoresMonitoreoPorMes3(IdsIdPlanta, IdsAnio, IdsUsuario);
                return View(db.IdsIndicadorAnual.ToList());
                
        }
    }
}