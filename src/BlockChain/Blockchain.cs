using Microsoft.EntityFrameworkCore;

namespace BlockChain;

public class Blockchain
{
    private readonly ApplicationContext _context;

    public Blockchain()
    {
        _context = new ApplicationContext();
    }
    
    private async Task<byte[]> GetPreviousHash() => await _context
        .Blocks
        .OrderBy(block => block.Id)
        .Select(block => block.SignedHash)
        .LastOrDefaultAsync() ?? new byte[] { 0, 0, 0, 0, 0, 0};

    private async Task<List<Block>> GetAllBlocks() => await _context
        .Blocks
        .ToListAsync();

    public async Task<Block> GetBlock(int id) => await _context
        .Blocks
        .FirstOrDefaultAsync(block => block.Id == id);

    public async Task<int> AddBlock(string data)
    {
        var previousHash = await GetPreviousHash();
        var block = await Block.CreateBlock(data, previousHash);
        
        _context.Blocks.Add(block);
        await _context.SaveChangesAsync();

        return block.Id;
    }

    public async Task UpdateBlock(Block block)
    {
        _context.Blocks.Update(block);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAllBlocks()
    {
        var allBlocks = await GetAllBlocks();
        _context.Blocks.RemoveRange(allBlocks);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Verify()
    {
        var blocks = await GetAllBlocks();
        foreach (var block in blocks)
        {
            if (await block.Verify())
            {
                return false;
            }
        }

        return true;
    }
}