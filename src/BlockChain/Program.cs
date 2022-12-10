using System.Text;
using BlockChain;

var blockchain = new Blockchain();
blockchain.AddBlock("data for block 0");
blockchain.AddBlock("data for block 1");
blockchain.AddBlock("data for block 2");
blockchain.AddBlock("data for block 3");
blockchain.AddBlock("data for block 4");
blockchain.AddBlock("data for block 5");

Console.WriteLine(blockchain.Verify());

var block2Hash = blockchain[2].SignedHash;
var damagedData = "damaged data";
var damagedDataBytes = block2Hash.Concat(Encoding.UTF8.GetBytes(damagedData)).ToArray();
blockchain[3].Data = damagedDataBytes;

Console.WriteLine(blockchain.Verify());