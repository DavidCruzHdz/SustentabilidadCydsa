using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            if (Session["idUsuario"] == null)
                return Redirect("Usuarios/Login");

            if (Session["Logeado"] == null)
                Session["Logeado"] = false;
            return View() ;
        }

     
        public ActionResult Index2()
        {
            if (Session["idUsuario"] == null)
                return Redirect("Usuarios/Login");

            return Redirect("Account/Login");
        }
        public ActionResult About()
        {
            if (Session["idUsuario"] == null)
            {
                return Redirect("/Usuarios/Login");
            }
            else
            {
                ViewBag.Message = "Gestión de Sustentabilidad";
                return View();
            }
        }

        public ActionResult Contact()
        {
            if (Session["idUsuario"] == null)
                return Redirect("/Usuarios/Login");
            ViewBag.Message = "Contacto";

            return View();
        }
    }
}