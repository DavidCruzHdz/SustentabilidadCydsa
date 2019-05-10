using CYDSA_Sustentabilidad.ADO;
using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CYDSA_Sustentabilidad.Controllers
{
    public class BitacoraMedicionesController : Controller
    {

        Entities db = new Entities();

        // GET: BitacoraMediciones
        public ActionResult BitacoraMediciones(int IdMes ,int IdIndicador )
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 6 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            if (Session["idUsuario"] == null)
            {
                return Redirect("/Usuarios/Login");
           }
            else
            {
                var IdPlanta = (int)Session["IdPlanta"];
                var IdAnio = (int)Session["IdAnio"];
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



                Entities db = new Entities();
                var idplanta = (int)Session["IdPlanta"];
                var idanio = (int)Session["IdAnio"];
                var a = db.IdsBitacora.Where(x => x.IdsIdIndicador == IdIndicador & x.IdsIdEmpresa == idplanta & x.IdsAnio == idanio & x.IdsMes == IdMes);
                if (a.Count() == 0)
                {
                    ViewBag.indicador = db.IdsCatIndicadores.Find(IdIndicador).IdsDescripcionIndicador;
                    ViewBag.mes = IdMes;
                }


                foreach (var item in a)
                {
                    int? id = item.IdsUsuarioCambio;
                }
                return View(a);
            }
        }
    }


    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }

            return View("~/views/error/_ErrorPage.cshtml");
        }
    }
}