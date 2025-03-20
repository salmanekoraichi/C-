using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Models
{
    /// <summary>
    /// Modèle représentant une note.
    /// </summary>
    public class Notes
    {
        public int Id { get; set; }
        public string Code_Eleve { get; set; }    // Référence à Eleve.Code
        public string Code_Matiere { get; set; }    // Référence à Matiere.Code
        public float Note { get; set; }
    }
}
