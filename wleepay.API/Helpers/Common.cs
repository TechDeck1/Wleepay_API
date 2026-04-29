using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using wleepay.API.Resources;
using Microsoft.AspNetCore.Http;
using wleepay.Core.DTO.Response;

namespace wleepay.API.Helpers
{
    public class Common
    {
        public static string Encrypt(string text, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // For AES, IV is always 16 bytes

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write all data to the stream
                            swEncrypt.Write(text);
                        }
                    }

                    // Return the encrypted bytes from the memory stream
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // For AES, IV is always 16 bytes

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string GenerateVerificationCode(int length)
        {
            const string validChars = "0123456789"; // Characters to use in the verification code
            StringBuilder verificationCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(0, validChars.Length);
                verificationCode.Append(validChars[randomIndex]);
            }

            return verificationCode.ToString();
        }

        public static string MaskPhoneNumber(string PhoneNumber)
        {
            // Check if the Phone number is at least 4 digits long
            if (PhoneNumber.Length >= 4)
            {
                // Get the last 2 digits of the Phone number
                string lastTwoDigits = PhoneNumber.Substring(PhoneNumber.Length - 2);

                // Mask the rest of the Phone number with asterisks (*)
                string maskedPhoneNumber = new string('*', PhoneNumber.Length - 2) + lastTwoDigits;

                return maskedPhoneNumber;
            }
            else
            {
                // If the Phone number is less than 4 digits, return it as it is (no masking)
                return PhoneNumber;
            }
        }

        public static DateTime ConvertISTtoLiberiaTime(DateTime istTime)
        {
          // Liberia Time is typically 5 hours behind IST
          TimeSpan timeDifference = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(30);

          // Convert the timestamp from IST to Liberia Time
          DateTime liberiaTime = istTime.Subtract(timeDifference);

          return liberiaTime;
        }

        public static InstructionDetails DeserializeInstructions(string instructions)
        {
            var instructionDetails = new InstructionDetails();
            var lines = instructions.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.StartsWith("Amount:"))
                    instructionDetails.Amount = decimal.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Date:"))
                    instructionDetails.Date = DateTime.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Description:"))
                {
                    instructionDetails.Description = line;
                }
                else if (line.StartsWith("Units:"))
                    instructionDetails.Units = decimal.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Tax:"))
                    instructionDetails.Tax = decimal.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Receipt Number:"))
                    instructionDetails.ReceiptNumber = line.Split(':')[1].Trim();
                else if (line.StartsWith("Account Number:"))
                    instructionDetails.AccountNumber = line.Split(':')[1].Trim();
                else if (line.StartsWith("Customer Name:"))
                    instructionDetails.CustomerName = line.Split(':')[1].Trim();
                else if (line.StartsWith("Token Type:"))
                    instructionDetails.TokenType = line.Split(':')[1].Trim();
                else if (line.StartsWith("Token:"))
                    instructionDetails.Token = line.Split(':')[1].Trim();
            }

            return instructionDetails;
        }

    }
}
