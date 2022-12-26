namespace BlockChain;

using System.Security.Cryptography;

public static class Cryptography
{
   public static async Task<bool> Verify(byte[] originalData, byte[] signedData)
   {
      using var rsa = new RSACryptoServiceProvider();
      try
      {
         var publicKey = await ArbiterClient.GetPublicKey();
         rsa.ImportSubjectPublicKeyInfo(publicKey, out _);
         return rsa.VerifyHash(originalData, CryptoConfig.MapNameToOID("SHA256")!, signedData);
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
}