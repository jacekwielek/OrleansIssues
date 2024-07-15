namespace Server.Grains;

[GenerateSerializer]
public class PersonState
{
    [Id(1)] public string PersonName { get; set; } = null!;
}