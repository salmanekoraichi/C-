using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Models
{
    /// <summary>
    /// Modèle représentant une filière.
    /// </summary>
    public class Filiere
    {
        public int Id { get; set; }
        public string Code { get; set; }         // Clé unique (ex: "FIL1")
        public string Designation { get; set; }
    }
}
