﻿using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class UnidadesController : Controller
    {
    
        // GET: Indicador
        public ActionResult Index()
        {
            Entities Context = new Entities();

            //List<IdsIndicadores> lista = db.IdsIndicadores.Where(a => a.IdsMes > 8).ToList();

            return View(Context.IdsCatUnidades.ToList());

        }
    }
}