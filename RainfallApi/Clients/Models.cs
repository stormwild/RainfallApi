namespace RainfallApi.Clients;

public class ReadingResponse
{
    public string Context { get; set; } = string.Empty;
    public Meta Meta { get; set; } = new();
    public List<Item> Items { get; set; } = new();
}

public class Meta
{
    public string Publisher { get; set; } = string.Empty;
    public string Licence { get; set; } = string.Empty;
    public string Documentation { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public List<string> HasFormat { get; set; } = new();
    public int Limit { get; set; }
}

public class Item
{
    public string Id { get; set; } = string.Empty;
    public string DateTime { get; set; } = string.Empty;
    public string Measure { get; set; } = string.Empty;
    public double Value { get; set; }
}
