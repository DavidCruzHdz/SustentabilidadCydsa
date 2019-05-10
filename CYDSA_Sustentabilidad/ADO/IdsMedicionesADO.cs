using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYDSA_Sustentabilidad.ADO
{
    public class IdsMedicionesADO
    {
        Entities Context;
        public IdsMedicionesADO()
        {
            Context = new Entities();
        }

        public void Insert(IdsMediciones entity)
        {
            Context.IdsMediciones.Add(entity);
            Context.SaveChanges();       
        }

        public void Update(IdsMediciones entity)
        {
            Context.IdsMediciones.Attach(entity);
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
        }

    

        public string TraerIndicador(int IdCia, int IdIndicador)
        {
            var a= Context.IdsCatIndicadores.Where(x => (x.IdsIdIndicador == IdIndicador)).FirstOrDefault().IdsDescripcionIndicador;
            return a;
        }


        public string TraerMedicion(int IdCia, int IdPlanta, int IdMedicion)
        {
            var a = Context.IdsCatMediciones.Where(x => (x.IdsIdMedicion == IdMedicion)).FirstOrDefault().IdsDescripcionMedicion;
            return a;
        }

        public string TraerElemento(int IdCia, int IdPlanta, int IdElemento)
        {
            var a = Context.IdsCatElementos.Where(x => (x.IdsIdElemento == IdElemento)).FirstOrDefault().IdsDescripcionElemento;
            return a;
        }


        public string TraerUnidad(int IdCia, int IdUnidad)
        {
            var a = Context.IdsCatUnidades.Where(x => (x.IdsIdUnidad == IdUnidad)).FirstOrDefault().IdsDescripcionUnidadMedida;
            return a;
        }
    }

}