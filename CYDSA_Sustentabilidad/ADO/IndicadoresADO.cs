using CYDSA_Sustentabilidad.ADO;
using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYDSA_Sustentabilidad.ADO
{
    public class IndicadoresADO
    {
        Entities Context;   // esta es la alias de la base de datos
        public IndicadoresADO()
        {
            Context = new Entities();
        }
       public IEnumerable<IdsCatIndicadores> cmbindicadores() // este metodo carga combo
        {
            return Context.IdsCatIndicadores.ToList();
        }



    }
}