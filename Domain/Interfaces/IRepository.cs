using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();       // Obtener todos
        Task<T> GetByIdAsync(int id);             // Obtener por ID
        Task AddAsync(T entity);                  // Agregar
        Task UpdateAsync(T entity);               // Actualizar
        Task DeleteAsync(int id);                 // Eliminar
    }
}
