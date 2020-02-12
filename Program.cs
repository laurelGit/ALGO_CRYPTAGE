using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{
    class Program
    {
        public static void ENC_DEC(string inputPath, string password, bool encryptMode, string outputPath)
        {
            using (var cypher = new AesManaged())
            using (var fsIn = new FileStream(inputPath, FileMode.Open))
            using (var fsOut = new FileStream(outputPath, FileMode.Create))
            {
                const int saltLength = 256;
                var salt = new byte[saltLength];
                var iv = new byte[cypher.BlockSize / 8];

                if (encryptMode)
                {
                    // Generate random salt and IV, then write them to file
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(salt);
                        rng.GetBytes(iv);
                    }
                    fsOut.Write(salt, 0, salt.Length);
                    fsOut.Write(iv, 0, iv.Length);
                }
                else
                {
                    // Read the salt and IV from the file
                    fsIn.Read(salt, 0, saltLength);
                    fsIn.Read(iv, 0, iv.Length);
                }

                // Generate a secure password, based on the password and salt provided
                var pdb = new Rfc2898DeriveBytes(password, salt);
                var key = pdb.GetBytes(cypher.KeySize / 8);

                // Encrypt or decrypt the file
                using (var cryptoTransform = encryptMode
                    ? cypher.CreateEncryptor(key, iv)
                    : cypher.CreateDecryptor(key, iv))
                using (var cs = new CryptoStream(fsOut, cryptoTransform, CryptoStreamMode.Write))
                {
                    fsIn.CopyTo(cs);
                }
            }
        }
        //static void Main(string[] args)
        //{
        //    ENC_DEC("C:\\test\\fileD.txt", "mann", false, "C:\\test\\fileDc.txt");
        //}
    }
}
