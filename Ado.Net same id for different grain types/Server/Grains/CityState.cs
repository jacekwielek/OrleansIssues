namespace Server.Grains;

[GenerateSerializer]
public class CityState
{
    [Id(1)] public string CityName { get; set; } = null!;
}
