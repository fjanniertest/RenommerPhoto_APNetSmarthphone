using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace RenommerPhoto_APNetSmarthphone
{
    /// <summary>
    /// Configuration
    /// </summary>
    public static class Configuration
    {

        /// <summary>
        /// Lire une clef
        /// </summary>
        /// <param name="clef">nom de la clef</param>
        /// <returns>le nom de la clef</returns>
        public static string LireUneClef(string clef)
        {
            string valeur = null;
            MesTraces mesTraces = MesTraces.GetInstance(); 
            try
            {
                var appSettings = ConfigurationManager.AppSettings[clef];
                if (appSettings == null)
                {
                    string strMessage = string.Format("la clef suivante '{0}' a un message d'erreur", clef);
                    mesTraces.WriteLine(strMessage);
                }
                valeur = appSettings;
            }
            catch (ConfigurationErrorsException ex)
            {
                string strMessage = string.Format("la clef suivante '{0}' a un message d'erreur : {1}", clef, ex.Message);
                mesTraces.WriteLine(strMessage);
            }
            return valeur;
        }

        /// <summary>
        /// Est ce que le répertorie existe
        /// </summary>
        /// <param name="repertoire">nom du répertooire</param>
        /// <param name="sousRepertoire">sous répertoire (APN ou smartphone)</param>
        /// <returns>true si le répertoire existe</returns>
        public static bool EstRepertoireExiste(string repertoire, string sousRepertoire)
        {
            string strRepertoire = Path.Combine(repertoire, sousRepertoire);
            MesTraces mesTraces = MesTraces.GetInstance();
            if (!Directory.Exists(strRepertoire))
            {
                string strMessage = string.Format("Le répertoire suivant : '{0}' n'existe pas !", strRepertoire);
                mesTraces.WriteLine(strMessage);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Est ce que les données sont corrects
        /// </summary>
        /// <returns>true si les données sont corrects</returns>
        public static bool EstDonneesCorrects()
        {
            //Est ce que la clef "LOGS" existe ?
            string strlogs = Configuration.LireUneClef("LOGS");
            if (strlogs == null) return false;

            //Est ce que la clef "IN" existe ?
            string strIN = Configuration.LireUneClef("IN");
            if (strIN == null) return false;

            //Est ce que la clef "OUT" existe ?
            string strOUT = Configuration.LireUneClef("OUT");
            if (strOUT == null) return false;

            //Est ce que le réperoire APN existe , modifier si j'achete un APN
            //if (!Configuration.EstRepertoireExiste(strIN, nameof(TTypeAppareil.APN))) return false;

            //Est ce que le réperoire Smartphone existe ?
            if (!Configuration.EstRepertoireExiste(strIN, nameof(TTypeAppareil.Smartphone))) return false;

            return true;
        }

    }
}
