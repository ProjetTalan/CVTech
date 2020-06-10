namespace Domaine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logs
    {
        [Key]
        public int LogID { get; set; }

        public int ModifID { get; set; }

        public int ProfileModifID { get; set; }

        public DateTime ModifDate { get; set; }

        [StringLength(100)]
        public string UpdateMsg { get; set; }

        public virtual Profiles Profiles { get; set; }

        public virtual Profiles Profiles1 { get; set; }
    }
}
