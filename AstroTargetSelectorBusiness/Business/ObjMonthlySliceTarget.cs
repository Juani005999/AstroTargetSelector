using System;
using System.Collections.Generic;
using System.Globalization;
using ApplicationTools;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un intervalle mois regroupant 1 intervalle de temps de calcul pour un objet céleste (00h) par nuit
    /// </summary>
    public class ObjMonthlySliceTarget : ObjGroupSliceTarget, IChartSlice
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ToolTip
        {
            get
            {
                return $"{DateHeure.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.YearMonthPattern)}"
                    + Environment.NewLine + $"{Resources.TempsDePoseMax} : {Math.Floor(TempsPoseCalcule)} s"
                    + Environment.NewLine + $"{Resources.Hauteur} : {Math.Floor(Hauteur.Coordonnee)} °"
                    + Environment.NewLine + $"{Resources.Azimut} : {Math.Floor(Azimut.Coordonnee)} ° ({Coordinate.GetDirectionString(Direction)})";
            }
        }

        /// <summary>
        /// <inheritdoc/>
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
                                                       DateHeure.Day);

                    // Clear de la liste des Slices
                    slices.Clear();

                    // On ajoute le slice du jour 1
                    slices.Add(new ObjDailySliceTarget(appToolFactory, appInputs, parentTarget)
                    {
                        DateHeure = dateDebut
                    });

                    // On ajoute le slice du jour 10
                    slices.Add(new ObjDailySliceTarget(appToolFactory, appInputs, parentTarget)
                    {
                        DateHeure = dateDebut.AddDays(10)
                    });

                    // On ajoute le slice du jour 20
                    slices.Add(new ObjDailySliceTarget(appToolFactory, appInputs, parentTarget)
                    {
                        DateHeure = dateDebut.AddDays(20)
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
        internal ObjMonthlySliceTarget(IAppToolFactory appToolFactory, IAppInputs appInputs, IObjTarget parentTarget)
            : base (appToolFactory, appInputs, parentTarget)
        {
        }

        #endregion

        #region Champs
        #endregion
    }
}
