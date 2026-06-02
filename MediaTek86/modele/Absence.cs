using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant une absence d'un personnel.
    /// </summary>
    public class Absence
    {
        public int Idpersonnel { get; set; }
        public DateTime Datedebut { get; set; }
        public DateTime Datefin { get; set; }
        public int Idmotif { get; set; }
        public string LibelleMotif { get; set; }

        public Absence(int idpersonnel, DateTime datedebut, DateTime datefin,
                       int idmotif, string libelleMotif)
        {
            Idpersonnel = idpersonnel;
            Datedebut = datedebut;
            Datefin = datefin;
            Idmotif = idmotif;
            LibelleMotif = libelleMotif;
        }
    }
}