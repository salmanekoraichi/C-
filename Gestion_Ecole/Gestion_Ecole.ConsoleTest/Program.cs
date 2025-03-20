using System;
using System.Collections.Generic;
using DB.LIB.DataAccess;  // Pour accéder aux DAO (ex: DAOEleve)
using DB.LIB.Models;      // Pour accéder aux classes modèles (ex: Eleve)
using DB.LIB.Interfaces;  // Pour accéder aux interfaces (ex: IDAO)
namespace Gestion_Ecole.ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Test du DAOEleve ===");

            // Création d'une instance du DAOEleve
            DAOEleve daoEleve = new DAOEleve();

            // 1. Insertion d'un élève
            Eleve e1 = new Eleve
            {
                Code = "001",
                Nom = "Dupont",
                Prenom = "Jean",
                Niveau = "Niv1",
                Code_Fil = "FIL1"
            };

            int insResult = daoEleve.insert(e1);
            Console.WriteLine("Insertion de E001 : " + (insResult > 0 ? "Réussie" : "Échouée"));

            // 2. Afficher tous les élèves
            Console.WriteLine("\nListe des élèves après insertion :");
            List<Eleve> listeEleves = daoEleve.findAll();
            foreach (Eleve e in listeEleves)
            {
                Console.WriteLine($"{e.Code} - {e.Nom} {e.Prenom} - {e.Niveau} - Filière : {e.Code_Fil}");
            }

            // 3. Recherche d'un élève par code
            Console.WriteLine("\nRecherche de l'élève avec le code 'E001' :");
            Eleve eFound = daoEleve.findById("001");
            if (eFound != null)
            {
                Console.WriteLine($"Élève trouvé : {eFound.Code} - {eFound.Nom} {eFound.Prenom}");
            }
            else
            {
                Console.WriteLine("Aucun élève trouvé avec ce code.");
            }

            // 4. Mise à jour : modification du niveau de l'élève
            Console.WriteLine("\nMise à jour du niveau de l'élève E001 (passage de Niv1 à Niv2)...");
            e1.Niveau = "Niv2";
            int updResult = daoEleve.update(e1);
            Console.WriteLine("Mise à jour : " + (updResult > 0 ? "Réussie" : "Échouée"));

            // Vérification après mise à jour
            Console.WriteLine("\nListe des élèves après mise à jour :");
            listeEleves = daoEleve.findAll();
            foreach (Eleve e in listeEleves)
            {
                Console.WriteLine($"{e.Code} - {e.Nom} {e.Prenom} - {e.Niveau} - Filière : {e.Code_Fil}");
            }

            // 5. Suppression de l'élève
            Console.WriteLine("\nSuppression de l'élève avec le code 'E001'...");
            int delResult = daoEleve.delete("E001");
            Console.WriteLine("Suppression : " + (delResult > 0 ? "Réussie" : "Échouée"));

            // Vérification finale après suppression
            Console.WriteLine("\nListe des élèves après suppression :");
            listeEleves = daoEleve.findAll();
            foreach (Eleve e in listeEleves)
            {
                Console.WriteLine($"{e.Code} - {e.Nom} {e.Prenom} - {e.Niveau} - Filière : {e.Code_Fil}");
            }

            Console.WriteLine("\nTest terminé. Appuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}
