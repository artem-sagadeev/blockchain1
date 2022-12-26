using System.Security.Cryptography;
using System.Text;

namespace BlockChain;

public class Block
{
    public int Id { get; set; }
    
    public byte[] PreviousHash { get; set; }
    
    public byte[] Data { get; set; }
    
    public byte[] SignedData { get; set; }
    
    public byte[] SignedHash { get; set; }

    public static async Task<Block> CreateBlock(string data, byte[] previousHash)
    {
        var block = new Block
        {
            PreviousHash = previousHash,
            Data = Encoding.UTF8.GetBytes(data)
        };

        var dataWithPreviousHash = block.PreviousHash.Concat(block.Data).ToArray();
        var hashedData = SHA256.HashData(dataWithPreviousHash);
        var (hashedDataWithTimestamp, signedData) = await ArbiterClient.GetSignature(hashedData);
        block.Data = hashedDataWithTimestamp;
        block.SignedData = signedData;
        
        var hash = SHA256.HashData(signedData);
        var (hashedSignWithTimestamp, signedHash) = await ArbiterClient.GetSignature(hash);
        block.SignedData = hashedSignWithTimestamp;
        block.SignedHash = signedHash;

        return block;
    }

    public async Task<bool> Verify()
    {
        var dataWithPreviousHash = PreviousHash.Concat(Data).ToArray();
        return await Cryptography.Verify(dataWithPreviousHash, SignedData) && 
               await Cryptography.Verify(SignedData, SignedHash);
    }
    
    private Block() {}
}