using OrientationAPI.Data;
using OrientationAPI.Models;
using OrientationAPI.Models.Dtos;

namespace OrientationAPI.Business
{
    public class DocumentService : IDocumentService
    {
        private DocumentRepository _documentRepository;
        public DocumentService(DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public void Add(Document entity)
        {
            _documentRepository.Add(entity);
            _documentRepository.SaveAll();
        }

        public Document get(int id)
        {
            return _documentRepository.Get(id);
        }

        public List<Document> GetAll()
        {
            return _documentRepository.GetList();
        }

        public async Task<Document> UploadDocument(UploadDocumentDto uploadDocumentDto)
        {
            await using (var memoryStream = new MemoryStream())
            {
                await uploadDocumentDto.File.CopyToAsync(memoryStream);
                // Upload the file if less than 2 MB
                if (memoryStream.Length > 2097151)
                {
                    return null;
                }
                var uploadedDocument = new Document()
                {
                    content = memoryStream.ToArray()
                };
                return uploadedDocument;
            }
            
        }

    }
}
