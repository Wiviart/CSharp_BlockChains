using System.Text.Json;

namespace BlockChains;

internal class JsonSaver
{
    private string json = "";
    private const string filePath = @"D:\CSharp\BlockChains\SaveData.json";

    public void SaveToJson(List<Block> blockChain)
    {
        var jsonList = blockChain.Select(block => JsonSerializer.Serialize(block)).ToList();
        File.WriteAllLines(filePath, jsonList);

        Console.WriteLine("Save to Json");
    }

    public BlockChain LoadFromJson()
    {
        var jsonList = File.ReadAllLines(filePath).ToList();
        List<Block> loadedChain = jsonList.Select(json => JsonSerializer.Deserialize<Block>(json)).ToList();

        return new BlockChain(loadedChain);
    }
}