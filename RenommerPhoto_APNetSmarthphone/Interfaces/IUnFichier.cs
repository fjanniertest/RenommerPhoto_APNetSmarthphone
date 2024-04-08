using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RenommerPhoto_APNetSmarthphone.Interfaces
{
    /// <summary>
    /// un Fichier
    /// </summary>
    /// <example>APN P1060611.JPG</example>
    /// <example>Smarphone IMG20210927113100.jpg</example>
    public interface IUnFichier
    {
        /// <summary>
        /// Modele du fichier
        /// </summary>
        string Modele { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        /// <remarks>le nom renommé : 2018_09_18 10_30_06 - New York - Photo APN 003 - High Line - Empire State Buiding.jpg</remarks>
        /// <remarks>le nom renommé : 2018_09_19 13_53_24 - New York - Photo Smartphone 015 - One Trade Trader.jpg</remarks>
        string Nom { get; set; }

        /// <summary>
        /// Type d'appareil : APN ou Smartphone
        /// </summary>
        TTypeAppareil TypeAppareil { get; }

        /// <summary>
        /// Nom du fichier
        /// </summary>
        FileInfo NomFichier { get; }

        /// <summary>
        /// Date du fichier au format yyyy_MM_dd HH_mm_ss
        /// </summary>
        string Date { get; }

        /// <summary>
        /// Numéro de la photo
        /// </summary>
        int Numero { get; set; }
    }
}
