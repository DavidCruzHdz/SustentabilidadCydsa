using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;


namespace CYDSA_Sustentabilidad.Controllers
{
    public class IndicadorController : Controller
    {
        Entities db = new Entities();
        // GET: Indicador
        public ActionResult Index(int IdMes)
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
                    Entities db = new Entities();

                    try
                    {

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
                    IdsMes.Add(new SelectListItem() { Text = "asdfsd Cape", Value = "NC" });
           
                    ViewBag.Locations = IdsMes;

                    Entities context = new Entities();
                    ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");


                    List<SelectListItem> IdsAnio = new List<SelectListItem>();
                    var contar = 1;

                    foreach (int item  in context.IdsIndicadores.ToList().Select(x => x.IdsAnio).Distinct().ToList())
                    {
                
                        IdsAnio.Add(new SelectListItem() { Text = item.ToString(), Value = contar.ToString() });
                        contar++;
                    }

                    ViewBag.dropdownAnio = IdsAnio;

                    var idPlanta = (int)Session["IdPlanta"];
                    var idAnio = (int)Session["IdAnio"];
           
                    var b = db.SpSelIndicadoresCYDSA(idPlanta, IdMes, idAnio);
                    var a = db.IdsIndicadores.Where(x => x.IdsIdEmpresa == idPlanta && x.IdsAnio == idAnio & x.IdsMes == IdMes);
                

                    if (a==null)
                    {
                        return View();
                    }
                    else
                    {
                    
                            return View(a);
                    }
                    }catch
                    {

                        return Redirect("/Mensuales/Mensuales");
                    }


            }

        }


       [HttpPost]
        public JsonResult Index2(int id = 0, int IdMes = 0,int IdAnio =0)
        {
            Entities db = new Entities();
            
          

            Session["hola"] = "hola2";
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
            ViewBag.Locations = IdsMes;
            Entities context = new Entities();

            ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
            List<SelectListItem> IdsAnio = new List<SelectListItem>();
            var contar = 1;

            foreach (int item in context.IdsIndicadores.ToList().Select(x => x.IdsAnio).Distinct().ToList())
            {

                IdsAnio.Add(new SelectListItem() { Text = item.ToString(), Value = contar.ToString() });
                contar++;
            }

            ViewBag.dropdownAnio = IdsAnio;

            return Json(db.IdsIndicadores.ToList().Where(x=>(x.IdsIdEmpresa==id||id ==0) &( x.IdsMes==IdMes||IdMes==0) &(x.IdsAnio==IdAnio||IdAnio==0) ).Select(x => new { IdsMes = x.IdsMes, Anio = x.IdsAnio, IdsIdIndicador = x.IdsIdIndicador, IdUnidad = x.IdsIdUnidad, IdsValorDelPeriodo = x.IdsValorDelPeriodo, IdsNotas=x.IdsNotasAdicionales}), JsonRequestBehavior.AllowGet);
        }

    }
}