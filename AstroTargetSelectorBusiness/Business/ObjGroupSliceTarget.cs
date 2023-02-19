using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ApplicationTools;
using AstroMoonCalc;

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
                if (!tempsPoseCalcule.HasValue)
                {
                    tempsPoseCalcule = Slices.Average(t => t.TempsPoseCalcule);
                }
                return tempsPoseCalcule.Value;
            }
        }

        /// <summary>
        /// Rotation angulaire correspondant au Slice
        /// </summary>
        public double RotationAngulaire
        {
            get
            {
                if (!rotationAngulaire.HasValue)
                {
                    rotationAngulaire = Slices.Average(t => t.RotationAngulaire);
                }

                return rotationAngulaire.Value;
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
                if (!estExclu.HasValue)
                    estExclu = !(Slices.Where(t => !t.EstExclu).Count() > 0);
                return estExclu.Value;
            }
        }

        /// <summary>
        /// Couleur du point dans le graphique
        /// </summary>
        public Color CouleurPointGraphique
        {
            get
            {
                //if (Hauteur.Coordonnee < factory.GetAppInputs().Inputs.HauteurMin)
                //    return Color.DarkGray;
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
                if (Hauteur.Coordonnee >= appInputs.Inputs.HauteurMin)
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
                if (Slices.Count > 0)
                    return Slices[1].Azimut;
                return Slices[0].Azimut;
            }
        }

        /// <summary>
        /// Renvoi la direction actuelle
        /// <para>Basée sur l'Azimut</para>
        /// </summary>
        public CoordinatesDirection Direction
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].Direction;
                return Slices[0].Direction;
            }
        }

        /// <summary>
        /// Renvoi le code du caractère correspondant à la Direction
        /// <para>Police utilisée WINGDING</para>
        /// </summary>
        public char DirectionCharacterCode
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].DirectionCharacterCode;
                return Slices[0].DirectionCharacterCode;
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
                if (Slices.Count > 0)
                    return Slices[1].AzimutCorrigee;
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
                if (Slices.Count > 0)
                    return Slices[1].AzimutPrecise;
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
                if (Slices.Count > 0)
                    return Slices[1].HauteurPrecise;
                return Slices[0].HauteurPrecise;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string MoonPhaseName
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].MoonPhaseName;
                return string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? MoonAlt
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].MoonAlt;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? MoonAz
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].MoonAz;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string MoonPhaseImage
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].MoonPhaseImage;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? MoonRise
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].MoonRise;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? MoonSet
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].MoonSet;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? SunAlt
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].SunAlt;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? SunAz
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].SunAz;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? SunRise
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].SunRise;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? SunSet
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].SunSet;
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? SolarNoon
        {
            get
            {
                if (Slices.Count > 0)
                    return Slices[1].SolarNoon;
                return null;
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
        internal ObjGroupSliceTarget(IAppToolFactory appToolFactory, IAppInputs appInputs, IObjTarget parentTarget)
        {
            this.appToolFactory = appToolFactory;
            this.appInputs = appInputs;
            this.parentTarget = parentTarget;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        protected readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Instance de l'objet applicatif appInputs
        /// </summary>
        protected readonly IAppInputs appInputs = null;

        /// <summary>
        /// Objet céleste parent de l'objet Slice
        /// </summary>
        protected readonly IObjTarget parentTarget = null;

        /// <summary>
        /// Permet de savoir si un objet céleste est exclu de la liste
        /// <para>Fait partie d'une zone exclue du ciel</para>
        /// <para>En dessous de la hauteur apparente (Hauteur du premier Slice)</para>
        /// </summary>
        private bool? estExclu = null;

        /// <summary>
        /// Temps de pose calculé pour l'intervalle
        /// </summary>
        private double? tempsPoseCalcule = null;

        /// <summary>
        /// Rotation angulaire pour l'intervalle
        /// </summary>
        private double? rotationAngulaire = null;

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps du mois
        /// </summary>
        protected List<IChartSlice> slices = new List<IChartSlice>();

        #endregion
    }
}
