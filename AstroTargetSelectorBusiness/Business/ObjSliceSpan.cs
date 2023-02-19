using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un intervalle regroupant des intervalles <see cref="ObjSliceTarget"/>
    /// </summary>
    internal class ObjSliceSpan : IObjSliceSpan
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double RotationAngulaireGlobale
        {
            get
            {
                if (!rotationAngulaire.HasValue)
                {
                    double rotationMoyenneDegS = Slices.Average(t => t.RotationAngulaire) * (180 / Math.PI);
                    //double rotationMoyenneDegS = Slices.Average(t => t.RotationAngulaire);
                    // On multiplie par le nombre de seconde de l'intervalle complet
                    rotationAngulaire = rotationMoyenneDegS * (dateHeureFin - dateHeureDebut).TotalSeconds;
                }

                return rotationAngulaire.Value;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string RotationAngulaireGlobaleFormated
        {
            get
            {
                return appToolFactory.GetCoordinate(RotationAngulaireGlobale, CoordinatesType.Degree).FormatedString;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IChartSlice> Slices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (slices.Count == 0)
                {
                    // Clear de la liste des Slices
                    slices.Clear();

                    // On vérifie la validité des Dates
                    if (dateHeureFin < dateHeureDebut)
                        dateHeureFin = dateHeureDebut.AddMinutes(30);

                    // On ajoute un slice toute les 10 min sur la tranche horaire concernée
                    DateTime dateHeureEnCours = dateHeureDebut;
                    while (dateHeureEnCours < dateHeureFin)
                    {
                        // On ajoute le slice
                        slices.Add(new ObjSliceTarget(appToolFactory, appInputs, parentTarget)
                        {
                            DateHeure = dateHeureEnCours
                        });
                        dateHeureEnCours = dateHeureEnCours.AddMinutes(1);
                    }
                }
                return slices;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjSliceSpan(IAppToolFactory appToolFactory, IAppInputs appInputs, IObjTarget parentTarget, DateTime dateHeureDebut, DateTime dateHeureFin)
        {
            this.appToolFactory = appToolFactory;
            this.appInputs = appInputs;
            this.parentTarget = parentTarget;
            this.dateHeureDebut = dateHeureDebut;
            this.dateHeureFin = dateHeureFin;
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
        /// Date et Heure du début de l'intervalle
        /// </summary>
        private DateTime dateHeureDebut { get; set; }

        /// <summary>
        /// Date et Heure de fin de l'intervalle
        /// </summary>
        private DateTime dateHeureFin { get; set; }

        /// <summary>
        /// Rotation angulaire pour l'intervalle
        /// </summary>
        private double? rotationAngulaire = null;

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps du mois
        /// </summary>
        protected List<IChartSlice> slices = new List<IChartSlice>();

        #endregion
    }
}
