using System;
using System.Threading.Tasks;

namespace ApplicationTools
{
    /// <summary>
    /// Objet permettant la gestion des messages de la Console
    /// </summary>
    internal class ConsoleQueue : IConsoleQueue
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        internal ConsoleQueue()
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="messageQueuedFunc"></param>
        public void Initialise(Func<string, string, string, bool> messageQueuedFunc)
        {
            this.messageQueuedFunc = messageQueuedFunc;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="objet"></param>
        /// <param name="commande"></param>
        /// <param name="retour"></param>
        public void AddConsoleMessage(string objet, string commande, string retour)
        {
            if (messageQueuedFunc != null)
                Task.Factory.StartNew(() => messageQueuedFunc(objet, commande, retour));
        }

        #endregion

        #region Champs

        /// <summary>
        /// Fonction appelée sur ajout de message dans la queue
        /// </summary>
        private Func<string, string, string, bool> messageQueuedFunc = null;

        #endregion
    }
}
