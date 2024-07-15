namespace Server.Grains;

public class CityGrain : Grain<CityState>, ICityGrain
{
    public async Task SaveCityAsync(string name)
    {
        State = new CityState {CityName = name};

        await WriteStateAsync();
    }
}
