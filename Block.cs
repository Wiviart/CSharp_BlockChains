using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

class Block
{
    private Block? previousBlock;
    public Block? PreviousBlock => previousBlock;
    private dynamic data;
    private DateTime timestamp;
    private string hash;
    public string Hash => hash;
    private int mineValue;

    public Block(Block? previousBlock, dynamic data)
    {
        this.previousBlock = previousBlock;
        this.data = data;
        this.timestamp = DateTime.Now;
        this.hash = CalculateHash();
        this.mineValue = 0;
    }

    public string CalculateHash()
    {
        string rawData = $"{GetPreviousBlockHash() + data + timestamp + mineValue}";

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(rawData);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public string GetData()
    {
        return JsonSerializer.Serialize(
            new
            {
                previousBlock = GetPreviousBlockHash(),
                data,
                timestamp,
                hash,
                mineValue
            }
        );
    }

    public string GetPreviousBlockHash()
    {
        return previousBlock?.Hash ?? "0000";
    }

    public bool IsValid()
    {
        return hash == CalculateHash();
    }

    public bool IsPreviousBlockValid(Block block)
    {
        return block.Hash == GetPreviousBlockHash();
    }

    public void Mine(int difficulty)
    {
        string compareString = new string('0', difficulty);
        while (hash.Substring(0, difficulty) != compareString)
        {
            mineValue++;
            hash = CalculateHash();
        }
    }
}