using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Hm.Web.Helper
{
    public class HmCommon
    {
        public string MStrErrorMessage;
        public string MStrKey = "@)!!KkdiProp123";
        public string MStrCryptData;
        public string CryptData;


        #region public bool Encrypt(bool a_bUseHashing)
        /// <summary>
        /// To encrypt the given string.
        /// </summary>
        /// <param name="a_bUseHashing">bool a_bUseHashing</param>
        /// <returns>bool</returns>
        public bool Encrypt(bool a_bUseHashing)
        {
            try
            {
                ///UTF8Encoding encodes Unicode characters using the UTF-8 encoding (UCS Transformation Format, 8-bit form). This encoding supports all Unicode character values.
                byte[] baKeyArray;//Byte array to store key value.
                byte[] baToEncryptArray = UTF8Encoding.UTF8.GetBytes(MStrCryptData);//To transform the plainText into a Byte array.
                if (a_bUseHashing)
                {
                    MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();//Create object for MD5 hash process.
                    baKeyArray = objHashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(MStrKey));//To compute hash key.
                    objHashMD5.Clear();//Release the objHashMD5 from memory.
                }
                else
                    baKeyArray = UTF8Encoding.UTF8.GetBytes(MStrKey);//To transform the encryption key to byte array.
                TripleDESCryptoServiceProvider objTDES = new TripleDESCryptoServiceProvider();//Create object for DES encryption process.
                objTDES.Key = baKeyArray;//Assign the encryption key.
                objTDES.Mode = CipherMode.ECB;//Assign the encryption mode.
                objTDES.Padding = PaddingMode.PKCS7;//Assign the encryption padding.
                ICryptoTransform objICryptTransform = objTDES.CreateEncryptor();//Create variable for crypto transform interface.
                byte[] baResultArray = objICryptTransform.TransformFinalBlock(baToEncryptArray, 0, baToEncryptArray.Length);//Encrypt and convert the given string.
                objTDES.Clear();//Release the objTDES from memory.
                MStrCryptData = Convert.ToBase64String(baResultArray, 0, baResultArray.Length);//Convert encrypted data into Base64 string format.
            }
            catch (Exception)
            {
                MStrErrorMessage = "Encryption failed.";//Assign the error message to property.
                return false;
            }
            return true;
        }
        #endregion

        #region public bool Decrypt(bool a_bUseHashing)
        /// <summary>
        /// To decrypt the given string.
        /// </summary>
        /// <param name="a_bUseHashing">bool a_bUseHashing</param>
        /// <returns>bool</returns>
        public bool Decrypt(bool a_bUseHashing)
        {
            try
            {
                ///UTF8Encoding encodes Unicode characters using the UTF-8 encoding (UCS Transformation Format, 8-bit form). This encoding supports all Unicode character values.
                byte[] baKeyArray;//Byte array to store key value.
                byte[] baToEncryptArray = Convert.FromBase64String(MStrCryptData);//Convert encrypted data into Base64 string and store into ByteArray.
                if (a_bUseHashing)
                {
                    MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();//Create object for MD5 hash process.
                    baKeyArray = objHashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(MStrKey));//To compute hash key.
                    objHashMD5.Clear();//Release the objHashMD5 from memory.
                }
                else
                    baKeyArray = UTF8Encoding.UTF8.GetBytes(MStrKey);//To transform the decyyption key to byte array.
                TripleDESCryptoServiceProvider objTDES = new TripleDESCryptoServiceProvider();//Create object for DES encryption process.
                objTDES.Key = baKeyArray;//Assign the decryption key.
                objTDES.Mode = CipherMode.ECB;//Assign the decryption mode.
                objTDES.Padding = PaddingMode.PKCS7;//Assign the decryption padding.
                ICryptoTransform objICryptTransform = objTDES.CreateDecryptor();//Create variable for crypto transform interface.
                byte[] baResultArray = objICryptTransform.TransformFinalBlock(baToEncryptArray, 0, baToEncryptArray.Length);//Decrypt and convert the given string.
                objTDES.Clear();//Release the objTDES from memory.
                MStrCryptData = UTF8Encoding.UTF8.GetString(baResultArray);//Convert the decrypted data into UFT8 format.
            }
            catch (Exception)
            {
                MStrErrorMessage = "Encryption Failed";//Assign the error message.
                return false;
            }
            return true;
        }
        #endregion
    }
}

