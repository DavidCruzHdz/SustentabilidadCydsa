using CYDSA_Sustentabilidad.ADO;
using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYDSA_Sustentabilidad.ADO
{
    public class MedicionesADO
    {
        Entities Context;  // esta es la alias de la base de datos
        public MedicionesADO()
        {
            Context = new Entities();
        }
        public IEnumerable<IdsCatMediciones> cmbmediciones(int idIndicador) // este metodo carga combo
        {
            return Context.IdsCatMediciones.ToList();//.Where(x=>x.IdsIdIndicador == idIndicador);
            //.IdsCatMedicion.ToList();
        }
        public IEnumerable<IdsCatMediciones> cmbmediciones() // este metodo carga combo
        {
            return Context.IdsCatMediciones.ToList();
                //.IdsCatMedicion.ToList();
        }

        public IEnumerable<IdsRelaciones> GetElemtos(int idMedicion) // este metodo carga combo
        {
            return Context.IdsRelaciones.ToList().Where(x => x.IdsIdMedicion == idMedicion);
            //.IdsCatMedicion.ToList();
        }



    }
}