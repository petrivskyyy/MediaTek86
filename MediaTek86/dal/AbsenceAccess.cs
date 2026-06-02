using System;
using System.Collections.Generic;
using MediaTek86.modele;

namespace MediaTek86.dal
{
    /// <summary>
    /// Classe d'accès aux données concernant les absences et les motifs.
    /// </summary>
    public class AbsenceAccess
    {
        /// <summary>
        /// Instance d'accès à la base de données.
        /// </summary>
        private readonly Access access = null;

        /// <summary>
        /// Constructeur : récupère l'instance unique d'Access.
        /// </summary>
        public AbsenceAccess()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Récupère la liste de tous les motifs (pour les listes déroulantes).
        /// </summary>
        /// <returns>Liste d'objets Motif</returns>
        public List<Motif> GetLesMotifs()
        {
            List<Motif> lesMotifs = new List<Motif>();
            if (access.Manager != null)
            {
                string req = "SELECT idmotif, libelle FROM motif ORDER BY idmotif;";
                List<object[]> records = access.Manager.ReqSelect(req);
                if (records != null)
                {
                    foreach (object[] record in records)
                    {
                        Motif motif = new Motif((int)record[0], (string)record[1]);
                        lesMotifs.Add(motif);
                    }
                }
            }
            return lesMotifs;
        }

        /// <summary>
        /// Récupère les absences d'un personnel, de la plus récente
        /// à la plus ancienne (CU n°5).
        /// </summary>
        /// <param name="idpersonnel">Identifiant du personnel</param>
        /// <returns>Liste d'objets Absence</returns>
        public List<Absence> GetLesAbsences(int idpersonnel)
        {
            List<Absence> lesAbsences = new List<Absence>();
            if (access.Manager != null)
            {
                string req = "SELECT a.idpersonnel AS idpersonnel, a.datedebut AS datedebut, ";
                req += "a.datefin AS datefin, a.idmotif AS idmotif, m.libelle AS libelle ";
                req += "FROM absence a JOIN motif m ON a.idmotif = m.idmotif ";
                req += "WHERE a.idpersonnel = @idpersonnel ";
                req += "ORDER BY a.datedebut DESC;";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", idpersonnel }
                };
                List<object[]> records = access.Manager.ReqSelect(req, parameters);
                if (records != null)
                {
                    foreach (object[] record in records)
                    {
                        Absence absence = new Absence(
                            (int)record[0],
                            (DateTime)record[1],
                            (DateTime)record[2],
                            (int)record[3],
                            (string)record[4]);
                        lesAbsences.Add(absence);
                    }
                }
            }
            return lesAbsences;
        }

        /// <summary>
        /// Vérifie si une absence existe déjà sur le créneau donné pour
        /// ce personnel (scénarios alternatifs 4c). Permet d'exclure
        /// une absence en cours de modification grâce à son ancienne date de début.
        /// </summary>
        /// <param name="idpersonnel">Identifiant du personnel</param>
        /// <param name="datedebut">Date de début à tester</param>
        /// <param name="datefin">Date de fin à tester</param>
        /// <param name="ancienneDatedebut">Ancienne date de début (cas modification), sinon null</param>
        /// <returns>true si un chevauchement existe, false sinon</returns>
        public bool AbsenceExisteDeja(int idpersonnel, DateTime datedebut, DateTime datefin, DateTime? ancienneDatedebut = null)
        {
            if (access.Manager != null)
            {
                // Deux créneaux se chevauchent si : debut1 <= fin2 ET debut2 <= fin1
                string req = "SELECT datedebut FROM absence ";
                req += "WHERE idpersonnel = @idpersonnel ";
                req += "AND datedebut <= @datefin AND datefin >= @datedebut ";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", idpersonnel },
                    { "@datedebut", datedebut },
                    { "@datefin", datefin }
                };
                // En modification, on ne compare pas l'absence avec elle-même
                if (ancienneDatedebut != null)
                {
                    req += "AND datedebut <> @ancienneDatedebut ";
                    parameters.Add("@ancienneDatedebut", ancienneDatedebut);
                }
                req += ";";
                List<object[]> records = access.Manager.ReqSelect(req, parameters);
                if (records != null)
                {
                    return records.Count > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Ajoute une absence dans la base de données (CU n°6).
        /// </summary>
        /// <param name="absence">L'absence à ajouter</param>
        public void AddAbsence(Absence absence)
        {
            if (access.Manager != null)
            {
                string req = "INSERT INTO absence (idpersonnel, datedebut, datefin, idmotif) ";
                req += "VALUES (@idpersonnel, @datedebut, @datefin, @idmotif);";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", absence.Idpersonnel },
                    { "@datedebut", absence.Datedebut },
                    { "@datefin", absence.Datefin },
                    { "@idmotif", absence.Idmotif }
                };
                access.Manager.ReqUpdate(req, parameters);
            }
        }

        /// <summary>
        /// Modifie une absence (CU n°8). La clé primaire étant composée
        /// de idpersonnel et datedebut, on identifie l'ancienne ligne
        /// grâce à son ancienne date de début.
        /// </summary>
        /// <param name="absence">Nouvelles valeurs de l'absence</param>
        /// <param name="ancienneDatedebut">Ancienne date de début (clé)</param>
        public void UpdateAbsence(Absence absence, DateTime ancienneDatedebut)
        {
            if (access.Manager != null)
            {
                string req = "UPDATE absence SET datedebut = @datedebut, ";
                req += "datefin = @datefin, idmotif = @idmotif ";
                req += "WHERE idpersonnel = @idpersonnel AND datedebut = @ancienneDatedebut;";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", absence.Idpersonnel },
                    { "@datedebut", absence.Datedebut },
                    { "@datefin", absence.Datefin },
                    { "@idmotif", absence.Idmotif },
                    { "@ancienneDatedebut", ancienneDatedebut }
                };
                access.Manager.ReqUpdate(req, parameters);
            }
        }

        /// <summary>
        /// Supprime une absence (CU n°7), identifiée par sa clé composée.
        /// </summary>
        /// <param name="absence">L'absence à supprimer</param>
        public void DelAbsence(Absence absence)
        {
            if (access.Manager != null)
            {
                string req = "DELETE FROM absence ";
                req += "WHERE idpersonnel = @idpersonnel AND datedebut = @datedebut;";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", absence.Idpersonnel },
                    { "@datedebut", absence.Datedebut }
                };
                access.Manager.ReqUpdate(req, parameters);
            }
        }
    }
}