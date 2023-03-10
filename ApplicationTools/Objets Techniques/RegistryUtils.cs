using System;
using Microsoft.Win32;

namespace ApplicationTools
{
    /// <summary>
    /// Boîte à outil de gestion de la Registry
    /// </summary>
    internal static class RegistryUtils
    {
        #region Méthodes internal/public

        /// <summary>
        /// Permet de savoir si un programme est installé sur le poste local
        /// </summary>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        internal static bool IsProgramInstalled(string programDisplayName, IAppLog appLog)
        {
            try
            {
                // Trace
                appLog.Log($"Vérification de l'installation du programme : {programDisplayName}", "RegistryUtils");

                return GetUninstallProgramKey(programDisplayName, appLog) != null;
            }
            catch (Exception err)
            {
                // On trace l'erreur
                appLog.LogException(err, "RegistryUtils");
                return false;
            }
        }

        /// <summary>
        /// Renvoi le champ DisplayVersion d'un programme installé sur le poste local
        /// </summary>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        internal static string GetDisplayVersion(string programDisplayName, IAppLog appLog)
        {
            try
            {
                string version = string.Empty;

                // Trace
                appLog.Log($"Lecture du champ DisplayVersion du programme : {programDisplayName}", "RegistryUtils");

                RegistryKey returnKey = GetUninstallProgramKey(programDisplayName, appLog);
                if (returnKey != null)
                    version = returnKey.GetValue("DisplayVersion") as string;

                // Trace et retour
                if (returnKey != null && !string.IsNullOrEmpty(version))
                    appLog.Log($"Programme {programDisplayName} installé en version {version}", "RegistryUtils");
                return version;
            }
            catch (Exception err)
            {
                // On trace l'erreur
                appLog.LogException(err, "RegistryUtils");
                return string.Empty;
            }
        }

        /// <summary>
        /// Renvoi le champ InstallLocation d'un programme installé sur le poste local
        /// </summary>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        internal static string GetInstallLocation(string programDisplayName, IAppLog appLog)
        {
            try
            {
                string location = string.Empty;

                // Trace
                appLog.Log($"Lecture du champ InstallLocation du programme : {programDisplayName}", "RegistryUtils");

                RegistryKey returnKey = GetUninstallProgramKey(programDisplayName, appLog);
                if (returnKey != null)
                    location = returnKey.GetValue("InstallLocation") as string;

                // Trace et retour
                if (returnKey != null && !string.IsNullOrEmpty(location))
                    appLog.Log($"Programme {programDisplayName} installé dans le répertoire {location}", "RegistryUtils");
                return location;
            }
            catch (Exception err)
            {
                // On trace l'erreur
                appLog.LogException(err, "RegistryUtils");
                return string.Empty;
            }
        }

        /// <summary>
        /// Renvoi le champ InstallLocation d'un programme installé sur le poste local
        /// </summary>
        /// <param name="programManufacturer">Clé Manufacturer dans la registry</param>
        /// <param name="programDisplayName">Clé DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        internal static string GetInstallLocation(string programManufacturer, string programDisplayName, IAppLog appLog)
        {
            try
            {
                string location = string.Empty;

                // Trace
                appLog.Log($"Lecture du champ InstallLocation du programme : {programDisplayName}", "RegistryUtils");

                RegistryKey returnKey = GetNodeProgramKey(programManufacturer, programDisplayName, appLog);
                if (returnKey != null)
                    location = returnKey.GetValue("InstallLocation") as string;

                // Trace et retour
                if (returnKey != null && !string.IsNullOrEmpty(location))
                    appLog.Log($"Programme {programDisplayName} installé dans le répertoire {location}", "RegistryUtils");
                return location;
            }
            catch (Exception err)
            {
                // On trace l'erreur
                appLog.LogException(err, "RegistryUtils");
                return string.Empty;
            }
        }

        /// <summary>
        /// Renvoi la valeur d'une clé dans la clé root HKEY_CURRENT_USER
        /// </summary>
        /// <param name="keyPath">Chemin de la clé dans la registry</param>
        /// <param name="keyName">Nom de la clé dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        internal static string GetCurrentUserValue(string keyPath, string keyName, IAppLog appLog)
        {
            try
            {
                // Trace
                appLog.Log($"Lecture dans HKEY_CURRENT_USER de la valeur de la clé : {keyPath}\\{keyName}", "RegistryUtils");

                // Clé root HKEY_CURRENT_USER
                RegistryKey keyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
                if (keyCurrentUser != null)
                {
                    // Sous clé et valeur
                    RegistryKey subKeyUninstall = keyCurrentUser.OpenSubKey(keyPath);
                    if (subKeyUninstall != null)
                        return subKeyUninstall.GetValue(keyName) as string; ;
                }
                return string.Empty;
            }
            catch(Exception err)
            {
                // On trace l'erreur
                appLog.LogException(err, "RegistryUtils");
                return string.Empty;
            }
        }

