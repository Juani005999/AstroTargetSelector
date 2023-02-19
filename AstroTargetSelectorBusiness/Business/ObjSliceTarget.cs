using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using ApplicationTools;
using AstroMoonCalc;
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
        public double RotationAngulaire
        {
            get
            {
                if (!rotationAngulaire.HasValue)
                {
                    // On recalcul le slice
                    ReCalcSlice();
                }

                return rotationAngulaire.Value;
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
                return GetDirection(Azimut.Coordonnee);
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
                string sunAlt = SunAlt.HasValue ? SunAlt.Value.ToString("0", CultureInfo.InvariantCulture) : "0";
                string sunAz = SunAz.HasValue ? SunAz.Value.ToString("0", CultureInfo.InvariantCulture) : "0";
                string moonAlt = MoonAlt.HasValue ? MoonAlt.Value.ToString("0", CultureInfo.InvariantCulture) : "0";
                string moonAz = MoonAz.HasValue ? MoonAz.Value.ToString("0", CultureInfo.InvariantCulture) : "0";
                string sunLabel = string.Empty;
                if (SunRise.HasValue && SunSet.HasValue)
                {
                    sunLabel = Environment.NewLine + $"{Resources.Soleil} : {SunRise.Value.ToString("t")} - {SunSet.Value.ToString("t")}";
                    if (SunAlt.HasValue && SunAlt.Value > 0)
                        sunLabel += $" / {Resources.Hauteur} : {sunAlt}° / {Resources.Azimut} : {sunAz}° ({Coordinate.GetDirectionString(GetDirection(SunAz.Value))})";
                }
                string moonLabel = Environment.NewLine + $"{Resources.Lune} : {MoonIlluminationPct} / {MoonPhaseName}";
                if (MoonRise.HasValue && MoonSet.HasValue)
                {
                    moonLabel += $" / {MoonRise.Value.ToString("t")} - {MoonSet.Value.ToString("t")}";
                    if (MoonAlt.HasValue && MoonAlt.Value > 0)
                        moonLabel += $" / {Resources.Hauteur} : {moonAlt}° / {Resources.Azimut} : {moonAz}° ({Coordinate.GetDirectionString(GetDirection(MoonAz.Value))})";
                }

                return $"{DateHeure.ToString("d")} - {DateHeure.ToString("t")}"
                    + Environment.NewLine + $"{Resources.TempsDePoseMax} : {Math.Floor(TempsPoseCalcule)} s"
                    + Environment.NewLine + $"{Resources.Hauteur} : {Math.Floor(Hauteur.Coordonnee)}°"
                    + Environment.NewLine + $"{Resources.Azimut} : {Math.Floor(Azimut.Coordonnee)}° ({Coordinate.GetDirectionString(Direction)})"
                    + sunLabel
                    + moonLabel;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string MoonPhaseName
        {
            get
            {
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonIllumination luneApparence = MoonCalc.GetMoonIllumination(dateUtc);
                    if (luneApparence != null)
                        return luneApparence.PhaseName;
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonPosition lunePosition = MoonCalc.GetMoonPosition(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (lunePosition != null)
                        return lunePosition.Altitude * (180 / Math.PI);
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonPosition lunePosition = MoonCalc.GetMoonPosition(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (lunePosition != null)
                        return (lunePosition.Azimuth * (180 / Math.PI)) + 180;
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonIllumination luneApparence = MoonCalc.GetMoonIllumination(dateUtc);
                    if (luneApparence != null)
                    {
                        switch (luneApparence.Phase)
                        {
                            case MoonPhase.NewMoon:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_New.png");
                            case MoonPhase.WaningCrescent:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_WaningCrescent.png");
                            case MoonPhase.FirstQuarter:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_FirstQuarter.png");
                            case MoonPhase.WaningGibbous:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_WaningGibbous.png");
                            case MoonPhase.FullMoon:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_Full.png");
                            case MoonPhase.WaxingGibbous:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_WaxingGibbous.png");
                            case MoonPhase.LastQuarter:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_LastQuarter.png");
                            case MoonPhase.WaxingCrescent:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_WaxingCrescent.png");
                            default:
                                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, "moon", "moon_New.png");
                        }
                    }
                }
                catch(Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string MoonIlluminationPct
        {
            get
            {
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonIllumination luneApparence = MoonCalc.GetMoonIllumination(dateUtc);
                    if (luneApparence != null)
                        return $"{(luneApparence.Fraction * 100).ToString("0.00", CultureInfo.InvariantCulture)}%";
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? MoonRise
        {
            get
            {
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonTimes luneTimes = MoonCalc.GetMoonTimes(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (luneTimes != null)
                        return new DateTime(luneTimes.Rise.Value.Ticks, DateTimeKind.Utc).ToLocalTime();
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    MoonTimes luneTimes = MoonCalc.GetMoonTimes(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (luneTimes != null)
                        return new DateTime(luneTimes.Set.Value.Ticks, DateTimeKind.Utc).ToLocalTime();
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    SunPosition soleilPosition = SunCalc.GetPosition(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (soleilPosition != null)
                        return soleilPosition.Altitude * (180 / Math.PI);
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    SunPosition soleilPosition = SunCalc.GetPosition(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (soleilPosition != null)
                        return (soleilPosition.Azimuth * (180 / Math.PI)) + 180;
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    SunTimes soleilTimes = SunCalc.GetTimes(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (soleilTimes != null)
                        return new DateTime(soleilTimes.Sunrise.Value.Ticks, DateTimeKind.Utc).ToLocalTime();
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    SunTimes soleilTimes = SunCalc.GetTimes(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (soleilTimes != null)
                        return new DateTime(soleilTimes.Sunset.Value.Ticks, DateTimeKind.Utc).ToLocalTime();
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
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
                try
                {
                    DateTime dateUtc = DateHeure.ToUniversalTime();
                    SunTimes soleilTimes = SunCalc.GetTimes(dateUtc, appInputs.Inputs.LieuObservation.LatitudeValue, appInputs.Inputs.LieuObservation.LongitudeValue);
                    if (soleilTimes != null)
                        return new DateTime(soleilTimes.SolarNoon.Value.Ticks, DateTimeKind.Utc).ToLocalTime();
                }
                catch (Exception err)
                {
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
                return null;
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
            //double heureCorrige = DateHeure.Hour - 1;
            //if (TimeZoneInfo.Local.IsDaylightSavingTime(DateHeure))
            //    heureCorrige -= 1;
            DateTime utcDateTime = DateHeure.ToUniversalTime();
            double heureCorrige = utcDateTime.Hour;
            heureCorrige += ((double)utcDateTime.Minute / 60);
            int moisCorrige = utcDateTime.Month;
            if (moisCorrige < 3)
                moisCorrige += 12;
            int anneeCorrige = utcDateTime.Year;
            if (moisCorrige < 3)
                anneeCorrige -= 1;
            double A = Math.Floor((double)(anneeCorrige / 100));
            double B = 2 - A + Math.Floor((double)(A / 4));
            double C = Math.Floor((double)(365.25 * anneeCorrige));
            double D = Math.Floor((double)(30.6001 * (moisCorrige + 1)));
            double JJ = B + C + D + utcDateTime.Day + ((double)heureCorrige / 24) + 1720994.5;

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

            rotationAngulaire = Math.Abs(0.000729
                                    * Math.Cos(appInputs.Inputs.LieuObservation.LatitudeValue)
                                    * Math.Cos((Math.PI / 180) * azimut.Coordonnee)
                                    / Math.Cos((Math.PI / 180) * hauteur.Coordonnee));
        }

        /// <summary>
        /// Renvoi la Direction en fonction de la coordonnée Azimut
        /// </summary>
        /// <param name="coordonnee"></param>
        /// <returns></returns>
        private CoordinatesDirection GetDirection (double coordonnee)
        {
            if (coordonnee > 337.5 || coordonnee <= 22.5)
                return CoordinatesDirection.N;
            if (coordonnee > 22.5 && coordonnee <= 67.5)
                return CoordinatesDirection.NE;
            if (coordonnee > 67.5 && coordonnee <= 112.5)
                return CoordinatesDirection.E;
            if (coordonnee > 112.5 && coordonnee <= 157.5)
                return CoordinatesDirection.SE;
            if (coordonnee > 157.5 && coordonnee <= 202.5)
                return CoordinatesDirection.S;
            if (coordonnee > 202.5 && coordonnee <= 247.5)
                return CoordinatesDirection.SO;
            if (coordonnee > 247.5 && coordonnee <= 292.5)
                return CoordinatesDirection.O;
            if (coordonnee > 292.5 && coordonnee <= 337.5)
                return CoordinatesDirection.NO;
            return CoordinatesDirection.N;
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

        /// <summary>
        /// Rotation angulaire pour l'intervalle
        /// </summary>
        private double? rotationAngulaire = null;

        #endregion
    }
}
