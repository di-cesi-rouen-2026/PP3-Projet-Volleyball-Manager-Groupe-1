using Microsoft.Data.Sqlite;



// Ce programme est uniquement dédié à tester les fonctions liées à la database.

namespace JeuxVolley
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DateOnly date1 = new DateOnly(2000, 6, 1);

            DatabaseClass.CreateDatabase();

            DatabaseClass.InsertValueIntoPartie("Nom de Partie");
            DatabaseClass.InsertValueIntoTournoi("2", 1);
            DatabaseClass.InsertValueIntoEquipe("Equipe 1", "Orléans", 0);
            DatabaseClass.InsertValueIntoEquipe("Equipe 2", "Bagdad", 0);
            DatabaseClass.InsertValueIntoJoueur("Robert", "Jeremy", 58, 91, 18, 80, 150000, 3);
            DatabaseClass.InsertValueIntoCoach("Coach", "One", 58, 91, 18, 150000);
            DatabaseClass.InsertValueIntoMatch(date1, 1, 2, 60, 25, 1, 2, 1);
            DatabaseClass.DeleteValueFromTable("PARTIE", 2);
            DatabaseClass.UdpdateStringValueFromTable("PARTIE", "nom", 1, "Nom nouveau");
            List<Dictionary<string, object?>> donnéeDansPartie = DatabaseClass.ReadAllValueFromTable("Partie");
            List<Dictionary<string, object?>> donnéeDansJoueur = DatabaseClass.ReadAllValueFromTable("Joueur");

            foreach (Dictionary<string, object?> ligneDansTable in donnéeDansPartie)
            {
                Console.WriteLine($"id d'objet dans la table Partie : {ligneDansTable["id"]}"); // Le 2 n'apparait jamais car il est supprimé par une des ligne au dessus
            }
            Console.WriteLine("\n");

            Dictionary<string, object?> test = DatabaseClass.ObtainValuesForId("equipe", "2");
            foreach (var kvp in test)
            {
                Console.WriteLine(kvp.Key + " " + kvp.Value);
            }

        }
    }
}