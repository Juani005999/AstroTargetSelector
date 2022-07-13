using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

using UTIL_DYNAMICLTNVLib;

namespace ApplicationTools
{
    #region TimeSpan

    /// <summary>
    /// Classe d'extension dédiée au TimeSpan
    /// </summary>
    public static class TimeSpanExtensionMethods
    {
        /// <summary>
        /// Renvoi le TimeSpan sous la forme d'une chaîne formatée
        /// </summary>
        /// <param name="span"></param>
        /// <returns>Chaîne formatée</returns>
        public static string ToReadableString(this TimeSpan span)
        {
            string chaineFormatee = string.Empty;

            // Si plus d'une semaine
            if (span.Days / 7 > 0)
                chaineFormatee += string.Format("{0:0} semaines, ", span.Days / 7);
            // Si plus d'un jour
            if (span.Days % 7 > 0)
                chaineFormatee += string.Format("{0:0} jours, ", span.Days % 7);
            // Si plus d'une heure
            if (span.Hours > 0)
                chaineFormatee += string.Format("{0:0} heures, ", span.Hours);
            // Si plus d'une minute
            if (span.Minutes > 0)
                chaineFormatee += string.Format("{0:0} minutes, ", span.Minutes);
            // Si plus d'une seconde
            if (span.Seconds > 0)
                chaineFormatee += string.Format("{0:0} secondes, ", span.Seconds);
            // Dans tous les cas on ajoute les millisecondes (nb de milliseconde dans la seconde courante)
            chaineFormatee += string.Format("{0:0} ms", span.Milliseconds);

            return chaineFormatee;
        }
    }

    #endregion

    #region DateTime

    /// <summary>
    /// Classe d'extension dédiée au DateTime
    /// </summary>
    public static class DateTimeExtensionMethods
    {
        /// <summary>
        /// Retourne le temps écoulé depuis <paramref name="d"/> en millisecondes.
        /// </summary>
        /// <remarks>
        /// La valeur est retournée sous la forme d'un Int32 par commodité.
        /// Une précision supplémentaire n'est pas nécessaire dans notre contexte.
        /// </remarks>
        /// <param name="d">DateTime de référence</param>
        /// <returns>Temps écoulé en millisecondes.</returns>
        public static int GetElapsed(this DateTime d)
        {
            return (int)(DateTime.Now - d).TotalMilliseconds;
        }
    }

    #endregion


    #region XmlNode

    /// <summary>
    /// Classe d'extension dédiée au XmlNode
    /// </summary>
    public static class XmlNodeExtensionMethods
    {
        /// <summary>
        /// Retourne la valeur (propriété <see cref="XmlNode.InnerText"/>) contenue dans le premier sous-noeud du noeud courant correspondant au chemin XPATH fourni.
        /// Si aucun noeud ne correspond, une chaîne vide est retournée.
        /// </summary>
        /// <param name="node">Noeud sur lequel il faut faire la recherche</param>
        /// <param name="xpath">Chemin XPATH du noeud recherché</param>
        /// <returns>Valeur du sous-noeud ou chaine vide</returns>
        public static string GetNodeValueOrEmpty(this XmlNode node, string xpath)
        {
            if (node == null) throw new ArgumentNullException("node");

            XmlNode res = node.SelectSingleNode(xpath);
            return (res != null) ? res.InnerText : String.Empty;
        }

        /// <summary>
        /// Retourne la valeur de l'attribut du noeud fourni.
        /// </summary>
        /// <param name="node">Noeud sur lequel il faut faire la recherche</param>
        /// <param name="attributeName">Nom de l'attribut</param>
        /// <returns>Valeur de l'attribut ou chaine vide</returns>
        public static string GetAttributeValueOrEmpty(this XmlNode node, string attributeName)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return (node.Attributes[attributeName] != null) ? node.Attributes[attributeName].Value : string.Empty;
        }
    }

    #endregion
}
