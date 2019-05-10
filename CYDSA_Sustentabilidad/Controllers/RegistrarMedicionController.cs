using CYDSA_Sustentabilidad.ADO;
using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class RegistrarMedicionController : Controller
    {
        //[Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        //public int AutoId { get; set; }
        Entities db = new Entities();

        // GET: RegistraMediciones
        public ActionResult RegistrarMedicion(int IdMes=0 ,int IdIndicador =0)
        {
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 3 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            if (Session["idUsuario"] == null)
            {

                return Redirect("/Usuarios/Login");
                //ViewBag.Message = "Mediciones";
                //return View();
            }
            else
            {
                    if (IdMes > 0)
                        Session["IdIndicador"] = IdIndicador;
                    if (IdIndicador > 0)
                        Session["IdMes2"] = IdMes;

                    IdMes = (int)Session["IdMes2"];
                    IdIndicador = (int)Session["IdIndicador"];

                //     List<SelectListItem> IdsAnio = new List<SelectListItem>()
                // {

                //      new SelectListItem { Text = "2019", Value = "2019" },
                //      new SelectListItem { Text = "2018", Value = "2018" },
                //      new SelectListItem { Text = "2017", Value = "2017" },
                //      new SelectListItem { Text = "2016", Value = "2016" },
                //      new SelectListItem { Text = "2015", Value = "2015" },
                //      new SelectListItem { Text = "2014", Value = "2014" },
                //      new SelectListItem { Text = "2013", Value = "2013" },
                //      new SelectListItem { Text = "2012", Value = "2012" },
                //      new SelectListItem { Text = "2011", Value = "2011" },
                //      new SelectListItem { Text = "2010", Value = "2010" },
                //      new SelectListItem { Text = "2009", Value = "2009" },
                //      new SelectListItem { Text = "2008", Value = "2008" },
                //      new SelectListItem { Text = "2007", Value = "2007" },
                //      new SelectListItem { Text = "2006", Value = "2006" },
                //      new SelectListItem { Text = "2005", Value = "2005" },
                //      new SelectListItem { Text = "2004", Value = "2004" },
                //      new SelectListItem { Text = "2003", Value = "2003" },
                //      new SelectListItem { Text = "2002", Value = "2002" },
                //      new SelectListItem { Text = "2001", Value = "2001" },
                //      new SelectListItem { Text = "2000", Value = "2000" },
                //};

                int AnioActual = DateTime.Now.Year;
                int AnioInicial = AnioActual - 20;              
                List<SelectListItem> IdsAnio = new List<SelectListItem>();

                for (int i = AnioActual; i >= AnioInicial; i--)
                {
                    IdsAnio.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
                }

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

                    Entities context = new Entities();
                    ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");

                    IndicadoresADO cargaIndicadorADO = new IndicadoresADO();
                    ViewBag.dropdownIndicadores = new SelectList(cargaIndicadorADO.cmbindicadores(), "IdsIdIndicador", "IdsDescripcionIndicador");

                    MedicionesADO cargaMedicionADO = new MedicionesADO();
                    //ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones().Where(x=>x.IdsIdIndicador ==IdIndicador), "IdsIdMedicion", "IdsDescripcionMedicion");

                    ElementosADO cargaElementoADO = new ElementosADO();
                    ViewBag.dropdownElementos = new SelectList(cargaElementoADO.cmbelementos(), "IdsIdElemento", "IdsDescripcionElemento");

                    UnidadesADO cargaUnidadesADO = new UnidadesADO();
                    ViewBag.dropdownUnidades = new SelectList(cargaUnidadesADO.cmbunidades(), "IdsIdUnidad", "IdsDescripcionUnidadMedida");

                    IdsMediciones enti = new IdsMediciones();
                    enti.IdsAnio = System.DateTime.Now.Year;

                    if (Session["MsjError"] != null)
                    {
                        ViewBag.Error = Session["MsjError"].ToString();
                        Session["MsjError"] = null;

                    }
                    Entities db = new Entities();
                    IdsMediciones model = new IdsMediciones();
                    if (Session["modelo"] != null)
                    {
                        model = Session["modelo"] as IdsMediciones;
                        //model = db.IdsMediciones.Where(x => x.IdsIdMe == (int)Session["idAnio"]);
                        ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones(model.IdsIdIndicador), "IdsIdMedicion", "IdsDescripcionMedicion");
                      //ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones().Where(x=>x.IdsIdIndicador ==IdIndicador), "IdsIdMedicion", "IdsDescripcionMedicion");

                }
                else
                    {
                        model.IdsAnio = (int)Session["IdAnio"];
                        model.IdsIdEmpresa = (int)Session["IdPlanta"];
                        model.IdsIdIndicador = IdIndicador;
                        model.IdsMes = IdMes;
                    }
                    ViewBag.dropdownMediciones = new SelectList(db.ComboMediciones((int)Session["IdPlanta"], (int)Session["IdAnio"], IdMes, IdIndicador), "IdsIdMedicion", "IdsDescripcionMedicion");
                    var IdPlanta = (int)Session["IdPlanta"];
                    var IdAnio = (int)Session["IdAnio"];
                    var lista = db.ComboMediciones(IdPlanta, IdAnio, IdMes, IdIndicador);
                    return View(model);
                    //if (lista !=null &lista.Count() >= 1)
                    //{
                    //    return View(model);
                    //}
                    //else
                    //{
                    //    return Redirect("/Mensuales/Mensuales");
                    //}
            }
        }


        // Post: RegistraMediciones
        [HttpPost]
        public ActionResult RegistrarMedicion(IdsMediciones Mediciones) // Mediciones es un arreglo de tipo modelo
        {

            Session["modelo"] = Mediciones;
            try
            {
                // Session["CIA"] = Mediciones.IdsIdCia;


                //List<SelectListItem> IdsAnio = new List<SelectListItem>();
                //for (int i = 2000; i < 2060; i++)
                //{
                //    new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) };
                //};
                //ViewBag.Locations = IdsAnio;



                //         List<SelectListItem> IdsAnio = new List<SelectListItem>()
                // {

                //      new SelectListItem { Text = "2019", Value = "2019" },
                //      new SelectListItem { Text = "2018", Value = "2018" },
                //      new SelectListItem { Text = "2017", Value = "2017" },
                //      new SelectListItem { Text = "2016", Value = "2016" },
                //      new SelectListItem { Text = "2015", Value = "2015" },
                //      new SelectListItem { Text = "2014", Value = "2014" },
                //      new SelectListItem { Text = "2013", Value = "2013" },
                //      new SelectListItem { Text = "2012", Value = "2012" },
                //      new SelectListItem { Text = "2011", Value = "2011" },
                //      new SelectListItem { Text = "2010", Value = "2010" },
                //      new SelectListItem { Text = "2009", Value = "2009" },
                //      new SelectListItem { Text = "2008", Value = "2008" },
                //      new SelectListItem { Text = "2007", Value = "2007" },
                //      new SelectListItem { Text = "2006", Value = "2006" },
                //      new SelectListItem { Text = "2005", Value = "2005" },
                //      new SelectListItem { Text = "2004", Value = "2004" },
                //      new SelectListItem { Text = "2003", Value = "2003" },
                //      new SelectListItem { Text = "2002", Value = "2002" },
                //      new SelectListItem { Text = "2001", Value = "2001" },
                //      new SelectListItem { Text = "2000", Value = "2000" },

                //};

                //for (int i = 0; i < maxIteraciones; i++)
                //{
                //    //IdsAnio.Add(i);
                //    new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) };
                //}


                int AnioActual = DateTime.Now.Year;
                int AnioInicial = AnioActual - 20;
                List<SelectListItem> IdsAnio = new List<SelectListItem>();

                for (int i = AnioActual; i >= AnioInicial; i--)
                {
                    IdsAnio.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
                }


                //DateTime fecha = New DateTime();



                //whie( < 2016)
                //{
                //    fecha = fecha.Year.Add(1);
                //    combo.Items.Add(fecha);
                //}




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

                Entities context = new Entities();
                ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");

                IndicadoresADO cargaIndicadorADO = new IndicadoresADO();
                ViewBag.dropdownIndicadores = new SelectList(cargaIndicadorADO.cmbindicadores(), "IdsIdIndicador", "IdsDescripcionIndicador");

                ElementosADO cargaElementoADO = new ElementosADO();
                ViewBag.dropdownElementos = new SelectList(cargaElementoADO.cmbelementos(), "IdsIdElemento", "IdsDescripcionElemento");

                UnidadesADO cargaUnidadesADO = new UnidadesADO();
                ViewBag.dropdownUnidades = new SelectList(cargaUnidadesADO.cmbunidades(), "IdsIdUnidad", "IdsDescripcionUnidadMedida");

                MedicionesADO cargaMedicionADO = new MedicionesADO();
                ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones(Mediciones.IdsIdIndicador), "IdsIdMedicion", "IdsDescripcionMedicion");

              
                }

            catch (Exception Error)
            {
                return View(Error);
            }
            Session["Medio"] = Mediciones;
            Entities db = new Entities();


           

            if (Mediciones.IdsIdIndicador > 0 & Mediciones.IdsIdMedicion > 0)
            {
                if (ModelState.IsValid)
                {
                    var IdsIdMedicion = Mediciones.IdsIdMedicion;
                    Mediciones.IdsIdMedicion = 0;

                    return RedirectToAction("ElementosList", new { idMedicion = IdsIdMedicion, idIndicador = Mediciones.IdsIdIndicador, idPlanta = Mediciones.IdsIdEmpresa, Anio = Mediciones.IdsAnio, mes = Mediciones.IdsMes, idPrecioDolar = Mediciones.IdsPrecioDolar });

                }
                else
                {
                    return View();
                }
                
            }
            else
            {
                return View();
            }


            //  return RedirectToAction("ElementosList", new { idMedicion = Mediciones.IdsIdMedicion ,idIndicador= Mediciones.IdsIdIndicador, idPlanta= Mediciones.IdsIdPlanta, idCia = 1 ,});
        }

        public ActionResult GetModel(int IdIndicador)
        {
            var result = new List<SelectListItem>();
            for (int i = 0; i < 5; i++)
            {
                result.Add(new SelectListItem { Text = string.Format("{0} - {1} - {2}", "hola", "hola2", i), Value = i.ToString() });
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ElementosList(int idMedicion, int idIndicador, int idPlanta, int Anio , int mes ,string idPrecioDolar="")
        {
            Entities db = new Entities();
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 3 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            if (Session["idUsuario"] == null)
            {

                return Redirect("/Usuarios/Login");
                //ViewBag.Message = "Mediciones";
                //return View();
            }
            else
            {


                idPlanta = (int)Session["IdPlanta"];
                Entities context = new Entities();
                switch (mes)
                {
                    case 1:
                        Session["IdMes"] = "Enero";
                        break;
                    case 2:
                        Session["IdMes"] = "Febrero";
                        break;
                    case 3:
                        Session["IdMes"] = "Marzo";
                        break;
                    case 4:
                        Session["IdMes"] = "Abril";
                        break;
                    case 5:
                        Session["IdMes"] = "Mayo";
                        break;
                    case 6:
                        Session["IdMes"] = "Junio";
                        break;
                    case 7:
                        Session["IdMes"] = "Julio";
                        break;
                    case 8:
                        Session["IdMes"] = "Agosto";
                        break;
                    case 9:
                        Session["IdMes"] = "Septiembre";
                        break;
                    case 10:
                        Session["IdMes"] = "Octubre";
                        break;
                    case 11:
                        Session["IdMes"] = "Noviembre";
                        break;
                    case 12:
                        Session["IdMes"] = "Diciembre";
                        break;
                    default:
                        Session["IdMes"] = "";
                        break;
                }
                var list = context.IdsRelaciones.ToList().Where(x => x.IdsIdMedicion == idMedicion && x.IdsIdIndicador == idIndicador && x.IdsIdEmpresa == idPlanta && x.IdsTipoDeCalculo == "M");


                ViewBag.medicion = context.IdsCatMediciones.Find(idMedicion).IdsDescripcionMedicion;
                var a = list.ToList();

                return View(a);
            }
        }
        [HttpPost]
        public ActionResult ElementosList(IList<IdsRelaciones> entity)
        {
            Entities db = new Entities();
            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }


            if (!ModelState.IsValid)
            {
                Entities context2 = new Entities();



                var list = context2.IdsRelaciones.ToList().Where(x => x.IdsIdMedicion == entity.FirstOrDefault().IdsIdMedicion && x.IdsIdIndicador == entity.FirstOrDefault().IdsIdIndicador && x.IdsIdEmpresa == (int)Session["IdPlanta"] && x.IdsTipoDeCalculo == "M");


                ViewBag.medicion = context2.IdsCatMediciones.Find(entity.FirstOrDefault().IdsIdMedicion).IdsDescripcionMedicion;
                var a = list.ToList();

                return View(a);
            }

            //try
            //{

            //if(ModelState.IsValid)
            //    {

            var PrecioDolar = "";

                if (PrecioDolar != null)
                {
                    PrecioDolar = Request["idPrecioDolar"];
                }
                else
                {
                    PrecioDolar = "0";
                }

                    IdsMediciones medicion = new IdsMediciones();
                  //  medicion = Session["Medio"] as IdsMediciones;
                Entities context = new Entities();

                //PrecioDolar = int.Parse(ViewData["PrecioDolar"].ToString());

                foreach (var item in entity)
                {
                    //medicion.IdsIdCia = 1;
                    medicion.IdsIdEmpresa = item.IdsIdEmpresa;
                    medicion.IdsMes = int.Parse(Request["Mes"].ToString()); 
                    medicion.IdsAnio =int.Parse(Request["Anio"].ToString()); 
                    medicion.IdsIdIndicador = item.IdsIdIndicador;
                    medicion.IdsIdMedicion = item.IdsIdMedicion;
                    medicion.IdsIdElemento = item.IdsIdElemento;

                //medicion.IdsCatElementos.IdsNomenclatura = item.IdsCatElementos.IdsNomenclatura;

                //if (item.IdsIdMedicion == 15 || item.IdsIdMedicion == 18)

                if (item.IdsIdMedicion == 14)
                {
                    if (item.Valor != 0 || Convert.ToDouble(PrecioDolar) != 0)
                    {

                        medicion.IdsPrecioDolar = decimal.Parse(PrecioDolar.ToString());
                        medicion.IdsValorCalculado = Math.Round(item.Valor, 3, MidpointRounding.AwayFromZero);

                        //medicion.IdsValorCalculado = (item.Valor / Convert.ToDouble(PrecioDolar));
                        //medicion.IdsValorCalculado = System.Math.Round(medicion.IdsValorCalculado, 3, MidpointRounding.AwayFromZero);
                    }
                }
                else
                {
                        medicion.IdsPrecioDolar = 0;
                        medicion.IdsValorCalculado = System.Math.Round(item.Valor, 3, MidpointRounding.AwayFromZero);
                }



                //if (item.IdsIdMedicion == 4)
                //{

                //        medicion.IdsValorCalculado = (item.Valor * 0.0036);
                //        medicion.IdsValorCalculado = System.Math.Round(medicion.IdsValorCalculado, 3, MidpointRounding.AwayFromZero);
                //        item.IdsIdUnidad = 2;
                //}
                //else
                //{
                //       medicion.IdsValorCalculado = System.Math.Round(item.Valor, 3, MidpointRounding.AwayFromZero);
                //}


                

                //medicion.IdsId = medicion.IdsIdMedicion.ToString.Distinct();
                //    medicion.Entity<IdsMediciones>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                //Searching records from list using LINQ query  


                //var CityList = (from N in ObjList where N.CityName.StartsWith(Prefix) select new { N.CityName });
                //return Json(CityList, JsonRequestBehavior.AllowGet);

                medicion.IdsIdUnidad = item.IdsIdUnidad;
                    medicion.IdsCierreMes = false;
                    medicion.IdsFechaAlta = System.DateTime.Now;
                medicion.IdsUsuarioAlta = (int)Session["idUsuario"];
                    medicion.IdsFechaCambio = System.DateTime.Now;
                medicion.IdsUsuarioCambio = (int)Session["idUsuario"];
                    medicion.IdsNotasAdicionales = "";
                    medicion.IdEstatus = true;
                medicion.IdsIdEmpresa = (int)Session["IdPlanta"];
                //medicion.IdsPrecioDolar = decimal.Parse(PrecioDolar.ToString()); 
                    medicion.IdsNotasAdicionales = "";
                medicion.IdsTipoDato = "M";
                    context.IdsMediciones.Add(medicion);
                    context.SaveChanges();


                //IdsBitacora Bitacora = new IdsBitacora()
                //{
                //    IdsIdCia = 1,
                //    IdsIdPlanta = medicion.IdsIdPlanta,
                //    IdsMes = medicion.IdsMes,
                //    IdsAnio = medicion.IdsAnio,
                //    IdsIdIndicador = medicion.IdsIdIndicador,
                //    IdsIdMedicion = medicion.IdsIdMedicion,
                //    IdsIdElemento = medicion.IdsIdElemento,
                //    IdsIdUnidad = medicion.IdsIdUnidad,
                //    IdsTipoDeCalculo = "M",
                //    IdsValorCalculado = item.Valor,
                //    IdsPrecioDolar = medicion.IdsPrecioDolar,
                //    IdsStatus = "0",
                //    IdsFechaCambio = System.DateTime.Now,
                //    IdsUsuarioCambio = "1"
                //};

                //context.IdsBitacora.Add(Bitacora);
                //context.SaveChanges();


                IdsBitacora Bitacora = new IdsBitacora()
                {
                    //IdsIdCia = 1,
                    IdsIdEmpresa = medicion.IdsIdEmpresa,
                    IdsMes = medicion.IdsMes,
                    IdsAnio = medicion.IdsAnio,
                    IdsIdIndicador = medicion.IdsIdIndicador,
                    IdsIdMedicion = medicion.IdsIdMedicion,
                    IdsIdElemento = medicion.IdsIdElemento,
                    IdsIdUnidad = medicion.IdsIdUnidad,
                    IdsTipoDeCalculo = "M",
                    IdsValorCalculado = item.Valor,
                    IdsPrecioDolar = medicion.IdsPrecioDolar,
                    IdsStatus = "0",
                    IdsFechaCambio = System.DateTime.Now,
                    IdsUsuarioCambio = (int)Session["idUsuario"]
                };

                context.IdsBitacora.Add(Bitacora);
                context.SaveChanges();


            }
            return Redirect("/RegistrarMedicion/RegistrarMedicion");
                //}
                //else
                //{    -- Falatan view bags 
                //    return View();
           //     //}
           // }
           // catch (Exception e)
           // {



           //     List<SelectListItem> IdsAnio = new List<SelectListItem>()
           // {
           //     new SelectListItem { Text = "2000", Value = "2000" },
           //     new SelectListItem { Text = "2001", Value = "2001" },
           //     new SelectListItem { Text = "2002", Value = "2002" },
           //     new SelectListItem { Text = "2003", Value = "2003" },
           //     new SelectListItem { Text = "2004", Value = "2004" },
           //     new SelectListItem { Text = "2005", Value = "2005" },
           //     new SelectListItem { Text = "2006", Value = "2006" },
           //     new SelectListItem { Text = "2007", Value = "2007" },
           //     new SelectListItem { Text = "2008", Value = "2008" },
           //     new SelectListItem { Text = "2009", Value = "2009" },
           //     new SelectListItem { Text = "2010", Value = "2010" },
           //     new SelectListItem { Text = "2011", Value = "2011" },
           //     new SelectListItem { Text = "2012", Value = "2012" },
           //     new SelectListItem { Text = "2013", Value = "2013" },
           //     new SelectListItem { Text = "2014", Value = "2014" },
           //     new SelectListItem { Text = "2015", Value = "2015" },
           //     new SelectListItem { Text = "2016", Value = "2016" },
           //     new SelectListItem { Text = "2017", Value = "2017" },
           //     new SelectListItem { Text = "2018", Value = "2018" },
           //};

           //     //for (int i = 0; i < maxIteraciones; i++)
           //     //{
           //     //    //IdsAnio.Add(i);
           //     //    new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) };
           //     //}

           //     ViewBag.Anios = IdsAnio;



           //     List<SelectListItem> IdsMes = new List<SelectListItem>()
           // {
           //     new SelectListItem { Text = "Enero", Value = "1" },
           //     new SelectListItem { Text = "Febrero", Value = "2" },
           //     new SelectListItem { Text = "Marzo", Value = "3" },
           //     new SelectListItem { Text = "Abril", Value = "4" },
           //     new SelectListItem { Text = "Mayo", Value = "5" },
           //     new SelectListItem { Text = "Junio", Value = "6" },
           //     new SelectListItem { Text = "Julio", Value = "7" },
           //     new SelectListItem { Text = "Agosto", Value = "8" },
           //     new SelectListItem { Text = "Septiembre", Value = "9" },
           //     new SelectListItem { Text = "Octubre", Value = "10" },
           //     new SelectListItem { Text = "Noviembre", Value = "11" },
           //     new SelectListItem { Text = "Diciembre", Value = "12" },

           // };
           //     //Assigning generic list to ViewBag
           //     ViewBag.Locations = IdsMes;
           //     Entities context = new Entities();

           //     ViewBag.dropdownPlanta = new SelectList(context.IdsCatPlantas.ToList(), "IdsIdPlanta", "IdsDescripcionPlanta");


           //     IndicadoresADO cargaIndicadorADO = new IndicadoresADO();
           //     ViewBag.dropdownIndicadores = new SelectList(cargaIndicadorADO.cmbindicadores(), "IdsIdIndicador", "IdsDescripcionIndicador");


           //     MedicionesADO cargaMedicionADO = new MedicionesADO();
           //     ViewBag.dropdownMediciones = new SelectList(cargaMedicionADO.cmbmediciones(), "IdsIdMedicion", "IdsDescripcionMedicion");

           //     ElementosADO cargaElementoADO = new ElementosADO();
           //     ViewBag.dropdownElementos = new SelectList(cargaElementoADO.cmbelementos(), "IdsIdElemento", "IdsDescripcionElemento");

           //     UnidadesADO cargaUnidadesADO = new UnidadesADO();
           //     ViewBag.dropdownUnidades = new SelectList(cargaUnidadesADO.cmbunidades(), "IdsIdUnidad", "IdsDescripcionUnidadMedida");

           //     IdsMediciones enti = new IdsMediciones();
           //     //enti.IdsAnio = System.DateTime.Now.Year;

           //     @Session["MsjError"] = "Todos las mediciones anteriores ya se encuentran registradas";

           //     return Redirect("RegistrarMedicion");
           // }


            //IdsMediciones Medio = new IdsMediciones()
            //{
            //    IdsIdCia=,
            //    IdsIdPlanta=,
            //    IdsMes=,
            //    IdsAnio=,
            //    IdsIdIndicador=,
            //    IdsIdMedicion=,
            //    IdsIdElemento=,
            //    IdsValorCalculado=,
            //    IdsIdUnidad=,
            //    IdsCierreMes=,
            //    IdsFechaAlta=,
            //    IdsUsuarioAlta=,
            //    IdsFechaCambio=,
            //    IdsUsuarioCambio=,


            //};
            
            //var list = context.IdsRelaciones.ToList().Where(x => x.IdsIdMedicion == medicion.IdsIdMedicion && x.IdsIdIndicadores == medicion.IdsIdIndicador && x.IdsIdPlanta == medicion.IdsIdPlanta && x.IdsIdCia == 1);

            //return View(list.ToList());
        }

        //public ActionResult Create()
        //{

        //    return View();

        //}

        private IEnumerable GetProfileByModuleID(int v)
        {
            throw new NotImplementedException();
        }


    }
}