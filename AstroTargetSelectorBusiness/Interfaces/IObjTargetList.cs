using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet représentant une liste d'objet <see cref="IObjTarget"/>
    /// </summary>
    public interface IObjTargetList
    {
        #region Propriétés

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// <para>Afin de forcer le rechargement de la liste depuis le fichier de configuration, il faut positionner la propriété <see cref="ForceUpdateListe"/> à true</para>
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
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration
        /// </summary>
        string TargetListeFullPathFile { get; }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des objets céleste
        /// </summary>
        string TargetListeFileName { get; }

        #endregion
    }
}
