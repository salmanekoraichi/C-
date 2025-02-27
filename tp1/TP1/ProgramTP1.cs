using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

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
