namespace Domaine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LanguageFluencies
    {
        [Key]
        public int LanguageFluencyID { get; set; }

        public int ProfileID { get; set; }

        public int LanguageID { get; set; }

        public int? ConversationFluencyID { get; set; }

        public int? SpokenFluencyID { get; set; }

        public int? TechnicalVocabFluencyID { get; set; }

        public int? WrittenExpressionFluencyID { get; set; }

        public int? WrittenComprehensionFluencyID { get; set; }

        public virtual Fluency Fluency { get; set; }

        public virtual Fluency Fluency1 { get; set; }

        public virtual Fluency Fluency2 { get; set; }

        public virtual Fluency Fluency3 { get; set; }

        public virtual Fluency Fluency4 { get; set; }

        public virtual Languages Languages { get; set; }

        public virtual Profiles Profiles { get; set; }
    }
}
