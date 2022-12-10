namespace BlockChain;

public class Blockchain
{
    private readonly List<Block> _blocks = new();
    private byte[] _previousHash;

    public Blockchain()
    {
        _previousHash = new byte[] { 0, 0, 0, 0, 0, 0, 0 };
    }

    public Block this[int index]
    {
        get => _blocks[index];
        set => _blocks[index] = value;
    }
    
    public void AddBlock(string data)
    {
        var number = _blocks.Count;
        var block = new Block(number, data, _previousHash);
        
        _blocks.Add(block);
        _previousHash = block.SignedHash;
    }

    public bool Verify()
    {
        return _blocks.All(block => block.Verify());
    }
}