using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine.Entities
{
    public class Fluency
    {
        [Key]
        public int FluencyID { get; set; }
        public string LevelDescription { get; set; }
    }
}
