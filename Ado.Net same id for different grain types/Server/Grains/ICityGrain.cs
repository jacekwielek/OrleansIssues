namespace Server.Grains;

public interface ICityGrain : IGrainWithStringKey
{
    Task SaveCityAsync(string name);
}
