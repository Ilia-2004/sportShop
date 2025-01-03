using System;
using System.Collections.Generic;

namespace sportShop.Repository;

internal interface IRepository<T> : IDisposable
  where T : class
{
  IEnumerable<T> GetObjectList();
  T GetObject(int id);
  void Create(T item);
  void Update(T item);
  void Delete(int id);
  void Save();
}