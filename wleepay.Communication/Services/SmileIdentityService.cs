using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

using wleepay.Communication.Interface;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wleepay.Communication.Services
{
    public class SmileIdentityService : ISmileIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IHTTPService _httpService;
        private Int32 timestamp = 0;
        private string secKey = string.Empty;
        private string partnerId = string.Empty;
        public SmileIdentityService(IConfiguration configuration, IHTTPService httpService)
        {
            _configuration = configuration;
            _httpService = httpService;
            string accountSid = configuration.GetSection("Twillio:SID").Value.ToString();
            string authToken = configuration.GetSection("Twillio:Token").Value.ToString();
            partnerId = "693";
            secKey = GenerateSecKey(out timestamp, partnerId);
        }

        public async Task<bool> ReEnrollUser(string userId)
        {
            string url = "https://portal.smileidentity.com/api/v2/partner/enrollee";
            object postObj = new {
                user_id = userId,
                allow_re_enroll = true,
                partner_id = partnerId,
                timestamp = timestamp,
                sec_key = secKey,
                email = "lionelbernard@wleepay.com",
                environment = "test"
            };
            var response = await _httpService.PostDataAsync(url, JsonConvert.SerializeObject(postObj));
            return true;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            string url = "https://portal.smileidentity.com/api/v2/partner/enrollee";
            object postObj = new
            {
                user_id = userId,
                delete = true,
                partner_id = partnerId,
                timestamp = timestamp,
                sec_key = secKey,
                email = "lionelbernard@wleepay.com",
                environment = "test"
            };

            var response = await _httpService.PostDataAsync(url, JsonConvert.SerializeObject(postObj));
            return true;
        }


        #region helper function to generate Sec Key
        private static string RsaDecryptWithPublic(string base64Input, string publicKey)
        {
            var bytesToDecrypt = Convert.FromBase64String(base64Input);
            var decryptEngine = new Pkcs1Encoding(new RsaEngine());
            using (var txtreader = new System.IO.StringReader(publicKey))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();
                decryptEngine.Init(false, keyParameter);
            }
            var decrypted = Encoding.UTF8.GetString(decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
            return decrypted;
        }
        private static string RsaEncryptWithPublic(string clearText, string publicKey)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(clearText);
            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            using (var txtreader = new System.IO.StringReader(publicKey))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();
                encryptEngine.Init(true, keyParameter);
            }
            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
            return encrypted;
        }
        private static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        static string GenerateSecKey(out Int32 timestamp, string partnerId)
        {
            // Download your API key from the Smile Identity portal
            // and either copy below or read it from a file.
            string api_key = "-----BEGIN PUBLIC KEY-----\n" +
                "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDD5/IzfQmJA5oLFLxNXrHsg2rH\n" +
                "aRU5lQWhajnrvrw45oT0W7qN5AxkzCZzMOyAOUBmK3hozeOjIo6MPXKmiD+8Ucfd\n" +
                "obfwgmoa2LuCkYaD3Kv32HQgccw8Ed7Ksu3b5LD6pcbnQYqxDy2OLblAYo1N4jBh" +
                "0c0NjbUGia0XoBLpnwIDAQAB\n" +
                "-----END PUBLIC KEY-----";

            // But for the sec_key calculation we need an int. 
            int partnerID = Int32.Parse(partnerId);

            // Get a Uxix timestamp
            timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            string plaintext = partnerID.ToString() + ":" + timestamp.ToString();
           
            // Hash the plaintext
            SHA256 mySHA256 = SHA256.Create();
            var bytesToEncrypt = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

            var encrypted = RsaEncryptWithPublic(ByteArrayToHexString(bytesToEncrypt), api_key);

            Console.WriteLine("Encrypted Message len : " + encrypted.Length); // should be 172
            Console.WriteLine("Encrypted Message:\r\n" + encrypted);

            string sec_key = encrypted + "|" + ByteArrayToHexString(bytesToEncrypt);

            return sec_key;
        }
        #endregion
    }
}
