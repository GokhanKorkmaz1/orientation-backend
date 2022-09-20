using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public interface IDemandService:IAppService<Demand>
    {
        //List<Demand> GetAll();
        List<Demand> GetListByUserId(int id);
        void Update(Demand entity);

        string CopyToDestinationFolder(string path);
        //byte[] UploadDocument(IFormFile file);
        Task<byte[]> UploadDocumentAsync(IFormFile file);
        Task<IFormFile> getFile(byte[] content);
    }
}
