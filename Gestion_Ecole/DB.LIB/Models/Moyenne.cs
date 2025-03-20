using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Models
{
    /// <summary>
    /// Modèle représentant une moyenne annuelle d’un élève.
    /// </summary>
    public class Moyenne
    {
        public int Id { get; set; }
        public string Code_Eleve { get; set; }    // Référence à Eleve.Code
        public string Code_Fil { get; set; }        // Référence à Filiere.Code
        public string Niveau { get; set; }
        public float MoyenneValue { get; set; }
    }
}
