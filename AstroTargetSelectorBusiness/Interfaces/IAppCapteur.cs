using System.Collections.Generic;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet applicatif permettant d'accéder à la collection des Capteurs
    /// </summary>
    public interface IAppCapteur
    {
        #region Propriétés

        /// <summary>
        /// Liste des Capteurs
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé et la liste est chargée depuis le fichier des paramètres</para>
        /// </summary>
        IObjCapteurList Capteurs { get; }

        /// <summary>
        /// Liste d'objets <see cref="IObjCapteur"/>
        /// </summary>
        List<IObjCapteur> ListeObjCapteur { get; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration des capteurs
        /// </summary>
        string CapteurListeFullPathFile { get; }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        string CapteurListeFileName { get; }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjCapteur"/></para>
        /// </summary>
        bool ForceUpdateListe { get; set; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi le capteur <see cref="IObjCapteur"/> correspondant au nom passé en paramètre
        /// <para>Si le capteur n'est pas présent dans la liste, il est crée et ajouté dans la liste des capteurs</para>
        /// </summary>
        /// <param name="nomCapteur">Nom du Capteur recherché</param>
        /// <param name="largeurCapteur">Largeur en pixel du Capteur</param>
        /// <returns><see cref="IObjCapteur"/>. Null si nomCapteur est vide ou si largeurCapteur est égal à 0.</returns>
        IObjCapteur GetCapteur(string nomCapteur, double largeurCapteur);

        #endregion
    }
}
