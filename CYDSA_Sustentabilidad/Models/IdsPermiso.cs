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
    
    public partial class IdsPermiso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IdsPermiso()
        {
            this.IdsRol = new HashSet<IdsRol>();
        }
    
        public int PermisoID { get; set; }
        public string Modulo { get; set; }
        public string Descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IdsRol> IdsRol { get; set; }
    }
}