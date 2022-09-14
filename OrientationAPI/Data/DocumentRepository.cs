using OrientationAPI.Models;

namespace OrientationAPI.Data
{
    public class DocumentRepository : IAppRepository<Document>
    {
        private OrientationContext _orientationContext;

        public DocumentRepository(OrientationContext orientationContext)
        {
            _orientationContext = orientationContext;
        }
        public void Add(Document entity)
        {
            _orientationContext.Add(entity);
        }

        public Document Get(int id)
        {
            return _orientationContext.documents.FirstOrDefault(d => d.id == id);
        }

        public List<Document> GetList()
        {
            return _orientationContext.documents.ToList();
        }

        public bool SaveAll()
        {
            return _orientationContext.SaveChanges() > 0;
        }
    }
}
