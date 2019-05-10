using CYDSA_Sustentabilidad.ADO;
using CYDSA_Sustentabilidad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYDSA_Sustentabilidad.ADO
{
    public class ElementosADO
    {
        Entities Context;  // esta es la alias de la base de datos
        public ElementosADO()
        {
            Context = new Entities();
        }
        public IEnumerable<IdsCatElementos> cmbelementos() // este metodo carga combo
        {
            return Context.IdsCatElementos.ToList();
            //.IdsCatMedicion.ToList();
        }
    }


}