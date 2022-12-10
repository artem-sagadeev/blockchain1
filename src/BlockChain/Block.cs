using System.Security.Cryptography;
using System.Text;

namespace BlockChain;

public class Block
{
    public int Number { get; set; }
    
    public byte[] Data { get; set; }
    
    public byte[] SignedData { get; set; }
    
    public byte[] SignedHash { get; set; }

    public Block(int number, string data, byte[] previousHash)
    {
        Number = number;
        Data = previousHash.Concat(Encoding.UTF8.GetBytes(data)).ToArray();
        
        var hashedData = SHA256.HashData(Data);
        var signedData = Cryptography.Sign(hashedData);
        SignedData = signedData;
        
        var hash = SHA256.HashData(signedData);
        var signedHash = Cryptography.Sign(hash);
        SignedHash = signedHash;
    }

    public bool Verify()
    {
        return Cryptography.Verify(Data, SignedData) && Cryptography.Verify(SignedData, SignedHash);
    }
}