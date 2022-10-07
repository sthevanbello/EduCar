using EduCar.Contexts;
using EduCar.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly EduCarContext _context;

        public BaseRepository(EduCarContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Insert(T item)
        {
            var result = _dbSet.Add(item);

            _context.SaveChanges();

            return result.Entity;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Patch(JsonPatchDocument patchItem, T item)
        {
            patchItem.ApplyTo(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Put(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }
        public void Delete(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
