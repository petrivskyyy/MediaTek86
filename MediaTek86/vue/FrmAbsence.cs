using System;
using System.Windows.Forms;
using MediaTek86.controleur;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenêtre de gestion des absences d'un personnel (CU n°5 à 8).
    /// </summary>
    public partial class FrmAbsence : Form
    {
        private readonly FrmAccueilControleur controleur;

        /// <summary>
        /// Personnel dont on gère les absences.
        /// </summary>
        private readonly Personnel personnel;

        /// <summary>
        /// Liaison de données pour le DataGridView des absences.
        /// </summary>
        private BindingSource bdgAbsences = new BindingSource();

        /// <summary>
        /// Mémorise la date de début de l'absence sélectionnée
        /// (clé primaire utilisée lors d'une modification).
        /// </summary>
        private DateTime? ancienneDatedebut = null;

        public FrmAbsence(FrmAccueilControleur controleur, Personnel personnel)
        {
            InitializeComponent();
            this.controleur = controleur;
            this.personnel = personnel;
            this.Text = "Absences de " + personnel.Prenom + " " + personnel.Nom;
            RemplirListeMotifs();
            RemplirListeAbsences();
        }

        /// <summary>
        /// Remplit la liste déroulante des motifs.
        /// </summary>
        private void RemplirListeMotifs()
        {
            cboMotif.DataSource = controleur.GetLesMotifs();
        }

        /// <summary>
        /// Remplit le DataGridView avec les absences du personnel.
        /// </summary>
        private void RemplirListeAbsences()
        {
            bdgAbsences.DataSource = controleur.GetLesAbsences(personnel.Idpersonnel);
            dgvAbsences.DataSource = bdgAbsences;
            if (dgvAbsences.Columns.Count > 0)
            {
                dgvAbsences.Columns["Idpersonnel"].Visible = false;
                dgvAbsences.Columns["Idmotif"].Visible = false;
                dgvAbsences.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvAbsences.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAbsences.ReadOnly = true;
            }
        }

        /// <summary>
        /// Retourne l'absence sélectionnée, ou null.
        /// </summary>
        private Absence GetAbsenceSelectionnee()
        {
            if (dgvAbsences.CurrentRow != null && bdgAbsences.Count > 0)
            {
                return (Absence)bdgAbsences.List[dgvAbsences.CurrentRow.Index];
            }
            return null;
        }

        /// <summary>
        /// Quand on clique sur une absence, on pré-remplit les zones de saisie
        /// (utile pour la modification, CU n°8).
        /// </summary>
        private void DgvAbsences_SelectionChanged(object sender, EventArgs e)
        {
            Absence absence = GetAbsenceSelectionnee();
            if (absence != null)
            {
                dtpDebut.Value = absence.Datedebut;
                dtpFin.Value = absence.Datefin;
                ancienneDatedebut = absence.Datedebut;
                foreach (Motif motif in cboMotif.Items)
                {
                    if (motif.Idmotif == absence.Idmotif)
                    {
                        cboMotif.SelectedItem = motif;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Contrôle commun aux ajouts et modifications : dates et chevauchement.
        /// </summary>
        /// <param name="exclureAncienne">true en modification</param>
        /// <returns>true si les saisies sont valides</returns>
        private bool ControleSaisies(bool exclureAncienne)
        {
            // 4a : champs remplis (le motif doit être sélectionné)
            if (cboMotif.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un motif.", "Information");
                return false;
            }
            // 4b : date de fin antérieure à la date de début
            if (dtpFin.Value < dtpDebut.Value)
            {
                MessageBox.Show("La date de fin ne peut pas être antérieure à la date de début.", "Erreur");
                return false;
            }
            // 4c : chevauchement avec une absence existante
            DateTime? exclusion = exclureAncienne ? ancienneDatedebut : null;
            if (controleur.AbsenceExisteDeja(personnel.Idpersonnel, dtpDebut.Value, dtpFin.Value, exclusion))
            {
                MessageBox.Show("Une absence est déjà programmée sur ce créneau.", "Erreur");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clic sur "Ajouter" une absence (CU n°6).
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (!ControleSaisies(false))
            {
                return;
            }
            Motif motif = (Motif)cboMotif.SelectedItem;
            Absence absence = new Absence(personnel.Idpersonnel, dtpDebut.Value,
                dtpFin.Value, motif.Idmotif, motif.Libelle);
            controleur.AddAbsence(absence);
            RemplirListeAbsences();
        }

        /// <summary>
        /// Clic sur "Modifier" une absence (CU n°8).
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            Absence absenceSelec = GetAbsenceSelectionnee();
            if (absenceSelec == null || ancienneDatedebut == null)
            {
                MessageBox.Show("Veuillez sélectionner une absence.", "Information");
                return;
            }
            if (!ControleSaisies(true))
            {
                return;
            }
            DialogResult reponse = MessageBox.Show(
                "Confirmez-vous l'enregistrement des modifications ?",
                "Confirmation", MessageBoxButtons.YesNo);
            if (reponse == DialogResult.No)
            {
                return;
            }
            Motif motif = (Motif)cboMotif.SelectedItem;
            Absence absence = new Absence(personnel.Idpersonnel, dtpDebut.Value,
                dtpFin.Value, motif.Idmotif, motif.Libelle);
            controleur.UpdateAbsence(absence, (DateTime)ancienneDatedebut);
            RemplirListeAbsences();
        }

        /// <summary>
        /// Clic sur "Supprimer" une absence (CU n°7).
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            Absence absence = GetAbsenceSelectionnee();
            if (absence == null)
            {
                MessageBox.Show("Veuillez sélectionner une absence.", "Information");
                return;
            }
            DialogResult reponse = MessageBox.Show(
                "Voulez-vous vraiment supprimer cette absence ?",
                "Confirmation de suppression", MessageBoxButtons.YesNo);
            if (reponse == DialogResult.Yes)
            {
                controleur.DelAbsence(absence);
                RemplirListeAbsences();
            }
        }

        /// <summary>
        /// Clic sur "Fermer".
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}