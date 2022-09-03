using System.Collections.Generic;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
    /// </summary>
    public interface IAppTarget
    {
        #region Propriétés

        /// <summary>
        /// Liste des Targets
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé et la liste est chargée depuis le fichier des paramètres</para>
        /// </summary>
        IObjTargetList Targets { get; }

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        List<IObjTarget> ListeObjTarget { get; }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjTarget"/></para>
        /// </summary>
        bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Permet de forcer le rechargement des Slices
        /// </summary>
        bool ForceUpdateSlices { get; set; }

        /// <summary>
        /// Renvoi la liste distinctes des différents type d'objet céleste
        /// </summary>
        List<string> ListeType { get; }

        /// <summary>
        /// Filtre sur Nom ou Description pour l'affichage dans la liste
        /// </summary>
        string FiltreNomDescription { get; set; }

        /// <summary>
        /// Filtre sur Type pour l'affichage dans la liste
        /// </summary>
        string FiltreType { get; set; }

        /// <summary>
        /// Filtre sur Rank pour l'affichage dans la liste
        /// </summary>
        string FiltreRank { get; set; }

        /// <summary>
        /// Filtre sur Magnitude pour l'affichage dans la liste
        /// </summary>
        string FiltreMagnitude { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration des objets célestes
        /// </summary>
        string TargetListeFullPathFile { get; }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des objets céleste
        /// </summary>
        string TargetListeFileName { get; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi l'Objet céleste <see cref="IObjTarget"/> correspondant au nom passé en paramètre
        /// </summary>
        /// <param name="nomTarget">Nom de la target recherchée</param>
        /// <returns><see cref="IObjTarget"/>. Null si non trouvé ou si nomTarget est vide.</returns>
        IObjTarget GetTarget(string nomTarget);

        #endregion
    }
}
