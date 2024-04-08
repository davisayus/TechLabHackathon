using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1
{
    public class EncryptCore
    {
        public EncryptCore() { }

        public string Encrypt_SHA256(string valueEncrypt, string claveEncrypt, string key)
        {
            try
            {
                string vEncrypt = $"{valueEncrypt}.{claveEncrypt}-{key}";
                var encoding = new ASCIIEncoding();
                var sbEncrypt = new StringBuilder();

                var streamEncrypt = SHA256.HashData(encoding.GetBytes(vEncrypt));
                for (int i = 0; i < streamEncrypt.Length; i++)
                    sbEncrypt.AppendFormat("{0:x2}", streamEncrypt[i]);

                return sbEncrypt.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public string Encrypt_HMACSHA256(string valueEncrypt, string keyEncrypt)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(keyEncrypt);
                byte[] dataBytes = Encoding.UTF8.GetBytes(valueEncrypt);

                using (var hmac = new HMACSHA256(keyBytes))
                {
                    byte[] hashBytes = hmac.ComputeHash(dataBytes);
                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //public static (string encryptedData, byte[] key) EncryptWithAES(string user, string password)
        //{
        //    using Aes aesAlg = Aes.Create();
        //    aesAlg.GenerateKey();

        //    byte[] iv = aesAlg.IV;

        //    using (MemoryStream msEncrypt = new MemoryStream())
        //    {
        //        using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    // Concatenar usuario y contraseña antes de cifrar
        //                    string data = $"{user}-{password}";
        //                    swEncrypt.Write(data);
        //                }
        //            }
        //        }
        //    }

        //    byte[] encryptedBytes = msEncrypt.ToArray();

        //    // Convertir a Base64 para almacenar o transmitir
        //    string encryptedData = Convert.ToBase64String(iv.Concat(encryptedBytes).ToArray());

        //    return (encryptedData, aesAlg.Key);
        //}

        //--


        //public static string HashPasswordWithSalt(string password)
        //{
        //    // Generar una nueva sal
        //    string salt = BCrypt.Net.BCrypt.GenerateSalt();

        //    // Aplicar hash a la contraseña con la sal
        //    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        //    // Concatenar la sal y el hash para almacenar juntos
        //    string hashedPasswordWithSalt = $"{salt}${hashedPassword}";

        //    return hashedPasswordWithSalt;
        //}

        //public static bool VerifyPassword(string enteredPassword, string hashedPasswordWithSalt)
        //{
        //    // Separar la sal y el hash almacenados
        //    string[] parts = hashedPasswordWithSalt.Split('$');
        //    string salt = parts[0];
        //    string hash = parts[1];

        //    // Verificar la contraseña introducida utilizando la sal y el hash almacenados
        //    return BCrypt.Net.BCrypt.Verify(enteredPassword, $"{salt}${hash}");
        //}


        //**


        //

        //public static (string encryptedData, RSAParameters publicKey) EncryptWithRSA(string user, string password)
        //{
        //    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        //    {
        //        RSAParameters publicKey = rsa.ExportParameters(false);

        //        // Concatenar usuario y contraseña antes de cifrar
        //        byte[] data = Encoding.UTF8.GetBytes($"{user}-{password}");

        //        // Cifrar los datos
        //        byte[] encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);

        //        // Convertir a Base64 para almacenar o transmitir
        //        string encryptedDataString = Convert.ToBase64String(encryptedData);

        //        return (encryptedDataString, publicKey);
        //    }
        //}


        //

    }
}
