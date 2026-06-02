using System;
using System.Collections.Generic;
using MediaTek86.dal;
using MediaTek86.modele;

namespace MediaTek86.controleur
{
    /// <summary>
    /// Contrôleur de la fenêtre principale (gestion du personnel
    /// et des absences).
    /// </summary>
    public class FrmAccueilControleur
    {
        /// <summary>
        /// Accès aux données du personnel et des services.
        /// </summary>
        private readonly PersonnelAccess personnelAccess;

        /// <summary>
        /// Accès aux données des absences et des motifs.
        /// </summary>
        private readonly AbsenceAccess absenceAccess;

        /// <summary>
        /// Constructeur : instancie les accès aux données.
        /// </summary>
        public FrmAccueilControleur()
        {
            personnelAccess = new PersonnelAccess();
            absenceAccess = new AbsenceAccess();
        }

        /// <summary>
        /// Récupère la liste de tous les personnels.
        /// </summary>
        public List<Personnel> GetLesPersonnels()
        {
            return personnelAccess.GetLesPersonnels();
        }

        /// <summary>
        /// Récupère la liste de tous les services.
        /// </summary>
        public List<Service> GetLesServices()
        {
            return personnelAccess.GetLesServices();
        }

        /// <summary>
        /// Récupère la liste de tous les motifs.
        /// </summary>
        public List<Motif> GetLesMotifs()
        {
            return absenceAccess.GetLesMotifs();
        }

        /// <summary>
        /// Récupère les absences d'un personnel (de la plus récente
        /// à la plus ancienne).
        /// </summary>
        public List<Absence> GetLesAbsences(int idpersonnel)
        {
            return absenceAccess.GetLesAbsences(idpersonnel);
        }

        /// <summary>
        /// Demande l'ajout d'un personnel.
        /// </summary>
        public void AddPersonnel(Personnel personnel)
        {
            personnelAccess.AddPersonnel(personnel);
        }

        /// <summary>
        /// Demande la modification d'un personnel.
        /// </summary>
        public void UpdatePersonnel(Personnel personnel)
        {
            personnelAccess.UpdatePersonnel(personnel);
        }

        /// <summary>
        /// Demande la suppression d'un personnel.
        /// </summary>
        public void DelPersonnel(Personnel personnel)
        {
            personnelAccess.DelPersonnel(personnel);
        }

        /// <summary>
        /// Vérifie si une absence existe déjà sur le créneau donné.
        /// </summary>
        public bool AbsenceExisteDeja(int idpersonnel, DateTime datedebut, DateTime datefin, DateTime? ancienneDatedebut = null)
        {
            return absenceAccess.AbsenceExisteDeja(idpersonnel, datedebut, datefin, ancienneDatedebut);
        }

        /// <summary>
        /// Demande l'ajout d'une absence.
        /// </summary>
        public void AddAbsence(Absence absence)
        {
            absenceAccess.AddAbsence(absence);
        }

        /// <summary>
        /// Demande la modification d'une absence.
        /// </summary>
        public void UpdateAbsence(Absence absence, DateTime ancienneDatedebut)
        {
            absenceAccess.UpdateAbsence(absence, ancienneDatedebut);
        }

        /// <summary>
        /// Demande la suppression d'une absence.
        /// </summary>
        public void DelAbsence(Absence absence)
        {
            absenceAccess.DelAbsence(absence);
        }
    }
}