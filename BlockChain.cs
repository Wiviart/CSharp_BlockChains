namespace BlockChains;

internal class BlockChain
{
    private List<Block> Chain { get; }

    public BlockChain()
    {
        var block = new Block(null, new { isGenesis = true });
        Chain = new List<Block> { block };
    }

    public BlockChain(List<Block> loadedChain)
    {
        Chain = loadedChain;
    }

    private Block GetLastBlock()
    {
        return Chain.Last();
    }

    public void AddBlock(dynamic data)
    {
        var lastBlock = GetLastBlock();
        var block = new Block(lastBlock, data);

        block.Mine(difficulty: 2);

        Chain.Add(block);
    }


    public void PrintChain()
    {
        foreach (var block in Chain)
        {
            Console.WriteLine(block.GetData());
        }
    }

    public bool IsValid()
    {
        for (var i = 1; i < Chain.Count; i++)
        {
            var currentBlock = Chain[i];
            var previousBlock = Chain[i - 1];
            if (currentBlock.IsValid() == false)
            {
                return false;
            }
            if (!currentBlock.IsPreviousBlockValid(previousBlock))
            {
                return false;
            }
        }
        return true;
    }
}