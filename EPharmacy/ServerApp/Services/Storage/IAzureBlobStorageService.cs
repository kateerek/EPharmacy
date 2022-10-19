using System.IO;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Services.Storage
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadFile(string filename, Stream stream);
    }
}