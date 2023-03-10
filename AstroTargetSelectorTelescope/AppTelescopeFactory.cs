using ApplicationTools;

namespace AstroTargetSelectorTelescope
{
    /// <summary>
    /// Fabrique d'objets Technique pour les télescope ASCOM
    /// </summary>
    public class AppTelescopeFactory : IAppTelescopeFactory
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AppTelescopeFactory(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IASCOMTelescope GetIASCOMTelescope()
        {
            if (ascomTelescope == null)
                ascomTelescope = new ASCOMTelescope(appToolFactory);
            return ascomTelescope;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Interface de l'Objet applicatif permettant la gestion du Télescope ASCOM
        /// </summary>
        private IASCOMTelescope ascomTelescope = null;

        #endregion
    }
}
