using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace ApplicationTools
{
    /// <summary>
    /// Objet Log
    /// </summary>
    public class AppToolLog
    {
        #region Enum

        /// <summary>
        /// Type de trace
        /// </summary>
        public enum TypeLog
        {
            /// <summary>
            /// Trace de type Infos
            /// </summary>
            Infos,

            /// <summary>
            /// Trace de type Warning
            /// </summary>
            Warning,

            /// <summary>
            /// Trace de type Error
            /// </summary>
            Error,

            /// <summary>
            /// Trace de type Fatal
            /// </summary>
            Fatal
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Path complet et Nom du fichier de log
        /// </summary>
        public string FullPathName
        {
            get
            {
                return toolFactory.GetAppContext().StartupPath + "\\" + toolFactory.GetAppContext().LogFileName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppToolLog(AppToolFactory toolFactory)
        {
            this.toolFactory = toolFactory;

            // Initialisation de l'objet de log
            Initialise();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet l'initialisation de l'objet de Log en fonction du Contexte applicatif
        /// </summary>
        public void Initialise()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Ouverture du fichier de log pour écraser le contenu ancien
                StreamWriter fichierLog = new StreamWriter(FullPathName);
                fichierLog.Close();

                // Initialisation du fichier de log
                InitialiseFichierLog();

                // Trace du Nom de l'application appelante et du répertoire de démarrage (fichier de log)
                LogInfos($"Initialisation de l'objet de log", GetType().Name, debutInitialisation.ElapsedMilliseconds);
                LogInfos($"Log FullPathName : {FullPathName}", GetType().Name);
            }
            catch (Exception err)
            {
                Console.WriteLine("Une erreur est survenue lors de l'initialisation l'objet de log : " + err.Message);
            }
        }

        /// <summary>
        /// Trace de type Info
        /// </summary>
        /// <param name="message"></param>
        public void LogInfos(string message,
                        string windowName = "",
                        double? millisecond = null,
                        [CallerMemberName] string memberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            callerWindowName = windowName;
            callerMemberName = memberName;
            callerFilePath = sourceFilePath;
            callerLineNumber = sourceLineNumber;
            
            log(TypeLog.Infos, message, millisecond);
        }

        /// <summary>
        /// Trace de type Warning
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message,
                        string windowName = "",
                        double? millisecond = null,
                        [CallerMemberName] string memberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            callerWindowName = windowName;
            callerMemberName = memberName;
            callerFilePath = sourceFilePath;
            callerLineNumber = sourceLineNumber;

            log(TypeLog.Warning, message, millisecond);
        }

        /// <summary>
        /// Trace de type Erreur
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message,
                        string windowName = "",
                        double? millisecond = null,
                        [CallerMemberName] string memberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            callerWindowName = windowName;
            callerMemberName = memberName;
            callerFilePath = sourceFilePath;
            callerLineNumber = sourceLineNumber;
            
            log(TypeLog.Error, message, millisecond);
        }

        /// <summary>
        /// Trace de type Erreur
        /// </summary>
        /// <param name="err">Erreur</param>
        public void LogError(Exception err,
                        string windowName = "",
                        double? millisecond = null,
                        [CallerMemberName] string memberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            callerWindowName = windowName;
            callerMemberName = memberName;
            callerFilePath = sourceFilePath;
            callerLineNumber = sourceLineNumber;
            
            log(TypeLog.Error, err.Message, millisecond);
        }

        /// <summary>
        /// Trace en console et dans le fichier de log
        /// </summary>
        /// <param name="typeLog"></param>
        /// <param name="message"></param>
        private void log(TypeLog typeLog,
                        string message,
                        double? millisecond = null)
        {
            try
            {
                // Trace en console
                Console.WriteLine(toolFactory.GetAppContext().ProductName + " (" + typeLog.ToString() + ") : " + message);

                // Trace dans le fichier de log
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();
                string chaineFinale = toolFactory.GetAppContext().ProductName;
                chaineFinale += ";" + callerWindowName;

                chaineFinale += ";" + callerMemberName;
                chaineFinale += ";" + callerFilePath;
                chaineFinale += ";" + callerLineNumber;
                
                chaineFinale += ";" + typeLog.ToString().ToUpper();
                chaineFinale += ";" + DateTime.Now.ToString("dd/MM/yyyy");
                chaineFinale += ";" + DateTime.Now.ToString("HH:mm:ss.ffff");
                if (millisecond.HasValue)
                    chaineFinale += ";" + millisecond.ToString();
                else
                    chaineFinale += ";";
                chaineFinale += ";" + message;

                // Ouverture du fichier de log et écriture de la trace
                StreamWriter fichierLog = new StreamWriter(FullPathName, true);
                fichierLog.WriteLine(chaineFinale);
                fichierLog.Close();
                Console.WriteLine("Trace dans Fichier de log en " + debutFonction.ElapsedMilliseconds + " ms");
            }
            catch (Exception err)
            {
                Console.WriteLine("Une erreur est survenue dans l'objet de log : " + err.Message);
            }
        }

        /// <summary>
        /// Permet la création des entêtes de colonnes dans le fichier de log
        /// </summary>
        private void InitialiseFichierLog()
        {
            try
            {
                // Création de la chaine (en-tête du tableau du fichier de log)
                string chaineFinale = "Produit";
                chaineFinale += ";Objet";
                chaineFinale += ";Fonction";
                chaineFinale += ";Fichier source";
                chaineFinale += ";Ligne";
                chaineFinale += ";Type";
                chaineFinale += ";Date";
                chaineFinale += ";Heure";
                chaineFinale += ";Durée";
                chaineFinale += ";Message";

                StreamWriter fichierLog = new StreamWriter(FullPathName, true);
                fichierLog.WriteLine(chaineFinale);
                fichierLog.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine("Une erreur est survenue dans l'objet de log : " + err.Message);
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la ToolFactory en cours
        /// </summary>
        private readonly AppToolFactory toolFactory = null;

        /// <summary>
        /// Nom de la page appelante
        /// </summary>
        private string callerWindowName = string.Empty;

        /// <summary>
        /// Nom de la méthode appelante
        /// </summary>
        private string callerMemberName = string.Empty;

        /// <summary>
        /// Nom du fichier de la fonction appelante
        /// </summary>
        private string callerFilePath = string.Empty;

        /// <summary>
        /// Numéro de ligne de l'appelant
        /// </summary>
        private int callerLineNumber = 0;

        #endregion
    }
}
