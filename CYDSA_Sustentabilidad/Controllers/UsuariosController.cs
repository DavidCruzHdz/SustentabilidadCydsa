using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Index()
        {

            Entities context = new Entities();

            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!context.IdsPantallasPermisos.Any(x => x.IdPantalla == 9 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }


            var entity = context.IdsUsuario.ToList().Where(x=>x.Estatus == true);
            return View(entity);
        }
        // GET: Usuarios
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Pantallas(int id)

        {
            Entities context = new Entities();

            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!context.IdsPantallasPermisos.Any(x => x.IdPantalla == 13 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }


            Session["IdUsuarioPermisos"] = id;

            var entity = context.IdsPantallas.ToList();
            var permisos = context.IdsPantallasPermisos.Where(x => x.IdUsuario == id);

            foreach (var item in permisos)
            {
                entity.Find(x => x.Id == item.IdPantalla).checkeado = true;

            }
            return View(context.IdsPantallas.ToList());
        }


        [HttpPost]
        public ActionResult Pantallas(IEnumerable<IdsPantallas> entity)
        {
            Entities context = new Entities();
            Entities context2 = new Entities();

            int idusuario = (int)Session["IdUsuarioPermisos"];
            var aa = context.IdsPantallasPermisos.Where(x => x.IdUsuario == idusuario);
            if (aa.Count() >= 1)
            {
                context.IdsPantallasPermisos.RemoveRange(aa);
                context.SaveChanges();
            }


            //entity2.IdPantalla = entity.FirstOrDefault().Id;
            //entity2.idUsuario = (int)Session["IdUsuario"];

            //context2.IdsPantallasPermisos.Add(entity2);
            //context2.SaveChanges();

            foreach (var item in entity)
            {
                if (item.checkeado == true) // si esta chequeado
                {
                    var idPantalla = item.Id;
                    var idUsuario = (int)Session["IdUsuarioPermisos"];
                    IdsPantallasPermisos entity2 = new IdsPantallasPermisos();

                    entity2.IdPantalla = idPantalla;
                    entity2.IdUsuario = (int)Session["IdUsuarioPermisos"];

                    context2.IdsPantallasPermisos.Add(entity2);
                    context2.SaveChanges();

                }


            }



            return Redirect("/Usuarios/Index");

        }



        [HttpPost]
        public ActionResult Login(IdsUsuario entity)
        { Entities context = new Entities();
            
            var exist = context.IdsUsuario.Where(x => x.IdsNombreUsuario == entity.IdsNombreUsuario && x.IdsPassword == entity.IdsPassword && x.Estatus == true).FirstOrDefault();
            if (exist != null)
            {
                Session["NombreUsuario"] = entity.IdsNombreUsuario;
                Session["logeado"] = true;
                Session["idUsuario"] = exist.IdsIdUsuario;
                Session["idPlantaDF"] = context.IdsPermisosPlantas.Where(x => x.IdsIdUsuario == exist.IdsIdUsuario && x.IdsPlantaDefault == true).FirstOrDefault().IdsIdEmpresa;
                

                return Redirect("/Home/Index");
            }
            else
            {
                Session["idUsuario"] = 0;
                Session["logeado"] = false;
                ViewBag.error = "usuario incorrecto";
                return View();
            }

        }
        // GET: Usuarios/Details/5
        public ActionResult DesLogear()
        {

            Session["logeado"] = false;
            Session["idUsuario"] = 0;
            return Redirect("/Usuarios/Login");
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Entities context = new Entities();

            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!context.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }


            ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(IdsUsuario entity)
        {
            //entity.IdsIdCia = 1;
            entity.IdsRol_id = 1;
            Entities context = new Entities();
            try
            {
                entity.Estatus = true;
                // TODO: Add insert logic here
                context.IdsUsuario.Add(entity);
                context.SaveChanges();

              
                IdsPermisosPlantas IdsPlantas = new IdsPermisosPlantas();
                IdsPlantas.IdsIdEmpresa = int.Parse(Request["IdsIdEmpresa"].ToString()); 
                IdsPlantas.IdsIdUsuario = entity.IdsIdUsuario;
                IdsPlantas.IdsFechaAlta = System.DateTime.Now;
                IdsPlantas.IdsUsuarioAlta = int.Parse(Session["idUsuario"].ToString()); 
                IdsPlantas.IdsPlantaDefault = true;

                context.IdsPermisosPlantas.Add(IdsPlantas);
                context.SaveChanges();

                //ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
                //return View();

                return Redirect("/usuarios");
            }
            catch 
            {
                ViewBag.dropdownPlanta = new SelectList(context.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {

            Entities db = new Entities();


            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }



            ViewBag.dropdownPlanta = new SelectList(db.IdsCatEmpresas.ToList(), "IdsIdEmpresa", "IdsDescripcionEmpresa");
            var list = db.IdsUsuario.Find(id);
            return View(list);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IdsUsuario collection)
        {
            try
            {
                Entities db = new Entities();

                int VarUsuario = int.Parse(Session["idUsuario"].ToString());

                if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
                {
                    return Redirect("/Home/Index");
                }

                db.IdsUsuario.Attach(collection);
                db.Entry(collection).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                // TODO: Add update logic here

                return RedirectToAction("/Index");
            }
            catch(Exception e)
            {
                return View(e);
            }
        }

        // GET: Usuarios/Delete/5
       

        // POST: Usuarios/Delete/5
       
        public ActionResult Delete(int id)
        {
            try
            {
                Entities db = new Entities();

                int VarUsuario = int.Parse(Session["idUsuario"].ToString());

                if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
                {
                    return Redirect("/Home/Index");
                }
                IdsUsuario usuario = new IdsUsuario();
                usuario = db.IdsUsuario.Where(x => x.IdsIdUsuario == id).FirstOrDefault();
                usuario.Estatus = false;

                db.IdsUsuario.Attach(usuario);

                db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View(e);
            }
        }

        public ActionResult Permisos(int id)
        {
            Entities db = new Entities();

            int VarUsuario = int.Parse(Session["idUsuario"].ToString());

            if (!db.IdsPantallasPermisos.Any(x => x.IdPantalla == 10 && x.IdUsuario == VarUsuario))
            {
                return Redirect("/Home/Index");
            }

            Session["IdUsuarioEmpresa"] = id;
            Entities context = new Entities();
            var entity = context.IdsCatEmpresas.ToList();
            var permisos = context.IdsPermisosPlantas.Where(x => x.IdsIdUsuario == id);

            foreach (var item in permisos)
            {
                entity.Find(x => x.IdsIdEmpresa == item.IdsIdEmpresa).checkeado = true;

            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult Permisos(IEnumerable<IdsCatEmpresas> entity)
        {

            Session["DefaultEmpresa"] = 0;
            Entities context = new Entities();
            var idUsuarioEmpresa = (int)Session["IdUsuarioEmpresa"];
            var aa = context.IdsPermisosPlantas.Where(x => x.IdsIdUsuario == idUsuarioEmpresa);
            if (aa.Count() >= 1)
            {
                if (aa.Where(x => x.IdsPlantaDefault == true).Count()>=1)
                {
                    Session["DefaultEmpresa"] = aa.Where(x => x.IdsPlantaDefault == true).FirstOrDefault().IdsIdEmpresa;
                }
               
                context.IdsPermisosPlantas.RemoveRange(aa);
                context.SaveChanges();
            }
            foreach (var item in entity)
            {
                if (item.checkeado == true) // si esta chequeado
                { var IdsIdEmpresa = item.IdsIdEmpresa;
                    var idEmpresa = (int)Session["idUsuarioEmpresa"];
                    IdsPermisosPlantas entity2 = new IdsPermisosPlantas();
                    entity2.IdsIdEmpresa = IdsIdEmpresa;
                    entity2.IdsIdUsuario = idEmpresa;

                    context.IdsPermisosPlantas.Add(entity2);
                    context.SaveChanges();

                }


            }


            Entities contex2 = new Entities();
            var cc = contex2.IdsPermisosPlantas.ToList();
            var a = Session["DefaultEmpresa"];
            var PlantasEncontradas = cc.Where(x => x.IdsIdUsuario == idUsuarioEmpresa && x.IdsIdEmpresa == (Int32)Session["DefaultEmpresa"]);

            // si la planta que estaba como defaul aun existe, se buelve a poner default 
            if (PlantasEncontradas.Count()>=1)
            {
                IdsPermisosPlantas editar = new IdsPermisosPlantas();
                editar = context.IdsPermisosPlantas.ToList().Where(x => x.IdsIdEmpresa == (Int32)Session["DefaultEmpresa"]&& x.IdsIdUsuario == idUsuarioEmpresa).FirstOrDefault();
                editar.IdsPlantaDefault = true;
                context.IdsPermisosPlantas.Add(editar);
             
                context.Entry(editar).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
            else// si no
            {
                if(!context.IdsPermisosPlantas.Any(x => x.IdsIdUsuario == idUsuarioEmpresa))// si quito todas, se vuelbe  a agregar la planta default
                {
                    IdsPermisosPlantas insertar = new IdsPermisosPlantas();
                    insertar.IdsFechaAlta = System.DateTime.Now;
                    insertar.IdsIdEmpresa = (Int32)Session["DefaultEmpresa"];
                    insertar.IdsIdUsuario = idUsuarioEmpresa;


                }
                IdsPermisosPlantas editar = new IdsPermisosPlantas();
                editar = context.IdsPermisosPlantas.ToList().Where(x => x.IdsIdUsuario == idUsuarioEmpresa).FirstOrDefault();
                editar.IdsPlantaDefault = true;              
                context.IdsPermisosPlantas.Attach(editar);
                context.Entry(editar).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
               
            }

            return Redirect("/Usuarios/Index"); 
        }
    }
}
