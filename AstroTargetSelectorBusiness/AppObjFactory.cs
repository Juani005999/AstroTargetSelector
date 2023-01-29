using System.Collections.Generic;
using ApplicationTools;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Fabrique d'objets Business (métier) et Logic (applicatif)
    /// </summary>
    public class AppObjFactory : AppToolFactory, IAppObjFactory
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
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IAppTCPServer GetAppTCPServer()
        {
            if (appUDPServer == null)
                appUDPServer = new AppTCPServer(this, GetAppTarget(), GetAppInputs());
            return appUDPServer;
        }

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public IAppTarget GetAppTarget()
        {
            if (appTarget == null)
                appTarget = new AppTarget(this, GetAppInputs());
            return appTarget;
        }

        /// <summary>
        /// Renvoi l'objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public IAppInputs GetAppInputs()
        {
            if (appInputs == null)
                appInputs = new AppInputs(this, GetAppCapteur());
            return appInputs;
        }

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Capteurs
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public IAppCapteur GetAppCapteur()
        {
            if (appCapteur == null)
                appCapteur = new AppCapteur(this);
            return appCapteur;
        }

        /// <summary>
        /// Renvoi la liste des intervalles de minutes possible pour les Slices de Target
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public Dictionary<string, string> GetListeMinuteIntervalle()
        {
            // Si la liste n'existe pas, on la créer
            if (listeMinuteIntervalle == null)
            {
                listeMinuteIntervalle = new Dictionary<string, string>();
                listeMinuteIntervalle.Add("5", "5");
                listeMinuteIntervalle.Add("10", "10");
                listeMinuteIntervalle.Add("15", "15");
                listeMinuteIntervalle.Add("30", "30");
            }
            return listeMinuteIntervalle;
        }

        /// <summary>
        /// Renvoi la liste des durées totales d'observation
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public Dictionary<string, string> GetListeTotalTimeSlice()
        {
            // Si la liste n'existe pas, on la créer
            if (listeTotalTimeSlice == null)
            {
                listeTotalTimeSlice = new Dictionary<string, string>();
                listeTotalTimeSlice.Add("1", "1");
                listeTotalTimeSlice.Add("2", "2");
                listeTotalTimeSlice.Add("3", "3");
            }
            return listeTotalTimeSlice;
        }

        /// <summary>
        /// Renvoi la liste des Filtres de Rank pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public Dictionary<string, string> GetListeFiltreRank()
        {
            // Si la liste n'existe pas, on la créer
            if (listeFiltreRank == null)
            {
                listeFiltreRank = new Dictionary<string, string>();
                listeFiltreRank.Add(Resources.Tous, Resources.Tous);
                listeFiltreRank.Add("1", "1");
                listeFiltreRank.Add("2", "2");
                listeFiltreRank.Add("3", "3");
                listeFiltreRank.Add("4", "4");
                listeFiltreRank.Add("5", "5");
            }
            return listeFiltreRank;
        }

        /// <summary>
        /// Renvoi la liste des Filtres de Magnitude pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public Dictionary<string, string> GetListeFiltreMagnitude()
        {
            // Si la liste n'existe pas, on la créer
            if (listeFiltreMagnitude == null)
            {
                listeFiltreMagnitude = new Dictionary<string, string>();
                listeFiltreMagnitude.Add(Resources.Tous, Resources.Tous);
                listeFiltreMagnitude.Add("7", "7");
                listeFiltreMagnitude.Add("8", "8");
                listeFiltreMagnitude.Add("9", "9");
                listeFiltreMagnitude.Add("10", "10");
                listeFiltreMagnitude.Add("11", "11");
                listeFiltreMagnitude.Add("12", "12");
            }
            return listeFiltreMagnitude;
        }

        /// <summary>
        /// Renvoi la liste des Modes de Visualisation pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public Dictionary<ModeVisualisation, string> GetListeModeVisualisation()
        {
            // Si la liste n'existe pas, on la créer
            if (listeModeVisualisation == null)
            {
                listeModeVisualisation = new Dictionary<ModeVisualisation, string>();
                listeModeVisualisation.Add(ModeVisualisation.Horaire, Resources.Horaire);
                listeModeVisualisation.Add(ModeVisualisation.Nuits, Resources.Nuits);
                listeModeVisualisation.Add(ModeVisualisation.Mensuel, Resources.Mensuel);
                listeModeVisualisation.Add(ModeVisualisation.Annuel, Resources.Annuel);
            }
            return listeModeVisualisation;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Objet applicatif permettant la gestion du serveur UDP
        /// </summary>
        private IAppTCPServer appUDPServer = null;

        /// <summary>
        /// Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// </summary>
        private IAppTarget appTarget = null;

        /// <summary>
        /// Objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
        /// </summary>
        private IAppInputs appInputs = null;

        /// <summary>
        /// Objet applicatif permettant d'accéder à la collection des Capteurs
        /// </summary>
        private IAppCapteur appCapteur = null;

        /// <summary>
        /// Liste des intervalles de minutes possible pour les Slices de Target
        /// </summary>
        private Dictionary<string, string> listeMinuteIntervalle = null;

        /// <summary>
        /// Liste des durées totales d'observation
        /// </summary>
        private Dictionary<string, string> listeTotalTimeSlice = null;

        /// <summary>
        /// Liste des Filtres de Rank pour l'affichage dans la liste
        /// </summary>
        private Dictionary<string, string> listeFiltreRank = null;

        /// <summary>
        /// Liste des Filtres de Magnitude pour l'affichage dans la liste
        /// </summary>
        private Dictionary<string, string> listeFiltreMagnitude = null;

        /// <summary>
        /// Liste des Filtres de Modes de Visualisation pour l'affichage dans la liste
        /// </summary>
        private Dictionary<ModeVisualisation, string> listeModeVisualisation = null;

        #endregion
    }
}
