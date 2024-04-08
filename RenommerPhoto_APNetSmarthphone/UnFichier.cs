using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenommerPhoto_APNetSmarthphone.Interfaces;
using System.IO;

namespace RenommerPhoto_APNetSmarthphone
{
    /// <summary>
    /// Un fichier
    /// Le fichier peut - etre :
    /// - une photo de l'apparail Photo Numérique
    /// - une photo de l'apparail du smartphone
    /// </summary>
    public abstract class UnFichier : IUnFichier
    {
        /// <summary>
        /// Modele du fichier
        /// </summary>
        public string Modele { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Type d'appareil : APN ou Smartphone
        /// </summary>
        public TTypeAppareil TypeAppareil { get; set; }

        /// <summary>
        /// Nom du fichier
        /// </summary>
        public FileInfo NomFichier { get; }

        /// <summary>
        /// Date du fichier au format yyyy_MM_dd HH_mm_ss
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// Numéro de la photo
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="fileInfo">nom du fichier</param>
        /// <param name="typeAppareil">type d'appareil</param>
        /// <param name="numero">numéro de la photo</param>
        public UnFichier(FileInfo fileInfo, TTypeAppareil typeAppareil, int numero)
        {
            this.NomFichier = fileInfo;
            this.TypeAppareil = typeAppareil;
            this.Date = fileInfo.LastWriteTime.ToString("yyyy_MM_dd HH_mm_ss");
            this.Numero = numero;
        }           
    }
}
