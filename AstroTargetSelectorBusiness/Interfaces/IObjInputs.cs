using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ApplicationTools;
using AstroTargetSelectorBusiness.Properties;

namespace AstroTargetSelectorBusiness
{
    #region Enums

    /// <summary>
    /// Mode de visualisation des données
    /// </summary>
    public enum ModeVisualisation
    {
        /// <summary>
        /// Liste de Slice sur une tranche horaire
        /// </summary>
        Horaire,

        /// <summary>
        /// Liste de Slice sur une tranche horaire
        /// </summary>
        Nuits,

        /// <summary>
        /// Liste de Slice sur les jours d'un mois
        /// </summary>
        Mensuel,

        /// <summary>
        /// Liste de Slice sur les mois d'une année
        /// </summary>
        Annuel
    }

    #endregion

    /// <summary>
    /// Interface de l'Objet représentant toutes les données nécessaires à l'application des règles applicatives
    /// </summary>
    public interface IObjInputs
    {
        #region Propriétés

        /// <summary>
        /// Date et Heure de l'observation
        /// </summary>
        DateTime DateHeureObservation { get; set; }

        /// <summary>
        /// Coordonnées (Longitude et Latitude) du lieu d'observation
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// </summary>
        Coordinates LieuObservation { get; set; }

        /// <summary>
        /// Capteur
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// </summary>
        IObjCapteur Capteur { get; set; }

        /// <summary>
        /// Bougé max.
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 1</para>
        /// </summary>
        double BougeMax { get; set; }

        /// <summary>
        /// Hauteur minimale
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 25</para>
        /// </summary>
        double HauteurMin { get; set; }

        /// <summary>
        /// Nombre d'intervalle de temps : (<see cref="TotalTimeSlice"/> * 60) / <see cref="MinuteIntervalSlice"/>
        /// </summary>
        int NombreSlice { get; }

        /// <summary>
        /// Nombre de minutes d'un Intervalle pour le calcul des temps de pose
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 10</para>
        /// </summary>
        int MinuteIntervalSlice { get; set; }

        /// <summary>
        /// Durée totale de l'observation pour définir le nombre de slice en fonction de l'intervalle
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 2</para>
        /// </summary>
        int TotalTimeSlice { get; set; }

        /// <summary>
        /// Liste des zones exclues
        /// </summary>
        List<CoordinatesDirection> ZonesExclues { get; set; }

        /// <summary>
        /// Mode de visualisation
        /// <para></para>
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 25</para>
        /// </summary>
        ModeVisualisation Visualisation { get; set; }

        #endregion
    }
}
