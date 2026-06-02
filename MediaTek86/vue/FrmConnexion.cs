using System;
using System.Windows.Forms;
using MediaTek86.controleur;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenêtre de connexion du responsable (CU n°1).
    /// </summary>
    public partial class FrmConnexion : Form
    {
        /// <summary>
        /// Contrôleur de la fenêtre.
        /// </summary>
        private readonly FrmConnexionControleur controleur;

        /// <summary>
        /// Constructeur : initialise les composants et le contrôleur.
        /// </summary>
        public FrmConnexion()
        {
            InitializeComponent();
            controleur = new FrmConnexionControleur();
        }

        /// <summary>
        /// Clic sur le bouton "Se connecter".
        /// Vérifie que les champs sont remplis (3a) puis l'authentification (3b).
        /// </summary>
        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string pwd = txtPwd.Text;
            if (login == "" || pwd == "")
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Information");
                return;
            }
            if (controleur.ControleAuthentification(login, pwd))
            {
                // Authentification réussie : ouverture de la fenêtre principale
                FrmAccueil frmAccueil = new FrmAccueil();
                frmAccueil.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect.", "Erreur");
            }
        }

        /// <summary>
        /// Clic sur le bouton "Annuler" : ferme l'application.
        /// </summary>
        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}