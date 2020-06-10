namespace Domaine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fluency")]
    public partial class Fluency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fluency()
        {
            LanguageFluencies = new HashSet<LanguageFluencies>();
            LanguageFluencies1 = new HashSet<LanguageFluencies>();
            LanguageFluencies2 = new HashSet<LanguageFluencies>();
            LanguageFluencies3 = new HashSet<LanguageFluencies>();
            LanguageFluencies4 = new HashSet<LanguageFluencies>();
        }

        public int FluencyID { get; set; }

        [StringLength(50)]
        public string LevelDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanguageFluencies> LanguageFluencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanguageFluencies> LanguageFluencies1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanguageFluencies> LanguageFluencies2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanguageFluencies> LanguageFluencies3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanguageFluencies> LanguageFluencies4 { get; set; }
    }
}
