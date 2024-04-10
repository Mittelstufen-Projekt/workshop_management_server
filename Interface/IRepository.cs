using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using WorkshopManagementServiceBackend.Models;

namespace WorkshopManagementServiceBackend.Interface
{
    public interface IRepository<T>
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(int id);
        Task<ICollection<T>> Find(string term);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
