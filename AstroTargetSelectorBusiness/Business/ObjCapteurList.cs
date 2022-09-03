using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant une liste d'objet <see cref="ObjCapteur"/>
    /// </summary>
    public class ObjCapteurList : IObjCapteurList
    {
        #region Propriétés

        /// <summary>
        /// Liste d'objets <see cref="IObjCapteur"/>
        /// </summary>
        public List<IObjCapteur> ListeObjCapteur
        {
            get
            {
                if (listeObjCapteur == null)
                {
                    listeObjCapteur = new List<IObjCapteur>();
                    ForceUpdateListe = true;
                }
                if (ForceUpdateListe)
                    ChargementListe();
                return listeObjCapteur;
            }
        }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjCapteur"/></para>
        /// </summary>
        public bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration
        /// </summary>
        public string CapteurListeFullPathFile
        {
            get
            {
                return appToolFactory.GetAppContext().UserAppDataPath + "\\" + CapteurListeFileName;
            }
        }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        public string CapteurListeFileName
        {
            get
            {
                return capteurListeFileName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjCapteurList(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;
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
                appToolFactory.GetLog().Log($"Rechargement de la liste des capteurs depuis le fichier de configuration", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On flush le flag de rechargement forcé
                ForceUpdateListe = false;

                // Clear de la liste actuelle
                listeObjCapteur.Clear();

                // TODO : Si le fichier de configuration n'existe pas sur le poste, on le télécharge ?

                // Lecture du fichier de configuration et ajout dans la liste
                appToolFactory.GetLog().Log($"Fichier de configuration contenant la liste des capteurs : {CapteurListeFullPathFile}", GetType().Name);
                if (File.Exists(CapteurListeFullPathFile))
                {
                    using (var reader = new StreamReader(CapteurListeFullPathFile))
                    {
                        // On passe la ligne d'en-tête
                        var lineTitre = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split('\t');
                            listeObjCapteur.Add(new ObjCapteur()
                            {
                                Nom = values[0],
                                Largeur = Convert.ToDouble(values[1])
                            });
                        }
                    }
                }
                else
                    appToolFactory.GetLog().Log($"Fichier de configuration contenant la liste des capteurs manquant. Aucun capteur chargé", GetType().Name, null, TypeLog.Warning);

                // Trace
                appToolFactory.GetLog().Log($"Chargement de {listeObjCapteur.Count} capteurs en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
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
        /// Liste d'objets <see cref="IObjCapteur"/>
        /// </summary>
        private List<IObjCapteur> listeObjCapteur = null;

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        private const string capteurListeFileName = "CapteurListe.csv";

        #endregion
    }
}
