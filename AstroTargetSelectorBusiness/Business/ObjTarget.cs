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
        /// Constellation de l'objet céleste
        /// </summary>
        public string Constellation { get; set; }

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
        public double Magnitude { get; set; }

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
        internal bool ForceUpdateSlices {
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
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
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
        /// Renvoi le Scoring calculé pour la Target
        /// <para>Moyenne des <see cref="ObjSliceTarget.TempsPoseCalcule"/> des Slices de la Target</para>
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
                return Math.Floor(Slices.Select(t => t.TempsPoseCalcule).Average());
                //return Math.Floor(Slices.Where(t => !t.EstExclu).Select(t => t.TempsPoseCalcule).Average());
            }
        }

        /// <summary>
        /// Renvoi le Rank calculé pour la Target
        /// <para>Barême basé sur le <see cref="Scoring"/></para>
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
                // Renvoi false si au moins 1 slice n'est pas exclu
                //return Slices[0].EstExclu;
                return !(Slices.Where(t => !t.EstExclu).Count() > 0);
            }
        }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Horaire</para>
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
                    for (int i = 0; i < factory.GetAppInputs().Inputs.NombreSlice; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        hourlySlices.Add(new ObjSliceTarget(factory, this)
                        {
                            DateHeure = factory.GetAppInputs().Inputs.DateHeureObservation.AddMinutes(i * factory.GetAppInputs().Inputs.MinuteIntervalSlice)
                        });
                    }
                }
                return hourlySlices;
            }
        }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Nuits</para>
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
                    DateTime dateDebut = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Day,
                                                        19, 0, 0);
                    for (int i = 0; i < 20; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        dailySlices.Add(new ObjSliceTarget(factory, this)
                        {
                            DateHeure = dateDebut.AddMinutes(i * 30)
                        });
                    }
                }
                return dailySlices;
            }
        }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Nuits</para>
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
                    DateTime dateEnCours = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Day);
                    while (dateEnCours <= factory.GetAppInputs().Inputs.DateHeureObservation.AddMonths(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        monthlySlices.Add(new ObjDailySliceTarget(factory, this)
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
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// <para>Intervalles du mode de visualisation Annuel</para>
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
                    DateTime dateEnCours = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                        1);
                    while (dateEnCours <= factory.GetAppInputs().Inputs.DateHeureObservation.AddYears(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        yearlySlices.Add(new ObjMonthlySliceTarget(factory, this)
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

        #region Méthodes

        /// <summary>
        /// Renvoi la série d'intervalles pour le graphique en fonction du mode de visualisation
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public List<IChartSlice> GetCurrentChartSlice(ObjInputs.ModeVisualisation mode)
        {
            switch(mode)
            {
                case ObjInputs.ModeVisualisation.Annuel:
                    return YearlySlices;

                case ObjInputs.ModeVisualisation.Mensuel:
                    return MonthlySlices;

                case ObjInputs.ModeVisualisation.Nuits:
                    return DailySlices;

                case ObjInputs.ModeVisualisation.Horaire:
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
            switch (factory.GetAppInputs().Inputs.Visualisation)
            {
                case ObjInputs.ModeVisualisation.Annuel:
                    // On ajoute 1 intervalle toute les 60min sur 10h à partir de 19h
                    DateTime dateAnnuel = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                        1,
                                                        0, 0, 0);
                    while (dateAnnuel <= factory.GetAppInputs().Inputs.DateHeureObservation.AddYears(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjSliceTarget(factory, this)
                        {
                            DateHeure = dateAnnuel
                        });
                        dateAnnuel = dateAnnuel.AddMonths(1);
                    }
                    break;

                case ObjInputs.ModeVisualisation.Mensuel:
                    // On ajoute 1 intervalle jour (contenant chacun 3 intervalles) pendant 1 mois
                    DateTime dateMensuel = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Day,
                                                        0, 0, 0);
                    while (dateMensuel <= factory.GetAppInputs().Inputs.DateHeureObservation.AddMonths(1))
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjDailySliceTarget(factory, this)
                        {
                            DateHeure = dateMensuel
                        });
                        dateMensuel = dateMensuel.AddDays(10);
                    }
                    break;

                case ObjInputs.ModeVisualisation.Nuits:
                    // On ajoute 1 intervalle toute les 60min sur 10h à partir de 19h
                    DateTime dateNuits = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation.Day,
                                                        19, 0, 0);
                    for (int i = 0; i < 10; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjSliceTarget(factory, this)
                        {
                            DateHeure = dateNuits.AddMinutes(i * 60)
                        });
                    }
                    break;

                case ObjInputs.ModeVisualisation.Horaire:
                default:
                    // On ajoute 1 intervalle toute les 10min sur 2h
                    for (int i = 0; i < 12; i++)
                    {
                        // On ajoute le nombre d'intervalles requis et on positionne la date et l'heure pour chacun des slices
                        slices.Add(new ObjSliceTarget(factory, this)
                        {
                            DateHeure = factory.GetAppInputs().Inputs.DateHeureObservation.AddMinutes(i * 10)
                        });
                    }
                    break;
            }
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
