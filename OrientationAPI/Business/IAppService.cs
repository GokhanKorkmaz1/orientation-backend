namespace OrientationAPI.Business
{
    public interface IAppService<T>
    {
        void Add(T entity);
        T get(int id);
        List<T> GetAll();
    }
}
