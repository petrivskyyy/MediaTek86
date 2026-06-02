using System;
using System.Windows.Forms;
using MediaTek86.controleur;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenêtre principale : liste des personnels et boutons de gestion
    /// (CU n°2 à 5).
    /// </summary>
    public partial class FrmAccueil : Form
    {
        /// <summary>
        /// Contrôleur de la fenêtre.
        /// </summary>
        private readonly FrmAccueilControleur controleur;

        /// <summary>
        /// Liaison de données pour le DataGridView des personnels.
        /// </summary>
        private BindingSource bdgPersonnels = new BindingSource();

        public FrmAccueil()
        {
            InitializeComponent();
            controleur = new FrmAccueilControleur();
            RemplirListePersonnels();
        }

        /// <summary>
        /// Remplit le DataGridView avec la liste des personnels.
        /// </summary>
        private void RemplirListePersonnels()
        {
            bdgPersonnels.DataSource = controleur.GetLesPersonnels();
            dgvPersonnels.DataSource = bdgPersonnels;
            if (dgvPersonnels.Columns.Count > 0)
            {
                // On masque l'identifiant et l'idservice (peu utiles à l'affichage)
                dgvPersonnels.Columns["Idpersonnel"].Visible = false;
                dgvPersonnels.Columns["Idservice"].Visible = false;
                dgvPersonnels.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPersonnels.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPersonnels.ReadOnly = true;
            }
        }

        /// <summary>
        /// Retourne le personnel actuellement sélectionné dans la liste,
        /// ou null si aucune ligne n'est sélectionnée.
        /// </summary>
        private Personnel GetPersonnelSelectionne()
        {
            if (dgvPersonnels.CurrentRow != null)
            {
                return (Personnel)bdgPersonnels.List[dgvPersonnels.CurrentRow.Index];
            }
            return null;
        }

        /// <summary>
        /// Clic sur "Ajouter" : ouvre la fenêtre d'ajout d'un personnel (CU n°2).
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            FrmPersonnel frmPersonnel = new FrmPersonnel(controleur);
            frmPersonnel.ShowDialog();
            RemplirListePersonnels();
        }

        /// <summary>
        /// Clic sur "Modifier" : ouvre la fenêtre de modification (CU n°4).
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            Personnel personnel = GetPersonnelSelectionne();
            if (personnel == null)
            {
                MessageBox.Show("Veuillez sélectionner un personnel.", "Information");
                return;
            }
            FrmPersonnel frmPersonnel = new FrmPersonnel(controleur, personnel);
            frmPersonnel.ShowDialog();
            RemplirListePersonnels();
        }

        /// <summary>
        /// Clic sur "Supprimer" : demande confirmation puis supprime (CU n°3).
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            Personnel personnel = GetPersonnelSelectionne();
            if (personnel == null)
            {
                MessageBox.Show("Veuillez sélectionner un personnel.", "Information");
                return;
            }
            DialogResult reponse = MessageBox.Show(
                "Voulez-vous vraiment supprimer " + personnel.Prenom + " " + personnel.Nom +
                " ?\n(Toutes ses absences seront aussi supprimées.)",
                "Confirmation de suppression", MessageBoxButtons.YesNo);
            if (reponse == DialogResult.Yes)
            {
                controleur.DelPersonnel(personnel);
                RemplirListePersonnels();
            }
        }

        /// <summary>
        /// Clic sur "Gérer les absences" : ouvre la fenêtre des absences (CU n°5).
        /// </summary>
        private void BtnAbsences_Click(object sender, EventArgs e)
        {
            Personnel personnel = GetPersonnelSelectionne();
            if (personnel == null)
            {
                MessageBox.Show("Veuillez sélectionner un personnel.", "Information");
                return;
            }
            FrmAbsence frmAbsence = new FrmAbsence(controleur, personnel);
            frmAbsence.ShowDialog();
        }

        /// <summary>
        /// Fermeture de la fenêtre : ferme l'application.
        /// </summary>
        private void FrmAccueil_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}