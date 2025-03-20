using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Models
{
    /// <summary>
    /// Modèle représentant une matière.
    /// </summary>
    public class Matiere
    {
        public int Id { get; set; }
        public string Code { get; set; }          // Clé unique (ex: "MAT1")
        public string Designation { get; set; }
        public int VH { get; set; }               // Volume horaire
        public string Code_Module { get; set; }     // Référence à Module.Code
    }
}
