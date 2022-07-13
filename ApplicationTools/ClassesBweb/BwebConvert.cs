using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;

namespace ApplicationTools
{
    /// <summary>
    /// Classe centralisant l'ensemble des méthodes de conversion de l'application.
    /// </summary>
    public static class BwebConvert
    {
        #region Méthodes de Dictionary<string,string>
        /// <summary>
        /// Convertit un dictionnaire de chaînes en une chaîne au format query-string.
        /// Exemple : cle1=val1&amp;cle2=val2&amp;cle3=val3...
        /// </summary>
        /// <remarks>
        /// <para>
        /// La chaîne retournée ne contient pas le point d'interrogation initial.
        /// Par ailleurs, chaque valeur du dictionnaire y est URL-encodée par <see cref="HttpUtility.UrlEncode(string)"/>.
        /// </para>
        /// <para>Au vu du format de sortie, les caractères '=' et '&amp;' sont interdits dans les clés.
        /// La méthode génère une exception si une clé contient l'un de ces deux caractères.</para>
        /// </remarks>
        /// <param name="d">Dictionnaire de chaîne</param>
        /// <returns>Chaîne au format Query-string</returns>
        /// <seealso cref="Dictionary{TKey,TValue}"/>
        public static string DictionaryToQueryString(Dictionary<string, string> d)
        {
            if ((d == null) || (d.Count == 0)) return String.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> k in d)
            {
                string cle = k.Key;
                if (cle.Contains("=") || cle.Contains("&"))
                {
                    throw new ArgumentException("Cle invalide dans le dictionnaire : " + cle);
                }
                sb.AppendFormat("{0}={1}&", cle, HttpUtility.UrlEncode(k.Value));
            }
            return sb.ToString().TrimEnd('&');
        }

