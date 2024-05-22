using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkshopManagementServiceBackend.Interface;
using WorkshopManagementServiceBackend.Models;

namespace WorkshopManagementServiceBackend.Repository
{
    /*
     * Repository implementiert das IRepository und muss deshalb alle im Interface definierten Methoden implementieren
     * Repository hält ein ObjeKt der Context Klasse um darüber auf die Datenbank zugreifen zu können
     * Ein Context Objekt wird beim konstruieren des Repositories übergeben 
     */
    public class Repository<E> : IRepository<E> where E : class     
    {
        private readonly WorkshopmanagementContext _context;       
        public Repository(WorkshopmanagementContext context) {
            _context = context;
        }
        /*
         * Die Create Methode ist dazu da, um einen neuen Eintrag in die Datenbank hinzuzufügen
         * Um das Repository generisch nutzen zu können muss für jede operation ein neues DbSet erzeugt werden 
         * Über ein DbSet kann auf ein Table zugegriffen werden
         * Da man nicht einfach an ein Propertie eines generischen über ein normalen Zugriff kommen kann, muss man es so machen.
         * Es wird einmal gecheckt ob das Propertie gefunden wurde und ob es den richtigen Typen hat
         * Anschließend wird auf den expliziten Wert des Properties zugegriffen
         */
        public async Task<E> Create(E entity)
        {
            _context.Set<E>().Add(entity);                          
                                                                    
            var idProperty = typeof(E).GetProperty("Id"); 
            if (idProperty != null && idProperty.PropertyType == typeof(int))
            {
                var idValue = (int)idProperty.GetValue(entity);
                await _context.SaveChangesAsync();
                return _context.Set<E>().Find(idValue);
            }
            else
            {
                throw new ArgumentException("The type must have an integer Id property.");
            }
        }

        /*
         * Die Delete Methode ist dazu da, um einen besteheden Eintrag aus der Datenbank zu löschen.
         * Der Eintrag mit der gegebenen Id wird gelöscht, solange ein Eintrag mit dieser Id existiert.
         * Durch SaveChangesAsync werden die Änderungen in der Datenbank verwirklicht
         */

        public async Task Delete(int id) 
        {
            var entity = await _context.Set<E>().FindAsync(id);     
            if (entity == null) { throw new NullReferenceException(); }
            _context.Set<E>().Remove(entity);
            await _context.SaveChangesAsync();                      
            return;
        }

        /*
         * Die Get Methode ist dazu da, um einen besteheden Eintrag aus der Datenbank zu bekommen.
         * Der Eintrag mit der gegebenen Id wird zurückgeben, solange ein Eintrag mit dieser Id existiert, ansonsten wird null zurückgeben.
         */
        public async Task<E> Get(int id)
        {
            var entry = await _context.Set<E>().FindAsync(id);
            return entry;
        }

        /*
         * Die GetAll Methode ist dazu da, um alle Einträge aus dem jeweiligen Table zu bekommen.
         */
        public async Task<ICollection<E>> GetAll()
        {
            return await _context.Set<E>().ToListAsync();
        }

        /*
         * Die Find Methode ist dazu da, um aus allen Einträgen die zu bekommen, bei welcher das Propertie Name dem übergebenen String ähnelt.
         * Es wird eine SQL Like operation durchgeführt.
         */
        public async Task<ICollection<E>> Find(string term)
        {
            var entityType = typeof(E);
            var properties = entityType.GetProperties();

            var dbSet = _context.Set<E>();

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

        /*
         * Die Update Methode ist dazu da, um einen bestehenden Eintrag zu updaten 
         */
        public async Task<E> Update(E entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
