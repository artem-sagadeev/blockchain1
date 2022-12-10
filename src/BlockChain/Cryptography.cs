using System.Text;

namespace BlockChain;

using System.Security.Cryptography;

public static class Cryptography
{
   private static readonly byte[] PrivateKey;
   private static readonly byte[] PublicKey;
   
   public static byte[] Sign(byte[] data)
   {
      using var rsa = new RSACryptoServiceProvider();

      try
      {
         rsa.ImportPkcs8PrivateKey(PrivateKey, out _);
         var signedBytes = rsa.SignHash(data, CryptoConfig.MapNameToOID("SHA256")!);
         return signedBytes;
      }
      catch (CryptographicException e)
      {
         Console.WriteLine(e.Message);
         return null;
      }
      finally
      {
         rsa.PersistKeyInCsp = false;
      }
   }
   
   public static bool Verify(byte[] originalData, byte[] signedData)
   {
      using var rsa = new RSACryptoServiceProvider();
      try
      {
         rsa.ImportSubjectPublicKeyInfo(PublicKey, out _);
         return rsa.VerifyData(originalData, CryptoConfig.MapNameToOID("SHA256")!, signedData);
      }
      catch (CryptographicException e)
      {
         Console.WriteLine(e.Message);
         return false;
      }
      finally
      {
         rsa.PersistKeyInCsp = false;
      }
   }
   
   static Cryptography()
   {
      var privateKeyHex = File.ReadAllText("private.key");
      PrivateKey = GetKey(privateKeyHex);
      
      var publicKeyHex = File.ReadAllText("public.key");
      PublicKey = GetKey(publicKeyHex);
   }
   
   private static byte[] GetKey(string hex)
   {
      var sb = new StringBuilder();
      for(var i = 0; i < hex.Length; i++)
      {
         var byteString = hex.Substring(i, 1);
         var b = Convert.ToByte(byteString, 16);
         sb.Append(Convert.ToString(b, 2).PadLeft(4, '0'));
      }
      var str = sb.ToString();
    
      var numOfBytes = str.Length / 8;
      var bytes = new byte[numOfBytes];
      for(var i = 0; i < numOfBytes; ++i)
      {
         bytes[i] = Convert.ToByte(str.Substring(8 * i, 8), 2);
      }

      return bytes;
   }
}