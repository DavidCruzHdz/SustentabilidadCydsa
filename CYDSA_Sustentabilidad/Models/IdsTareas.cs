//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CYDSA_Sustentabilidad.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class IdsTareas
    {
        public int IdsId { get; set; }
        public int IdsEmpresa { get; set; }
        public int IdsMes { get; set; }
        public int IdsAnio { get; set; }
        public int IdsIdElemento { get; set; }
        public string IdsDescripciondeTarea { get; set; }
        public int IdsIdUsuario { get; set; }
        public Nullable<decimal> IdsTiempoEstimadoHR { get; set; }
        public Nullable<decimal> IdsTiempoEstimadoDias { get; set; }
        public System.DateTime IdsFechaInicio { get; set; }
        public Nullable<System.DateTime> IdsFechaTermino { get; set; }
        public Nullable<System.DateTime> IdsFechaAlta { get; set; }
        public Nullable<int> IdsUsuarioAlta { get; set; }
        public Nullable<System.DateTime> IdsFechaCambio { get; set; }
        public Nullable<int> IdsUsuarioCambio { get; set; }
    }
}
