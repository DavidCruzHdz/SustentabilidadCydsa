using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class ElementosController : Controller
    {
        // GET: Indicador
        public ActionResult Index()
        {
            Entities Context = new Entities();

            return View(Context.IdsCatElementos.ToList());

        }
    }
}