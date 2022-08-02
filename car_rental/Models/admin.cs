
namespace car_rental.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public admin()
        {
            this.rents = new HashSet<rent>();
        }
    
        public int admin_id { get; set; }
        public string name { get; set; }
        public string cnic { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rent> rents { get; set; }
    }
}
