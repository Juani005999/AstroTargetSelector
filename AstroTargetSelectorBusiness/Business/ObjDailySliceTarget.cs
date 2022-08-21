using System;
using System.Collections.Generic;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un intervalle nuit regroupant 3 intervalles de temps de calcul pour un objet céleste (20h - 00h - 04h)
    /// </summary>
    public class ObjDailySliceTarget : ObjGroupSliceTarget, IChartSlice
    {
        #region Propriétés

        /// <summary>
        /// ToolTip du Slice à afficher dans le graphique
        /// </summary>
        public string ToolTip
        {
            get
            {
                return $"{DateHeure.ToString("d")}"
                    + Environment.NewLine + $"{Resources.TempsDePoseMax} : {Math.Floor(TempsPoseCalcule)} s"
                    + Environment.NewLine + $"{Resources.Hauteur} : {Math.Floor(Hauteur.Coordonnee)} °";
            }
        }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        public override List<IChartSlice> Slices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (slices.Count == 0)
                {
                    DateTime dateDebut = new DateTime(DateHeure.Year,
                                                       DateHeure.Month,
                                                       DateHeure.Day,
                                                       20, 0, 0);

                    // Clear de la liste des Slices
                    slices.Clear();

                    // On ajoute le slice de 20h
                    slices.Add(new ObjSliceTarget(factory, parentTarget)
                    {
                        DateHeure = dateDebut
                    });

                    // On ajoute le slice de 00h
                    slices.Add(new ObjSliceTarget(factory, parentTarget)
                    {
                        DateHeure = dateDebut.AddHours(4)
                    });

                    // On ajoute le slice de 04h
                    slices.Add(new ObjSliceTarget(factory, parentTarget)
                    {
                        DateHeure = dateDebut.AddHours(8)
                    });
                }
                return slices;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjDailySliceTarget(AppObjFactory factory, ObjTarget parentTarget)
            : base(factory, parentTarget)
        {
            this.factory = factory;
            this.parentTarget = parentTarget;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Objet céleste parent de l'objet Slice
        /// </summary>
        private readonly ObjTarget parentTarget = null;

        #endregion
    }
}
