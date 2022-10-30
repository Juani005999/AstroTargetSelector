using ApplicationTools.Properties;
using System;
using System.Globalization;

namespace ApplicationTools
{
    /// <summary>
    /// Position N/S/E/O de la coordonnée de localisation
    /// </summary>
    public enum CoordinatesDirection
    {
        /// <summary>
        /// Nord
        /// </summary>
        N,

        /// <summary>
        /// Nord-Est
        /// </summary>
        NE,

        /// <summary>
        /// Est
        /// </summary>
        E,

        /// <summary>
        /// Sud-Est
        /// </summary>
        SE,

        /// <summary>
        /// Sud
        /// </summary>
        S,

        /// <summary>
        /// Sud-Ouest
        /// </summary>
        SO,

        /// <summary>
        /// Ouest
        /// </summary>
        O,

        /// <summary>
        /// Nord-Ouest
        /// </summary>
        NO
    }

    /// <summary>
    /// Type Longitude/Latitude pour les <see cref="Coordinate"/>
    /// </summary>
    public enum CoordinatesType
    {
        /// <summary>
        /// Longitude
        /// <para>Format : XX° XX' XX.XX" [E/O]</para>
        /// </summary>
        Longitude,

        /// <summary>
        /// Latitude
        /// <para>Format : XX° XX' XX.XX" [N/S]</para>
        /// </summary>
        Latitude,

        /// <summary>
        /// RA (Ascension Droite)
        /// <para>Format : XXh XXm XX.XXs</para>
        /// </summary>
        RA,

        /// <summary>
        /// DEC (Déclinaison)
        /// <para>Format : [+/-] XX° XX' XX.XX"</para>
        /// </summary>
        DEC,

        /// <summary>
        /// Degré
        /// <para>Format : [+/-] XX° XX' XX.XX"</para>
        /// </summary>
        Degree
    }

    /// <summary>
    /// Donnée de type Coordonnée de localisation : XX° XX' XX.XX" [N/S/E/O]
    /// Donnée de type Coordonnée RA (Ascension Droite) : XXh XXm XX.XXs
    /// Donnée de type Degré (DEC, Grandeur, Alt/Az, ...) : [+/-] XX° XX' XX.XX"
    /// </summary>
    public class Coordinate
    {
        #region Propriétés

        /// <summary>
        /// Valeur décimal de la coordonnée
        /// </summary>
        public double Coordonnee
        {
            get
            {
                return coordonnee;
            }
        }

        /// <summary>
        /// Valeur du champ '°' de la coordonnée
        /// </summary>
        public double Degrees
        {
            get
            {
                return Math.Truncate(Math.Abs(coordonnee));
            }
        }

        /// <summary>
        /// Valeur du champ 'h' de la coordonnée de type RA
        /// </summary>
        public double Hours
        {
            get
            {
                return Math.Truncate(Math.Abs(coordonnee));
            }
        }

        /// <summary>
        /// Valeur du champ ''' de la coordonnée
        /// </summary>
        public double Minutes
        {
            get
            {
                // Calcul spécifique pour les données de type RA
                if (coordinatesType == CoordinatesType.RA)
                    return Math.Truncate((Math.Abs(coordonnee) - Hours) * 60);
                    //return Math.Truncate((coordonnee - (Hours * 15)) * 4);

                // Retour pour le cas standard
                return Math.Truncate((Math.Abs(coordonnee) - Degrees) * 60);
            }
        }

        /// <summary>
        /// Valeur du champ '"' de la coordonnée
        /// </summary>
        public double Seconds
        {
            get
            {
                // Calcul spécifique pour les données de type RA
                if (coordinatesType == CoordinatesType.RA)
                    return (((Math.Abs(coordonnee) - Hours) * 60) - Minutes) * 60;
                    //return (coordonnee - (Hours * 15) - (Minutes / 4)) * 240;

                return (((Math.Abs(coordonnee) - Degrees) * 60) - Minutes) * 60;
            }
        }

        /// <summary>
        /// Coordonnée sous la forme d'une chaîne de caractère formatée
        /// </summary>
        public string FormatedString
        {
            get
            {
                // Coordonnée de type Longitude / Latitude
                if (coordinatesType == CoordinatesType.Latitude || coordinatesType == CoordinatesType.Longitude)
                {
                    // On positionne la direction en fonction du Type (Longitude/Latitude) et de la valeur de la coordonnée
                    var direction = coordinatesType == CoordinatesType.Latitude ?
                                        coordonnee < 0 ? CoordinatesDirection.S : CoordinatesDirection.N
                                        : coordonnee < 0 ? CoordinatesDirection.O : CoordinatesDirection.E;
                    return Degrees + "° " + Minutes + "' " + string.Format("{0:0.00}", Seconds) + "\" " + GetDirectionString(direction);
                }

                // Coordonnée de type RA
                if (coordinatesType == CoordinatesType.RA)
                {
                    return Hours + "h " + Minutes + "' " + string.Format("{0:0.00}", Seconds) + "\" ";
                }

                // Par défaut, Coordonnée de type Degré
                string signe = coordonnee > 0 ? "+" : "-";
                return signe + Degrees + "° " + Minutes + "' " + string.Format("{0:0.00}", Seconds) + "\" ";
            }
        }