        /// <summary>
        /// Charge un dictionnaire de chaînes à partir d'une chaîne au format query-string.
        /// </summary>
        /// <remarks>
        /// Cette méthode retourne un dictionnaire de données construit à partir d'une chaîne crée
        /// préalablement par la méthode <see cref="DictionaryToQueryString"/>.
        /// </remarks>
        /// <param name="s">Chaîne au format Query-string</param>
        /// <returns>Dictionnaire de chaînes</returns>
        /// <exception cref="ArgumentException"/>
        /// <seealso cref="DictionaryToQueryString"/>
        public static Dictionary<string, string> DictionaryFromQueryString(string s)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(s)) return d;

            string[] keyValuePairs = s.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string kvp in keyValuePairs) 
            {
                string[] kv = kvp.Split('=');
                if (kv.Length != 2) throw new ArgumentException(String.Format("Parametre invalide [{0}] dans la chaine [{1}]", kvp, s));

                d.Add(kv[0], HttpUtility.UrlDecode(kv[1]));
            }
            return d;
        }

        /// <summary>
        /// Convertit un dictionnaire de chaînes en une chaîne au format JSON.
        /// Exemple : {"cle1":"val1", "cle2":"val2", ...}
        /// </summary>
        /// <remarks>
        /// Les caractères double-quote présent dans les valeurs sont echappés.
        /// </remarks>
        /// <param name="d">Dictionnaire de chaînes</param>
        /// <returns>Chaîne au format JSON.</returns>
        /// <seealso cref="Dictionary{TKey,TValue}"/>
        public static string DictionaryToJson(Dictionary<string, string> d)
        {
            if ((d == null) || (d.Count == 0)) return "{}";
            StringBuilder json = new StringBuilder("{");
            foreach (KeyValuePair<string, string> k in d)
            {
                json.AppendFormat("\"{0}\":\"{1}\",", k.Key, k.Value.Replace("\"", "\\\""));
            }
            return json.ToString().TrimEnd(',') + "}";
        }

        /// <summary>
        /// Convertit un dictionnaire de chaînes en une chaîne encodée en Base64-modifié.
        /// </summary>
        /// <remarks>
        /// Cette méthode convertit un dictionnaire de données en une chaîne encodée en Base64-modifié.
        /// <para>
        /// Cette conversion s'effectue en 2 étapes :
        /// <list type="bullet">
        /// <item><description>le dictionnaire est transformé en query string par <see cref="BwebConvert.DictionaryToQueryString"/></description></item>
        /// <item><description>le flux est encodé en Base64-modifié par <see cref="HttpServerUtility.UrlTokenEncode"/></description></item>
        /// </list>
        /// </para>
        /// <para>
        /// La méthode <see cref="HttpServerUtility.UrlTokenEncode"/> ne réalise pas un encodage strict en Base64, mais correspond 
        /// à une version spécifique qui modifie certains caractères, afin de pouvoir transiter sans problème dans les URLs.
        /// </para>
        /// </remarks>
        /// <param name="d">Dictionnaire de chaînes</param>
        /// <returns>Chaîne encodée en Base64-modifié.</returns>
        public static string DictionaryToEncodedString(Dictionary<string, string> d)
        {
            if ((d == null) || (d.Count == 0)) return "";

            byte[] b = Encoding.UTF8.GetBytes(DictionaryToQueryString(d));
            return HttpServerUtility.UrlTokenEncode(b);
        }

        /// <summary>
        /// Charge un dictionnaire de chaînes à partir d'une chaîne encodée en Base64-modifié.
        /// </summary>
        /// <remarks>
        /// Cette méthode retourne un dictionnaire de données construit à partir d'une chaîne encodée crée
        /// préalablement par la méthode <see cref="DictionaryToEncodedString"/>.
        /// </remarks>
        /// <param name="s">Chaîne encodée en Base64 (modifié).</param>
        /// <returns>Dictionnaire de chaînes</returns>
        /// <exception cref="ArgumentException"/>
        /// <seealso cref="DictionaryToEncodedString"/>
        public static Dictionary<string, string> DictionaryFromEncodedString(string s)
        {
            if (String.IsNullOrEmpty(s)) return new Dictionary<string, string>();

            byte[] b;
            try
            {
                b = HttpServerUtility.UrlTokenDecode(s);
            }
            catch (FormatException ex) 
            {
                throw new ArgumentException("Chaine entree invalide : " + s, ex);
            }
            if (b == null) 
            {
                throw new ArgumentException("Chaine entree invalide : " + s);
            }

            return DictionaryFromQueryString(Encoding.UTF8.GetString(b));
        }

        /// <summary>
        /// Convertit un dictionnaire de chaînes en une chaîne cryptée puis encodée en Base64-modifié.
        /// </summary>
        /// <remarks>
        /// Cette méthode convertit un dictionnaire de données en une chaîne cryptée puis encodée en Base64-modifié.
        /// <para>
        /// Cette conversion s'effectue en 3 étapes :
        /// <list type="bullet">
        /// <item><description>le dictionnaire est transformé en query string par <see cref="BwebConvert.DictionaryToQueryString"/></description></item>
        /// <item><description>le flux est encrypté par <see cref="BwebConvert.Encrypt"/></description></item>
        /// <item><description>le flux est encodé en Base64-modifié par <see cref="HttpServerUtility.UrlTokenEncode"/></description></item>
        /// </list>
        /// </para>
        /// <para>Le décryptage devra être effectué par la méthode <see cref="DictionaryFromEncryptedString"/>.</para>
        /// </remarks>
        /// <param name="d">Dictionnaire de chaînes</param>
        /// <returns>Chaîne encodée en Base64-modifié.</returns>
        public static string DictionaryToEncryptedString(Dictionary<string, string> d) 
        {
            if ((d == null) || (d.Count == 0)) return "";

            byte[] decrypted = Encoding.UTF8.GetBytes(DictionaryToQueryString(d));
            byte[] encrypted = Encrypt(decrypted);

            return HttpServerUtility.UrlTokenEncode(encrypted);
        }

        /// <summary>
        /// Charge un dictionnaire de chaînes à partir d'une chaîne encryptée.
        /// </summary>
        /// <remarks>
        /// Cette méthode retourne un dictionnaire de données construit à partir d'une chaîne encryptée crée
        /// préalablement par la méthode <see cref="DictionaryToEncryptedString"/>.
        /// </remarks>
        /// <param name="s">Chaîne encryptée</param>
        /// <returns>Dictionnaire de chaînes</returns>
        /// <exception cref="ArgumentException"/>
        /// <seealso cref="DictionaryToEncryptedString"/>
        public static Dictionary<string, string> DictionaryFromEncryptedString(string s) 
        {
            if (String.IsNullOrEmpty(s)) return new Dictionary<string, string>();

            byte[] encrypted;
            try
            {
                encrypted = HttpServerUtility.UrlTokenDecode(s);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Chaine entree invalide : " + s, ex);
            }
            if (encrypted == null)
            {
                throw new ArgumentException("Chaine entree invalide : " + s);
            }
            byte[] decrypted = Decrypt(encrypted);
            return DictionaryFromQueryString(Encoding.UTF8.GetString(decrypted));
        }
        #endregion

        #region Méthodes de Sérialisation et déserialisation binaires
        /// <summary>
        /// Sérialisation binaire d'un objet.
        /// </summary>
        /// <remarks>
        /// La méthode se base sur l'objet <see cref="BinaryFormatter"/> pour réaliser la sérialisation.
        /// Si l'objet passé en paramètre n'est pas sérialisable, la méthode génère une <see cref="System.Runtime.Serialization.SerializationException"/>.
        /// </remarks>
        /// <param name="o">Objet à sérialiser</param>
        /// <returns>Tableau d'octets contenant l'objet sérialisé</returns>
        public static byte[] Serialise(object o) 
        {
            byte[] b;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, o);
                b = stream.GetBuffer();
            }
            return b;
        }

        /// <summary>
        /// Déserialisation binaire d'un objet.
        /// </summary>
        /// <remarks>
        /// La méthode se base sur l'objet <see cref="BinaryFormatter"/> pour réaliser la désérialisation.
        /// Si la désérialisation échoue, la méthode génère une <see cref="System.Runtime.Serialization.SerializationException"/>.
        /// </remarks>
        /// <param name="b">Tableau d'octets contenant l'objet sérialisé</param>
        /// <returns>Objet désérialisé</returns>
        public static object Deserialise(byte[] b) 
        {
            object o;
            using (MemoryStream stream = new MemoryStream(b))
            {
                BinaryFormatter bf = new BinaryFormatter();
                o = bf.Deserialize(stream);
            }
            return o;
        }
        #endregion

        #region Méthodes de Cryptage et décryptage
        /// <summary>
        /// Encryptage d'un flux d'octet.
        /// </summary>
        /// <remarks>
        /// Cette méthode réalise un encryptage symétrique un flux d'octet en AES.
        /// <para>Le cryptage est effectué par un objet de type <see cref="AesManaged"/>.</para>
        /// <para>La clé de cryptage est retournée par la méthode <see cref="GetAesKey"/>.</para>
        /// <para>Le décryptage doit être effectué par la méthode <see cref="Decrypt"/>.</para>
        /// </remarks>
        /// <param name="buffer">Données à crypter</param>
        /// <returns>Données cryptées</returns>
        /// <seealso cref="AesManaged"/>
        public static byte[] Encrypt(byte[] buffer) 
        {
            byte[] output;
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = GetAesKey();
                aes.IV = GetAesIV();

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor();

                // Create the streams used for encryption. 
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(buffer, 0, buffer.Length);
                        csEncrypt.FlushFinalBlock();
                        output = ms.ToArray();
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// Décryptage d'un flux d'octet.
        /// </summary>
        /// <remarks>
        /// Cette méthode réalise un décryptage symétrique un flux d'octet en AES.
        /// <para>Le décryptage est effectué par un objet de type <see cref="AesManaged"/>.</para>
        /// <para>La clé de cryptage est retournée par la méthode <see cref="GetAesKey"/>.</para>
        /// <para>Le cryptage a été effectué préalablement par la méthode <see cref="Encrypt"/>.</para>
        /// </remarks>
        /// <param name="buffer">Données à décrypter</param>
        /// <returns>Données décryptées</returns>
        /// <seealso cref="AesManaged"/>
        public static byte[] Decrypt(byte[] buffer)
        {
            byte[] output = new byte[buffer.Length];

            // Create an AesManaged object 
            // with the specified key and IV. 
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = GetAesKey();
                aes.IV = GetAesIV();

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        // A voir : 
                        int nbBytes = csDecrypt.Read(output, 0, buffer.Length);
                        Array.Resize(ref output, nbBytes);
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// Retourne la clé de cryptage utilisée par les méthodes <see cref="Encrypt"/> et <see cref="Decrypt"/>.
        /// </summary>
        /// <returns>Clé de cryptage</returns>
        private static byte[] GetAesKey() 
        {
            return new byte[] { 98, 202, 28, 163, 102, 132, 115, 116, 72, 187, 50, 229, 128, 100, 35, 130, 130, 56, 51, 124, 132, 135, 4, 69, 156, 177, 31, 126, 244, 182, 199, 8 };
        }

        /// <summary>
        /// Retourne le vecteur d'initialisation utilisé par les méthodes <see cref="Encrypt"/> et <see cref="Decrypt"/>.
        /// </summary>
        /// <returns>Vecteur d'initialisation</returns>
        private static byte[] GetAesIV() 
        {
            return new byte[] { 209, 14, 9, 4, 159, 150, 243, 45, 173, 57, 104, 124, 113, 220, 108, 22 };
        }

        #endregion
    }
}
