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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class IdsBitacora
    {
        public int IdsId { get; set; }
        [DisplayName("Planta")]
        public int IdsIdEmpresa { get; set; }
        [DisplayName("Mes")]
        public int IdsMes { get; set; }
        [DisplayName("A�o")]
        public int IdsAnio { get; set; }
        [DisplayName("Indicadores")]
        public int IdsIdIndicador { get; set; }
        [DisplayName("Mediciones")]
        public int IdsIdMedicion { get; set; }
        [DisplayName("Elementos")]
        public int IdsIdElemento { get; set; }
        [DisplayName("Unidad")]
        public int IdsIdUnidad { get; set; }
        [DisplayName("Tipo de Calculo")]
        public string IdsTipoDeCalculo { get; set; }
        [DisplayName("Valor Capturado")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public double IdsValorCalculado { get; set; }
        [DisplayName("Precio Dolar")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<decimal> IdsPrecioDolar { get; set; }
        public string IdsStatus { get; set; }
        [DisplayName("Modificacion")]
        public Nullable<System.DateTime> IdsFechaCambio { get; set; }
        [DisplayName("Usuario")]
        public Nullable<int> IdsUsuarioCambio { get; set; }
        [DisplayName("Nombre del usuario")]
        public string NombreUsuario { get; set; }

        public virtual IdsCatIndicadores IdsCatIndicadores { get; set; }
        public virtual IdsCatMediciones IdsCatMediciones { get; set; }
        public virtual IdsCatElementos IdsCatElementos { get; set; }
        public virtual IdsCatUnidades IdsCatUnidades { get; set; }
        public virtual IdsCatEmpresas IdsCatEmpresas { get; set; }
    }
}
