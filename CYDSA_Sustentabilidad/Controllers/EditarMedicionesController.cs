﻿using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CYDSA_Sustentabilidad.Controllers
{
    public class EditarMedicionesController : Controller
    {
        // GET: EditarMediciones
        public ActionResult ListadoMediciones()
        {
          


            Entities db = new Entities();


      
            return View(db.IdsMediciones.ToList());

        }

        // GET: EditarMediciones/Details/5
        public ActionResult ConsultarMedicion(int id)
        {
            return View();
        }

        // GET: EditarMediciones/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditarMediciones(int id)
        {
            return View();
        }

        // POST: EditarMediciones/Edit/5
        [HttpPost]
        public ActionResult EditarMediciones(int id, FormCollection collection)
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

        // GET: EditarMediciones/Delete/5
        public ActionResult BorrarMediciones(int id)
        {
            return View();
        }

        // POST: EditarMediciones/Delete/5
        [HttpPost]
        public ActionResult BorrarMediciones(int id, FormCollection collection)
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