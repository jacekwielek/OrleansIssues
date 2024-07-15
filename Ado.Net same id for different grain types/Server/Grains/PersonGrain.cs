namespace Server.Grains;

public class PersonGrain : Grain<PersonState>, IPersonGrain
{
    public async Task SavePersonAsync(string personName)
    {
        State = new PersonState {PersonName = personName};

        await WriteStateAsync();
    }

    public Task<PersonState> GetPersonDetailsAsync()
    {
        return Task.FromResult(State);
    }
}
