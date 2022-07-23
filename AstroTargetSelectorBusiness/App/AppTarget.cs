using AstroTargetSelectorBusiness.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        public ObjTargetList Targets
        {
            get
            {
                if (targets == null)
                    targets = new ObjTargetList(factory);
                return targets;
            }
        }

        /// <summary>
        /// Renvoi la liste distinctes des différents type d'objet céleste
        /// </summary>
        public List<string> ListeType
        {
            get
            {
                // Trace et Chrono
                factory.GetLog().Log($"Rechargement de la liste des Type de targets depuis la liste des Targets", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Clear de la liste en cours
                listeType.Clear();

                // On ajoute l'éléments "Tous"
                listeType.Add(Resources.Tous);

                // On parcours la liste des targets groupée par Type
                foreach (var group in targets.ListeObjTarget.GroupBy(target => target.Type))
                {
                    listeType.Add(group.Key);
                }

                // Trace et retour
                factory.GetLog().Log($"Chargement de {listeType.Count - 1} type en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                return listeType;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppTarget(AppObjFactory factory)
        {
            this.factory = factory;
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
        /// Liste distinctes des différents type d'objet céleste
        /// </summary>
        private List<string> listeType = new List<string>();

        #endregion
    }
}
