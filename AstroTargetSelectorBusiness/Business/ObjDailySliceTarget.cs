using System;
using System.Collections.Generic;
using ApplicationTools;
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
                    + Environment.NewLine + $"{Resources.Hauteur} : {Math.Floor(Hauteur.Coordonnee)} °"
                    + Environment.NewLine + $"{Resources.Azimut} : {Math.Floor(Azimut.Coordonnee)} ° ({Direction})";
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
                    slices.Add(new ObjSliceTarget(appToolFactory, appInputs, parentTarget)
                    {
                        DateHeure = dateDebut
                    });

                    // On ajoute le slice de 00h
                    slices.Add(new ObjSliceTarget(appToolFactory, appInputs, parentTarget)
                    {
                        DateHeure = dateDebut.AddHours(4)
                    });

                    // On ajoute le slice de 04h
                    slices.Add(new ObjSliceTarget(appToolFactory, appInputs, parentTarget)
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
        internal ObjDailySliceTarget(IAppToolFactory appToolFactory, IAppInputs appInputs, IObjTarget parentTarget)
            : base(appToolFactory, appInputs, parentTarget)
        {
        }

        #endregion

        #region Champs
        #endregion
    }
}
