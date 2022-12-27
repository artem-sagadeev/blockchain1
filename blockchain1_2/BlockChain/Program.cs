using BlockChain;

var blockchain = new Blockchain();

await blockchain.AddBlock("data for block 0");
await blockchain.AddBlock("data for block 1");
await blockchain.AddBlock("data for block 2");
await blockchain.AddBlock("data for block 3");
await blockchain.AddBlock("data for block 4");
await blockchain.AddBlock("data for block 5");

Console.WriteLine(await blockchain.Verify());