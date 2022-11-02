using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxUtilsLib
{
    public class FCollections
    {
        /// <summary>
        /// Словарь значений, в которой каждый элемент имеет свой вес. Может быть использован для имитации случайного выбора
        /// </summary>
        /// <typeparam name="T">Любой тип</typeparam>
        public class LuckList<T> : IEnumerable<KeyValuePair<T, int>>, ICloneable
        {
            internal Random rnd = new();
            public KeyValuePair<T, int> this[int index] { get => sequence[index]; set { if (value.Value < 0) throw new ArgumentException("Luck parameter can't be equal or below zero", nameof(value)); sequence[index] = value; } }
            internal List<KeyValuePair<T, int>> sequence = new();

            public int Count => sequence.Count;

            public LuckList()
            {

            }

            public LuckList(List<KeyValuePair<T, int>> sequence)
            {
                this.sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            }

            public LuckList(IEnumerable<KeyValuePair<T, int>> enumerable)
            {
                if (enumerable == null) 
                    throw new ArgumentNullException(nameof(enumerable));
                this.sequence = new List<KeyValuePair<T, int>>(enumerable);
            }

            public LuckList(LuckList<T> sequence)
            {
                if (sequence == null)
                    throw new ArgumentNullException(nameof(sequence));
                this.sequence = sequence.sequence;
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

            public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
            {
                return ((IEnumerable<KeyValuePair<T, int>>)sequence).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)sequence).GetEnumerator();
            }

            public object Clone()
            {
                return new LuckList<T>(sequence);
            }

            public void Add(KeyValuePair<T, int> item)
            {
                sequence.Add(item);
            }

            public void Add(T item, int weight)
            {
                Add(new(item, weight));
            }

            public void Insert(int index, KeyValuePair<T, int> item)
            {
                sequence.Insert(index, item);
            }

            public bool Remove(KeyValuePair<T, int> item)
            {
                return sequence.Remove(item);
            }

            public void RemoveAt(int index)
            {
                sequence.RemoveAt(index);
            }

            public void Clear()
            {
                sequence.Clear();
            }
        }

    }
}
