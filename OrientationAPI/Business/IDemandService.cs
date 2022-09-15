using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public interface IDemandService:IAppService<Demand>
    {
        List<Demand> GetListByUserId(int id);
        void Update(Demand entity);
        byte[] UploadDocument(IFormFile file);
        Task<byte[]> UploadDocumentAsync(IFormFile file);
    }
}
