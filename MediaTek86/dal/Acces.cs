using System;
using System.Configuration;
using System.Windows.Forms;
using MediaTek86.bddmanager;

namespace MediaTek86.dal
{
    /// <summary>
    /// Classe d'accès aux données : récupère la chaîne de connexion
    /// et fournit l'instance unique de BddManager.
    /// </summary>
    public class Access
    {
        /// <summary>
        /// Nom de la connexion défini dans App.config.
        /// </summary>
        private static readonly string connectionName = "MediaTek86.Properties.Settings.mediatek86ConnectionString";

        /// <summary>
        /// Instance unique de la classe (singleton).
        /// </summary>
        private static Access instance = null;

        /// <summary>
        /// Objet d'accès à la base de données.
        /// </summary>
        public BddManager Manager { get; set; }

        /// <summary>
        /// Constructeur privé : récupère la chaîne de connexion
        /// et instancie le BddManager.
        /// </summary>
        private Access()
        {
            string stringConnect = GetConnectionString(connectionName);
            try
            {
                Manager = BddManager.GetInstance(stringConnect);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Erreur de connexion à la base de données.", "Erreur");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création d'une seule instance de la classe (singleton).
        /// </summary>
        /// <returns>L'instance unique d'Access</returns>
        public static Access GetInstance()
        {
            if (instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Récupère la chaîne de connexion dans le fichier App.config.
        /// </summary>
        /// <param name="name">Nom de la connexion</param>
        /// <returns>La chaîne de connexion</returns>
        private string GetConnectionString(string name)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            return settings != null ? settings.ConnectionString : null;
        }
    }
}