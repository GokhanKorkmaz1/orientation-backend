using OrientationAPI.Models;
using OrientationAPI.Models.Dtos;

namespace OrientationAPI.Business
{
    public interface IDocumentService:IAppService<Document>
    {
        Task<Document> UploadDocument(UploadDocumentDto uploadDocumentDto);
    }
}
