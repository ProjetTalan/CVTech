namespace Domaine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Profiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profiles()
        {
            LanguageFluencies = new HashSet<LanguageFluencies>();
            Logs = new HashSet<Logs>();
            Logs1 = new HashSet<Logs>();
            ProExps = new HashSet<ProExps>();
            ProfileTech = new HashSet<ProfileTech>();
            TrainCertif = new HashSet<TrainCertif>();
            Titles = new HashSet<Titles>();
            Titles1 = new HashSet<Titles>();
        }

        [Key]
        public int ProfileID { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        public int? GenderID { get; set; }

        public int? NationalityID { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Zip { get; set; }

        public int? CityID { get; set; }

        [StringLength(20)]
        public string PhoneNumer { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(32)]
        public string ProfilePassword { get; set; }

        public Role? ProfileMainType { get; set; }

        [StringLength(32)]
        public string PhotoFileName { get; set; }

        public virtual Cities Cities { get; set; }

        public virtual Genders Genders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanguageFluencies> LanguageFluencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logs> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logs> Logs1 { get; set; }

        public virtual Nationalities Nationalities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProExps> ProExps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileTech> ProfileTech { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainCertif> TrainCertif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Titles> Titles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Titles> Titles1 { get; set; }
    }
}
