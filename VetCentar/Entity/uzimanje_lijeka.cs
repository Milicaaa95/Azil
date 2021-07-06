namespace VetCentar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vet_centar.uzimanje_lijeka")]
    public partial class uzimanje_lijeka
    {
        public int id { get; set; }

        public DateTime datum_uzimanja { get; set; }

        public int veterinar_id { get; set; }

        public int lijek_id { get; set; }

        public int nalaz_id { get; set; }

        public int kolicina { get; set; }

        public virtual lijek lijek { get; set; }

        public virtual nalaz nalaz { get; set; }

        public virtual veterinar veterinar { get; set; }
    }
}
