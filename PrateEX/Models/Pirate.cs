using System;
using System.ComponentModel.DataAnnotations;

namespace PrateEX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ZhenDataAnnotation;

    public partial class Pirate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pirate()
        {
            Crews = new HashSet<Crew>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        [CurrentTime(ErrorMessage ="Assign date can not be later than now")]
        public DateTime Date { get; set; }

        [StringLength(20)]
        public string MyHabit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Crew> Crews { get; set; }
    }
}

namespace ZhenDataAnnotation
{
    public class CurrentTimeAttribute:ValidationAttribute{

        public CurrentTimeAttribute() { }

        public override bool IsValid(object value)
        {
            return (System.DateTime.Now >= (DateTime)value? true: false);
        }
    }    
}
