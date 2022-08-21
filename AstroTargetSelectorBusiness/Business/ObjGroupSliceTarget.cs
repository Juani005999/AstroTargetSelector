using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un intervalle regroupant des intervalles <see cref="ObjSliceTarget"/>
    /// </summary>
    public abstract class ObjGroupSliceTarget
    {
        #region Propriétés

        /// <summary>
        /// Date et Heure de l'intervalle
        /// </summary>
        public DateTime DateHeure { get; set; }

        /// <summary>
        /// Temps de pose calculé pour l'intervalle
        /// </summary>
        public double TempsPoseCalcule
        {
            get
            {
                //return Slices[0].TempsPoseCalcule;
                return Slices.Average(t => t.TempsPoseCalcule);
            }
        }

        /// <summary>
        /// Permet de savoir si un objet céleste est exclu de la liste
        /// <para>Fait partie d'une zone exclue du ciel</para>
        /// <para>En dessous de la hauteur apparente (Hauteur du premier Slice)</para>
        /// </summary>
        public bool EstExclu
        {
            get
            {
                // Renvoi false si au moins 1 slice n'est pas exclu
                return !(Slices.Where(t => !t.EstExclu).Count() > 0);
                //return !(Slices.Where(t => !t.EstExclu).Count() > 0) || Hauteur.Coordonnee < factory.GetAppInputs().Inputs.HauteurMin;
            }
        }

        /// <summary>
        /// Couleur du point dans le graphique
        /// </summary>
        public Color CouleurPointGraphique
        {
            get
            {
                if (Hauteur.Coordonnee < factory.GetAppInputs().Inputs.HauteurMin)
                    return Color.DarkGray;
                if (TempsPoseCalcule >= ObjTarget.MinTempsPoseRank5)
                    return Color.FromArgb(0, 192, 0);
                if (TempsPoseCalcule >= ObjTarget.MinTempsPoseRank4)
                    return Color.FromArgb(128, 255, 128);
                if (TempsPoseCalcule >= ObjTarget.MinTempsPoseRank3)
                    return Color.FromArgb(255, 255, 128);
                if (TempsPoseCalcule >= ObjTarget.MinTempsPoseRank2)
                    return Color.FromArgb(255, 192, 128);
                if (TempsPoseCalcule >= ObjTarget.MinTempsPoseRank1)
                    return Color.FromArgb(255, 128, 0);
                return Color.Red;
            }
        }

        /// <summary>
        /// Renvoi la Hauteur calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi la Hauteur du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate Hauteur
        {
            get
            {
                // Renvoi la valeur de la Hauteur du slice du intermédiaire
                if (Slices.Count > 0)
                    return Slices[1].Hauteur;
                return Slices[0].Hauteur;
            }
        }

        /// <summary>
        /// Couleur du point dans le graphique
        /// <para>Vert si au dessus de la hauteur min, sinon rouge</para>
        /// </summary>
        public Color CouleurHauteur
        {
            get
            {
                if (Hauteur.Coordonnee >= factory.GetAppInputs().Inputs.HauteurMin)
                    return Color.FromArgb(0, 192, 0);
                return Color.Red;
            }
        }

        /// <summary>
        /// Renvoi l'Azimut calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate Azimut
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].Azimut;
            }
        }

        /// <summary>
        /// Renvoi l'Azimut Corrigee calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut Corrigee du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate AzimutCorrigee
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].AzimutCorrigee;
            }
        }

        /// <summary>
        /// Renvoi l'Azimut Precise calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut Precise du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate AzimutPrecise
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].AzimutPrecise;
            }
        }

        /// <summary>
        /// Renvoi la Hauteur Precise calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi la Hauteur Precise du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate HauteurPrecise
        {
            get
            {
                // Renvoi la valeur de la Hauteur du premier slice
                return Slices[0].HauteurPrecise;
            }
        }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        public abstract List<IChartSlice> Slices { get; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjGroupSliceTarget(AppObjFactory factory, ObjTarget parentTarget)
        {
            this.factory = factory;
            this.parentTarget = parentTarget;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Objet céleste parent de l'objet Slice
        /// </summary>
        private readonly ObjTarget parentTarget = null;

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps du mois
        /// </summary>
        protected List<IChartSlice> slices = new List<IChartSlice>();

        #endregion
    }
}