        /// <summary>
        /// Renvoi la sous-clé uninstall d'un programme
        /// </summary>
        /// <param name="programManufacturer">Champ DisplayName dans la registry</param>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        internal static RegistryKey GetNodeProgramKey(string programManufacturer, string programDisplayName, IAppLog appLog)
        {
            // Cle Retour
            RegistryKey keyRetour = null;

            // Trace
            appLog.Log($"Recherche de la clé d'installation du programme : {programDisplayName}", "RegistryUtils");

            // On parcours la registry sur la clé root LocalMachine32
            RegistryKey keyLocalMachine32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            if ((keyRetour = GetUninstallProgramInSoftwareKey(keyLocalMachine32, programManufacturer, programDisplayName, appLog)) != null)
                return keyRetour;

            // On parcours la registry sur la clé root LocalMachine64
            RegistryKey keyLocalMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            if ((keyRetour = GetUninstallProgramInSoftwareKey(keyLocalMachine64, programManufacturer, programDisplayName, appLog)) != null)
                return keyRetour;

            // Trace et retour
            appLog.Log($"Programme {programManufacturer} / {programDisplayName} NON installé sur le poste", "RegistryUtils");
            return keyRetour;
        }

        #endregion

        #region Méthodes private

        /// <summary>
        /// Renvoi la sous-clé uninstall d'un programme
        /// </summary>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        private static RegistryKey GetUninstallProgramKey(string programDisplayName, IAppLog appLog)
        {
            // Cle Retour
            RegistryKey keyRetour = null;

            // Trace
            appLog.Log($"Recherche de la clé d'installation du programme : {programDisplayName}", "RegistryUtils");

            // On parcours la registry sur la clé root CurrentUser
            RegistryKey keyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            if ((keyRetour = GetUninstallProgramInRootKey(keyCurrentUser, programDisplayName, appLog)) != null)
                return keyRetour;

            // On parcours la registry sur la clé root LocalMachine32
            RegistryKey keyLocalMachine32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            if ((keyRetour = GetUninstallProgramInRootKey(keyLocalMachine32, programDisplayName, appLog)) != null)
                return keyRetour;

            // On parcours la registry sur la clé root LocalMachine64
            RegistryKey keyLocalMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            if ((keyRetour = GetUninstallProgramInRootKey(keyLocalMachine64, programDisplayName, appLog)) != null)
                return keyRetour;

            // Trace et retour
            appLog.Log($"Programme {programDisplayName} NON installé sur le poste", "RegistryUtils");
            return keyRetour;
        }

        /// <summary>
        /// Renvoi la sous-clé uninstall d'un programme pour la clé root spécifiée
        /// </summary>
        /// <param name="rootKey"></param>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        private static RegistryKey GetUninstallProgramInRootKey(RegistryKey rootKey, string programDisplayName, IAppLog appLog)
        {
            // Cle Retour
            RegistryKey keyRetour = null;

            // On vérifie la clé root
            if (rootKey != null)
            {
                // On récupère la clé des uninstall
                RegistryKey subKeyUninstall = rootKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (subKeyUninstall != null)
                {
                    // Trace
                    appLog.Log($"Parcours des clés de {subKeyUninstall.Name}", "RegistryUtils");

                    // On parcours les sous-clés
                    foreach (String keyName in subKeyUninstall.GetSubKeyNames())
                    {
                        RegistryKey subkey = subKeyUninstall.OpenSubKey(keyName);
                        string displayName = subkey.GetValue("DisplayName") as string;
                        if (!string.IsNullOrEmpty(displayName) && displayName.Contains(programDisplayName))
                        {
                            appLog.Log($"Programme {programDisplayName} installé sur le poste.", "RegistryUtils");
                            return subkey;
                        }
                    }
                }
            }
            return keyRetour;
        }

        /// <summary>
        /// Renvoi la sous-clé uninstall d'un programme pour la clé root spécifiée
        /// </summary>
        /// <param name="rootKey"></param>
        /// <param name="programManufacturer">Champ DisplayName dans la registry</param>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="appLog">Instance de l'objet de log en cours</param>
        /// <returns></returns>
        private static RegistryKey GetUninstallProgramInSoftwareKey(RegistryKey rootKey, string programManufacturer, string programDisplayName, IAppLog appLog)
        {
            // Cle Retour
            RegistryKey keyRetour = null;

            // On vérifie la clé root
            if (rootKey != null)
            {
                // On récupère la clé des uninstall
                string uninstallKey = @"SOFTWARE\" + programManufacturer;
                if (!string.IsNullOrEmpty(programDisplayName))
                    uninstallKey += @"\" + programDisplayName;
                RegistryKey subKeyUninstall = rootKey.OpenSubKey(uninstallKey);
                if (subKeyUninstall != null)
                {
                    return subKeyUninstall;
                }
            }
            return keyRetour;
        }

        #endregion
    }
}
