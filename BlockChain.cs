class BlockChain
{
    private List<Block> chain;
    public BlockChain()
    {
        Block block = new Block(null, new { isGenesis = true });
        chain = new List<Block> { block };
    }

    public BlockChain(List<Block> loadedChain)
    {
        chain = loadedChain;
    }

    Block GetLastBlock()
    {
        return chain.Last();
    }

    public void AddBlock(dynamic data)
    {
        Block lastBlock = GetLastBlock();
        Block block = new Block(lastBlock, data);

        block.Mine(difficulty: 2);

        chain.Add(block);
    }

    public List<Block> GetChain() => chain;

    public void PrintChain()
    {
        foreach (Block block in chain)
        {
            Console.WriteLine(block.GetData());
        }
    }

    public bool IsValid()
    {
        for (int i = 1; i < chain.Count; i++)
        {
            Block currentBlock = chain[i];
            Block previousBlock = chain[i - 1];
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