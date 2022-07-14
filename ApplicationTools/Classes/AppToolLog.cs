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
                Log(TypeLog.Infos,
                    $"Initialisation de l'objet de log",
                    debutInitialisation.ElapsedMilliseconds,
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                    GetType().Name);
                Log(TypeLog.Infos,
                    $"Log FullPathName : {FullPathName}",
                    null,
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                    GetType().Name);
            }
            catch (Exception err)
            {
                Console.WriteLine("Une erreur est survenue lors de l'initialisation l'objet de log : " + err.Message);
            }
        }

        /// <summary>
        /// Trace en console et dans le fichier de log
        /// </summary>
        /// <param name="typeLog"></param>
        /// <param name="message"></param>
        public void Log(TypeLog typeLog,
                        string message,
                        double? millisecond = null,
                        string callerModuleName = "",
                        string callerClassName = "",
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
                chaineFinale += ";" + callerModuleName;
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
