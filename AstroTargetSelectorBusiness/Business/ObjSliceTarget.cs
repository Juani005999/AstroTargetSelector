using System;
using System.Drawing;
using System.Text;
using ApplicationTools;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un intervalle de temps de calcul pour un objet céleste
    /// </summary>
    public class ObjSliceTarget : IChartSlice
    {
        #region Constantes

        /// <summary>
        /// Temps de pose maximum (si dépassement de capacité)
        /// </summary>
        public const int TempsPoseCalculeMax = 300;

        #endregion

        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime DateHeure { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double TempsPoseCalcule
        {
            get
            {
                if (!tempsPoseCalcule.HasValue)
                {
                    // On recalcul le slice
                    ReCalcSlice();
                }

                return tempsPoseCalcule.Value > TempsPoseCalculeMax ? TempsPoseCalculeMax : tempsPoseCalcule.Value;
            }
        }

        /// <summary>
        /// Temps de pose calculé pour l'intervalle sous la forme d'une chaîne de caractère formaté
        /// </summary>
        public string TempsPoseCalculeFormatedString
        {
            get
            {
                return Math.Floor(TempsPoseCalcule).ToString() + "s";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Color CouleurPointGraphique
        {
            get
            {
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        /// </summary>
        public Coordinate Azimut
        {
            get
            {
                // Si le membre local n'a pas de valeur, on recalcul le slice
                if (azimut == null)
                    ReCalcSlice();
                return azimut;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public CoordinatesDirection Direction
        {
            get
            {
                if (Azimut.Coordonnee > 337.5 || Azimut.Coordonnee <= 22.5)
                    return CoordinatesDirection.N;
                if (Azimut.Coordonnee > 22.5 && Azimut.Coordonnee <= 67.5)
                    return CoordinatesDirection.NE;
                if (Azimut.Coordonnee > 67.5 && Azimut.Coordonnee <= 112.5)
                    return CoordinatesDirection.E;
                if (Azimut.Coordonnee > 112.5 && Azimut.Coordonnee <= 157.5)
                    return CoordinatesDirection.SE;
                if (Azimut.Coordonnee > 157.5 && Azimut.Coordonnee <= 202.5)
                    return CoordinatesDirection.S;
                if (Azimut.Coordonnee > 202.5 && Azimut.Coordonnee <= 247.5)
                    return CoordinatesDirection.SO;
                if (Azimut.Coordonnee > 247.5 && Azimut.Coordonnee <= 292.5)
                    return CoordinatesDirection.O;
                if (Azimut.Coordonnee > 292.5 && Azimut.Coordonnee <= 337.5)
                    return CoordinatesDirection.NO;
                return CoordinatesDirection.N;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public char DirectionCharacterCode
        {
            get
            {
                switch(Direction)
                {
                    case CoordinatesDirection.NE:
                        return '\u2197';
                    case CoordinatesDirection.E:
                        return '\u2192';
                    case CoordinatesDirection.SE:
                        return '\u2198';
                    case CoordinatesDirection.S:
                        return '\u2193';
                    case CoordinatesDirection.SO:
                        return '\u2199';
                    case CoordinatesDirection.O:
                        return '\u2190';
                    case CoordinatesDirection.NO:
                        return '\u2196';
                    case CoordinatesDirection.N:
                    default:
                        return '\u2191';
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate Hauteur
        {
            get
            {
                // Si le membre local n'a pas de valeur, on recalcul le slice
                if (hauteur == null)
                    ReCalcSlice();
                return hauteur;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate AzimutPrecise
        {
            get
            {
                // Si le membre local n'a pas de valeur, on recalcul le slice
                if (azimutPrecise == null)
                    ReCalcSlice();
                return azimutPrecise;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate AzimutCorrigee
        {
            get
            {
                // Si le membre local n'a pas de valeur, on recalcul le slice
                if (azimutCorrigee == null)
                    ReCalcSlice();
                return azimutCorrigee;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate HauteurPrecise
        {
            get
            {
                // Si le membre local n'a pas de valeur, on recalcul le slice
                if (hauteurPrecise == null)
                    ReCalcSlice();
                return hauteurPrecise;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool EstExclu
        {
            get
            {
                // Parcours des zones à exclure
                foreach (CoordinatesDirection direction in appInputs.Inputs.ZonesExclues)
                {
                    if (direction == Direction)
                        return true;
                }

                // Vérif sur la Hauteur apparente du premier Slice
                if (Hauteur.Coordonnee < appInputs.Inputs.HauteurMin)
                    return true;

                // Objet non exclu de la liste
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ToolTip
        {
            get
            {
                return $"{DateHeure.ToString("HH")}h{DateHeure.ToString("mm")}"
                    + Environment.NewLine + $"{Resources.TempsDePoseMax} : {Math.Floor(TempsPoseCalcule)} s"
                    + Environment.NewLine + $"{Resources.Hauteur} : {Math.Floor(Hauteur.Coordonnee)} °"
                    + Environment.NewLine + $"{Resources.Azimut} : {Math.Floor(Azimut.Coordonnee)} ° ({Coordinate.GetDirectionString(Direction)})";
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjSliceTarget(IAppToolFactory appToolFactory, IAppInputs appInputs, IObjTarget parentTarget)
        {
            this.appToolFactory = appToolFactory;
            this.appInputs = appInputs;
            this.parentTarget = parentTarget;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// /Recalcul le Slice et mets à jour les membres
        /// </summary>
        private void ReCalcSlice()
        {
            // Calcul pour Julien
            double heureCorrige = DateHeure.Hour - 1;
            if (TimeZoneInfo.Local.IsDaylightSavingTime(DateHeure))
                heureCorrige -= 1;
            heureCorrige += ((double)DateHeure.Minute / 60);
            int moisCorrige = DateHeure.Month;
            if (moisCorrige < 3)
                moisCorrige += 12;
            int anneeCorrige = DateHeure.Year;
            if (moisCorrige < 3)
                anneeCorrige -= 1;
            double A = Math.Floor((double)(anneeCorrige / 100));
            double B = 2 - A + Math.Floor((double)(A / 4));
            double C = Math.Floor((double)(365.25 * anneeCorrige));
            double D = Math.Floor((double)(30.6001 * (moisCorrige + 1)));
            double JJ = B + C + D + DateHeure.Day + ((double)heureCorrige / 24) + 1720994.5;

            // Angle rotation de la Terre
            double rotT = JJ - 2451545;
            double rotF = rotT - Math.Floor(rotT);
            double rotThetaIntermediaire = 2 * Math.PI * (rotF + 0.779057273264 + (0.00273781191135448 * rotT));
            double rotTheta = rotThetaIntermediaire % (2 * Math.PI);

            // Temps Sideral GreenWitch
            double greenwitchT = (JJ - 2451545) / 36525;
            double greenwitchTS = rotTheta + ((0.014506
                                                + (4612.156534 * greenwitchT)
                                                + (1.3915817 * Math.Pow(greenwitchT, 2))
                                                - (0.00000044 * Math.Pow(greenwitchT, 3))
                                                - (0.000029956 * Math.Pow(greenwitchT, 4))
                                                - (0.0000000368 * Math.Pow(greenwitchT, 5))) / 60 / 60 * Math.PI / 180);
            double greenwitchTSCorrige = greenwitchTS % (2 * Math.PI);

            // Temps Sideral Local
            double LST = ((greenwitchTSCorrige + ((Math.PI / 180) * appInputs.Inputs.LieuObservation.LongitudeValue)) % (2 * Math.PI))
                        - (((Math.PI / 180) * parentTarget.RA.Coordonnee) * 15);
            double H = LST < 0 ? LST + (2 * Math.PI) : LST > Math.PI ? LST - (2 * Math.PI) : LST;

            // Sin/Cos
            double sinDec = Math.Sin((Math.PI / 180) * parentTarget.DEC.Coordonnee);
            double cosDec = Math.Cos((Math.PI / 180) * parentTarget.DEC.Coordonnee);
            double tanDec = Math.Tan((Math.PI / 180) * parentTarget.DEC.Coordonnee);
            double sinLatitude = Math.Sin((Math.PI / 180) * appInputs.Inputs.LieuObservation.LatitudeValue);
            double cosLatitude = Math.Cos((Math.PI / 180) * appInputs.Inputs.LieuObservation.LatitudeValue);
            double cosH = Math.Cos(H);
            double sinH = Math.Sin(H);

            // Hauteur
            hauteurPrecise = appToolFactory.GetCoordinate(Math.Asin((sinDec * sinLatitude) + (cosDec * cosLatitude * cosH)) / (Math.PI / 180), CoordinatesType.Degree);
            hauteur = appToolFactory.GetCoordinate(Math.Floor(Math.Asin((sinDec * sinLatitude) + (cosDec * cosLatitude * cosH)) / (Math.PI / 180)), CoordinatesType.Degree);

            // Azimut
            azimutCorrigee = appToolFactory.GetCoordinate(Math.Atan2(sinH, (cosH * sinLatitude) - (cosLatitude * tanDec)) - Math.PI, CoordinatesType.Degree);
            azimutPrecise = appToolFactory.GetCoordinate((Convert.ToDouble(azimutCorrigee.Coordonnee) + (2 * Math.PI)) / (Math.PI / 180), CoordinatesType.Degree);
            azimut = appToolFactory.GetCoordinate(Math.Floor((Convert.ToDouble(azimutCorrigee.Coordonnee) + (2 * Math.PI)) / (Math.PI / 180)), CoordinatesType.Degree);
            double cosAzimut = Math.Cos((Math.PI / 180) * azimut.Coordonnee);

            tempsPoseCalcule = Math.Abs(230
                                    * (appInputs.Inputs.BougeMax
                                    * Math.Cos((Math.PI / 180) * hauteur.Coordonnee))
                                    / (0.5
                                    * appInputs.Inputs.Capteur.Largeur
                                    * cosLatitude
                                    * cosAzimut)
                                    * 60);
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Instance de l'objet applicatif appInputs
        /// </summary>
        private readonly IAppInputs appInputs = null;

        /// <summary>
        /// Objet céleste parent de l'objet Slice
        /// </summary>
        private readonly IObjTarget parentTarget = null;

        /// <summary>
        /// Azimut calculé du slice
        /// </summary>
        private Coordinate azimut = null;

        /// <summary>
        /// Azimut calculé du slice
        /// </summary>
        private Coordinate azimutPrecise = null;

        /// <summary>
        /// Azimut corrigée calculé du slice
        /// </summary>
        private Coordinate azimutCorrigee = null;

        /// <summary>
        /// Hauteur calculée du slice
        /// </summary>
        private Coordinate hauteur = null;

        /// <summary>
        /// Hauteur corrigée Precise du slice
        /// </summary>
        private Coordinate hauteurPrecise = null;

        /// <summary>
        /// Temps de pose calculé pour l'intervalle
        /// </summary>
        private double? tempsPoseCalcule = null;

        #endregion
    }
}
