using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

// Classe Employee avec ses propriétés
public class Employee
{
    public string Nom { get; set; }
    public double Salaire { get; set; }
    public string Poste { get; set; }
    public DateTime DateEmbauche { get; set; }

    public Employee(string nom, double salaire, string poste, DateTime dateEmbauche)
    {
        Nom = nom;
        Salaire = salaire;
        Poste = poste;
        DateEmbauche = dateEmbauche;
    }

    public override string ToString()
    {
        return $"Employee: {Nom}, Salaire: {Salaire}, Poste: {Poste}, Date d'embauche: {DateEmbauche.ToShortDateString()}";
    }
}
