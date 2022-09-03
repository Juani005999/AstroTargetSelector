using System;

namespace ApplicationTools
{
    /// <summary>
    /// Donnée de type Coordonnées complète de localisation : Longitude + Latitude
    /// <para>La longitude et la latitude sont de type <see cref="Coordinate"/></para>
    /// </summary>
    public class Coordinates
    {
        #region Propriétés

        /// <summary>
        /// Renvoi la Latitude sous la forme d'une chaîne de caractère formatée
        /// </summary>
        public string Latitude
        {
            get
            {
                return CoordonneeLatitude.FormatedString;
            }
        }

        /// <summary>
        /// Renvoi la Latitude sous la forme d'un decimal
        /// </summary>
        public double LatitudeValue
        {
            get
            {
                return latitude;
            }
        }

        /// <summary>
        /// Renvoi la Longitude sous la forme d'une chaîne de caractère formatée
        /// </summary>
        public string Longitude
        {
            get
            {
                return CoordonneeLongitude.FormatedString;
            }
        }

        /// <summary>
        /// Renvoi la Longitude sous la forme d'un decimal
        /// </summary>
        public double LongitudeValue
        {
            get
            {
                return longitude;
            }
        }

        /// <summary>
        /// Renvoi le lieu sous la forme d'une chaîne de caractères formatée (Latitude - Longitude)
        /// <para>XX° XX' XX" N/S - XX° XX' XX E/O"</para>
        /// </summary>
        public string LocalisationComplete
        {
            get
            {
                return Latitude + " / " + Longitude;
            }
        }

        /// <summary>
        /// Renvoi l'objet <see cref="Coordinate"/> correspondant à la Longitude
        /// <para>Si le membre interne est nul, on renvoi un nouvel objet avec pour valeur 0</para>
        /// </summary>
        public Coordinate CoordonneeLongitude
        {
            get
            {
                return coordinateLongitude ?? new Coordinate(0, CoordinatesType.Longitude);
            }
        }

        /// <summary>
        /// Renvoi l'objet <see cref="Coordinate"/> correspondant à la Latitude
        /// <para>Si le membre interne est nul, on renvoi un nouvel objet avec pour valeur 0</para>
        /// </summary>
        public Coordinate CoordonneeLatitude
        {
            get
            {
                return coordinateLatitude ?? new Coordinate(0, CoordinatesType.Latitude);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        internal Coordinates(double latitude, double longitude)
        {
            // Valorisation des champs interne
            this.latitude = latitude;
            this.longitude = longitude;
            coordinateLatitude = new Coordinate(latitude, CoordinatesType.Latitude);
            coordinateLongitude = new Coordinate(longitude, CoordinatesType.Longitude);
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet de positionner de nouvelles coordonnées (Longitude/Latitude) pour l'objet en cours
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public void UpdateCoordonnees(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            coordinateLatitude.UpdateCoordonnee(latitude);
            coordinateLongitude.UpdateCoordonnee(longitude);
        }

        /// <summary>
        /// Valide une saisie au format string
        /// <para>L'allocation mémoire pour le paramètre ref <paramref name="coordonnees"/> doit être réalisé par l'appelant</para>
        /// </summary>
        /// <param name="degreeLongitude"></param>
        /// <param name="minuteLongitude"></param>
        /// <param name="secondeLongitude"></param>
        /// <param name="directionLongitude"></param>
        /// <param name="degreeLatitude"></param>
        /// <param name="minuteLatitude"></param>
        /// <param name="secondeLatitude"></param>
        /// <param name="directionLatitude"></param>
        /// <param name="coordonnees"></param>
        /// <param name="appLog"></param>
        /// <returns></returns>
        public static bool TryParse(string degreeLongitude, string minuteLongitude, string secondeLongitude, string directionLongitude,
                                    string degreeLatitude, string minuteLatitude, string secondeLatitude, string directionLatitude,
                                    ref Coordinates coordonnees,
                                    IAppLog appLog)
        {
            // On vérifie la validité du paramètre ref "coordonnees"
            if (coordonnees == null)
            {
                appLog.Log($"Paramètre ref 'coordonnees' null", "Coordinates", null, TypeLog.Fatal);
                return false;
            }

            // On TryParse Longitude
            Coordinate coordonneeLongitude = coordonnees.CoordonneeLongitude;
            if (!Coordinate.TryParse(degreeLongitude, minuteLongitude, secondeLongitude, directionLongitude, ref coordonneeLongitude, appLog))
            {
                appLog.Log($"TryParse pour l'objet Coordinate Longitude à renvoyer False", "Coordinates", null, TypeLog.Warning);
                return false;
            }

            // On TryParse Latitude
            Coordinate coordonneeLatitude = coordonnees.coordinateLatitude;
            if (!Coordinate.TryParse(degreeLatitude, minuteLatitude, secondeLatitude, directionLatitude, ref coordonneeLatitude, appLog))
            {
                appLog.Log($"TryParse pour l'objet Coordinate Latitude à renvoyer False", "Coordinates", null, TypeLog.Warning);
                return false;
            }

            // Update de l'objet en entrée et retour
            coordonnees.UpdateCoordonnees(coordonneeLatitude.Coordonnee, coordonneeLongitude.Coordonnee);
            return true;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Longitude du Lieu
        /// </summary>
        private double longitude = 0;

        /// <summary>
        /// Latitude du Lieu
        /// </summary>
        private double latitude = 0;

        /// <summary>
        /// Objet Longitude du Lieu <see cref="Coordinate"/>
        /// </summary>
        private Coordinate coordinateLongitude = null;

        /// <summary>
        /// Objet Latitude du Lieu <see cref="Coordinate"/>
        /// </summary>
        private Coordinate coordinateLatitude = null;

        #endregion
    }
}
