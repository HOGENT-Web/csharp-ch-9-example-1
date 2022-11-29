using Microsoft.AspNetCore.Components.Forms;

namespace BogusStore.Client.Files;

public interface IStorageService
{
    Task UploadImageAsync(string sas, IBrowserFile file);
}