using System.Collections.Generic;
using NexTech_Assessment_API.Models;

namespace NexTech_Assessment_API.Interfaces
{
    public interface IRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        bool Add(T item);
        bool Delete(T Item);
        bool Edit(T item);
        bool Exists(int id);
    }
}
