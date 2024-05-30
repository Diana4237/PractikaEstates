//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PractikaEstates
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplies()
        {
            this.Demand = new HashSet<Demand>();
        }
    
        public int Id_supply { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Id_client { get; set; }
        public Nullable<int> Id_agent { get; set; }
        public Nullable<int> Id_estate { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Client Client { get; set; }
        public virtual EstateObj EstateObj { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Demand> Demand { get; set; }
    }
}
