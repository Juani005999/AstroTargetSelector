using ApplicationTools;
using System;
using System.Drawing;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un intervalle de temps de calcul pour un objet céleste
    /// </summary>
    public class ObjSliceTarget
    {
        #region Constantes

        /// <summary>
        /// Temps de pose maximum (si dépassement de capacité)
        /// </summary>
        public const int TempsPoseCalculeMax = 300;

        #endregion

        #region Propriétés

        /// <summary>
        /// Date et Heure de l'intervalle
        /// </summary>
        public DateTime DateHeure { get; set; }

        /// <summary>
        /// Temps de pose calculé pour l'intervalle
        /// </summary>
        public decimal TempsPoseCalcule
        {
            get
            {
                if (!tempsPoseCalcule.HasValue)
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
                    double LST = ((greenwitchTSCorrige + ((Math.PI / 180) * Convert.ToDouble(factory.GetAppInputs().Inputs.LieuObservation.LongitudeValue))) % (2 * Math.PI))
                                - (((Math.PI / 180) * Convert.ToDouble(parentTarget.RA.Coordonnee)) * 15);
                    double H = LST < 0 ? LST + (2 * Math.PI) : LST > Math.PI ? LST - (2 * Math.PI) : LST;

                    // Sin/Cos
                    double sinDec = Math.Sin((Math.PI / 180) * Convert.ToDouble(parentTarget.DEC.Coordonnee));
                    double cosDec = Math.Cos((Math.PI / 180) * Convert.ToDouble(parentTarget.DEC.Coordonnee));
                    double tanDec = Math.Tan((Math.PI / 180) * Convert.ToDouble(parentTarget.DEC.Coordonnee));
                    double sinLatitude = Math.Sin((Math.PI / 180) * Convert.ToDouble(factory.GetAppInputs().Inputs.LieuObservation.LatitudeValue));
                    double cosLatitude = Math.Cos((Math.PI / 180) * Convert.ToDouble(factory.GetAppInputs().Inputs.LieuObservation.LatitudeValue));
                    double cosH = Math.Cos(H);
                    double sinH = Math.Sin(H);

                    // Hauteur
                    hauteurPrecise = factory.GetCoordinate(Convert.ToDecimal(Math.Asin((sinDec * sinLatitude) + (cosDec * cosLatitude * cosH)) / (Math.PI / 180)), CoordinatesType.Degree);
                    hauteur = factory.GetCoordinate(Convert.ToDecimal(Math.Floor(Math.Asin((sinDec * sinLatitude) + (cosDec * cosLatitude * cosH)) / (Math.PI / 180))), CoordinatesType.Degree);

                    // Azimut
                    azimutCorrigee = factory.GetCoordinate(Convert.ToDecimal(Math.Atan2(sinH, (cosH * sinLatitude) - (cosLatitude * tanDec)) - Math.PI), CoordinatesType.Degree);
                    azimutPrecise = factory.GetCoordinate(Convert.ToDecimal((Convert.ToDouble(azimutCorrigee.Coordonnee) + (2 * Math.PI)) / (Math.PI / 180)), CoordinatesType.Degree);
                    azimut = factory.GetCoordinate(Convert.ToDecimal(Math.Floor((Convert.ToDouble(azimutCorrigee.Coordonnee) + (2 * Math.PI)) / (Math.PI / 180))), CoordinatesType.Degree);
                    double cosAzimut = Math.Cos((Math.PI / 180) * Convert.ToDouble(azimut.Coordonnee));

                    tempsPoseCalcule = Math.Abs(230
                                            * (factory.GetAppInputs().Inputs.BougeMax
                                            * Convert.ToDecimal(Math.Cos((Math.PI / 180) * Convert.ToDouble(hauteur.Coordonnee))))
                                            / (Convert.ToDecimal(0.5)
                                            * factory.GetAppInputs().Inputs.Capteur.Largeur
                                            * Convert.ToDecimal(cosLatitude)
                                            * Convert.ToDecimal(cosAzimut))
                                            * 60);
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
        /// Couleur du point dans le graphique
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
        /// Azimut calculé du slice
        /// </summary>
        public Coordinate Azimut
        {
            get
            {
                // Si le membre local n'a pas de valeur, on force le recalcul en appelant la propriété TempsPoseCalcule
                decimal tempsPose = 0;
                if (azimut == null)
                    tempsPose = TempsPoseCalcule;
                return azimut;
            }
        }

        /// <summary>
        /// Hauteur calculé du slice
        /// </summary>
        public Coordinate Hauteur
        {
            get
            {
                // Si le membre local n'a pas de valeur, on force le recalcul en appelant la propriété TempsPoseCalcule
                decimal tempsPose = 0;
                if (hauteur == null)
                    tempsPose = TempsPoseCalcule;
                return hauteur;
            }
        }

        /// <summary>
        /// Azimut Precise calculé du slice
        /// </summary>
        public Coordinate AzimutPrecise
        {
            get
            {
                // Si le membre local n'a pas de valeur, on force le recalcul en appelant la propriété TempsPoseCalcule
                decimal tempsPose = 0;
                if (azimutPrecise == null)
                    tempsPose = TempsPoseCalcule;
                return azimutPrecise;
            }
        }

        /// <summary>
        /// Azimut Corrigee calculé du slice
        /// </summary>
        public Coordinate AzimutCorrigee
        {
            get
            {
                // Si le membre local n'a pas de valeur, on force le recalcul en appelant la propriété TempsPoseCalcule
                decimal tempsPose = 0;
                if (azimutCorrigee == null)
                    tempsPose = TempsPoseCalcule;
                return azimutCorrigee;
            }
        }

        /// <summary>
        /// Hauteur Precise calculé du slice
        /// </summary>
        public Coordinate HauteurPrecise
        {
            get
            {
                // Si le membre local n'a pas de valeur, on force le recalcul en appelant la propriété TempsPoseCalcule
                decimal tempsPose = 0;
                if (hauteurPrecise == null)
                    tempsPose = TempsPoseCalcule;
                return hauteurPrecise;
            }
        }

        /// <summary>
        /// Permet de savoir si le slice de l'objet céleste est exclu de la liste
        /// <para>Fait partie d'une zone exclue du ciel</para>
        /// <para>En dessous de la hauteur apparente (Hauteur du premier Slice)</para>
        /// </summary>
        public bool EstExclu
        {
            get
            {
                // Parcours des zones à exclure
                foreach (CoordinatesDirection direction in factory.GetAppInputs().Inputs.ZonesExclues)
                {
                    double coordonne = Convert.ToDouble(Azimut.Coordonnee);
                    switch (direction)
                    {
                        case CoordinatesDirection.N:
                            if (coordonne > 337.5 || coordonne <= 22.5)
                                return true;
                            break;
                        case CoordinatesDirection.NE:
                            if (coordonne > 22.5 && coordonne <= 67.5)
                                return true;
                            break;
                        case CoordinatesDirection.E:
                            if (coordonne > 67.5 && coordonne <= 112.5)
                                return true;
                            break;
                        case CoordinatesDirection.SE:
                            if (coordonne > 112.5 && coordonne <= 157.5)
                                return true;
                            break;
                        case CoordinatesDirection.S:
                            if (coordonne > 157.5 && coordonne <= 202.5)
                                return true;
                            break;
                        case CoordinatesDirection.SO:
                            if (coordonne > 202.5 && coordonne <= 247.5)
                                return true;
                            break;
                        case CoordinatesDirection.O:
                            if (coordonne > 247.5 && coordonne <= 292.5)
                                return true;
                            break;
                        case CoordinatesDirection.NO:
                            if (coordonne > 292.5 && coordonne <= 337.5)
                                return true;
                            break;

                        default:
                            break;
                    }
                }

                // Vérif sur la Hauteur apparente du premier Slice
                if (Hauteur.Coordonnee < factory.GetAppInputs().Inputs.HauteurMin)
                    return true;

                // Objet non exclu de la liste
                return false;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjSliceTarget(AppObjFactory factory, ObjTarget parentTarget)
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
        private decimal? tempsPoseCalcule = null;

        #endregion
    }
}
