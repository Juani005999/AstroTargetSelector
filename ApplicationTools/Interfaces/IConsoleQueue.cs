using System;

namespace ApplicationTools
{
    /// <summary>
    /// Interface de l'objet permettant la gestion des messages de la Console
    /// </summary>
    public interface IConsoleQueue
    {
        /// <summary>
        /// Ajout d'un message à la console
        /// </summary>
        /// <param name="objet"></param>
        /// <param name="commande"></param>
        /// <param name="retour"></param>
        void AddConsoleMessage(string objet, string commande, string retour);

        /// <summary>
        /// Initialisation de la ConsoleQueue
        /// <para>Permet de définir la méthode delegate a appelé sur ajout d'un message</para>
        /// </summary>
        void Initialise(Func<string, string, string, bool> messageQueuedFunc);
    }
}