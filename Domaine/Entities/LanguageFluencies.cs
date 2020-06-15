using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine.Entities
{
    public class LanguageFluencies
    {
        [Key]
        public int LanguageFluencyID { get; set; }

        public int ProfileID { get; set; }
        [ForeignKey("ProfileID"), Required]
        virtual public Profile Profile { get; set; }

        public int LanguageID { get; set; }
        [ForeignKey("LanguageID"), Required]
        virtual public Languages Languages { get; set; }

        public int ConversationFluencyID { get; set;}
        [ForeignKey("ConversationFluencyID")]
        virtual public Fluency ConversationFluency { get; set; }

        public int SpokenFluencyID { get; set; }
        [ForeignKey("SpokenFluencyID")]
        virtual public Fluency SpokenFluency { get; set; }

        public int TechnicalVocabFluencyID { get; set; }
        [ForeignKey("TechnicalVocabFluencyID")]
        virtual public Fluency TechnicalVocabFluency { get; set; }

        public int WrittenExpressionFluencyID { get; set; }
        [ForeignKey("WrittenExpressionFluencyID")]
        virtual public Fluency WrittenExpressionFluency { get; set; }

        public int WrittenComprehensionFluencyID { get; set; }
        [ForeignKey("WrittenComprehensionFluencyID")]
        virtual public Fluency WrittenComprehensionFluency { get; set; }
    }
}
