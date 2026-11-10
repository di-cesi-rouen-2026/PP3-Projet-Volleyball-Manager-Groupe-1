using Microsoft.Data.Sqlite;

namespace JeuxVolley
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DateOnly date1 = new DateOnly(2000, 6, 1);

            SqliteConnection connection = DatabaseClass.ConnectionDB();
            DatabaseClass.CreateDatabase(connection);
            DatabaseClass.InsertValueIntoPartie(connection, "à l'aide");
            DatabaseClass.InsertValueIntoTournoi(connection, "2", 1);
            DatabaseClass.InsertValueIntoEquipe(connection, "Equipe 1", "Orléans", 0);
            DatabaseClass.InsertValueIntoEquipe(connection, "Equipe 2", "Bagdad", 0);
            DatabaseClass.InsertValueIntoJoueur(connection, "Robert", "Jeremy", 58, 91, 18, 80, 150000, 3);
            DatabaseClass.InsertValueIntoCoach(connection, "Coach", "One", 58, 91, 18, 150000);
            DatabaseClass.InsertValueIntoMatch(connection, date1, 1, 2, 60, 25, 1, 2, 1);

        }
    }
}