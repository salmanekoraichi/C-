using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

// Classe abstraite Document avec auto-incrément pour le numéro d'enregistrement
public abstract class Document
{
    private static int compteur = 1;
    public int Numero { get; private set; }
    public string Titre { get; set; }

    public Document(string titre)
    {
        Titre = titre;
        Numero = compteur++;
    }

    public abstract string Description();
}