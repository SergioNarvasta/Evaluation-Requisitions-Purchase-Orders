using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWeb_DRA.Utils
{
    public static class Cryptography
    {
        public static byte[] ValueVector
        {
            get
            {
                byte[] valorVector = new byte[16];

                Random random = new Random(SEED_IV);
                random.NextBytes(valorVector);

                return valorVector;
            }
        }
        public static byte[] ValueKey
        {
            get
            {
                byte[] valorVector = new byte[16];

                Random random = new Random(SEED_KEY);
                random.NextBytes(valorVector);

                return valorVector;
            }
        }
        public static string EncriptarAES(string val)
        {
            return BytesToStringBase64(Encriptar(val
                , ValueKey
                , ValueVector));
        }
        public static string DesencriptarAES(string val)
        {
            return Desencriptar(val
                , ValueKey
                , ValueVector);
        }

        #region Private
        private const int SEED_IV = 20200101;
        private const int SEED_KEY = 20201231;
        private static string BytesToStringBase64(byte[] valor)
        {
            return ((valor != null && valor.Length >= 0) ? System.Convert.ToBase64String(valor) : string.Empty);
        }
        private static byte[] Encriptar(string password, byte[] Key, byte[] IV)
        {
            if (password == null || password.Length <= 0)
            {
                return null;
            }
            if (Key == null || Key.Length <= 0)
            {
                return null;
            }
            if (IV == null || IV.Length <= 0)
            {
                return null;
            }
            MemoryStream msEncrypt = null;
            CryptoStream csEncrypt = null;
            StreamWriter swEncrypt = null;

            AesCryptoServiceProvider aesManagedAlg = null;

            try
            {
                aesManagedAlg = new AesCryptoServiceProvider();
                aesManagedAlg.Key = Key;
                aesManagedAlg.IV = IV;

                ICryptoTransform encryptor = aesManagedAlg.CreateEncryptor(aesManagedAlg.Key, aesManagedAlg.IV);

                msEncrypt = new MemoryStream();
                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                swEncrypt = new StreamWriter(csEncrypt);

                swEncrypt.Write(password);
            }
            finally
            {
                if (swEncrypt != null)
                    swEncrypt.Close();
                if (csEncrypt != null)
                    csEncrypt.Close();
                if (msEncrypt != null)
                    msEncrypt.Close();

                if (aesManagedAlg != null)
                    aesManagedAlg.Clear();
            }

            return msEncrypt.ToArray();

        }
        private static string Desencriptar(string password, byte[] Key, byte[] IV)
        {
            byte[] passwordCifrado = Convert.FromBase64String(password);

            if (password == null || password.Length <= 0)
            {
                return null;
            }
            if (Key == null || Key.Length <= 0)
            {
                return null;
            }
            if (IV == null || IV.Length <= 0)
            {
                return null;
            }

            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;

            AesCryptoServiceProvider aesManagedAlg = null;

            string plaintext = null;

            try
            {
                aesManagedAlg = new AesCryptoServiceProvider();
                aesManagedAlg.Key = Key;
                aesManagedAlg.IV = IV;

                ICryptoTransform decryptor = aesManagedAlg.CreateDecryptor(aesManagedAlg.Key, aesManagedAlg.IV);

                msDecrypt = new MemoryStream(passwordCifrado);
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);

                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                if (srDecrypt != null)
                    srDecrypt.Close();
                if (csDecrypt != null)
                    csDecrypt.Close();
                if (msDecrypt != null)
                    msDecrypt.Close();

                if (aesManagedAlg != null)
                    aesManagedAlg.Clear();
            }

            return plaintext;
        }
        #endregion
    }
}
