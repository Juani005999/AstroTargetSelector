using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un objet céleste
    /// </summary>
    public class ObjTarget : IObjTarget
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
        /// <inheritdoc/>
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Constellation { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate RA { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate DEC { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Magnitude { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate GrandeurWidth { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate GrandeurHeight { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool ForceUpdateSlices {
            set
            {
                slices.Clear();
                hourlySlices.Clear();
                dailySlices.Clear();
                monthlySlices.Clear();
                yearlySlices.Clear();
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
                    InitialiseMainSlices();
                }
                return slices;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Scoring
        {
            get
            {
                //// Si on a au moins 1 Slice non Exclu
                //if (Slices.Where(t => !t.EstExclu).Count() > 0)
                //{
                //    return Math.Floor(Slices.Where(t => !t.EstExclu).Select(t => t.TempsPoseCalcule).Average());
                //}
                //return 0;

                // Retour
                Stopwatch chrono = Stopwatch.StartNew();
                double retour = Math.Floor(Slices.Select(t => t.TempsPoseCalcule).Average());
                //return Math.Floor(Slices.Where(t => !t.EstExclu).Select(t => t.TempsPoseCalcule).Average());

                //appToolFactory.GetLog().Log($"Calcul scoring [{Nom} en {chrono.ElapsedMilliseconds} ms]");

                return retour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Rank
        {
            get
            {
                // Le Rank est basé sur le scoring
                double result;
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

                // Retour
                return Math.Floor(result);
            }
        }

        /// <summary>
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        /// </summary>
        public bool EstExclu
        {
            get
            {
                // Renvoi false si au moins 1 slice n'est pas exclu
                //return Slices[0].EstExclu;
                return !(Slices.Where(t => !t.EstExclu).Count() > 0);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IChartSlice> HourlySlices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (hourlySlices.Count == 0)
                {
                    //factory.GetLog().Log($"Accès à HourlySlices de l'objet {Nom}");

                    // Clear de la liste des Slices
                    hourlySlices.Clear();
                    for (int i = 0; i < appInputs.Inputs.NombreSlice; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        hourlySlices.Add(new ObjSliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = appInputs.Inputs.DateHeureObservation.AddMinutes(i * appInputs.Inputs.MinuteIntervalSlice)
                        });
                    }
                }
                return hourlySlices;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IChartSlice> DailySlices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (dailySlices.Count == 0)
                {
                    //factory.GetLog().Log($"Accès à DailySlices de l'objet {Nom}");

                    // Clear de la liste des Slices
                    dailySlices.Clear();
                    // On ajoute 1 intervalle toute les 30min sur 10h à partir de 19h
                    DateTime dateDebut = new DateTime(appInputs.Inputs.DateHeureObservation.Year,
                                                        appInputs.Inputs.DateHeureObservation.Month,
                                                        appInputs.Inputs.DateHeureObservation.Day,
                                                        19, 0, 0);
                    for (int i = 0; i < 20; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        dailySlices.Add(new ObjSliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = dateDebut.AddMinutes(i * 30)
                        });
                    }
                }
                return dailySlices;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IChartSlice> MonthlySlices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (monthlySlices.Count == 0)
                {
                    //factory.GetLog().Log($"Accès à DailySlices de l'objet {Nom}");

                    // Clear de la liste des Slices
                    monthlySlices.Clear();
                    // On ajoute 1 intervalle jour (contenant chacun 3 intervalles) pendant 1 mois
                    DateTime dateEnCours = new DateTime(appInputs.Inputs.DateHeureObservation.Year,
                                                        appInputs.Inputs.DateHeureObservation.Month,
                                                        appInputs.Inputs.DateHeureObservation.Day);
                    while (dateEnCours <= appInputs.Inputs.DateHeureObservation.AddMonths(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        monthlySlices.Add(new ObjDailySliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = dateEnCours
                        });
                        dateEnCours = dateEnCours.AddDays(2);
                    }
                }
                return monthlySlices;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IChartSlice> YearlySlices
        {
            get
            {
                // On recharge les slices uniquement si nécessaire
                if (yearlySlices.Count == 0)
                {
                    //factory.GetLog().Log($"Accès à DailySlices de l'objet {Nom}");

                    // Clear de la liste des Slices
                    yearlySlices.Clear();
                    // On ajoute 1 intervalle mois (contenant chacun 3 intervalles) pendant 1 an
                    DateTime dateEnCours = new DateTime(appInputs.Inputs.DateHeureObservation.Year,
                                                        appInputs.Inputs.DateHeureObservation.Month,
                                                        1);
                    while (dateEnCours <= appInputs.Inputs.DateHeureObservation.AddYears(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        yearlySlices.Add(new ObjMonthlySliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = dateEnCours
                        });
                        dateEnCours = dateEnCours.AddMonths(1);
                    }
                }
                return yearlySlices;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTarget(IAppToolFactory appToolFactory, IAppInputs appInputs)
        {
            this.appToolFactory = appToolFactory;
            this.appInputs = appInputs;

            // Initialisation des objets
            RA = appToolFactory.GetCoordinate(0, CoordinatesType.RA);
            DEC = appToolFactory.GetCoordinate(0, CoordinatesType.DEC);
            GrandeurWidth = appToolFactory.GetCoordinate(0, CoordinatesType.Degree);
            GrandeurHeight = appToolFactory.GetCoordinate(0, CoordinatesType.Degree);
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IChartSlice> GetCurrentChartSlice(ModeVisualisation mode)
        {
            switch(mode)
            {
                case ModeVisualisation.Annuel:
                    return YearlySlices;

                case ModeVisualisation.Mensuel:
                    return MonthlySlices;

                case ModeVisualisation.Nuits:
                    return DailySlices;

                case ModeVisualisation.Horaire:
                default:
                    return HourlySlices;
            }
        }

        /// <summary>
        /// Permet l'initialisation de la collection principale de slice en fonction du mode de visualisation
        /// </summary>
        private void InitialiseMainSlices()
        {
            // Clear de la liste des Slices
            slices.Clear();

            // Mise en place des slices principaux en fonction du mode de visualisation
            switch (appInputs.Inputs.Visualisation)
            {
                case ModeVisualisation.Annuel:
                    // On ajoute 1 intervalle toute les 60min sur 10h à partir de 19h
                    DateTime dateAnnuel = new DateTime(appInputs.Inputs.DateHeureObservation.Year,
                                                        appInputs.Inputs.DateHeureObservation.Month,
                                                        1,
                                                        0, 0, 0);
                    while (dateAnnuel <= appInputs.Inputs.DateHeureObservation.AddYears(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjSliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = dateAnnuel
                        });
                        dateAnnuel = dateAnnuel.AddMonths(1);
                    }
                    break;

                case ModeVisualisation.Mensuel:
                    // On ajoute 1 intervalle jour (contenant chacun 3 intervalles) pendant 1 mois
                    DateTime dateMensuel = new DateTime(appInputs.Inputs.DateHeureObservation.Year,
                                                        appInputs.Inputs.DateHeureObservation.Month,
                                                        appInputs.Inputs.DateHeureObservation.Day,
                                                        0, 0, 0);
                    while (dateMensuel <= appInputs.Inputs.DateHeureObservation.AddMonths(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjDailySliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = dateMensuel
                        });
                        dateMensuel = dateMensuel.AddDays(10);
                    }
                    break;

                case ModeVisualisation.Nuits:
                    // On ajoute 1 intervalle toute les 60min sur 10h à partir de 19h
                    DateTime dateNuits = new DateTime(appInputs.Inputs.DateHeureObservation.Year,
                                                        appInputs.Inputs.DateHeureObservation.Month,
                                                        appInputs.Inputs.DateHeureObservation.Day,
                                                        19, 0, 0);
                    for (int i = 0; i < 10; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjSliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = dateNuits.AddMinutes(i * 60)
                        });
                    }
                    break;

                case ModeVisualisation.Horaire:
                default:
                    // On ajoute 1 intervalle toute les 10min sur 2h
                    for (int i = 0; i < 12; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjSliceTarget(appToolFactory, appInputs, this)
                        {
                            DateHeure = appInputs.Inputs.DateHeureObservation.AddMinutes(i * 10)
                        });
                    }
                    break;
            }
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
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        private List<IChartSlice> slices = new List<IChartSlice>();

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Horaire</para>
        /// </summary>
        private List<IChartSlice> hourlySlices = new List<IChartSlice>();

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Nuits</para>
        /// </summary>
        private List<IChartSlice> dailySlices = new List<IChartSlice>();

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Mensuel</para>
        /// </summary>
        private List<IChartSlice> monthlySlices = new List<IChartSlice>();

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Annuel</para>
        /// </summary>
        private List<IChartSlice> yearlySlices = new List<IChartSlice>();

        #endregion
    }
}
