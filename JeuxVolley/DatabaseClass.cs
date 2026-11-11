using JeuxVolley;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;


public class DatabaseClass
{
    public static SqliteConnection ConnectionDB()
    {
        SqliteConnection databaseConnection = new SqliteConnection("DataSource=DatabaseTEST.db");
        return databaseConnection;
    }

    public static void CreateDatabase(SqliteConnection databaseConnection)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS PARTIE (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nom VARCHAR(100) NOT NULL);
                                CREATE TABLE IF NOT EXISTS TOURNOI (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    division VARCHAR(50) NOT NULL,
                                    id_partie INTEGER NOT NULL,
                                    FOREIGN KEY (id_partie) REFERENCES PARTIE(id) ON DELETE CASCADE);
                                CREATE TABLE IF NOT EXISTS EQUIPE (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nom VARCHAR(100) NOT NULL,
                                    ville_origine VARCHAR(100),
                                    est_rival BOOLEAN DEFAULT 0);
                                CREATE TABLE IF NOT EXISTS JOUEUR (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nom VARCHAR(50) NOT NULL,
                                    prenom VARCHAR(50) NOT NULL,
                                    attaque INTEGER DEFAULT 50,
                                    defense INTEGER DEFAULT 50,
                                    passe INTEGER DEFAULT 50,
                                    energie INTEGER DEFAULT 100,
                                    blessure BOOLEAN DEFAULT 0,
                                    prix DECIMAL(10,2) DEFAULT 0.00,
                                    note DECIMAL(3,1) DEFAULT 0.0,
                                    id_equipe INTEGER NULL,
                                    FOREIGN KEY (id_equipe) REFERENCES EQUIPE(id) ON DELETE SET NULL);
                                CREATE TABLE IF NOT EXISTS COACH (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nom VARCHAR(50) NOT NULL,
                                    prenom VARCHAR(50) NOT NULL,
                                    boost_attaque INTEGER DEFAULT 0,
                                    boost_defense INTEGER DEFAULT 0,
                                    boost_passe INTEGER DEFAULT 0,
                                    prix DECIMAL(10,2) DEFAULT 0.00,
                                    id_equipe INTEGER NULL,
                                    FOREIGN KEY (id_equipe) REFERENCES EQUIPE(id) ON DELETE SET NULL);
                                CREATE TABLE IF NOT EXISTS MATCH (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    date_match DATE NOT NULL,
                                    score_equipe1 INTEGER DEFAULT 0,
                                    score_equipe2 INTEGER DEFAULT 0,
                                    vainqueur VARCHAR(100) NULL,
                                    perdant VARCHAR(100) NULL,
                                    id_tournoi INTEGER NOT NULL,
                                    id_equipe_domicile INTEGER NOT NULL,
                                    id_equipe_exterieur INTEGER NOT NULL,
                                    FOREIGN KEY (id_tournoi) REFERENCES TOURNOI(id) ON DELETE CASCADE,
                                    FOREIGN KEY (id_equipe_domicile) REFERENCES EQUIPE(id),
                                    FOREIGN KEY (id_equipe_exterieur) REFERENCES EQUIPE(id));";
        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }

    public static void InsertValueIntoEquipe(SqliteConnection databaseConnection, string nom, string villeOrigine, int estRival)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO EQUIPE (nom, ville_origine, est_rival)
                                 VALUES (@nom, @ville_origine, @est_rival)";
        tableCmd.Parameters.AddWithValue("@nom", nom);
        tableCmd.Parameters.AddWithValue("@ville_origine", villeOrigine);
        tableCmd.Parameters.AddWithValue("@est_rival", estRival);

        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }
    public static void InsertValueIntoJoueur(SqliteConnection databaseConnection, string nom, string prenom, int attaque, int defense, int passe, int energie, int prix, int note, int idEquipe = 1, int blessure = 0)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO JOUEUR (nom, prenom, attaque, defense, passe, energie, blessure, prix, note, id_equipe)
                                 VALUES (@nom, @prenom, @attaque, @defense, @passe, @energie, @blessure, @prix, @note, @idEquipe)";
        tableCmd.Parameters.AddWithValue("@nom", nom);
        tableCmd.Parameters.AddWithValue("@prenom", prenom);
        tableCmd.Parameters.AddWithValue("@attaque", attaque);
        tableCmd.Parameters.AddWithValue("@defense", defense);
        tableCmd.Parameters.AddWithValue("@passe", passe);
        tableCmd.Parameters.AddWithValue("@energie", energie);
        tableCmd.Parameters.AddWithValue("@prix", prix);
        tableCmd.Parameters.AddWithValue("@note", note);
        tableCmd.Parameters.AddWithValue("@idEquipe", idEquipe);
        tableCmd.Parameters.AddWithValue("@blessure", blessure);

        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }
    public static void InsertValueIntoCoach(SqliteConnection databaseConnection, string nom, string prenom, int boostAttaque, int boostDefense, int boostPasse, int prix, int idEquipe = 1)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO COACH (nom, prenom, boost_attaque, boost_defense, boost_passe, prix, id_equipe)
                                 VALUES (@nom, @prenom, @boost_attaque, @boost_defense, @boost_passe, @prix, @idEquipe)";
        tableCmd.Parameters.AddWithValue("@nom", nom);
        tableCmd.Parameters.AddWithValue("@prenom", prenom);
        tableCmd.Parameters.AddWithValue("@boost_attaque", boostAttaque);
        tableCmd.Parameters.AddWithValue("@boost_defense", boostDefense);
        tableCmd.Parameters.AddWithValue("@boost_passe", boostPasse);
        tableCmd.Parameters.AddWithValue("@prix", prix);
        tableCmd.Parameters.AddWithValue("@idEquipe", idEquipe);

        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }
    public static void InsertValueIntoMatch(SqliteConnection databaseConnection, DateOnly date, int idEquipeDomicile, int idEquipeExterieur, int scoreEquipe1, int scoreEquipe2, int vainqueur, int perdant, int idTournoi)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO MATCH (date_match, score_equipe1, score_equipe2, vainqueur, perdant, id_tournoi, id_equipe_domicile, id_equipe_exterieur)
                                 VALUES (@date, @score_equipe1, @score_equipe2, @vainqueur, @perdant, @id_tournoi, @id_equipe_domicile, @id_equipe_exterieur)";
        tableCmd.Parameters.AddWithValue("@date", date);
        tableCmd.Parameters.AddWithValue("@score_equipe1", scoreEquipe1);
        tableCmd.Parameters.AddWithValue("@score_equipe2", scoreEquipe2);
        tableCmd.Parameters.AddWithValue("@vainqueur", vainqueur);
        tableCmd.Parameters.AddWithValue("@perdant", perdant);
        tableCmd.Parameters.AddWithValue("@id_tournoi", idTournoi);
        tableCmd.Parameters.AddWithValue("@id_equipe_domicile", idEquipeDomicile);
        tableCmd.Parameters.AddWithValue("@id_equipe_exterieur", idEquipeExterieur);

        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }
    public static void InsertValueIntoTournoi(SqliteConnection databaseConnection, string division, int idPartie)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO TOURNOI (division, id_partie)
                                 VALUES (@division, @id_partie)";
        tableCmd.Parameters.AddWithValue("@division", division);
        tableCmd.Parameters.AddWithValue("@id_partie", idPartie);

        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }
    public static void InsertValueIntoPartie(SqliteConnection databaseConnection, string nom)
    {
        databaseConnection.Open();
        var tableCmd = databaseConnection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO PARTIE (nom)
                                 VALUES (@nom)";
        tableCmd.Parameters.AddWithValue("@nom", nom);

        tableCmd.ExecuteNonQuery();
        databaseConnection.Close();
    }
    public static void ReadAllValueFromPartie(SqliteConnection databaseConnection)
    {
        string SQLiteQuery = "SELECT * FROM PARTIE";
        databaseConnection.Open();
        using (SqliteCommand databaseCommand = new SqliteCommand(SQLiteQuery, databaseConnection))
        {
            using(SqliteDataReader databaseReader = databaseCommand.ExecuteReader())
            {
                while (databaseReader.Read())
                {
                    int id = Convert.ToInt32(databaseReader["id"]);
                    string nom = databaseReader["nom"].ToString();

                    // Ici indiquer quoi faire avec chaque ligne de la table PARTIE.
                    Console.WriteLine($"id = {id}   nom = {nom}"); // Exemple
                }
                    
            }
        }
        databaseConnection.Close();
    }

    public static void DeleteValueFromTable(SqliteConnection databaseConnection, string table, int id)
    {
        databaseConnection.Open();
        string SQLiteQuery = $"DELETE FROM {table} WHERE id = {id}";
        using(SqliteCommand databaseCommand = new SqliteCommand(SQLiteQuery, databaseConnection))
        {
            databaseCommand.Parameters.AddWithValue("@id", id);
            databaseCommand.ExecuteNonQuery();
        }
        databaseConnection.Close();
    }

    public static void UdpdateStringValueFromTable(SqliteConnection databaseConnection, string table, string colonne, int id, string newValue)
    {
        databaseConnection.Open();
        string SQLiteQuery = $"UPDATE {table} SET {colonne} = @{colonne} WHERE id = {id}";
        using (SqliteCommand databaseCommand = new SqliteCommand(SQLiteQuery, databaseConnection))
        {
            databaseCommand.Parameters.AddWithValue("@id", id);
            databaseCommand.Parameters.AddWithValue($"@{colonne}", newValue);
            databaseCommand.ExecuteNonQuery();
        }
        databaseConnection.Close();
    }


}
