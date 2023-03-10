using System;
using System.Collections.Generic;
using ApplicationTools;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de la Fabrique d'objets Business (métier) et Logic (applicatif)
    /// </summary>
    public interface IAppObjFactory : IAppToolFactory
    {
        #region Méthodes

        /// <summary>
        /// Renvoi l'objet applicatif permettant la gestion du serveur UDP
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        IAppTCPServer GetAppTCPServer();

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        IAppTarget GetAppTarget();

        /// <summary>
        /// Renvoi l'objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        IAppInputs GetAppInputs();

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Capteurs
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        IAppCapteur GetAppCapteur();

        /// <summary>
        /// Renvoi la liste des intervalles de minutes possible pour les Slices de Target
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        Dictionary<string, string> GetListeMinuteIntervalle();

        /// <summary>
        /// Renvoi la liste des durées totales d'observation
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        Dictionary<string, string> GetListeTotalTimeSlice();

        /// <summary>
        /// Renvoi la liste des Filtres de Rank pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        Dictionary<string, string> GetListeFiltreRank();

        /// <summary>
        /// Renvoi la liste des Filtres de Magnitude pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        Dictionary<string, string> GetListeFiltreMagnitude();

        /// <summary>
        /// Renvoi la liste des Modes de Visualisation pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        Dictionary<ModeVisualisation, string> GetListeModeVisualisation();

        /// <summary>
        /// Renvoi une nouvelle instance de l'objet <see cref="IObjSliceSpan"/>
        /// </summary>
        /// <param name="parentTarget"></param>
        /// <param name="dateHeureDebut"></param>
        /// <param name="dateHeureFin"></param>
        /// <returns></returns>
        IObjSliceSpan GetObjSliceSpan(IObjTarget parentTarget, DateTime dateHeureDebut, DateTime dateHeureFin);

        /// <summary>
        /// Renvoi une nouvelle instance de l'objet <see cref="IObjMosaicRect"/>
        /// </summary>
        /// <returns></returns>
        IObjMosaicRect GetObjMosaicRect();

        /// <summary>
        /// Interface de l'Objet applicatif permettant la gestion du Télescope ASCOM
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        /// <returns></returns>
        IAppASCOMTelescope GetAppASCOMTelescope();

        #endregion
    }
}
