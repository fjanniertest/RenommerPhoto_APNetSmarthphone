using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RenommerPhoto_APNetSmarthphone
{
    /// <summary>
    /// Ma Photo du smartphone
    /// </summary>
    public class MaPhoto_Smartphone : UnFichier
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="fileInfo">nom du fichier</param>
        /// <param name="numero">numéro de la photo</param>
        public MaPhoto_Smartphone(FileInfo fileInfo, int numero) : base(fileInfo, TTypeAppareil.Smartphone, numero)
        {
            this.Modele = Configuration.LireUneClef("Modele_JPG_SMARTPHONE");
            this.Nom = string.Format(this.Modele, base.Date, numero.ToString("000"));
        }
    }
}
