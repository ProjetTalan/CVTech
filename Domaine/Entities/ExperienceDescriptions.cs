namespace Domaine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExperienceDescriptions
    {
        [Key]
        public int ExpID { get; set; }

        public int? ProExpID { get; set; }

        public int? LanguageID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int? PositionID { get; set; }

        public string Description { get; set; }

        public virtual Languages Languages { get; set; }

        public virtual Positions Positions { get; set; }

        public virtual ProExps ProExps { get; set; }
    }
}
