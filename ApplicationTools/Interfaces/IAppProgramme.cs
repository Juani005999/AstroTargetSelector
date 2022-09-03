
using ApplicationTools.Properties;
using System;

namespace ApplicationTools
{
    /// <summary>
    /// Interface de l'Objet représentant un logiciel
    /// </summary>
    public interface IAppProgramme
    {
        #region Propriétés

        /// <summary>
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// TimeOut (en s) pour le démarage de l'application
        /// </summary>
        int StartTimeout { get; }

        /// <summary>
        /// Nom de fichier du Logiciel sur le poste
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        string ProcessName { get; }

        /// <summary>
        /// Singleton permettant de savoir si le programme est installé sur le poste
        /// </summary>
        bool IsInstalled { get; }

        /// <summary>
        /// Permet de savoir si le programme est en cours d'exécution sur le poste
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Version du Logiciel sur le poste
        /// </summary>
        string FileVersion { get; }

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        string InstallLocation { get; }

        /// <summary>
        /// Fichier exécutable du Logiciel sur le poste
        /// </summary>
        string ExecutableFile { get; }

        /// <summary>
        /// Serveur
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Port du Serveur Stellarium
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        string Port { get; set; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode permettant le positionnement de la sélection dans Stellarium
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande Stellarium</para>
        /// </summary>
        /// <param name="nomTarget"></param>
        /// <param name="dateObservation"></param>
        /// <exception cref="Exception">Exception survenue lors du traitement</exception>
        void FocusTo(string nomTarget, DateTime dateObservation);

        #endregion
    }
}
