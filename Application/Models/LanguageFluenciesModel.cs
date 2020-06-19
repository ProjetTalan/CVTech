using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class LanguageFluenciesModel
    {
        public int ProfileID { get; set; }
        virtual public string Profile { get; set; }

        public string LanguagesName { get; set; }

        public int? ConversationFluencyID { get; set; }

        public int? SpokenFluencyID { get; set; }

        public int? TechnicalVocabFluencyID { get; set; }

        public int? WrittenExpressionFluencyID { get; set; }

        public int? WrittenComprehensionFluencyID { get; set; }
    }
}
