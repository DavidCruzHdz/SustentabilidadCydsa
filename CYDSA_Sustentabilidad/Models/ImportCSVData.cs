using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CYDSA_Sustentabilidad.Models
{
    public class ImportCSVData
    {
        public IEnumerable<System.Web.Mvc.SelectListItem> Names { get; set; }    // This is to display dynamic column      
        public HttpPostedFileBase File { get; set; }
        public DataTable Data { get; set; }
    }
}