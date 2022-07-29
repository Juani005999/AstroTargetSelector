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
        /// Permet de forcer le rechargement des Slices
        /// </summary>
        internal bool ForceUpdateSlices { get; set; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        public List<ObjSliceTarget> Slices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (slices.Count == 0 || ForceUpdateSlices)
                {
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
                }
                ForceUpdateSlices = false;
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
                // Moyenne des temps de pose des Slices
                if (!scoring.HasValue || ForceUpdateSlices)
                    scoring = Slices.Select(t => t.TempsPoseCalcule).Average();

                //// On rapporte le résultat au Bougé max. afin d'avoir une concordance des Scoring et Rank
                //if (factory.GetAppInputs().Inputs.BougeMax != 0)
                //    result /= factory.GetAppInputs().Inputs.BougeMax;

                // Retour
                return Math.Floor(scoring.Value);
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
                // Le Rank est basé sur le scoring
                decimal result;
                if (Scoring > MinTempsPoseRank5)
                    result = 5;
                else if (Scoring > MinTempsPoseRank4)
                    result = 4;
                else if (Scoring > MinTempsPoseRank3)
                    result = 3;
                else if (Scoring > MinTempsPoseRank2)
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
        /// Renvoi l'Azimut calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate Azimut
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].Azimut;
            }
        }

        /// <summary>
        /// Renvoi la Hauteur calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi la Hauteur du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate Hauteur
        {
            get
            {
                // Renvoi la valeur de la Hauteur du premier slice
                return Slices[0].Hauteur;
            }
        }

        /// <summary>
        /// Renvoi l'Azimut Corrigee calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut Corrigee du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate AzimutCorrigee
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].AzimutCorrigee;
            }
        }

        /// <summary>
        /// Renvoi l'Azimut Precise calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi l'Azimut Precise du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate AzimutPrecise
        {
            get
            {
                // Renvoi la valeur d'Azimut du premier slice
                return Slices[0].AzimutPrecise;
            }
        }

        /// <summary>
        /// Renvoi la Hauteur Precise calculé pour la Target
        /// <para>Si nécessaire, lance le calcul des <see cref="Slices"/></para>
        /// <para>Renvoi la Hauteur Precise du premier <see cref="Slices"/> de la période d'observation</para>
        /// </summary>
        public Coordinate HauteurPrecise
        {
            get
            {
                // Renvoi la valeur de la Hauteur du premier slice
                return Slices[0].HauteurPrecise;
            }
        }

        /// <summary>
        /// Permet de savoir si un objet céleste est exclu de la liste
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
                    switch(direction)
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
        private List<ObjSliceTarget> slices = new List<ObjSliceTarget>();

        /// <summary>
        /// Singleton pour optimisation du Scoring
        /// <para>Moyenne des <see cref="ObjSliceTarget.TempsPoseCalcule"/> des Slices de la Target</para>
        /// </summary>
        private decimal? scoring = null;

        #endregion
    }
}
