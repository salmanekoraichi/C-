using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.LIB.Models
{
    /// <summary>
    /// Modèle représentant un module.
    /// </summary>
    public class Module
    {
        public int Id { get; set; }
        public string Code { get; set; }         // Clé unique (ex: "MOD1")
        public string Designation { get; set; }
        public string Niveau { get; set; }
        public int Semestre { get; set; }
        public string Code_Fil { get; set; }       // Référence à Filiere.Code
    }
}
