using BlockChains;

var blockChain = new BlockChain();

blockChain.AddBlock(new { from = "John", to = "Doe", amount = 100 });
blockChain.AddBlock("Second block");
blockChain.AddBlock("Third block");

blockChain.PrintChain();
Console.WriteLine(blockChain.IsValid());

