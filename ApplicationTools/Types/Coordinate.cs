using System;

namespace ApplicationTools
{
    /// <summary>
    /// Position N/S/E/O de la coordonnée de localisation
    /// </summary>
    public enum CoordinatesPosition
    {
        /// <summary>
        /// Nord
        /// </summary>
        N,

        /// <summary>
        /// Est
        /// </summary>
        E,

        /// <summary>
        /// Sud
        /// </summary>
        S,

        /// <summary>
        /// Ouest
        /// </summary>
        O
    }

    /// <summary>
    /// Type Longitude/Latitude pour les <see cref="Coordinate"/>
    /// </summary>
    public enum CoordinatesType
    {
        /// <summary>
        /// Longitude
        /// </summary>
        Longitude,

        /// <summary>
        /// Latitude
        /// </summary>
        Latitude
    }

    /// <summary>
    /// Donnée de type Coordonnée de localisation : XX° XX' XX" [N/S/E/O]
    /// </summary>
    public class Coordinate
    {
        #region Propriétés

        /// <summary>
        /// Valeur du champ '°' de la coordonnée
        /// </summary>
        public decimal Degrees
        {
            get
            {
                return Math.Truncate(Math.Abs(coordonnee));
            }
        }

        /// <summary>
        /// Valeur du champ ''' de la coordonnée
        /// </summary>
        public decimal Minutes
        {
            get
            {
                return Math.Truncate((Math.Abs(coordonnee) - Degrees) * 60);
            }
        }

        /// <summary>
        /// Valeur du champ '"' de la coordonnée
        /// </summary>
        public decimal Seconds
        {
            get
            {
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
                // On positionne la direction en fonction du Type (Longitude/Latitude) et de la valeur de la coordonnée
                var direction = coordinatesType == CoordinatesType.Latitude ?
                                    coordonnee < 0 ? CoordinatesPosition.S : CoordinatesPosition.N
                                    : coordonnee < 0 ? CoordinatesPosition.O : CoordinatesPosition.E;
                return Degrees + "° " + Minutes + "' " + string.Format("{0:0.00}", Seconds) + "\" " + direction.ToString();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="coordonnee"></param>
        /// <param name="coordinatesType"></param>
        internal Coordinate(decimal coordonnee, CoordinatesType coordinatesType)
        {
            // Valorisation du champ interne
            this.coordonnee = coordonnee;
            this.coordinatesType = coordinatesType;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Valeur de la coordonnée
        /// </summary>
        private decimal coordonnee = 0;

        /// <summary>
        /// Type Longitude/Latitude
        /// </summary>
        private CoordinatesType coordinatesType = CoordinatesType.Longitude;

        #endregion
    }
}