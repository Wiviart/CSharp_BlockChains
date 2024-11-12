using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

class Block
{
    public Block? PreviousBlock { get; }

    private dynamic data;
    private DateTime timestamp;
    public string Hash { get; private set; }

    private int mineValue;

    public Block(Block? previousBlock, dynamic data)
    {
        this.PreviousBlock = previousBlock;
        this.data = data;
        this.timestamp = DateTime.Now;
        this.Hash = CalculateHash();
        this.mineValue = 0;
    }

    public string CalculateHash()
    {
        var rawData = $"{GetPreviousBlockHash() + data + timestamp + mineValue}";
        var bytes = Encoding.UTF8.GetBytes(rawData);
        var hashBytes = SHA256.HashData(bytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    public string GetData()
    {
        return JsonSerializer.Serialize(
            new
            {
                previousBlock = GetPreviousBlockHash(),
                data,
                timestamp,
                Hash,
                mineValue
            }
        );
    }

    public string GetPreviousBlockHash()
    {
        return PreviousBlock?.Hash ?? "0000";
    }

    public bool IsValid()
    {
        return Hash == CalculateHash();
    }

    public bool IsPreviousBlockValid(Block block)
    {
        return block.Hash == GetPreviousBlockHash();
    }

    public void Mine(int difficulty)
    {
        string compareString = new string('0', difficulty);
        while (Hash[..difficulty] != compareString)
        {
            mineValue++;
            Hash = CalculateHash();
        }
    }
}