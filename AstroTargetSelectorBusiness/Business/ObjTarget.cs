using System;
using System.Collections.Generic;
using System.Linq;

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
        public decimal RA { get; set; }

        /// <summary>
        /// DEC : Déclinaison de l'objet céleste
        /// <para>Valeur exprimée en "degrés" décimal</para>
        /// </summary>
        public decimal DEC { get; set; }

        /// <summary>
        /// Magnitude de l'objet céleste
        /// </summary>
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Grandeur : Largeur de l'objet céleste
        /// </summary>
        public decimal GrandeurWidth { get; set; }

        /// <summary>
        /// Grandeur : Hauteur de l'objet céleste
        /// </summary>
        public decimal GrandeurHeight { get; set; }

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
                        DateHeure = factory.GetAppInputs().Inputs.DateHeureObservation.AddMinutes(i * 15)
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
                return Math.Floor(Slices.Select(t => t.TempsPoseCalcule).Average());
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
                if (scoring > MinTempsPoseRank5)
                    return 5;
                if (scoring > MinTempsPoseRank4)
                    return 4;
                if (scoring > MinTempsPoseRank3)
                    return 3;
                if (scoring > MinTempsPoseRank2)
                    return 2;
                if (scoring > MinTempsPoseRank1)
                    return 1;
                return 0;
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
