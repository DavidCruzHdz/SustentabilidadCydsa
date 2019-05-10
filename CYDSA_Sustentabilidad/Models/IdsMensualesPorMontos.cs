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

    public partial class IdsMensualesPorMontos
    {
        public int IdsId { get; set; }
        public int IdsIdEmpresa { get; set; }
        [DisplayName("indicadores")]
        public int IdsIdIndicador { get; set; }
        [DisplayName("Ene.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesEnero { get; set; }
        [DisplayName("Feb.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesFebrero { get; set; }
        [DisplayName("Mzo.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesMarzo { get; set; }
        [DisplayName("Abr.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesAbril { get; set; }
        [DisplayName("May.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesMayo { get; set; }
        [DisplayName("Jun.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesJunio { get; set; }
        [DisplayName("Jul.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesJulio { get; set; }
        [DisplayName("Ago.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesAgosto { get; set; }
        [DisplayName("Sep.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesSeptiembre { get; set; }
        [DisplayName("Oct.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesOctubre { get; set; }
        [DisplayName("Nov.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesNoviembre { get; set; }
        [DisplayName("Dic.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsMesDiciembre { get; set; }
        [DisplayName("Anual")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Nullable<double> IdsAnual { get; set; }
        public Nullable<int> IdsAnio { get; set; }
        public Nullable<int> IdsUsuario { get; set; }
        public int IdsIdRegistro { get; set; }
        [DisplayName("Medici�n")]
        public int IdsIdUnidad { get; set; }

        public virtual IdsCatEmpresas IdsCatEmpresas { get; set; }
        public virtual IdsCatIndicadores IdsCatIndicadores { get; set; }
        public virtual IdsCatUnidades IdsCatUnidades { get; set; }
    }
}