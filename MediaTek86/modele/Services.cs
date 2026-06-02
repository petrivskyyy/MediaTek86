namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant un service (administratif, prêt...).
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Identifiant du service.
        /// </summary>
        public int Idservice { get; set; }

        /// <summary>
        /// Nom du service.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        public Service(int idservice, string nom)
        {
            Idservice = idservice;
            Nom = nom;
        }

        /// <summary>
        /// Permet d'afficher le nom du service dans une liste déroulante.
        /// </summary>
        public override string ToString()
        {
            return Nom;
        }
    }
}