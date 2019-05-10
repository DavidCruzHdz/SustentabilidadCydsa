using CYDSA_Sustentabilidad.ADO;
using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class ListarMedicionesController : Controller
    {
        // G.ñlET: ListarMediciones
        public ActionResult ListaMedicion( int IdMes=0,int IdIndicador=0)
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
                //ViewBag.Message = "Contacto";
                //return View();
            }
            else
            {


                //Entities sp = new Entities();
                if (IdMes > 0)
                {
                    Session["LIdMes"] = IdMes;
                    Session["LIdIndicador"] = IdIndicador;
                }
             

                //sp.SpSelIndicadoresCYDSA(1, IdPlanta, IdMes, IdAnio);

                //ViewData["IdPlanta"] = IdPlanta;
                //ViewData["IdAnio"] = IdAnio;
                //ViewData["IdMes"] = IdMes;
                if (IdIndicador > 0)
                    Session["IdIndicador"] = IdIndicador;

                var VarPlanta = (int)@Session["IdPlanta"];
                var veranio = (int)@Session["IdAnio"];
                //Test
                var aaaa = (int)Session["IdIndicador"];


                //var varIndicador = (int)@Session["IdIndicador"];

                var a = db.IdsMediciones.ToList().Where(x => (x.IdsIdEmpresa == (int)@Session["IdPlanta"]) & (x.IdsAnio == (int)@Session["IdAnio"]) & (x.IdsMes == (int)Session["LIdMes"]) & (x.IdsIdIndicador == (int)@Session["IdIndicador"]));

                if (a.Count() == 0)
                {
                    ViewBag.indicador = db.IdsCatIndicadores.Find(IdIndicador).IdsDescripcionIndicador;
                    ViewBag.mes = IdMes;
                }

                return View(a);
            }
        }

        // GET: ListarMediciones/Details/5
        public ActionResult ConsultaMedicion(int id)
        {
            return View();
        }
        

        // GET: ListarMediciones/Edit/5
        public ActionResult EditMedicion(int id, int IdPlanta = 0, int IdAnio = 0, int IdMes = 0, int IdIndicador = 0)
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
                //ViewBag.Message = "Contacto";
                //return View();
            }
            else
            {
                Entities context = new Entities();

                List<SelectListItem> IdsAnio = new List<SelectListItem>()
            {
                 new SelectListItem { Text = "2018", Value = "2018" },
                 new SelectListItem { Text = "2017", Value = "2017" },
                 new SelectListItem { Text = "2016", Value = "2016" },
                 new SelectListItem { Text = "2015", Value = "2015" },
                 new SelectListItem { Text = "2014", Value = "2014" },
                 new SelectListItem { Text = "2013", Value = "2013" },
                 new SelectListItem { Text = "2012", Value = "2012" },
                 new SelectListItem { Text = "2011", Value = "2011" },
                 new SelectListItem { Text = "2010", Value = "2010" },
                 new SelectListItem { Text = "2009", Value = "2009" },
                 new SelectListItem { Text = "2008", Value = "2008" },
                 new SelectListItem { Text = "2007", Value = "2007" },
                 new SelectListItem { Text = "2006", Value = "2006" },
                 new SelectListItem { Text = "2005", Value = "2005" },
                 new SelectListItem { Text = "2004", Value = "2004" },
                 new SelectListItem { Text = "2003", Value = "2003" },
                 new SelectListItem { Text = "2002", Value = "2002" },
                 new SelectListItem { Text = "2001", Value = "2001" },
                 new SelectListItem { Text = "2000", Value = "2000" },
           };

                ViewBag.Anios = IdsAnio;



                List<SelectListItem> IdsMes = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Enero", Value = "1" },
                new SelectListItem { Text = "Febrero", Value = "2" },
                new SelectListItem { Text = "Marzo", Value = "3" },
                new SelectListItem { Text = "Abril", Value = "4" },
                new SelectListItem { Text = "Mayo", Value = "5" },
                new SelectListItem { Text = "Junio", Value = "6" },
                new SelectListItem { Text = "Julio", Value = "7" },
                new SelectListItem { Text = "Agosto", Value = "8" },
                new SelectListItem { Text = "Septiembre", Value = "9" },
                new SelectListItem { Text = "Octubre", Value = "10" },
                new SelectListItem { Text = "Noviembre", Value = "11" },
                new SelectListItem { Text = "Diciembre", Value = "12" },

            };
                //Assigning generic list to ViewBag
                ViewBag.Locations = IdsMes;

                ViewBag.IdPlanta = Request["IdPlanta"];

                ViewBag.IdAnio = Request["IdAnio"];
                ViewBag.IdMes = Request["IdsMes"];
                ViewBag.IdIndicador = Request["IdIndicador"];
                ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");


                IndicadoresADO cargaIndicadorADO = new IndicadoresADO();
                ViewBag.dropdownIndicadores = new SelectList(cargaIndicadorADO.cmbindicadores(), "IdsIdIndicador", "IdsDescripcionIndicador");


                MedicionesADO cargaMedicionADO = new MedicionesADO();
                ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones(), "IdsIdMedicion", "IdsDescripcionMedicion");

                ElementosADO cargaElementoADO = new ElementosADO();
                ViewBag.dropdownElementos = new SelectList(cargaElementoADO.cmbelementos(), "IdsIdElemento", "IdsDescripcionElemento");

                UnidadesADO cargaUnidadesADO = new UnidadesADO();
                ViewBag.dropdownUnidades = new SelectList(cargaUnidadesADO.cmbunidades(), "IdsIdUnidad", "IdsDescripcionUnidadMedida");


                return View(context.IdsMediciones.Find(id));
            }
        }

        // POST: ListarMediciones/Edit/5
        [HttpPost]
        public ActionResult EditMedicion(IdsMediciones entity)
        {

            Entities context = new Entities();

            List<SelectListItem> IdsMes = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Enero", Value = "1" },
                new SelectListItem { Text = "Febrero", Value = "2" },
                new SelectListItem { Text = "Marzo", Value = "3" },
                new SelectListItem { Text = "Abril", Value = "4" },
                new SelectListItem { Text = "Mayo", Value = "5" },
                new SelectListItem { Text = "Junio", Value = "6" },
                new SelectListItem { Text = "Julio", Value = "7" },
                new SelectListItem { Text = "Agosto", Value = "8" },
                new SelectListItem { Text = "Septiembre", Value = "9" },
                new SelectListItem { Text = "Octubre", Value = "10" },
                new SelectListItem { Text = "Noviembre", Value = "11" },
                new SelectListItem { Text = "Diciembre", Value = "12" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = IdsMes;

             UnidadesADO cargaUnidadesADO = new UnidadesADO();
            ViewBag.dropdownUnidades = new SelectList(cargaUnidadesADO.cmbunidades(), "IdsIdUnidad", "IdsDescripcionUnidadMedida");

            ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");


            IndicadoresADO cargaIndicadorADO = new IndicadoresADO();
            ViewBag.dropdownIndicadores = new SelectList(cargaIndicadorADO.cmbindicadores(), "IdsIdIndicador", "IdsDescripcionIndicador");


            MedicionesADO cargaMedicionADO = new MedicionesADO();
            ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones(), "IdsIdMedicion", "IdsDescripcionMedicion");

            ElementosADO cargaElementoADO = new ElementosADO();
            ViewBag.dropdownElementos = new SelectList(cargaElementoADO.cmbelementos(), "IdsIdElemento", "IdsDescripcionElemento");

            if (!ModelState.IsValid)
            {

                List<SelectListItem> IdsAnio = new List<SelectListItem>()
            {
                 new SelectListItem { Text = "2018", Value = "2018" },
                 new SelectListItem { Text = "2017", Value = "2017" },
                 new SelectListItem { Text = "2016", Value = "2016" },
                 new SelectListItem { Text = "2015", Value = "2015" },
                 new SelectListItem { Text = "2014", Value = "2014" },
                 new SelectListItem { Text = "2013", Value = "2013" },
                 new SelectListItem { Text = "2012", Value = "2012" },
                 new SelectListItem { Text = "2011", Value = "2011" },
                 new SelectListItem { Text = "2010", Value = "2010" },
                 new SelectListItem { Text = "2009", Value = "2009" },
                 new SelectListItem { Text = "2008", Value = "2008" },
                 new SelectListItem { Text = "2007", Value = "2007" },
                 new SelectListItem { Text = "2006", Value = "2006" },
                 new SelectListItem { Text = "2005", Value = "2005" },
                 new SelectListItem { Text = "2004", Value = "2004" },
                 new SelectListItem { Text = "2003", Value = "2003" },
                 new SelectListItem { Text = "2002", Value = "2002" },
                 new SelectListItem { Text = "2001", Value = "2001" },
                 new SelectListItem { Text = "2000", Value = "2000" },
           };

                ViewBag.Anios = IdsAnio;


                return View(context.IdsMediciones.Find(entity.IdsId));
            }
            //           try
            //{
            // TODO: Add update logic here

            //if (context.IdsMediciones.Find(entity.IdsId).IdsIdMedicion == 4)
            //{

            //    entity.IdsValorCalculado = (entity.IdsValorCalculado * 0.0036);
            //    entity.IdsValorCalculado = System.Math.Round(entity.IdsValorCalculado, 3, MidpointRounding.AwayFromZero);
            //    entity.IdsIdUnidad = 2;

            //}
            //else
            //{
            //    entity.IdsValorCalculado = System.Math.Round(entity.IdsValorCalculado, 3, MidpointRounding.AwayFromZero);
            //}


            entity.IdsValorCalculado = System.Math.Round(entity.IdsValorCalculado, 3, MidpointRounding.AwayFromZero);


            IdsMediciones entity2 = new IdsMediciones();
                entity2 = context.IdsMediciones.Find(entity.IdsId);
                entity2.IdsValorCalculado = entity.IdsValorCalculado;
                entity2.IdsIdUnidad = entity.IdsIdUnidad;
                entity2.IdsNotasAdicionales = entity.IdsNotasAdicionales;
                context.IdsMediciones.Attach(entity2);
                context.Entry(entity2).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                Entities db = new Entities();

               

                IdsBitacora Bitacora = new IdsBitacora()
                {
                    //IdsIdCia = 1,
                    IdsIdEmpresa = entity2.IdsIdEmpresa,
                    IdsMes = entity2.IdsMes,
                    IdsAnio = entity2.IdsAnio,
                    IdsIdIndicador = entity2.IdsIdIndicador,
                    IdsIdMedicion = entity2.IdsIdMedicion,
                    IdsIdElemento = entity2.IdsIdElemento,
                    IdsIdUnidad = entity.IdsIdUnidad,
                    IdsTipoDeCalculo = "M",
                    IdsValorCalculado = entity.IdsValorCalculado,
                    IdsPrecioDolar = entity.IdsPrecioDolar,
                    IdsStatus = "1",
                    IdsFechaCambio = System.DateTime.Now,
                    IdsUsuarioCambio = (int)Session["idUsuario"]
                };

                context.IdsBitacora.Add(Bitacora);
                context.SaveChanges();

                return RedirectToAction("ListaMedicion", db.IdsMediciones.ToList());
            //}
            //catch(Exception e)
            //{

          
             
            //    return View();
            //}
        }

        // GET: ListarMediciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListarMediciones/Delete/5
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
