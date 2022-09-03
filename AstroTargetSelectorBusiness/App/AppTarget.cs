using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApplicationTools;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
    /// </summary>
    public class AppTarget : IAppTarget
    {
        #region Propriétés

        /// <summary>
        /// Liste des Targets
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé et la liste est chargée depuis le fichier des paramètres</para>
        /// </summary>
        public IObjTargetList Targets
        {
            get
            {
                if (targets == null)
                    targets = new ObjTargetList(appToolFactory, appInputs);
                return targets;
            }
        }

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        public List<IObjTarget> ListeObjTarget
        {
            get
            {
                // On force le rechargement de la liste et/ou des Slices si nécessaire
                Targets.ForceUpdateListe = ForceUpdateListe;
                Targets.ForceUpdateSlices = ForceUpdateSlices;

                // Appel à la liste chargée depuis l'objet métier
                listeObjTarget = Targets.ListeObjTarget.Where(t => !t.EstExclu).ToList();

                // Flush du flag permettant de forcer le rechargement de la liste depuis le fichier de configuration
                ForceUpdateListe = false;
                ForceUpdateSlices = false;

                // Filtre sur Nom / Description / Constellation
                if (!string.IsNullOrWhiteSpace(FiltreNomDescription))
                {
                    string filtre = FiltreNomDescription.Replace(" ", "").ToLower();
                    listeObjTarget = listeObjTarget.Where(t => t.Nom.Replace(" ", "").ToLower().Contains(filtre)
                                                                || t.Description.Replace(" ", "").ToLower().Contains(filtre)
                                                                || t.Constellation.Replace(" ", "").ToLower().Contains(filtre)).ToList();
                }

                // Filtre sur Type
                if (!string.IsNullOrEmpty(FiltreType) && FiltreType != Resources.Tous)
                    listeObjTarget = listeObjTarget.Where(t => t.Type == FiltreType).ToList();

                // Filtre sur Rank
                if (!string.IsNullOrEmpty(FiltreRank) && FiltreRank != Resources.Tous)
                    listeObjTarget = listeObjTarget.Where(t => t.Rank >= Convert.ToDouble(FiltreRank)).ToList();

                // Filtre sur Magnitude
                if (!string.IsNullOrEmpty(FiltreMagnitude) && FiltreMagnitude != Resources.Tous)
                    listeObjTarget = listeObjTarget.Where(t => t.Magnitude <= Convert.ToDouble(FiltreMagnitude)).ToList();

                // Retour
                return listeObjTarget;
            }
        }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjTarget"/></para>
        /// </summary>
        public bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Permet de forcer le rechargement des Slices
        /// </summary>
        public bool ForceUpdateSlices { get; set; }

        /// <summary>
        /// Renvoi la liste distinctes des différents type d'objet céleste
        /// </summary>
        public List<string> ListeType
        {
            get
            {
                // Clear de la liste en cours
                listeType.Clear();

                // On ajoute l'éléments "Tous"
                listeType.Add(Resources.Tous);

                // On parcours la liste des targets groupée par Type
                Targets.ForceUpdateListe = ForceUpdateListe;
                foreach (var group in Targets.ListeObjTarget.GroupBy(target => target.Type))
                {
                    listeType.Add(group.Key);
                }

                // Retour
                return listeType;
            }
        }

        /// <summary>
        /// Filtre sur Nom ou Description pour l'affichage dans la liste
        /// </summary>
        public string FiltreNomDescription { get; set; }

        /// <summary>
        /// Filtre sur Type pour l'affichage dans la liste
        /// </summary>
        public string FiltreType { get; set; }

        /// <summary>
        /// Filtre sur Rank pour l'affichage dans la liste
        /// </summary>
        public string FiltreRank { get; set; }

        /// <summary>
        /// Filtre sur Magnitude pour l'affichage dans la liste
        /// </summary>
        public string FiltreMagnitude { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration des objets célestes
        /// </summary>
        public string TargetListeFullPathFile
        {
            get
            {
                return Targets.TargetListeFullPathFile;
            }
        }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des objets céleste
        /// </summary>
        public string TargetListeFileName
        {
            get
            {
                return Targets.TargetListeFileName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppTarget(IAppToolFactory appToolFactory, IAppInputs appInputs)
        {
            this.appToolFactory = appToolFactory;
            this.appInputs = appInputs;

            // Positionnement des valeurs par défaut
            FiltreType = ListeType.First();
            FiltreRank = Resources.Tous;
            FiltreMagnitude = Resources.Tous;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi l'Objet céleste <see cref="ObjTarget"/> correspondant au nom passé en paramètre
        /// </summary>
        /// <param name="nomTarget">Nom de la target recherchée</param>
        /// <returns><see cref="ObjTarget"/>. Null si non trouvé ou si nomTarget est vide.</returns>
        public IObjTarget GetTarget(string nomTarget)
        {
            if (string.IsNullOrEmpty(nomTarget))
                return null;
            return Targets.ListeObjTarget.Where(t => t.Nom == nomTarget).FirstOrDefault();
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
        /// Liste des Targets
        /// </summary>
        private IObjTargetList targets = null;

        /// <summary>
        /// Liste d'objets <see cref="IObjTarget"/>
        /// </summary>
        private List<IObjTarget> listeObjTarget = null;

        /// <summary>
        /// Liste distinctes des différents type d'objet céleste
        /// </summary>
        private List<string> listeType = new List<string>();

        #endregion
    }
}
