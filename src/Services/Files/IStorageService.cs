using Bogus;

namespace BogusStore.Services.Files;

public interface IStorageService
{
    Uri GenerateImageUrl();
}

public class FakeStorageService : IStorageService
{
    public Uri GenerateImageUrl()
    {
        Faker f = new Faker();
        return new Uri(f.Image.PicsumUrl());
    }
}