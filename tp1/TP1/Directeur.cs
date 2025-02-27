using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Classe Directeur implémentant le Singleton
public sealed class Directeur
{
    private static Directeur instance = null;
    private static readonly object padlock = new object();
    private GestionEmployes gestionEmployes;

    private Directeur()
    {
    }

    public static Directeur Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new Directeur();
                return instance;
            }
        }
    }

    public void SetGestionEmployes(GestionEmployes gestion)
    {
        gestionEmployes = gestion;
    }

    public double GetSalaireTotal()
    {
        if (gestionEmployes != null)
            return gestionEmployes.CalculerSalaireTotal();
        return 0;
    }

    public double GetSalaireMoyen()
    {
        if (gestionEmployes != null)
            return gestionEmployes.CalculerSalaireMoyen();
        return 0;
    }
}

// Programme principal TP1
class ProgramTP1
{
    static void Main(string[] args)
    {
        // Création de la gestion des employés
        GestionEmployes gestion = new GestionEmployes();
        
        // Ajout de quelques employés
        gestion.AjouterEmploye(new Employee("Alice", 2500, "Développeuse", DateTime.Now));
        gestion.AjouterEmploye(new Employee("Bob", 3000, "Analyste", DateTime.Now));
        gestion.AjouterEmploye(new Employee("Charlie", 4000, "Manager", DateTime.Now));

        // Récupération de l'instance unique du directeur et affectation de la gestion
        Directeur directeur = Directeur.Instance;
        directeur.SetGestionEmployes(gestion);

        // Affichage des informations
        Console.WriteLine("Salaire total de l'entreprise : " + directeur.GetSalaireTotal());
        Console.WriteLine("Salaire moyen de l'entreprise : " + directeur.GetSalaireMoyen());

        Console.WriteLine("\nAppuyez sur une touche pour terminer...");
        Console.ReadKey();
    }
}
