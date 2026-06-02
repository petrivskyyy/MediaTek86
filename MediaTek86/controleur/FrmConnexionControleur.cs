using MediaTek86.dal;

namespace MediaTek86.controleur
{
    /// <summary>
    /// Contrôleur de la fenêtre de connexion (CU n°1 : Se connecter).
    /// </summary>
    public class FrmConnexionControleur
    {
        /// <summary>
        /// Objet d'accès aux données du personnel (et du responsable).
        /// </summary>
        private readonly PersonnelAccess personnelAccess;

        /// <summary>
        /// Constructeur : instancie l'accès aux données.
        /// </summary>
        public FrmConnexionControleur()
        {
            personnelAccess = new PersonnelAccess();
        }

        /// <summary>
        /// Demande à la couche d'accès de contrôler le couple login/mot de passe.
        /// </summary>
        /// <param name="login">Login saisi</param>
        /// <param name="pwd">Mot de passe saisi</param>
        /// <returns>true si l'authentification est correcte</returns>
        public bool ControleAuthentification(string login, string pwd)
        {
            return personnelAccess.ControleAuthentification(login, pwd);
        }
    }
}