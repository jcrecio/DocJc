namespace DocJc.Contracts.Services
{
    using System.Collections.Generic;
    using Model.Models;

    public interface IMemoryStore<T>
    {
        IList<T> GetAll();
        void Add(T item);
        void AddRange(IList<T> items);
        void Remove(T item);
        void Clear();
    }
}
