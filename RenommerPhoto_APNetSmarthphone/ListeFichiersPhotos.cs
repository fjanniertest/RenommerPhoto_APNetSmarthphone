using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RenommerPhoto_APNetSmarthphone.Interfaces;

namespace RenommerPhoto_APNetSmarthphone
{
    /// <summary>
    /// une liste de photos
    /// </summary>
    public class ListeFichiersPhotos : List<IUnFichier>
    {
        #region Champs

        /// <summary>
        /// la seule instance de la classe
        /// </summary>
        private static ListeFichiersPhotos instance;

        /// <summary>
        /// mes traces
        /// </summary>
        private MesTraces mesTraces;

        /// <summary>
        /// Elément non trouvé
        /// </summary>
        private const int NON_TROUVE = -1;

        /// <summary>
        /// Est ce que les données sont corrects ?
        /// </summary>
        private bool estDonneesCorrectes;

        #endregion Champs

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private ListeFichiersPhotos()
        {
            this.mesTraces = MesTraces.GetInstance();
            this.estDonneesCorrectes = Configuration.EstDonneesCorrects();
        }

        #endregion Constructeur

        #region Méthodes publics

        /// <summary>
        /// Get Instance
        /// </summary>
        /// <returns>la seule instance</returns>
        public static ListeFichiersPhotos GetInstance()
        {
            if (instance == null)
                instance = new ListeFichiersPhotos();
            return instance;
        }


        /// <summary>
        /// Fabriquer la liste des fichiers photos
        /// </summary>
        /// <returns>true si la classe est chargé en mémoire</returns>
        public bool Fabriquer()
        {
            //Est ce que les données de configuration sont corrects
            if (!estDonneesCorrectes)
            {
                mesTraces.WriteLine("les données du fichier de configurations ne sont pas corrects !");
                return false;
            }

            FileInfo fi = null;
            IUnFichier unFichier = null;
            TTypeAppareil typeAppareil = TTypeAppareil.NonDefini;

            //Récupérer la liste des photos
            //Exemple : :\Mes Documents\Photos\Morlaix
            string strIN = Configuration.LireUneClef("IN");
            string[] listeRepertoires = Directory.GetDirectories(strIN);
            bool estErreur = false;
            int intNumeroAPN = 1;
            int intNumeroSmartphone = 1;
            //vérifier que les sous répertoires : smarthone et APN existe !
            foreach (string repertoire in listeRepertoires)
            {
                int i = 1;
                string strRepertoireslisteFichiers = Path.Combine(strIN, repertoire);
                string[] listeFichiers = Directory.GetFiles(strRepertoireslisteFichiers);

                //il faut order les fichiers par date de derniere écriture,
                // par défaut, windows ne trie pas par date !
                SortedList<string, string> listeTri = new SortedList<string, string>();
                foreach (string strFichier in listeFichiers)
                {
                    fi = new FileInfo(strFichier);
                    listeTri.Add(fi.LastWriteTime.ToString("yyyy_MM_dd HH_mm_ss"), strFichier);
                }
                foreach (string strFichier in listeTri.Values)
                {
                    fi = new FileInfo(strFichier);
                    typeAppareil = GetType(fi);
                    switch (typeAppareil)
                    {
                        case TTypeAppareil.APN:
                            unFichier = new MaPhoto_APN(fi, intNumeroAPN);
                            intNumeroAPN++;
                            this.Add(unFichier);
                            break;
                        case TTypeAppareil.Smartphone:
                            unFichier = new MaPhoto_Smartphone(fi, intNumeroSmartphone);
                            intNumeroSmartphone++;
                            this.Add(unFichier);
                            break;
                        default:
                            mesTraces.WriteLine("Le type de l'appareil n'est pas déclaré");
                            estErreur = true;
                            break;
                    }
                    if (estErreur)
                        return false;
                }
                i++;
            }
            return true;
        }

        /// <summary>
        /// Sauvegarder tous les fichiers
        /// </summary>
        /// <returns>true si tous les fichiers ont été renommés sans erreur</returns>
        public bool Sauvegarder()
        {
            bool estCorrect = false;
            IUnFichier unFichier = null;
            string strIN = string.Empty;
            string strOUT = string.Empty;

            try
            {
                //Supprimer tous les fichiers
                strIN = Configuration.LireUneClef("IN");
                strOUT = Configuration.LireUneClef("OUT");

                //Création du répertoire si besoin
                if (!Directory.Exists(strOUT))
                    Directory.CreateDirectory(strOUT);

                string[] listeFichiers = Directory.GetFiles(strOUT);
                Console.WriteLine("Supprimer tous les fichiers");
                foreach (string strfichier in listeFichiers)
                {
                    Console.WriteLine(string.Format("\tSupprimer le fichier : '{0}'", strfichier));
                    File.Delete(strfichier);
                }

                //Créer tous les fichiers
                string strFichierSource = string.Empty;
                string strFichierDestination= string.Empty;

                string strMessage = string.Format("Début des fichiers : {0}", strOUT);
                mesTraces.WriteLine(strMessage);
                Console.WriteLine(strMessage);
                int intNumeroAPN = 0;
                int intNumeroSmartphone = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    unFichier = this[i];
                    switch (unFichier.TypeAppareil)
                    {
                        case TTypeAppareil.APN:
                            strFichierSource = Path.Combine(strIN, "APN", unFichier.NomFichier.Name);
                            intNumeroAPN++;
                            break;
                        case TTypeAppareil.Smartphone   :
                            strFichierSource = Path.Combine(strIN, "Smartphone", unFichier.NomFichier.Name);
                            intNumeroSmartphone++;
                            break;

                    }
                    strFichierDestination = Path.Combine(strOUT, unFichier.Nom);
                    File.Copy(strFichierSource , strFichierDestination, true);
                    strMessage = String.Format("\t{0} : Copie du fichier source '{1}' vers '{2}'", i.ToString("000"), strFichierSource, strFichierDestination);
                    mesTraces.WriteLine(strMessage);
                    Console.WriteLine(strMessage);
                }
                strMessage = string.Format("Fin des fichiers : {0}", strOUT);
                mesTraces.WriteLine(strMessage);
                Console.WriteLine(strMessage);
                strMessage = string.Format("Nombre de fichiers : APN={0}, Smartphone={1}, Total={2}", intNumeroAPN, intNumeroSmartphone, intNumeroAPN+intNumeroSmartphone);
                mesTraces.WriteLine(strMessage);
                Console.WriteLine(strMessage);
                estCorrect = true;
            }
            catch (Exception e)
            {
                mesTraces.WriteLine(e.Message);
            }
            return estCorrect;

        }
        #endregion Méthodes publics

        #region Méthodes privées

        /// <summary>
        /// Récupérer le type de l'appareil
        /// </summary>
        /// <param name="">nom du fichier</param>
        /// <returns>le type de l'appareil</returns>
        private TTypeAppareil GetType(FileInfo fi)
        {

            if (fi.DirectoryName.ToLower().Contains("apn"))
                return TTypeAppareil.APN;

            if (fi.DirectoryName.ToLower().Contains("smartphone"))
                return TTypeAppareil.Smartphone;

            return TTypeAppareil.NonDefini;

        }
        #endregion Méthodes privées
    }
}
