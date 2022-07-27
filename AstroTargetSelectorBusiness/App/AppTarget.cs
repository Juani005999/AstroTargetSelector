using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
    /// </summary>
    public class AppTarget
    {
        #region Propriétés

        /// <summary>
        /// Liste des Targets
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé et la liste est chargée depuis le fichier des paramètres</para>
        /// </summary>
        internal ObjTargetList Targets
        {
            get
            {
                if (targets == null)
                    targets = new ObjTargetList(factory);
                return targets;
            }
        }

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        public List<ObjTarget> ListeObjTarget
        {
            get
            {
                // Trace et Chrono
                //factory.GetLog().Log($"Rechargement de la liste des Targets depuis l'objet Métier", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On force le rechargement de la liste si nécessaire
                Targets.ForceUpdateListe = ForceUpdateListe;

                // Appel à la liste chargée depuis l'objet métier
                listeObjTarget = Targets.ListeObjTarget.ToList();

                // Flush du flag permettant de forcer le rechargement de la liste depuis le fichier de configuration
                ForceUpdateListe = false;

                // Filtre sur Nom / Description
                if (!string.IsNullOrEmpty(FiltreNomDescription))
                    listeObjTarget = listeObjTarget.Where(t => t.Nom.ToLower().Contains(FiltreNomDescription.ToLower())
                                                                || t.Description.ToLower().Contains(FiltreNomDescription.ToLower())).ToList();

                // Filtre sur Type
                if (!string.IsNullOrEmpty(FiltreType) && FiltreType != Resources.Tous)
                    listeObjTarget = listeObjTarget.Where(t => t.Type == FiltreType).ToList();

                // Filtre sur Rank
                if (!string.IsNullOrEmpty(FiltreRank) && FiltreRank != Resources.Tous)
                    listeObjTarget = listeObjTarget.Where(t => t.Rank >= Convert.ToDecimal(FiltreRank)).ToList();

                // Filtre sur Magnitude
                if (!string.IsNullOrEmpty(FiltreMagnitude) && FiltreMagnitude != Resources.Tous)
                    listeObjTarget = listeObjTarget.Where(t => t.Magnitude <= Convert.ToDecimal(FiltreMagnitude)).ToList();

                // TODO : Tri

                // Trace et retour
                //factory.GetLog().Log($"Chargement de {listeObjTarget.Count - 1} targets en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                return listeObjTarget;
            }
        }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjTarget"/></para>
        /// </summary>
        public bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Renvoi la liste distinctes des différents type d'objet céleste
        /// </summary>
        public List<string> ListeType
        {
            get
            {
                // Trace et Chrono
                //factory.GetLog().Log($"Rechargement de la liste des Type de targets depuis l'objet Métier", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Clear de la liste en cours
                listeType.Clear();

                // On ajoute l'éléments "Tous"
                listeType.Add(Resources.Tous);

                // On parcours la liste des targets groupée par Type
                foreach (var group in Targets.ListeObjTarget.Where(t => !t.EstExclu).GroupBy(target => target.Type))
                {
                    listeType.Add(group.Key);
                }

                // Trace et retour
                //factory.GetLog().Log($"Chargement de {listeType.Count - 1} type en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppTarget(AppObjFactory factory)
        {
            this.factory = factory;

            // Positionnement des valeurs par défaut
            FiltreType = ListeType.First();
            FiltreRank = factory.GetListeFiltreRank().First().Key;
            FiltreMagnitude = factory.GetListeFiltreMagnitude().First().Key;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi l'Objet céleste <see cref="ObjTarget"/> correspondant au nom passé en paramètre
        /// </summary>
        /// <param name="nomTarget">Nom de la target recherchée</param>
        /// <returns><see cref="ObjTarget"/>. Null si non trouvé ou si nomTarget est vide.</returns>
        public ObjTarget GetTarget(string nomTarget)
        {
            if (string.IsNullOrEmpty(nomTarget))
                return null;
            return Targets.ListeObjTarget.Where(t => t.Nom == nomTarget).FirstOrDefault();
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Liste des Targets
        /// </summary>
        private ObjTargetList targets = null;

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        private List<ObjTarget> listeObjTarget = null;

        /// <summary>
        /// Liste distinctes des différents type d'objet céleste
        /// </summary>
        private List<string> listeType = new List<string>();

        #endregion
    }
}
