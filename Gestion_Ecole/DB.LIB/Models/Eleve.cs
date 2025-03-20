using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Models
{
    /// <summary>
    /// Modèle représentant un élève.
    /// </summary>
    public class Eleve
    {
        public int Id { get; set; }
        public string Code { get; set; }       // Clé unique (ex: "E123")
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Niveau { get; set; }
        public string Code_Fil { get; set; }     // Référence à la filière
    }
}
