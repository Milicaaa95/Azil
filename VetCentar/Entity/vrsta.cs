namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.vrsta")]
    public partial class vrsta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vrsta()
        {
            ljubimacs = new HashSet<ljubimac>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string naziv { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ljubimac> ljubimacs { get; set; }

        public override bool Equals(object obj)
        {
            var vrsta = obj as vrsta;
            return vrsta != null &&
                   id == vrsta.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return naziv;
        }
    }
}
