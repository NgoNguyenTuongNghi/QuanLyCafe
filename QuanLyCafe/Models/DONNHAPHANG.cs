//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCafe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DONNHAPHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONNHAPHANG()
        {
            this.CHITIETNHAPHANGs = new HashSet<CHITIETNHAPHANG>();
        }
    
        public int SOHOADONNHAP { get; set; }
        public System.DateTime NGAYNHAPHANG { get; set; }
        public bool TINHTRANGDONNHAP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNHAPHANG> CHITIETNHAPHANGs { get; set; }
    }
}
