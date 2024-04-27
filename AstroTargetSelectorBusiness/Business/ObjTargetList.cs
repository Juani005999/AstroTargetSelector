using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant une liste d'objet <see cref="ObjTarget"/>
    /// </summary>
    public class ObjTargetList : IObjTargetList
    {
        #region Constantes

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des objets céleste
        /// </summary>
        private const string targetListeFileName = "TargetListe.csv";

        #endregion

        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTarget> ListeObjTarget
        {
            get
            {
                // Création du singleton si nécessaire
                if (listeObjTarget == null)
                {
                    listeObjTarget = new List<IObjTarget>();
                    ForceUpdateListe = true;
                }
                
                // Rechargement depuis le fichier de configuration si nécessaire
                if (ForceUpdateListe)
                    ChargementListe().GetAwaiter().GetResult();

                // Rechargement des Slices si nécessaire
                if (ForceUpdateSlices)
                {
                    listeObjTarget.ForEach(t => t.ForceUpdateSlices = true);
                }
                ForceUpdateSlices = false;

                // On renvoi la liste filtrée les objets exclus pour prise en compte dans les filtre (Type)
                return listeObjTarget;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool ForceUpdateListe { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool ForceUpdateSlices { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string TargetListeFullPathFile
        {
            get
            {
                return Path.Combine(appToolFactory.GetAppContext().UserAppDataPath, TargetListeFileName);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string TargetListeFileName
        {
            get
            {
                return targetListeFileName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTargetList(IAppToolFactory appToolFactory, IAppInputs appInputs)
        {
            this.appToolFactory = appToolFactory;
            this.appInputs = appInputs;
            ForceUpdateListe = false;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet le chargement de la liste des objets céleste depuis le fichier de configuration
        /// </summary>
        private async Task ChargementListe()
        {
            try
            {
                // Trace et Chrono
                appToolFactory.GetLog().Log($"Rechargement de la liste des targets depuis le fichier de configuration", GetType().Name);
                Stopwatch chrono = Stopwatch.StartNew();

                // On flush le flag de rechargement forcé
                ForceUpdateListe = false;

                // Clear de la liste actuelle
                listeObjTarget.Clear();

                // Lecture du fichier de configuration et ajout dans la liste
                appToolFactory.GetLog().Log($"Fichier de configuration contenant la liste des objets céleste : {TargetListeFullPathFile}", GetType().Name);
                if (File.Exists(TargetListeFullPathFile))
                {
                    using (var reader = new StreamReader(TargetListeFullPathFile))
                    {
                        // On passe la ligne d'en-tête
                        var lineTitre = await reader.ReadLineAsync().ConfigureAwait(false);
                        while (!reader.EndOfStream)
                        {
                            var line = await reader.ReadLineAsync().ConfigureAwait(false);
                            var values = line.Split('\t');
                            if (values.Length > 9)
                            {
                                listeObjTarget.Add(new ObjTarget(appToolFactory, appInputs)
                                {
                                    Nom = values[0],
                                    Type = values[1],
                                    Description = values[2],
                                    Constellation = values[8],
                                    RA = appToolFactory.GetCoordinate(Convert.ToDouble(values[3].Replace(',', '.'), CultureInfo.InvariantCulture), CoordinatesType.RA),
                                    DEC = appToolFactory.GetCoordinate(Convert.ToDouble(values[4].Replace(',', '.'), CultureInfo.InvariantCulture), CoordinatesType.DEC),
                                    Magnitude = Convert.ToDouble(values[5].Replace(',', '.'), CultureInfo.InvariantCulture),
                                    GrandeurWidth = appToolFactory.GetCoordinate(Convert.ToDouble(values[6].Replace(',', '.'), CultureInfo.InvariantCulture), CoordinatesType.Degree),
                                    GrandeurHeight = appToolFactory.GetCoordinate(Convert.ToDouble(values[7].Replace(',', '.'), CultureInfo.InvariantCulture), CoordinatesType.Degree)
                                });
                            }
                        }
                    }
                }
                else
                    appToolFactory.GetLog().Log($"Fichier de configuration contenant la liste des objets céleste manquant. Aucun objet chargé", GetType().Name, null, TypeLog.Warning);

                // Trace
                appToolFactory.GetLog().Log($"Chargement de {listeObjTarget.Count} targets en {chrono.ElapsedMilliseconds} ms", GetType().Name, chrono.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                appToolFactory.GetLog().LogException(err, GetType().Name);
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
        /// Liste d'objets <see cref="IObjTarget"/>
        /// </summary>
        private List<IObjTarget> listeObjTarget = null;

        #endregion
    }
}
