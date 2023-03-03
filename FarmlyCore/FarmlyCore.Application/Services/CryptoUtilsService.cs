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
        private static byte[] _privateKey = Convert.FromBase64String("MIICWwIBAAKBgQCFOvpCh8oIcsFWsp6ZZPTI210AFXI5xULwWzHkb3pebrfPFV9EmYyZ7ZWAMAUl9iZYPOHfx2Q+BPSkIvN7+/p4BcjCzaVUTcUJgk8q5CC5L6JAMPRqFLJyen5FVERKfCIyAUFHPHCv8pFBuH+hqUXjyOZCcE28YRN8j/s+VXCB9QIDAQABAoGABQcBtIr9M65o30WkOHOXzRCCBOPKSKXJ7dnzGwSd4HxbEURkMOl+ZbySXKDzQMh2D/RULaaPgMudE6iB+Za7j8fAbG9k63gh/uMfG4fBTL6yvcnyrQdS+o4pN0h1moiwT+MxBgHdQZVzhU4eNahPSey5ctio0irz7eQMwTQ52c0CQQDZZ6Yr/fZYihlMxOZ1GtT89kfW3LNWPw54Z+9Ev0eQweoyUl8ln+lm2Et4Pu9UA8DA8Z1pkVljLHXtd3dhy5BLAkEAnOHeb54rBYm2pBkN/1g9bj9uzwUVQYD2yD6PB3T3gI+8UUWH1DnnSarOH5ddSn9/raY48T0T0u6z0pVz1CtOvwJARiQCoL7W88naT8JW+AEoJlTu6Itb8L93U7F0qKFt3W4yuPmKaQEmtF+kOa9UV66+SYGqX5zdUfnOMkcQ5SRf8QJAfo7Dx518LVKEMWs43IXNwNORS5ZZKGemLbIx5h0Y0PCjRZjm03EGwWn2MIwGKWQjlu1iQFI6XM5N6JLCRyXMtwJAau2JUapieDO6fDCTS7Ve9cNjUQ+euEMrUWfhfGBdf3dF+RlMAOXm6uJEhpvSBazA/uh3lwbq32ggu0Wf1Bye2g==");

        private static byte[] _publicKey = Convert.FromBase64String("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCFOvpCh8oIcsFWsp6ZZPTI210AFXI5xULwWzHkb3pebrfPFV9EmYyZ7ZWAMAUl9iZYPOHfx2Q+BPSkIvN7+/p4BcjCzaVUTcUJgk8q5CC5L6JAMPRqFLJyen5FVERKfCIyAUFHPHCv8pFBuH+hqUXjyOZCcE28YRN8j/s+VXCB9QIDAQAB");

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