        /// <summary>
        /// Renvoi la direction pour les données de type Longitude ou Latitude
        /// </summary>
        public string Direction
        {
            get
            {
                // Coordonnée de type Longitude
                if (coordinatesType == CoordinatesType.Longitude)
                {
                    return coordonnee < 0 ? GetDirectionString(CoordinatesDirection.O) : GetDirectionString(CoordinatesDirection.E);
                }

                // Coordonnée de type LongitLatitudeude
                if (coordinatesType == CoordinatesType.Latitude)
                {
                    return coordonnee < 0 ? GetDirectionString(CoordinatesDirection.S) : GetDirectionString(CoordinatesDirection.N);
                }

                // Pour les autres type, on renvoi une chaîne vide
                return string.Empty;
            }
        }

        /// <summary>
        /// Type de coordonnées
        /// </summary>
        public CoordinatesType CoordinatesType
        {
            get
            {
                return coordinatesType;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="coordonnee"></param>
        /// <param name="coordinatesType"></param>
        internal Coordinate(double coordonnee, CoordinatesType coordinatesType)
        {
            // Valorisation du champ interne
            this.coordonnee = coordonnee;
            this.coordinatesType = coordinatesType;
        }

        #endregion

        #region Méthodes
        
        /// <summary>
        /// Permet de positionner une nouvelle coordonnée pour l'objet en cours
        /// </summary>
        /// <param name="coordonnee"></param>
        public void UpdateCoordonnee(double coordonnee)
        {
            this.coordonnee = coordonnee;
        }

        /// <summary>
        /// Valide une saisie au format string
        /// <para>L'allocation mémoire pour le paramètre ref <paramref name="coordonnee"/> doit être réalisé par l'appelant</para>
        /// </summary>
        /// <param name="degree"></param>
        /// <param name="minute"></param>
        /// <param name="seconde"></param>
        /// <param name="direction"></param>
        /// <param name="coordonnee"></param>
        /// <param name="appLog"></param>
        /// <returns></returns>
        public static bool TryParse(string degree, string minute, string seconde, string direction, ref Coordinate coordonnee, IAppLog appLog)
        {
            // On vérifie la validité du paramètre ref "coordonnees"
            if (coordonnee == null)
            {
                appLog.Log($"Paramètre ref 'coordonnee' null", "Coordinate", null, TypeLog.Fatal);
                return false;
            }

            // Validation Degrés
            double degreeDec;
            if (string.IsNullOrEmpty(degree) || !double.TryParse(degree, NumberStyles.Number, CultureInfo.InvariantCulture, out degreeDec))
            {
                appLog.Log($"Mauvais format de degrés pour le TryParse en Coordinate", "Coordinate", null, TypeLog.Warning);
                return false;
            }

            // Validation Minutes
            double minuteDec;
            if (string.IsNullOrEmpty(minute) || !double.TryParse(minute, NumberStyles.Number, CultureInfo.InvariantCulture, out minuteDec))
            {
                appLog.Log($"Mauvais format de minutes pour le TryParse en Coordinate", "Coordinate", null, TypeLog.Warning);
                return false;
            }

            // Validation Secondes
            double secondeDec;
            if (string.IsNullOrEmpty(seconde) || !double.TryParse(seconde, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out secondeDec))
            {
                appLog.Log($"Mauvais format de secondes pour le TryParse en Coordinate", "Coordinate", null, TypeLog.Warning);
                return false;
            }

            // Validation Direction
            if (string.IsNullOrEmpty(direction))
            {
                appLog.Log($"Mauvais format de direction pour le TryParse en Coordinate", "Coordinate", null, TypeLog.Warning);
                return false;
            }
            CoordinatesDirection directionDec = GetDirectionFromString(direction);

            // Données valides, on valorise l'objet coordonnee
            double valCoordonnee = degreeDec + (minuteDec / 60) + (secondeDec / 3600);
            if (coordonnee.coordinatesType == CoordinatesType.Latitude && directionDec == CoordinatesDirection.S)
                valCoordonnee *= -1;
            else if (coordonnee.coordinatesType == CoordinatesType.Longitude && directionDec == CoordinatesDirection.O)
                valCoordonnee *= -1;
            coordonnee.UpdateCoordonnee(valCoordonnee);

            return true;
        }

        /// <summary>
        /// Valide une saisie au format string
        /// <para>L'allocation mémoire pour le paramètre ref <paramref name="coordonnee"/> doit être réalisé par l'appelant</para>
        /// </summary>
        /// <param name="inputCoordonee"></param>
        /// <param name="coordonnee"></param>
        /// <returns></returns>
        public static bool TryParseFromFormatedString(string inputCoordonee, ref Coordinate coordonnee)
        {
            // On vérifie la validité du paramètre ref "coordonnees"
            if (coordonnee == null)
                return false;

            // Validation string inputCoordonee
            if (string.IsNullOrEmpty(inputCoordonee))
                return false;

            // On supprime les espaces et on modifie les caractères m et s si nécessaire
            inputCoordonee = inputCoordonee.Replace(" ", "");
            inputCoordonee = inputCoordonee.ToLower().Replace("m", "'");
            inputCoordonee = inputCoordonee.ToLower().Replace("s", "\"");

            // Validation Degree ou Heure
            double hourDec;
            int indexSepHour = 0;
            if (inputCoordonee.IndexOf("h") != -1 || inputCoordonee.IndexOf("°") != -1)
            {
                indexSepHour = inputCoordonee.IndexOf("h");
                if (indexSepHour == -1)
                    indexSepHour = inputCoordonee.IndexOf("°");
                string hour = inputCoordonee.Substring(0, indexSepHour);
                if (string.IsNullOrEmpty(hour) || !double.TryParse(hour, NumberStyles.Number, CultureInfo.InvariantCulture, out hourDec))
                    return false;
            }
            else
                return false;

            // Validation Minutes
            double minuteDec;
            int indexSepMinute = 0;
            if (inputCoordonee.IndexOf("'") != -1)
            {
                indexSepMinute = inputCoordonee.IndexOf("'");
                string minute = inputCoordonee.Substring(indexSepHour + 1, indexSepMinute - (indexSepHour + 1));
                if (string.IsNullOrEmpty(minute) || !double.TryParse(minute, NumberStyles.Number, CultureInfo.InvariantCulture, out minuteDec))
                    return false;
            }
            else
                return false;

            // Validation Secondes
            double secondeDec;
            int indexSepSeconde = 0;
            if (inputCoordonee.IndexOf("\"") != -1)
            {
                indexSepSeconde = inputCoordonee.IndexOf("\"");
                string seconde = inputCoordonee.Substring(indexSepMinute + 1, indexSepSeconde - (indexSepMinute + 1));
                if (string.IsNullOrEmpty(seconde)) // || !double.TryParse(seconde, out secondeDec))
                    return false;
                seconde = seconde.Replace(",", ".");
                if (!double.TryParse(seconde, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out secondeDec))
                    return false;
            }
            else
                return false;

            // Données valides, on valorise l'objet coordonnee
            double valCoordonnee = Math.Abs(hourDec) + (minuteDec / 60) + (secondeDec / 3600);

            //// Direction
            //if (coordonnee.CoordinatesType == CoordinatesType.Longitude || coordonnee.CoordinatesType == CoordinatesType.Latitude)
            //{
            //    if (inputCoordonee.IndexOf("\"") != -1)
            //    {
            //        indexSepSeconde = inputCoordonee.IndexOf("\"");
            //        string direction = inputCoordonee.Substring(indexSepSeconde + 1, inputCoordonee.Length - (indexSepSeconde + 1));
            //        if (direction == CoordinatesDirection.S.ToString() || direction == CoordinatesDirection.O.ToString())
            //            valCoordonnee *= -1;
            //    }
            //    else
            //        return false;
            //}
            //else if (hourDec < 0)
            if (hourDec < 0)
                valCoordonnee *= -1;
            coordonnee.UpdateCoordonnee(valCoordonnee);

            return true;
        }

        /// <summary>
        /// Renvoi la direction sous la forme d'une chaîne internationalisée
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string GetDirectionString(CoordinatesDirection direction)
        {
            switch(direction)
            {
                case CoordinatesDirection.NE:
                    return Resources.NE;
                case CoordinatesDirection.E:
                    return Resources.E;
                case CoordinatesDirection.SE:
                    return Resources.SE;
                case CoordinatesDirection.S:
                    return Resources.S;
                case CoordinatesDirection.SO:
                    return Resources.SO;
                case CoordinatesDirection.O:
                    return Resources.O;
                case CoordinatesDirection.NO:
                    return Resources.NO;
                case CoordinatesDirection.N:
                default:
                    return Resources.N;
            }
        }

        /// <summary>
        /// Renvoi la direction sous la forme <see cref="CoordinatesDirection"/>
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static CoordinatesDirection GetDirectionFromString(string direction)
        {
            if (direction == Resources.NE)
                return CoordinatesDirection.NE;
            else if (direction == Resources.E)
                return CoordinatesDirection.E;
            else if (direction == Resources.SE)
                return CoordinatesDirection.SE;
            else if (direction == Resources.S)
                return CoordinatesDirection.S;
            else if (direction == Resources.SO)
                return CoordinatesDirection.SO;
            else if (direction == Resources.O)
                return CoordinatesDirection.O;
            else if (direction == Resources.NO)
                return CoordinatesDirection.NO;
            return CoordinatesDirection.N;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Valeur de la coordonnée
        /// </summary>
        private double coordonnee = 0;

        /// <summary>
        /// Type Longitude/Latitude
        /// </summary>
        private CoordinatesType coordinatesType = CoordinatesType.Longitude;

        #endregion
    }
}