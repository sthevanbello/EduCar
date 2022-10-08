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
        /// <summary>
        /// Inserir um item na base de dados
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Retorna o item que foi inserido na base de dados</returns>
        public T Insert(T item);
        /// <summary>
        /// Exibir uma lista do item da base de dados
        /// </summary>
        /// <returns>Retorna uma lista do item solicitado</returns>
        public List<T> GetAll();
        /// <summary>
        /// Exibir um item de acordo com o id informado
        /// </summary>
        /// <param name="id">Id a ser pesquisado</param>
        /// <returns>Retorna um item a partir do Id informado</returns>
        public T GetById(int id);
        /// <summary>
        /// Modificar parcialmente um item na base de dados
        /// </summary>
        /// <param name="patchItem">Parâmetro a ser modificado</param>
        /// <param name="item">Item que terá o parâmetro modificado</param>
        public void Patch(JsonPatchDocument patchItem, T item);
        /// <summary>
        /// Modificar o item na base de dados
        /// </summary>
        /// <param name="item">Item a ser modificado</param>
        public void Put(T item);
        /// <summary>
        /// Excluir um item da base de dados
        /// </summary>
        /// <param name="item">Item a ser excluído</param>
        public void Delete(T item);
    }
}
