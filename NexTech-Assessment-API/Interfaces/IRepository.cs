using System.Collections.Generic;

namespace NexTechAssessmentAPI.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Add(T item);
        bool Delete(T Item);
        bool Edit(T item);
        bool Exists(int id);
    }
}
