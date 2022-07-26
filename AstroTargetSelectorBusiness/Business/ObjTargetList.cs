using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant une liste d'objet <see cref="ObjTarget"/>
    /// </summary>
    public class ObjTargetList
    {
        #region Propriétés

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// <para>Afin de forcer le rechargement de la liste depuis le fichier de configuration, il faut positionner la propriété <see cref="ForceUpdateListe"/> à true</para>
        /// </summary>
        internal List<ObjTarget> ListeObjTarget
        {
            get
            {
                if (listeObjTarget == null)
                {
                    listeObjTarget = new List<ObjTarget>();
                    ForceUpdateListe = true;
                }
                if (ForceUpdateListe)
                    ChargementListe();
                return listeObjTarget;
            }
        }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjTarget"/></para>
        /// </summary>
        internal bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration
        /// </summary>
        internal string TargetListeFullPathFile
        {
            get
            {
                return factory.GetAppContext().UserAppDataPath + "\\" + targetListeFileName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTargetList(AppObjFactory factory)
        {
            this.factory = factory;
            ForceUpdateListe = false;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet le chargement de la liste des objets céleste depuis le fichier de configuration
        /// </summary>
        private void ChargementListe()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Rechargement de la liste des targets depuis le fichier de configuration", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On flush le flag de rechargement forcé
                ForceUpdateListe = false;

                // Clear de la liste actuelle
                listeObjTarget.Clear();

                // TODO : Si le fichier de configuration n'existe pas sur le poste, on le télécharge ?

                // Lecture du fichier de configuration et ajout dans la liste
                factory.GetLog().Log($"Fichier de configuration contenant la liste des objets céleste : {TargetListeFullPathFile}", GetType().Name);
                if (File.Exists(TargetListeFullPathFile))
                {
                    using (var reader = new StreamReader(TargetListeFullPathFile))
                    {
                        // On passe la ligne d'en-tête
                        var lineTitre = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split('\t');
                            listeObjTarget.Add(new ObjTarget(factory)
                            {
                                Nom = values[0],
                                Type = values[1],
                                Description = values[2],
                                RA = factory.GetCoordinate(Convert.ToDecimal(values[3]), CoordinatesType.RA),
                                DEC = factory.GetCoordinate(Convert.ToDecimal(values[4]), CoordinatesType.DEC),
                                Magnitude = Convert.ToDecimal(values[5]),
                                GrandeurWidth = factory.GetCoordinate(Convert.ToDecimal(values[6]), CoordinatesType.Degree),
                                GrandeurHeight = factory.GetCoordinate(Convert.ToDecimal(values[7]), CoordinatesType.Degree)
                            });
                        }
                    }
                }
                else
                    factory.GetLog().Log($"Fichier de configuration contenant la liste des objets céleste manquant. Aucun objet chargé", GetType().Name, null, AppLog.TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Chargement de {listeObjTarget.Count} targets en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        private List<ObjTarget> listeObjTarget = null;

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des objets céleste
        /// </summary>
        private const string targetListeFileName = "TargetListe.csv";

        #endregion
    }
}
