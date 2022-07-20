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
                Log($"Initialisation de l'objet de log", GetType().Name);
                Log($"Log FullPathName : {FullPathName}", GetType().Name);
            }
            catch (Exception err)
            {
                Console.WriteLine("Une erreur est survenue lors de l'initialisation l'objet de log : " + err.Message);
            }
        }

        /// <summary>
        /// Trace en console et dans le fichier de log
        /// </summary>
        /// <param name="message">Trace</param>
        /// <param name="callerClassName">Classe appelante</param>
        /// <param name="millisecond">Durée en ms</param>
        /// <param name="typeLog">Type de trace <see cref="TypeLog"/></param>
        /// <param name="callerModuleName">Module appelant : par défaut System.Reflection.Assembly.GetCallingAssembly().GetName().Name</param>
        /// <param name="callerMemberName">Fonction appelante</param>
        /// <param name="callerFilePath">Fichier contenant la fonction appelante</param>
        /// <param name="callerLineNumber">Ligne dans le fichier de l'appelant</param>
        public void Log(string message,
                        string callerClassName = "",
                        double? millisecond = null,
                        TypeLog typeLog = TypeLog.Infos,
                        string callerModuleName = "",
                        [CallerMemberName] string callerMemberName = "",
                        [CallerFilePath] string callerFilePath = "",
                        [CallerLineNumber] int callerLineNumber = 0)
        {
            try
            {
                // Trace en console
                Console.WriteLine(toolFactory.GetAppContext().ProductName + " (" + typeLog.ToString() + ") : " + message);

                // Trace dans le fichier de log
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();
                string chaineFinale = toolFactory.GetAppContext().ProductName;
                chaineFinale += ";";
                chaineFinale += string.IsNullOrEmpty(callerModuleName) ? System.Reflection.Assembly.GetCallingAssembly().GetName().Name : callerModuleName;
                chaineFinale += ";" + callerClassName;

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
        /// Trace une Exception en console et dans le fichier de log
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="callerClassName">Classe appelante</param>
        /// <param name="typeLog">Type de trace <see cref="TypeLog"/></param>
        /// <param name="callerMemberName">Fonction appelante</param>
        /// <param name="callerFilePath">Fichier contenant la fonction appelante</param>
        /// <param name="callerLineNumber">Ligne dans le fichier de l'appelant</param>
        public void LogException(Exception ex,
                        string callerClassName = "",
                        TypeLog typeLog = TypeLog.Error,
                        [CallerMemberName] string callerMemberName = "",
                        [CallerFilePath] string callerFilePath = "",
                        [CallerLineNumber] int callerLineNumber = 0)
        {
            try
            {
                // Appel de la fonction de base
                Log(ex.Message, callerClassName, null, typeLog, System.Reflection.Assembly.GetCallingAssembly().GetName().Name, callerMemberName, callerFilePath, callerLineNumber);
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
                chaineFinale += ";Module";
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

        #endregion
    }
}
