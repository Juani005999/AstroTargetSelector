namespace AstroTargetSelectorTelescope
{
    /// <summary>
    /// Interface de la Fabrique d'objets Technique pour les télescope ASCOM
    /// </summary>
    public interface IAppTelescopeFactory
    {
        /// <summary>
        /// Interface de l'Objet applicatif permettant la gestion du Télescope ASCOM
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        /// <returns></returns>
        IASCOMTelescope GetIASCOMTelescope();
    }
}