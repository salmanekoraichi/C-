using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class Livre : Document
{
    public string Auteur { get; set; }
    public int NbPages { get; set; }

    public Livre(string titre, string auteur, int nbPages) : base(titre)
    {
        Auteur = auteur;
        NbPages = nbPages;
    }

    public override string Description()
    {
        return $"Livre n°{Numero} : \"{Titre}\" par {Auteur} ({NbPages} pages)";
    }
}