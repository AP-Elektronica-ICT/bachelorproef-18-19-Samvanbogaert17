using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Encryption : MonoBehaviour {
    public static string key = "Ferranti";

    // script used for encryption and decryption for the highscores.txt file
    // used for encrypting a file
    public static void EncryptFile(string filePath, string key)
    {
        byte[] plainContent = File.ReadAllBytes(filePath);
        using (var DES = new DESCryptoServiceProvider())
        {

            DES.IV = Encoding.UTF8.GetBytes(key);
            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.Mode = CipherMode.CBC;
            DES.Padding = PaddingMode.PKCS7;

            using (var memStream = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(plainContent, 0, plainContent.Length);
                cryptoStream.FlushFinalBlock();
                File.WriteAllBytes(filePath, memStream.ToArray());
                print("Encrypted succesfully " + filePath);
            }
        }
    }
    // used for decrypting a file
    public static void DecryptFile(string filePath, string key)
    {
        byte[] encrypted = File.ReadAllBytes(filePath);
        using (var DES = new DESCryptoServiceProvider())
        {
            DES.IV = Encoding.UTF8.GetBytes(key);
            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.Mode = CipherMode.CBC;
            DES.Padding = PaddingMode.PKCS7;

            using (var memStream = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(encrypted, 0, encrypted.Length);
                cryptoStream.FlushFinalBlock();
                File.WriteAllBytes(filePath, memStream.ToArray());
                print("Decrypted succesfully " + filePath);
            }
        }
    }
    // used to append the playing player and score from the datascript script to the online encrypted highscores.txt file
    public static void AppendStringToFile(string fileName, string plainText, byte[] key, byte[] iv)
    {
        using (var algo = new DESCryptoServiceProvider())
        {
            algo.Key = key;
            // The IV is set below
            algo.Mode = CipherMode.CBC;
            algo.Padding = PaddingMode.PKCS7;

            // Create the streams used for encryption.
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] previous = null;
                int previousLength = 0;

                long length = file.Length;

                // No check is done that the file is correct!
                if (length != 0)
                {
                    // The IV length is equal to the block length
                    byte[] block = new byte[iv.Length];

                    if (length >= iv.Length * 2)
                    {
                        // At least 2 blocks, take the penultimate block
                        // as the IV
                        file.Position = length - iv.Length * 2;
                        file.Read(block, 0, block.Length);
                        algo.IV = block;
                    }
                    else
                    {
                        // A single block present, use the IV given
                        file.Position = length - iv.Length;
                        algo.IV = iv;
                    }

                    // Read the last block
                    file.Read(block, 0, block.Length);

                    // And reposition at the beginning of the last block
                    file.Position = length - iv.Length;

                    // We use a MemoryStream because the CryptoStream
                    // will close the Stream at the end
                    using (var ms = new MemoryStream(block))
                    // Create a decrytor to perform the stream transform.
                    using (ICryptoTransform decryptor = algo.CreateDecryptor())
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read all data from the stream. The decrypted last
                        // block can be long up to block length characters
                        // (so up to iv.Length) (this with AES + CBC)
                        previous = new byte[iv.Length];
                        previousLength = cs.Read(previous, 0, previous.Length);
                    }
                }
                else
                {
                    // Use the IV given
                    algo.IV = iv;
                }

                // Create an encryptor to perform the stream transform
                using (ICryptoTransform encryptor = algo.CreateEncryptor())
                using (CryptoStream cs = new CryptoStream(file, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    // Rewrite the last block, if present. We even skip
                    // the case of block present but empty
                    if (previousLength != 0)
                    {
                        cs.Write(previous, 0, previousLength);
                    }

                    // Write all data to the stream
                    sw.Write(System.Environment.NewLine);
                    sw.Write(plainText);
                    sw.Close();
                }
            }
        }
    }
}