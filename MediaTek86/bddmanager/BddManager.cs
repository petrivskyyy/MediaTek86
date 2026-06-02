using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MediaTek86.bddmanager
{
    /// <summary>
    /// Classe singleton permettant la connexion à la base de données
    /// et l'exécution des requêtes SQL.
    /// </summary>
    public class BddManager
    {
        /// <summary>
        /// Instance unique de la classe (singleton).
        /// </summary>
        private static BddManager instance = null;

        /// <summary>
        /// Objet de connexion à la base de données.
        /// </summary>
        private readonly MySqlConnection connection;

        /// <summary>
        /// Constructeur privé pour créer la connexion et l'ouvrir.
        /// </summary>
        /// <param name="stringConnect">Chaîne de connexion</param>
        private BddManager(string stringConnect)
        {
            connection = new MySqlConnection(stringConnect);
            connection.Open();
        }

        /// <summary>
        /// Création d'une seule instance de la classe (singleton).
        /// </summary>
        /// <param name="stringConnect">Chaîne de connexion</param>
        /// <returns>L'instance unique de BddManager</returns>
        public static BddManager GetInstance(string stringConnect)
        {
            if (instance == null)
            {
                instance = new BddManager(stringConnect);
            }
            return instance;
        }

        /// <summary>
        /// Exécution d'une requête de type SELECT (lecture).
        /// </summary>
        /// <param name="stringQuery">Requête SQL</param>
        /// <param name="parameters">Paramètres de la requête</param>
        /// <returns>Liste de tableaux d'objets (les enregistrements)</returns>
        public List<object[]> ReqSelect(string stringQuery, Dictionary<string, object> parameters = null)
        {
            List<object[]> records = new List<object[]>();
            MySqlCommand command = new MySqlCommand(stringQuery, connection);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }
            }
            command.Prepare();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                int nbCols = reader.FieldCount;
                while (reader.Read())
                {
                    object[] attributes = new object[nbCols];
                    reader.GetValues(attributes);
                    records.Add(attributes);
                }
            }
            return records;
        }

        /// <summary>
        /// Exécution d'une requête autre que SELECT (INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="stringQuery">Requête SQL</param>
        /// <param name="parameters">Paramètres de la requête</param>
        public void ReqUpdate(string stringQuery, Dictionary<string, object> parameters = null)
        {
            MySqlCommand command = new MySqlCommand(stringQuery, connection);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }
            }
            command.Prepare();
            command.ExecuteNonQuery();
        }
    }
}