namespace OrientationAPI.Data
{
    public interface IAppRepository<T>
    {
        List<T> GetList();
        T Get(int id);
        void Add(T entity);
        bool SaveAll();
    }
}
