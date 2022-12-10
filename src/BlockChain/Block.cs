using System.Security.Cryptography;
using System.Text;

namespace BlockChain;

public class Block
{
    public int Number { get; set; }
    
    public byte[] PreviousHash { get; set; }
    
    public byte[] Data { get; set; }
    
    public byte[] SignedData { get; set; }
    
    public byte[] SignedHash { get; set; }

    public Block(int number, string data, byte[] previousHash)
    {
        Number = number;
        PreviousHash = previousHash;
        Data = Encoding.UTF8.GetBytes(data);

        var dataWithPreviousHash = PreviousHash.Concat(Data).ToArray();
        var hashedData = SHA256.HashData(dataWithPreviousHash);
        var signedData = Cryptography.Sign(hashedData);
        SignedData = signedData;
        
        var hash = SHA256.HashData(signedData);
        var signedHash = Cryptography.Sign(hash);
        SignedHash = signedHash;
    }

    public bool Verify()
    {
        var dataWithPreviousHash = PreviousHash.Concat(Data).ToArray();
        return Cryptography.Verify(dataWithPreviousHash, SignedData) && Cryptography.Verify(SignedData, SignedHash);
    }
}