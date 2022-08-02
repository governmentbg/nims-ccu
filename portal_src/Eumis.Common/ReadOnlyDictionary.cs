using System;
using System.Collections.Generic;

namespace Eumis.Common
{
    /// <summary>
    /// Хештаблица, чиито данни са достъпни само за четене
    /// </summary>
    /// <typeparam name="TKey">тип на ключа</typeparam>
    /// <typeparam name="TValue">тип на стойността</typeparam>
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IDictionary<TKey, TValue> _dict;

        /// <summary>
        /// Конструктор на хештаблицата
        /// </summary>
        /// <param name="backingDict">хештаблица</param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> backingDict)
        {
            _dict = backingDict;
        }

        /// <summary>
        /// Добавя запис към хештаблица
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">стойност</param>
        public void Add(TKey key, TValue value)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Връща дали хештаблицата съдържа даден елемент
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }

        /// <summary>
        /// Връща колекция от ключове на хештаблицата
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _dict.Keys; }
        }

        /// <summary>
        /// Невалиден метод за превахване на елемент от хештаблицата
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Връща стойност 
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">стойност</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        /// <summary>
        /// Връща колекция от стойностите
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _dict.Values; }
        }
        
        /// <summary>
        /// Имплементира индексът на хештаблицата
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return _dict[key]; }
            set { throw new InvalidOperationException(); }
        }

        /// <summary>
        /// Невалиден метод за добавяне на запис към хештаблицата
        /// </summary>
        /// <param name="item">запис</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Невалиден метод за чистене на хештаблицата
        /// </summary>
        public void Clear()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Връща дали хештаблицата съдърща даден запис
        /// </summary>
        /// <param name="item">запис</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dict.Contains(item);
        }

        /// <summary>
        /// Копира записите от хештаблицата към масив
        /// </summary>
        /// <param name="array">масив</param>
        /// <param name="arrayIndex">индекс на масив</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dict.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Връща брой записи
        /// </summary>
        public int Count
        {
            get { return _dict.Count; }
        }

        /// <summary>
        /// Показва че хештаблицата е само за четене
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Невалиден метод за изтриване на запис
        /// </summary>
        /// <param name="item">запис</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Енумератор на хештаблицата
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        System.Collections.IEnumerator
               System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_dict).GetEnumerator();
        }
    }
}
