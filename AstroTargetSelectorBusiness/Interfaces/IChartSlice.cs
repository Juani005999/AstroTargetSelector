using System;
using System.Drawing;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface permettant la représentation d'un intervalle dans le graphique
    /// </summary>
    public interface IChartSlice
    {
        #region Propriétés

        /// <summary>
        /// Temps de pose calculé pour l'intervalle
        /// </summary>
        double TempsPoseCalcule { get; }

        /// <summary>
        /// Date et Heure de l'intervalle
        /// </summary>
        DateTime DateHeure { get; set; }

        /// <summary>
        /// Permet de savoir si le slice de l'objet céleste est exclu de la liste
        /// <para>Fait partie d'une zone exclue du ciel</para>
        /// <para>En dessous de la hauteur apparente (Hauteur du premier Slice)</para>
        /// </summary>
        bool EstExclu { get; }

        /// <summary>
        /// Couleur du point dans le graphique
        /// </summary>
        Color CouleurPointGraphique { get; }

        /// <summary>
        /// Hauteur calculé du slice
        /// </summary>
        Coordinate Hauteur { get; }

        /// <summary>
        /// Couleur du point dans le graphique
        /// <para>Vert si au dessus de la hauteur min, sinon rouge</para>
        /// </summary>
        Color CouleurHauteur { get; }

        /// <summary>
        /// Renvoi l'Azimut calculé pour la Target
        /// </summary>
        Coordinate Azimut { get; }

        /// <summary>
        /// Renvoi la direction actuelle
        /// <para>Basée sur l'Azimut</para>
        /// </summary>
        CoordinatesDirection Direction { get; }

        /// <summary>
        /// Renvoi le code du caractère correspondant à la Direction
        /// <para>Police utilisée WINGDING</para>
        /// </summary>
        char DirectionCharacterCode { get; }

        /// <summary>
        /// Renvoi l'Azimut Corrigee calculé pour la Target
        /// </summary>
        Coordinate AzimutCorrigee { get; }

        /// <summary>
        /// Renvoi l'Azimut Precise calculé pour la Target
        /// </summary>
        Coordinate AzimutPrecise { get; }

        /// <summary>
        /// Renvoi la Hauteur Precise calculé pour la Target
        /// </summary>
        Coordinate HauteurPrecise { get; }

        /// <summary>
        /// ToolTip du Slice à afficher dans le graphique
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// Nom de la phase lunaire correspondant au Slice
        /// </summary>
        string MoonPhaseName { get; }

        /// <summary>
        /// Altitude la Lune correspondant au Slice
        /// </summary>
        double? MoonAlt { get; }

        /// <summary>
        /// Image de la phase lunaire
        /// </summary>
        string MoonPhaseImage { get; }

        /// <summary>
        /// Azimut la Lune correspondant au Slice
        /// </summary>
        double? MoonAz { get; }

        /// <summary>
        /// SunRMoonRiseise correspondant au Slice
        /// </summary>
        DateTime? MoonRise { get; }

        /// <summary>
        /// MoonSet correspondant au Slice
        /// </summary>
        DateTime? MoonSet { get; }

        /// <summary>
        /// Altitude du Soleil correspondant au Slice
        /// </summary>
        double? SunAlt { get; }

        /// <summary>
        /// Azimut du Soleil correspondant au Slice
        /// </summary>
        double? SunAz { get; }

        /// <summary>
        /// SunRise correspondant au Slice
        /// </summary>
        DateTime? SunRise { get; }

        /// <summary>
        /// SunSet correspondant au Slice
        /// </summary>
        DateTime? SunSet { get; }

        /// <summary>
        /// SolarNoon correspondant au Slice
        /// </summary>
        DateTime? SolarNoon { get; }

        #endregion
    }
}
