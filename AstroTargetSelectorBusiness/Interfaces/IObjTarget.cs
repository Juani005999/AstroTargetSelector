using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet représentant un objet céleste
    /// </summary>
    public interface IObjTarget
    {
        #region Propriétés

        /// <summary>
        /// Nom de l'objet céleste
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Type de l'objet céleste (amas globulaire, nébuleuse planétaire, galaxie spirale, ...)
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Description de l'objet céleste
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Constellation de l'objet céleste
        /// </summary>
        string Constellation { get; set; }

        /// <summary>
        /// RA : Acsension droite de l'objet céleste
        /// <para>Valeur exprimée en "degrés horaires" décimal</para>
        /// </summary>
        Coordinate RA { get; set; }

        /// <summary>
        /// DEC : Déclinaison de l'objet céleste
        /// <para>Valeur exprimée en "degrés" décimal</para>
        /// </summary>
        Coordinate DEC { get; set; }

        /// <summary>
        /// Magnitude de l'objet céleste
        /// </summary>
        double Magnitude { get; set; }

        /// <summary>
        /// Grandeur : Largeur de l'objet céleste
        /// </summary>
        Coordinate GrandeurWidth { get; set; }

        /// <summary>
        /// Grandeur : Hauteur de l'objet céleste
        /// </summary>
        Coordinate GrandeurHeight { get; set; }

        /// <summary>
        /// Permet de forcer le rechargement des Slices
        /// </summary>
        bool ForceUpdateSlices { set; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        List<IChartSlice> Slices { get; }

        /// <summary>
        /// Renvoi le Scoring calculé pour la Target
        /// <para>Moyenne des <see cref="ObjSliceTarget.TempsPoseCalcule"/> des Slices de la Target</para>
        /// </summary>
        double Scoring { get; }

        /// <summary>
        /// Renvoi le Rank calculé pour la Target
        /// <para>Barême basé sur le <see cref="Scoring"/></para>
        /// </summary>
        double Rank { get; }

        /// <summary>
        /// Renvoi l'Azimut calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        Coordinate Azimut { get; }

        /// <summary>
        /// Renvoi la Hauteur calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi la Hauteur du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        Coordinate Hauteur { get; }

        /// <summary>
        /// Renvoi l'Azimut Corrigee calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut Corrigee du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        Coordinate AzimutCorrigee { get; }

        /// <summary>
        /// Renvoi l'Azimut Precise calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut Precise du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        Coordinate AzimutPrecise { get; }

        /// <summary>
        /// Renvoi la Hauteur Precise calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi la Hauteur Precise du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        Coordinate HauteurPrecise { get; }

        /// <summary>
        /// Permet de savoir si un objet céleste est exclu de la liste
        /// <para>Fait partie d'une zone exclue du ciel</para>
        /// <para>En dessous de la hauteur apparente (Hauteur du premier Slice)</para>
        /// </summary>
        bool EstExclu { get; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Horaire</para>
        /// </summary>
        List<IChartSlice> HourlySlices { get; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Nuits</para>
        /// </summary>
        List<IChartSlice> DailySlices { get; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Nuits</para>
        /// </summary>
        List<IChartSlice> MonthlySlices { get; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Annuel</para>
        /// </summary>
        List<IChartSlice> YearlySlices { get; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi la série d'intervalles pour le graphique en fonction du mode de visualisation
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        List<IChartSlice> GetCurrentChartSlice(ModeVisualisation mode);

        #endregion
    }
}
