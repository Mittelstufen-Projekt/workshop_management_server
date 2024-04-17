using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkshopManagementServiceBackend.Interface;
using WorkshopManagementServiceBackend.Models;

namespace WorkshopManagementServiceBackend.Repository
{
    public class Repository<E> : IRepository<E> where E : class
    {
        private readonly WorkshopmanagementContext _context;
        public Repository(WorkshopmanagementContext context) { 
            _context = context;
            
        }

        public async Task<E> Create(E entity)
        {
            _context.Set<E>().Add(entity);
            var idProperty = typeof(E).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(int))
            {
                var idValue = (int)idProperty.GetValue(entity);
                _context.SaveChanges();
                return _context.Set<E>().Find(idValue);
            }
            else
            {
                throw new ArgumentException("The type T must have an integer Id property.");
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<E>().FindAsync(id);
            _context.Set<E>().Remove(entity);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<E> Get(int id)
        {
            return await _context.Set<E>().FindAsync(id);
        }

        public async Task<ICollection<E>> GetAll()
        {
            return await _context.Set<E>().ToListAsync();
        }

        public async Task<ICollection<E>> Find(string term)
        {
            var entityType = typeof(E);
            var properties = entityType.GetProperties();

            var dbSet = _context.Set<E>();

            // Erstelle eine Abfrage, die nach allen Eigenschaften sucht, die den Suchstring enthalten
            var query = dbSet.AsQueryable();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, property.Name), $"%{term}%"));
                }
            }

            return await query.ToListAsync();
        }

        public async Task<E> Update(E entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
