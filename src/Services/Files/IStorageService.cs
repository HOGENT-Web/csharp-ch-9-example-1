using BogusStore.Domain.Files;

namespace BogusStore.Services.Files;

public interface IStorageService
{
    Uri BasePath { get; }
    Uri GenerateImageUploadSas(Image image);
}