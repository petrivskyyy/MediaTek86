namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant un motif d'absence.
    /// </summary>
    public class Motif
    {
        public int Idmotif { get; set; }
        public string Libelle { get; set; }

        public Motif(int idmotif, string libelle)
        {
            Idmotif = idmotif;
            Libelle = libelle;
        }

        /// <summary>
        /// Affichage du libellé dans une liste déroulante.
        /// </summary>
        public override string ToString()
        {
            return Libelle;
        }
    }
}