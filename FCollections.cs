using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxUtilsLib
{
    namespace FCollections
    {
        /// <summary>
        /// Словарь значений, в которой каждый элемент имеет свой вес. Может быть использован для имитации случайного выбора
        /// </summary>
        /// <typeparam name="T">Любой тип</typeparam>
        public class LuckList<T> : IEnumerable<KeyValuePair<T, int>>, ICloneable
        {
            internal Random rnd = new();
            /// <summary>
            /// Получает значение в хранилище по индексу
            /// </summary>
            public KeyValuePair<T, int> this[int index] { get => sequence[index]; set { if (value.Value <= 0) throw new ArgumentException("Luck parameter can't be equal or below zero", nameof(value)); sequence[index] = value; } }
            internal List<KeyValuePair<T, int>> sequence = new();

            /// <summary>
            /// Количество элементов в экземпляре <see cref="LuckList{T}"/>
            /// </summary>
            public int Count => sequence.Count;

            /// <summary>
            /// Создаёт новый пустой экземпляр <see cref="LuckList{T}"/> со стандартной вместимостью, равной <see cref="List{T}"/>
            /// </summary>
            public LuckList()
            {

            }

            /// <summary>
            /// Создаёт новый экземпляр <see cref="LuckList{T}"/> который содержит элементы, скопированные из <paramref name="enumerable"/> с вместимостью, достаточной для хранения скопированных элементов
            /// </summary>
            /// <param name="enumerable">Данные для хранения</param>
            /// <exception cref="ArgumentNullException"></exception>
            public LuckList(IEnumerable<KeyValuePair<T, int>> enumerable)
            {
                if (enumerable == null) 
                    throw new ArgumentNullException(nameof(enumerable));
                this.sequence = new List<KeyValuePair<T, int>>(enumerable);
            }

            /// <summary>
            /// Создаёт копию <see cref="LuckList{T}"/> с теми же элементами, что и в <paramref name="sequence"/>
            /// </summary>
            /// <param name="sequence"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public LuckList(LuckList<T> sequence)
            {
                if (sequence == null)
                    throw new ArgumentNullException(nameof(sequence));
                this.sequence = sequence.sequence.ToList();
            }

            /// <summary>
            /// Берёт случайный элемент из словаря в соответствии с его весом
            /// </summary>
            /// <param name="removeElement">Должен ли выбранный элемент быть удалён из списка</param>
            /// <returns>Случайно выбранный элемент</returns>
            /// <exception cref="InvalidOperationException">Если в списке нет элементов</exception>
            public T? GetRandomElement(bool removeElement = false)
            {
                if (!sequence.Any())
                    throw new InvalidOperationException("Sequence contains no elements");
                int totalWeight = sequence.Sum((x) => x.Value);
                int itemWeightIndex = rnd.Next(0, totalWeight);
                int currentWeightIndex = 0;

                foreach (KeyValuePair<T, int> item in sequence)
                {
                    currentWeightIndex += item.Value;
                    if (currentWeightIndex >= itemWeightIndex)
                    {
                        if (removeElement)
                            sequence.Remove(item);
                        return item.Key;
                    }
                }

                return default;
            }

            /// <inheritdoc cref="List{T}.GetEnumerator"/>
            public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
            {
                return ((IEnumerable<KeyValuePair<T, int>>)sequence).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)sequence).GetEnumerator();
            }

            /// <summary>
            /// Создаёт и возвращает копию <see cref="LuckList{T}"/> идентичную этому экземпляру
            /// </summary>
            public object Clone()
            {
                return new LuckList<T>(sequence);
            }

            /// <summary>
            /// Добавляет элемент (ключ в <paramref name="item"/>) с весом, заданным значением <paramref name="item"/> в конец словаря
            /// </summary>
            /// <param name="item">Пара ключ-значение с элементом в виде ключа и его весом, в виде значения</param>
            public void Add(KeyValuePair<T, int> item)
            {
                sequence.Add(item);
            }

            /// <summary>
            /// Добавляет элемент с заданным весом в конец словаря
            /// </summary>
            /// <param name="item">Элемент для добавления</param>
            /// <param name="weight">Вес предмета</param>
            public void Add(T item, int weight)
            {
                Add(new(item, weight));
            }

            /// <summary>
            /// Добавляет элемент (ключ в <paramref name="item"/>) с весом, заданным значением <paramref name="item"/> в позицию, равную <paramref name="index"/>
            /// </summary>
            /// <param name="index"></param>
            /// <param name="item"></param>
            public void Insert(int index, KeyValuePair<T, int> item)
            {
                sequence.Insert(index, item);
            }

            /// <summary>
            /// Убирает из словаря элемент, значение и вес которого равны значению и ключу в <paramref name="item"/>
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public bool Remove(KeyValuePair<T, int> item)
            {
                return sequence.Remove(item);
            }

            /// <summary>
            /// Убирает из словаря элемент в позиции, равной <paramref name="index"/>
            /// </summary>
            /// <param name="index"></param>
            public void RemoveAt(int index)
            {
                sequence.RemoveAt(index);
            }

            /// <summary>
            /// Очищает словарь от всех хранимых значений
            /// </summary>
            public void Clear()
            {
                sequence = new();
            }
        }

    }
}
