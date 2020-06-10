namespace Domaine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfileTech")]
    public partial class ProfileTech
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfileID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TechID { get; set; }

        public int? TechLevelID { get; set; }

        public virtual Profiles Profiles { get; set; }

        public virtual TechLevels TechLevels { get; set; }

        public virtual Technologies Technologies { get; set; }
    }
}
