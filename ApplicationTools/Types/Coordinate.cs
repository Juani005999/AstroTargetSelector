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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="coordonnee"></param>
        internal Coordinate(decimal coordonnee)
        {
            // Valorisation du champ interne
            this.coordonnee = coordonnee;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Valeur de la coordonnée
        /// </summary>
        private decimal coordonnee = 0;

        #endregion
    }
}