using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un objet céleste
    /// </summary>
    public class ObjTarget
    {
        #region Constantes

        /// <summary>
        /// Temps de pose minimum pour avoir le Rank niveau 5
        /// </summary>
        public const int MinTempsPoseRank5 = 50;

        /// <summary>
        /// Temps de pose minimum pour avoir le Rank niveau 4
        /// </summary>
        public const int MinTempsPoseRank4 = 30;

        /// <summary>
        /// Temps de pose minimum pour avoir le Rank niveau 3
        /// </summary>
        public const int MinTempsPoseRank3 = 20;

        /// <summary>
        /// Temps de pose minimum pour avoir le Rank niveau 2
        /// </summary>
        public const int MinTempsPoseRank2 = 10;

        /// <summary>
        /// Temps de pose minimum pour avoir le Rank niveau 1
        /// </summary>
        public const int MinTempsPoseRank1 = 5;

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de l'objet céleste
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Type de l'objet céleste (amas globulaire, nébuleuse planétaire, galaxie spirale, ...)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Description de l'objet céleste
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// RA : Acsension droite de l'objet céleste
        /// <para>Valeur exprimée en "degrés horaires" décimal</para>
        /// </summary>
        public Coordinate RA { get; set; }

        /// <summary>
        /// DEC : Déclinaison de l'objet céleste
        /// <para>Valeur exprimée en "degrés" décimal</para>
        /// </summary>
        public Coordinate DEC { get; set; }

        /// <summary>
        /// Magnitude de l'objet céleste
        /// </summary>
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Grandeur : Largeur de l'objet céleste
        /// </summary>
        public Coordinate GrandeurWidth { get; set; }

        /// <summary>
        /// Grandeur : Hauteur de l'objet céleste
        /// </summary>
        public Coordinate GrandeurHeight { get; set; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        public List<ObjSliceTarget> Slices
        {
            get
            {
                //factory.GetLog().Log($"Chargement de la liste des Slices de l'objet {Nom}", GetType().Name);

                // Clear de la liste des Slices
                slices.Clear();
                for (int i = 0; i < factory.GetAppInputs().Inputs.NombreSlice; i++)
                {
                    // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                    slices.Add(new ObjSliceTarget(factory, this)
                    {
                        DateHeure = factory.GetAppInputs().Inputs.DateHeureObservation.AddMinutes(i * factory.GetAppInputs().Inputs.MinuteIntervalSlice)
                    });
                }
                return slices;
            }
        }
        
        /// <summary>
        /// Renvoi le Scoring calculé pour la Target
        /// <para>Moyenne des <see cref="ObjSliceTarget.TempsPoseCalcule"/> des Slices de la Target</para>
        /// </summary>
        public decimal Scoring
        {
            get
            {
                //factory.GetLog().Log($"Calcul du Scoring de l'objet {Nom}", GetType().Name);

                // Moyenne des temps de pose des Slices
                decimal result = Slices.Select(t => t.TempsPoseCalcule).Average();

                //// On rapporte le résultat au Bougé max. afin d'avoir une concordance des Scoring et Rank
                //if (factory.GetAppInputs().Inputs.BougeMax != 0)
                //    result /= factory.GetAppInputs().Inputs.BougeMax;

                // Retour
                return Math.Floor(result);
            }
        }

        /// <summary>
        /// Renvoi le Rank calculé pour la Target
        /// <para>Barême basé sur le <see cref="Scoring"/></para>
        /// </summary>
        public decimal Rank
        {
            get
            {
                //factory.GetLog().Log($"Calcul du Rank de l'objet {Nom}", GetType().Name);

                // Le Rank est basé sur le scoring
                decimal scoring = Scoring;
                decimal result;
                if (scoring > MinTempsPoseRank5)
                    result = 5;
                else if (scoring > MinTempsPoseRank4)
                    result = 4;
                else if (scoring > MinTempsPoseRank3)
                    result = 3;
                else if (scoring > MinTempsPoseRank2)
                    result = 2;
                else
                    result = 1;

                //// On rapporte le résultat au Bougé max. afin d'avoir une concordance des Scoring et Rank
                //if (factory.GetAppInputs().Inputs.BougeMax != 0)
                //    result /= factory.GetAppInputs().Inputs.BougeMax;

                // Retour
                return Math.Floor(result);
            }
        }

        /// <summary>
        /// Renvoi le Rank calculé pour la Target
        /// <para>Barême basé sur le <see cref="Scoring"/></para>
        /// </summary>
        public double Azimut
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].Azimut;
            }
        }

        /// <summary>
        /// Permet de savoir si un objet céleste fait partie d'une zone exclue du ciel
        /// </summary>
        public bool EstExclu
        {
            get
            {
                // Parcours des zones à exclure
                foreach (CoordinatesDirection direction in factory.GetAppInputs().Inputs.ZonesExclues)
                {
                    switch(direction)
                    {
                        case CoordinatesDirection.N:
                            if (Azimut > 337.5 || Azimut <= 22.5)
                                return true;
                            break;
                        case CoordinatesDirection.NE:
                            if (Azimut > 22.5 && Azimut <= 67.5)
                                return true;
                            break;
                        case CoordinatesDirection.E:
                            if (Azimut > 67.5 && Azimut <= 112.5)
                                return true;
                            break;
                        case CoordinatesDirection.SE:
                            if (Azimut > 112.5 && Azimut <= 157.5)
                                return true;
                            break;
                        case CoordinatesDirection.S:
                            if (Azimut > 157.5 && Azimut <= 202.5)
                                return true;
                            break;
                        case CoordinatesDirection.SO:
                            if (Azimut > 202.5 && Azimut <= 247.5)
                                return true;
                            break;
                        case CoordinatesDirection.O:
                            if (Azimut > 247.5 && Azimut <= 292.5)
                                return true;
                            break;
                        case CoordinatesDirection.NO:
                            if (Azimut > 292.5 && Azimut <= 337.5)
                                return true;
                            break;

                        default:
                            break;
                    }
                }

                // Renvoi la valeur d'Azimut du premier slice
                return false;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTarget(AppObjFactory factory)
        {
            this.factory = factory;

            // Initialisation des objets
            RA = factory.GetCoordinate(0, CoordinatesType.RA);
            DEC = factory.GetCoordinate(0, CoordinatesType.DEC);
            GrandeurWidth = factory.GetCoordinate(0, CoordinatesType.Degree);
            GrandeurHeight = factory.GetCoordinate(0, CoordinatesType.Degree);
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        public List<ObjSliceTarget> slices = new List<ObjSliceTarget>();

        #endregion
    }
}
