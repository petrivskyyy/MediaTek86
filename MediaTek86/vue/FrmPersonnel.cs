using System;
using System.Windows.Forms;
using MediaTek86.controleur;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenêtre d'ajout ou de modification d'un personnel (CU n°2 et n°4).
    /// </summary>
    public partial class FrmPersonnel : Form
    {
        private readonly FrmAccueilControleur controleur;

        /// <summary>
        /// Personnel en cours de modification (null si on est en ajout).
        /// </summary>
        private readonly Personnel personnel;

        /// <summary>
        /// Indique si on est en mode modification.
        /// </summary>
        private readonly bool enModification;

        /// <summary>
        /// Constructeur pour l'ajout d'un personnel.
        /// </summary>
        public FrmPersonnel(FrmAccueilControleur controleur)
        {
            InitializeComponent();
            this.controleur = controleur;
            this.enModification = false;
            RemplirListeServices();
            this.Text = "Ajouter un personnel";
        }

        /// <summary>
        /// Constructeur pour la modification d'un personnel.
        /// </summary>
        public FrmPersonnel(FrmAccueilControleur controleur, Personnel personnel)
        {
            InitializeComponent();
            this.controleur = controleur;
            this.personnel = personnel;
            this.enModification = true;
            RemplirListeServices();
            PreRemplirChamps();
            this.Text = "Modifier un personnel";
        }

        /// <summary>
        /// Remplit la liste déroulante des services.
        /// </summary>
        private void RemplirListeServices()
        {
            cboService.DataSource = controleur.GetLesServices();
        }

        /// <summary>
        /// Pré-remplit les champs avec les infos du personnel (modification).
        /// </summary>
        private void PreRemplirChamps()
        {
            txtNom.Text = personnel.Nom;
            txtPrenom.Text = personnel.Prenom;
            txtTel.Text = personnel.Tel;
            txtMail.Text = personnel.Mail;
            // Sélectionne le service actuel dans la liste déroulante
            foreach (Service service in cboService.Items)
            {
                if (service.Idservice == personnel.Idservice)
                {
                    cboService.SelectedItem = service;
                    break;
                }
            }
        }

        /// <summary>
        /// Clic sur "Enregistrer" : valide les champs puis enregistre.
        /// </summary>
        private void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            // Vérifie que tous les champs sont remplis (4a)
            if (txtNom.Text.Trim() == "" || txtPrenom.Text.Trim() == "" ||
                txtTel.Text.Trim() == "" || txtMail.Text.Trim() == "" ||
                cboService.SelectedItem == null)
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Information");
                return;
            }
            Service service = (Service)cboService.SelectedItem;
            if (enModification)
            {
                // Demande de confirmation (CU n°4, point 4)
                DialogResult reponse = MessageBox.Show(
                    "Confirmez-vous l'enregistrement des modifications ?",
                    "Confirmation", MessageBoxButtons.YesNo);
                if (reponse == DialogResult.No)
                {
                    return;
                }
                Personnel p = new Personnel(personnel.Idpersonnel, txtNom.Text.Trim(),
                    txtPrenom.Text.Trim(), txtTel.Text.Trim(), txtMail.Text.Trim(),
                    service.Idservice, service.Nom);
                controleur.UpdatePersonnel(p);
            }
            else
            {
                Personnel p = new Personnel(0, txtNom.Text.Trim(), txtPrenom.Text.Trim(),
                    txtTel.Text.Trim(), txtMail.Text.Trim(), service.Idservice, service.Nom);
                controleur.AddPersonnel(p);
            }
            this.Close();
        }

        /// <summary>
        /// Clic sur "Annuler" : ferme la fenêtre sans rien enregistrer.
        /// </summary>
        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}