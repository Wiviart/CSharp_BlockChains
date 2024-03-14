using System.Text.Json;
using System.Linq;

class JsonSaver
{
    string json = "";
    const string filePath = "D:\\CSharp\\BlockChains\\SaveData.json";

    public void SaveToJson(List<Block> blockChain)
    {
        List<string> jsonList = new List<string>();
        foreach (Block block in blockChain)
        {
            jsonList.Add(JsonSerializer.Serialize(block));
        }
        File.WriteAllLines(filePath, jsonList);

        Console.WriteLine("Save to Json");
    }

    public BlockChain LoadFromJson()
    {
        List<string> jsonList = File.ReadAllLines(filePath).ToList();
        List<Block> loadedChain = jsonList.Select(json => JsonSerializer.Deserialize<Block>(json)).ToList();

        return new BlockChain(loadedChain);
    }
}