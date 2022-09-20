using OrientationAPI.Data;
using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public class DemandService : IDemandService
    {
        private DemandRepository _demandRepository;

        public DemandService(DemandRepository demandRepository)
        {
            _demandRepository = demandRepository;
        }
        public void Add(Demand entity)
        {
            _demandRepository.Add(entity);
            _demandRepository.SaveAll();
        }

        public Demand get(int id)
        {
            return _demandRepository.Get(id);
        }

        public List<Demand> GetListByUserId(int id)
        {
            return _demandRepository.GetListByUserId(id);
        }

        public List<Demand> GetAll()
        {
            return _demandRepository.GetList();
        }

        public void Update(Demand entity)
        {
            _demandRepository.Update(entity);
            _demandRepository.SaveAll();
        }

        public async Task<byte[]> UploadDocumentAsync(IFormFile file)
        {
            await using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                // Upload the file if less than 2 MB
                if (memoryStream.Length > 2097151)
                {
                    return null;
                }
                byte[] data = memoryStream.ToArray();
                return data;
            }

        }

        public async Task<IFormFile> getFile(byte[] content)
        {
            await using (var memoryStream = new MemoryStream(content))
            {
                IFormFile file =  new FormFile(memoryStream, 0, content.Length, "file", "filename");
                
                return file;
            }

        }

        public string CopyToDestinationFolder(string path)
        {
            string rootPath = @"C:\Users\tabim38\Downloads\";
            string fileName = Path.GetFileName(path);
            rootPath += fileName;
            File.Copy(path, rootPath, true);

            return fileName;
        }
    }
}
