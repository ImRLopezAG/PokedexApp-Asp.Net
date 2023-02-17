namespace Database.Core;

public interface IBaseRepository<TEntity> where TEntity : class {
  Task Save(TEntity Entity);
  Task Update(TEntity Entity);
  Task Remove(TEntity Entity);
  Task<IEnumerable<TEntity>> GetAll();
  Task<TEntity> GetEntity(int id);
}
