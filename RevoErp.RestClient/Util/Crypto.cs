using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RevoErp.RestClient.Util
{
    public static class Crypto
    {
        public static string SHA1(string data)
        {
            byte[] temp1 = Encoding.UTF8.GetBytes(data);
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] temp2 = sha.ComputeHash(temp1);
            sha.Clear();
            var output = BitConverter.ToString(temp2);
            output = output.Replace("-", "");
            output = output.ToLower();
            return output;
        }
        public static byte[] MD5Bytes(string data)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            Console.WriteLine("dataBytes:" + string.Join(",", dataBytes) + " = " + BytesToString(dataBytes));


            var hash = md5.ComputeHash(dataBytes);

            Console.WriteLine(" -> md5" + string.Join(",", hash) + " = " + BytesToString(hash));
            return hash;
        }
        public static string MD5(string data)
        {
            byte[] md5bytes = MD5Bytes(data);
            StringBuilder sb = new StringBuilder();
            var str = BytesToString(md5bytes);
            return str;
        }
        public static string BytesToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        public static string ToHex(this byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
        public static byte[] Encrypt(string data, byte[] key, byte[] iv)
        {
            Console.WriteLine("iv:" + string.Join(",", iv) + " = " + BytesToString(iv));
            var toEncryptBytes = Encoding.UTF8.GetBytes(data);
            using (var provider = new AesCryptoServiceProvider())
            {
                provider.Key = key;
                provider.IV = iv;
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(provider.IV, 0, 8);
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(toEncryptBytes, 0, toEncryptBytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// aes-256-cbc 解码
        /// </summary>
        /// <param name="cipherData"></param>
        /// <param name="keyString"></param>
        /// <param name="ivString"></param>
        /// <returns></returns>
        public static string DecryptB64(string cipherData, string keyString, string ivString)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = Encoding.UTF8.GetBytes(ivString);

            try
            {
                using (var rijndaelManaged =
                       new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC })
                using (var memoryStream =
                       new MemoryStream(Convert.FromBase64String(cipherData)))
                using (var cryptoStream =
                       new CryptoStream(memoryStream,
                           rijndaelManaged.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            // You may want to catch more exceptions here...
        }

        /// <summary>
        /// aes-256-cbc 加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="KeyString"></param>
        /// <param name="IVString"></param>
        /// <returns></returns>
        public static string EncryptString(string message, string KeyString, string IVString)
        {
            byte[] Key = Encoding.UTF8.GetBytes(KeyString);
            byte[] IV = Encoding.UTF8.GetBytes(IVString);

            string encrypted = null;
            RijndaelManaged rj = new RijndaelManaged();
            rj.Key = Key;
            rj.IV = IV;
            rj.Mode = CipherMode.CBC;
            rj.Padding = PaddingMode.PKCS7;
            try
            {
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, rj.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(message);
                        sw.Close();
                    }
                    cs.Close();
                }
                byte[] encoded = ms.ToArray();
                encrypted = Convert.ToBase64String(encoded);

                ms.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
            finally
            {
                rj.Clear();
            }
            return encrypted;
        }

    
}
}
