namespace Server.Grains;

public interface IPersonGrain : IGrainWithStringKey
{
    Task SavePersonAsync(string personName);
}