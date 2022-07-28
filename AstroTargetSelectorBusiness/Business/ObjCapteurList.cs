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
    public class ObjCapteurList
    {
        #region Propriétés

        /// <summary>
        /// Liste d'objets <see cref="ObjCapteur"/>
        /// </summary>
        internal List<ObjCapteur> ListeObjCapteur
        {
            get
            {
                if (listeObjCapteur == null)
                {
                    listeObjCapteur = new List<ObjCapteur>();
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
        internal bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration
        /// </summary>
        internal string CapteurListeFullPathFile
        {
            get
            {
                return factory.GetAppContext().UserAppDataPath + "\\" + CapteurListeFileName;
            }
        }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        internal string CapteurListeFileName
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
        internal ObjCapteurList(AppObjFactory factory)
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
                factory.GetLog().Log($"Rechargement de la liste des capteurs depuis le fichier de configuration", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On flush le flag de rechargement forcé
                ForceUpdateListe = false;

                // Clear de la liste actuelle
                listeObjCapteur.Clear();

                // TODO : Si le fichier de configuration n'existe pas sur le poste, on le télécharge ?

                // Lecture du fichier de configuration et ajout dans la liste
                factory.GetLog().Log($"Fichier de configuration contenant la liste des capteurs : {CapteurListeFullPathFile}", GetType().Name);
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
                            listeObjCapteur.Add(new ObjCapteur(factory)
                            {
                                Nom = values[0],
                                Largeur = Convert.ToDecimal(values[1])
                            });
                        }
                    }
                }
                else
                    factory.GetLog().Log($"Fichier de configuration contenant la liste des capteurs manquant. Aucun capteur chargé", GetType().Name, null, AppLog.TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Chargement de {listeObjCapteur.Count} capteurs en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
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
        /// Liste d'objets <see cref="ObjCapteur"/>
        /// </summary>
        private List<ObjCapteur> listeObjCapteur = null;

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        private const string capteurListeFileName = "CapteurListe.csv";

        #endregion
    }
}
