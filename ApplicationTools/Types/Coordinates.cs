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
                Coordinate coordonnee = new Coordinate(latitude, CoordinatesType.Latitude);
                return coordonnee.FormatedString;
            }
        }

        /// <summary>
        /// Renvoi la Longitude sous la forme d'une chaîne de caractère formatée
        /// </summary>
        public string Longitude
        {
            get
            {
                Coordinate coordonnee = new Coordinate(longitude, CoordinatesType.Longitude);
                return coordonnee.FormatedString;
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        internal Coordinates(decimal latitude, decimal longitude)
        {
            // Valorisation des champs interne
            this.latitude = latitude;
            this.longitude = longitude;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Longitude du Lieu
        /// </summary>
        private decimal longitude = 0;

        /// <summary>
        /// Latitude du Lieu
        /// </summary>
        private decimal latitude = 0;

        #endregion
    }
}
