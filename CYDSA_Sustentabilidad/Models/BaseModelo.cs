﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace CYDSA_Sustentabilidad.Controllers
{ 
    public class BaseModelo
    {
        public int PaginaActual { get; set; }
        public int TotalDeRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }
        public RouteValueDictionary ValoresQueryString { get; set; }
    }
}