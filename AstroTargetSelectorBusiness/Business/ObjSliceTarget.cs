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
        public const int TempsPoseCalculeMax = 500;

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
                double hauteur = Math.Floor(Math.Asin((sinDec * sinLatitude) + (cosDec * cosLatitude * cosH)) / (Math.PI / 180));

                // Azimut
                double azimutCorrigee = Math.Atan2(sinH, (cosH * sinLatitude) - (cosLatitude * tanDec)) - Math.PI;
                azimut = Math.Floor((azimutCorrigee + (2 * Math.PI)) / (Math.PI / 180));
                double cosAzimut = Math.Cos((Math.PI / 180) * azimut.Value);

                decimal tempsPoseCalcule = Math.Abs(230
                                        * (factory.GetAppInputs().Inputs.BougeMax
                                        * Convert.ToDecimal(Math.Cos((Math.PI / 180) * hauteur)))
                                        / (Convert.ToDecimal(0.5)
                                        * factory.GetAppInputs().Inputs.Capteur.Largeur
                                        * Convert.ToDecimal(cosLatitude)
                                        * Convert.ToDecimal(cosAzimut))
                                        * 60);
                return tempsPoseCalcule > TempsPoseCalculeMax ? TempsPoseCalculeMax : tempsPoseCalcule;
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
                decimal tempsPose = TempsPoseCalcule;
                if (tempsPose >= ObjTarget.MinTempsPoseRank5)
                    return Color.FromArgb(0, 192, 0);
                if (tempsPose >= ObjTarget.MinTempsPoseRank4)
                    return Color.FromArgb(128, 255, 128);
                if (tempsPose >= ObjTarget.MinTempsPoseRank3)
                    return Color.FromArgb(255, 255, 128);
                if (tempsPose >= ObjTarget.MinTempsPoseRank2)
                    return Color.FromArgb(255, 192, 128);
                if (tempsPose >= ObjTarget.MinTempsPoseRank1)
                    return Color.FromArgb(255, 128, 0);
                return Color.Red;
            }
        }

        /// <summary>
        /// Azimut calculé du slice
        /// </summary>
        public double Azimut
        {
            get
            {
                // Si le membre local n'a pas de valeur, on force le recalcul en appelant
                decimal tempsPose = 0;
                if (!azimut.HasValue)
                    tempsPose = TempsPoseCalcule;
                return azimut.Value;
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
        private double? azimut = null;

        #endregion
    }
}
