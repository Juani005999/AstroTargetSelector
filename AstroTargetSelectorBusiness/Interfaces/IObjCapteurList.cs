using System.Collections.Generic;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet représentant une liste d'objet <see cref="ObjCapteur"/>
    /// </summary>
    public interface IObjCapteurList
    {
        #region Propriétés

        /// <summary>
        /// Liste d'objets <see cref="IObjCapteur"/>
        /// </summary>
        List<IObjCapteur> ListeObjCapteur { get; }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjCapteur"/></para>
        /// </summary>
        bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration
        /// </summary>
        string CapteurListeFullPathFile { get; }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        string CapteurListeFileName { get; }

        #endregion
    }
}
