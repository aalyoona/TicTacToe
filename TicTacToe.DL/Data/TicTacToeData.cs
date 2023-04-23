using Microsoft.EntityFrameworkCore;

namespace TicTacToe.DL
{
    public class TicTacToeData : ITicTacToeData
    {
        private readonly DbContext _context;
        private readonly IDictionary<Type, object> _repositories;

        public TicTacToeData(TicTacToeDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<GameEntity> Games
        {
            get
            {
                return GetRepository<GameEntity>();
            }
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!_repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericRepository<T>), _context);
                _repositories.Add(typeOfRepository, newRepository);
            }

            return (IGenericRepository<T>)_repositories[typeOfRepository];
        }
    }
}


