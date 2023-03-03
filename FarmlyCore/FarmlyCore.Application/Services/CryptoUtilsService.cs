using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace FarmlyCore.Application.Services
{
    public interface ICryptoUtilsService
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }

    public class CryptoUtils : ICryptoUtilsService
    {
        private static byte[] _privateKey = Convert.FromBase64String("");

        private static byte[] _publicKey = Convert.FromBase64String("");

        public string Encrypt(string input)
        {
            try
            {
                byte[] encryptedOutput;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    UnicodeEncoding ByteConverter = new UnicodeEncoding();

                    RSAParameters RSAKeyInfo = RSA.ExportParameters(false);
                    
                    RSAKeyInfo.Modulus = _publicKey;

                    RSAKeyInfo.Exponent = ByteConverter.GetBytes("65537");

                    RSAKeyInfo.D = _privateKey;

                    RSA.ImportParameters(RSAKeyInfo);

                    var inputBytes = Encoding.Unicode.GetBytes(input);

                    encryptedOutput = RSA.Encrypt(inputBytes, false);
                }

                return Convert.ToBase64String(encryptedOutput);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public string Decrypt(string input)
        {
            try
            {
                byte[] outputBytes;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    UnicodeEncoding ByteConverter = new UnicodeEncoding();

                    var inputBytes = Convert.FromBase64String(input);

                    RSAParameters RSAKeyInfo = RSA.ExportParameters(false);

                    RSAKeyInfo.Modulus = _publicKey;

                    RSAKeyInfo.Exponent = ByteConverter.GetBytes("65537");

                    RSAKeyInfo.D = _privateKey;

                    RSA.ImportParameters(RSAKeyInfo);

                    outputBytes = RSA.Decrypt(inputBytes, false);
                }

                return Encoding.Unicode.GetString(outputBytes);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}
