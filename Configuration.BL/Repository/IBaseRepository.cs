using Configuration.DAL.Entity;
using System.Collections.Generic;

namespace Configuration.BL.Repository
{
    public interface IBaseRepository<T>
    {
        OperationResult<List<T>> GetItems();
        
        OperationResult<List<T>> GetActiveItems();

        OperationResult<T> GetOne(int id);

        OperationResult<bool> Save(T item);

        OperationResult<bool> Edit(int id, T item);

        OperationResult<bool> Delete(int id);
    }
}
