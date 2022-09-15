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

        public byte[] UploadDocument(IFormFile file)
        {
             using (var memoryStream = new MemoryStream())
            {
                 file.CopyTo(memoryStream);
                // Upload the file if less than 2 MB
                if (memoryStream.Length > 2097151)
                {
                    return null;
                }
                
                byte[] data = memoryStream.ToArray();
                return data;
            }

        }

        public async Task<byte[]> UploadDocumentAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
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
    }
}
