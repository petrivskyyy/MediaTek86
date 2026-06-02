using System;
using System.Collections.Generic;
using MediaTek86.modele;

namespace MediaTek86.dal
{
    /// <summary>
    /// Classe d'accès aux données concernant le personnel,
    /// les services, les motifs et la connexion du responsable.
    /// </summary>
    public class PersonnelAccess
    {
        /// <summary>
        /// Instance d'accès à la base de données.
        /// </summary>
        private readonly Access access = null;

        /// <summary>
        /// Constructeur : récupère l'instance unique d'Access.
        /// </summary>
        public PersonnelAccess()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Contrôle l'authentification du responsable (CU n°1 : Se connecter).
        /// Le mot de passe est comparé avec sa version hashée en SHA2 256.
        /// </summary>
        /// <param name="login">Login saisi</param>
        /// <param name="pwd">Mot de passe saisi (en clair)</param>
        /// <returns>true si le couple login/pwd existe, false sinon</returns>
        public bool ControleAuthentification(string login, string pwd)
        {
            if (access.Manager != null)
            {
                string req = "SELECT login FROM responsable ";
                req += "WHERE login = @login AND pwd = SHA2(@pwd, 256);";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@login", login },
                    { "@pwd", pwd }
                };
                List<object[]> records = access.Manager.ReqSelect(req, parameters);
                if (records != null)
                {
                    return records.Count > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Récupère et retourne la liste de tous les personnels.
        /// </summary>
        /// <returns>Liste d'objets Personnel</returns>
        public List<Personnel> GetLesPersonnels()
        {
            List<Personnel> lesPersonnels = new List<Personnel>();
            if (access.Manager != null)
            {
                string req = "SELECT p.idpersonnel AS idpersonnel, p.nom AS nom, ";
                req += "p.prenom AS prenom, p.tel AS tel, p.mail AS mail, ";
                req += "p.idservice AS idservice, s.nom AS nomservice ";
                req += "FROM personnel p JOIN service s ON p.idservice = s.idservice ";
                req += "ORDER BY p.nom, p.prenom;";
                List<object[]> records = access.Manager.ReqSelect(req);
                if (records != null)
                {
                    foreach (object[] record in records)
                    {
                        Personnel personnel = new Personnel(
                            (int)record[0],
                            (string)record[1],
                            (string)record[2],
                            (string)record[3],
                            (string)record[4],
                            (int)record[5],
                            (string)record[6]);
                        lesPersonnels.Add(personnel);
                    }
                }
            }
            return lesPersonnels;
        }

        /// <summary>
        /// Récupère et retourne la liste de tous les services.
        /// </summary>
        /// <returns>Liste d'objets Service</returns>
        public List<Service> GetLesServices()
        {
            List<Service> lesServices = new List<Service>();
            if (access.Manager != null)
            {
                string req = "SELECT idservice, nom FROM service ORDER BY nom;";
                List<object[]> records = access.Manager.ReqSelect(req);
                if (records != null)
                {
                    foreach (object[] record in records)
                    {
                        Service service = new Service((int)record[0], (string)record[1]);
                        lesServices.Add(service);
                    }
                }
            }
            return lesServices;
        }

        /// <summary>
        /// Ajoute un personnel dans la base de données (CU n°2).
        /// </summary>
        /// <param name="personnel">Le personnel à ajouter</param>
        public void AddPersonnel(Personnel personnel)
        {
            if (access.Manager != null)
            {
                string req = "INSERT INTO personnel (nom, prenom, tel, mail, idservice) ";
                req += "VALUES (@nom, @prenom, @tel, @mail, @idservice);";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@nom", personnel.Nom },
                    { "@prenom", personnel.Prenom },
                    { "@tel", personnel.Tel },
                    { "@mail", personnel.Mail },
                    { "@idservice", personnel.Idservice }
                };
                access.Manager.ReqUpdate(req, parameters);
            }
        }

        /// <summary>
        /// Modifie un personnel dans la base de données (CU n°4).
        /// </summary>
        /// <param name="personnel">Le personnel à modifier</param>
        public void UpdatePersonnel(Personnel personnel)
        {
            if (access.Manager != null)
            {
                string req = "UPDATE personnel SET nom = @nom, prenom = @prenom, ";
                req += "tel = @tel, mail = @mail, idservice = @idservice ";
                req += "WHERE idpersonnel = @idpersonnel;";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", personnel.Idpersonnel },
                    { "@nom", personnel.Nom },
                    { "@prenom", personnel.Prenom },
                    { "@tel", personnel.Tel },
                    { "@mail", personnel.Mail },
                    { "@idservice", personnel.Idservice }
                };
                access.Manager.ReqUpdate(req, parameters);
            }
        }

        /// <summary>
        /// Supprime un personnel de la base de données (CU n°3).
        /// Supprime d'abord ses absences pour respecter les clés étrangères.
        /// </summary>
        /// <param name="personnel">Le personnel à supprimer</param>
        public void DelPersonnel(Personnel personnel)
        {
            if (access.Manager != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idpersonnel", personnel.Idpersonnel }
                };
                // On supprime d'abord les absences liées
                string reqAbs = "DELETE FROM absence WHERE idpersonnel = @idpersonnel;";
                access.Manager.ReqUpdate(reqAbs, parameters);
                // Puis le personnel
                string req = "DELETE FROM personnel WHERE idpersonnel = @idpersonnel;";
                access.Manager.ReqUpdate(req, parameters);
            }
        }
    }
}