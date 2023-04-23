using Microsoft.EntityFrameworkCore;

namespace TicTacToe.DL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TicTacToeDbContext _context;
        private readonly DbSet<T> _table;

        public GenericRepository(TicTacToeDbContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }

        public async Task<ICollection<T>> GetAllAsync() => await _table.ToListAsync();

        public async Task<T> GetByIdAsync(object id) => await _table.FindAsync(id);

        public async Task<T> AddAsync(T obj)
        {
            _table.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> UpdateAsync(T updated, int key)
        {
            T existing = await _context.Set<T>().FindAsync(key);
            _context.Entry(existing).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<int> DeleteAsync(object id)
        {
            var entity = _table.Find(id);
            _table.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}