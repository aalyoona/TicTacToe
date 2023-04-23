namespace TicTacToe.DL
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T obj);
        Task<int> DeleteAsync(object id);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> UpdateAsync(T updated, int key);
    }
}
