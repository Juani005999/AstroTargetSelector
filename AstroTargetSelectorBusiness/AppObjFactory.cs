using ApplicationTools;
using System.Runtime.CompilerServices;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Fabrique d'objets Business (métier) et Logic (applicatif)
    /// </summary>
    public class AppObjFactory : AppToolFactory
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AppObjFactory()
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public AppTarget GetAppTarget()
        {
            if (appTarget == null)
                appTarget = new AppTarget(this);
            return appTarget;
        }

        /// <summary>
        /// Renvoi l'objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public AppInputs GetAppInputs()
        {
            if (appInputs == null)
                appInputs = new AppInputs(this);
            return appInputs;
        }

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Capteurs
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public AppCapteur GetAppCapteur()
        {
            if (appCapteur == null)
                appCapteur = new AppCapteur(this);
            return appCapteur;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// </summary>
        private AppTarget appTarget = null;

        /// <summary>
        /// Objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
        /// </summary>
        private AppInputs appInputs = null;

        /// <summary>
        /// Objet applicatif permettant d'accéder à la collection des Capteurs
        /// </summary>
        private AppCapteur appCapteur = null;

        #endregion
    }
}
