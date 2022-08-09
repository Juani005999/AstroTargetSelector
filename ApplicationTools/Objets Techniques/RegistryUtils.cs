using Microsoft.Win32;
using System;

namespace ApplicationTools
{
    /// <summary>
    /// Boîte à outil de gestion de la Registry
    /// </summary>
    internal static class RegistryUtils
    {
        /// <summary>
        /// Permet de savoir si un programme est installé sur le poste local
        /// </summary>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="programFileName">Nom complet du fichier exécutable</param>
        /// <param name="factory">Instance de la fabrique d'objet technique en cours</param>
        /// <returns></returns>
        internal static bool IsProgramInstalled(string programDisplayName, out string programFileName, AppToolFactory factory)
        {
            // Trace
            factory.GetLog().Log($"Vérification de l'installation du programme : {programDisplayName}");
            programFileName = string.Empty;

            // On parcours la registry sur la clé root CurrentUser
            RegistryKey keyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            if (IsProgramInKey(keyCurrentUser, programDisplayName, out programFileName, factory))
            {
                return true;
            }

            // On parcours la registry sur la clé root LocalMachine32
            RegistryKey keyLocalMachine32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            if (IsProgramInKey(keyLocalMachine32, programDisplayName, out programFileName, factory))
                return true;

            // On parcours la registry sur la clé root LocalMachine64
            RegistryKey keyLocalMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            if (IsProgramInKey(keyLocalMachine64, programDisplayName, out programFileName, factory))
                return true;

            // Trace et retour
            factory.GetLog().Log($"Programme {programDisplayName} NON installé sur le poste");
            return false;
        }

        /// <summary>
        /// Permet de savoir si un programme est présent dans les sou-clés (uninstall) de la clé root
        /// </summary>
        /// <param name="key"></param>
        /// <param name="programDisplayName"></param>
        /// <param name="programFileName"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        private static bool IsProgramInKey(RegistryKey key, string programDisplayName, out string programFileName, AppToolFactory factory)
        {
            programFileName = string.Empty;
            if (key != null)
            {
                // On récupère la clé des uninstall
                RegistryKey subKey = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (subKey != null)
                {
                    // Trace
                    factory.GetLog().Log($"Parcours des clés de {subKey.Name}");

                    // On parcours les sous-clés
                    foreach (String keyName in subKey.GetSubKeyNames())
                    {
                        RegistryKey subkey = subKey.OpenSubKey(keyName);
                        string displayName = subkey.GetValue("DisplayName") as string;
                        if (!string.IsNullOrEmpty(displayName) && displayName.Contains(programDisplayName))
                        {
                            programFileName = subkey.GetValue("InstallLocation") as string;
                            programFileName += "Stellarium.exe";
                            string programVersion = subkey.GetValue("DisplayVersion") as string;
                            factory.GetLog().Log($"Programme {programDisplayName} [{programVersion}] installé sur le poste. {programFileName}");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
