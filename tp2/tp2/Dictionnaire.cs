using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

// Classe Dictionnaire héritant de Document
public class Dictionnaire : Document
{
    public string Langue { get; set; }
    public int NbDefinitions { get; set; }

    public Dictionnaire(string titre, string langue, int nbDefinitions) : base(titre)
    {
        Langue = langue;
        NbDefinitions = nbDefinitions;
    }

    public override string Description()
    {
        return $"Dictionnaire n°{Numero} : \"{Titre}\" en {Langue} ({NbDefinitions} définitions)";
    }
}
