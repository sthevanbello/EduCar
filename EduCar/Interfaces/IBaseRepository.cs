using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de BaseRepository utilizando Generics T.
    /// <para>Esta interface recebe uma classe e de acordo com a classe os métodos poderão ser usados sem a necessidade de serem criados em todos os repositórios</para>
    /// <para>T é a classe recebida para ser utilizada</para>
    /// </summary>
    /// <typeparam name="T">Classe recebida</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        public T Insert(T item);
        public List<T> GetAll();
        public T GetById(int id);
        public void Put(T item);
        public void Patch(JsonPatchDocument patchItem, T item);
        public void Delete(T item);
    }
}
