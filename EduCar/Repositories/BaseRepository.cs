using EduCar.Contexts;
using EduCar.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Utilização de um repositório Genérico
    /// Repositório usado como base para os métodos Post, Get, GetById, Update e Delete
    /// </summary>
    /// <typeparam name="T">Classe a ser utilizada no repositório</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        // Injeção de dependência do contexto e do repositório a serem utilizados
        private readonly DbSet<T> _dbSet;
        private readonly EduCarContext _context;

        public BaseRepository(EduCarContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // Atribuição do T (Model utilizada) ao _dbSet para utilizar os métodos de acordo com a Model utilizada
        }
        /// <summary>
        /// Inserir um item na base de dados
        /// </summary>
        /// <param name="item">Item a ser inserido</param>
        /// <returns>Retorna o item que foi inserido na base de dados</returns>
        public T Insert(T item)
        {
            var result = _dbSet.Add(item);

            _context.SaveChanges();

            return result.Entity;
        }
        /// <summary>
        /// Exibir uma lista do item da base de dados
        /// </summary>
        /// <returns>Retorna uma lista do item solicitado</returns>
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        /// <summary>
        /// Exibir um item de acordo com o id informado
        /// </summary>
        /// <param name="id">Id a ser pesquisado</param>
        /// <returns>Retorna um item a partir do Id informado</returns>
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        /// <summary>
        /// Modificar parcialmente um item na base de dados
        /// </summary>
        /// <param name="patchItem">Parâmetro a ser modificado</param>
        /// <param name="item">Item que terá o parâmetro modificado</param>
        public void Patch(JsonPatchDocument patchItem, T item)
        {
            patchItem.ApplyTo(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        /// <summary>
        /// Modificar o item na base de dados
        /// </summary>
        /// <param name="item">Item a ser modificado</param>
        public void Put(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }
        /// <summary>
        /// Excluir um item da base de dados
        /// </summary>
        /// <param name="item">Item a ser excluído</param>
        public void Delete(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
