using System;
using System.Collections;
using System.Collections.Generic;

namespace visualprogramming.Lab4
{
    public class GenericsRing<T> : IEquatable<GenericsRing<T>>, IComparable<GenericsRing<T>>, IEnumerable<T>
    {
        private readonly T[] _items;
        private int _headIndex = -1;
        private int _count = 0;
        private int _capacity;

        public int Capacity => _capacity;
        public int Count => _count;

        public GenericsRing(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than 0");

            _capacity = capacity;
            _items = new T[capacity];
        }

        public void Push(T item)
        {
            _headIndex = (_headIndex + 1) % Capacity;
            _items[_headIndex] = item;

            if (_count < Capacity)
                _count++;
        }

        public T Head()
        {
            if (_count == 0)
                throw new InvalidOperationException("Buffer is empty");
            return _items[_headIndex];
        }

        public T Get(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            int realIndex = (_headIndex - (_count - 1 - index) + Capacity) % Capacity;
            return _items[realIndex];
        }

        public bool Equals(GenericsRing<T> other)
        {
            if (other == null) return false;
            if (Count != other.Count) return false;

            for (int i = 0; i < Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(this.Get(i), other.Get(i)))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj) =>
            obj is GenericsRing<T> ring && Equals(ring);

        public override int GetHashCode() => HashCode.Combine(Capacity, Count);

        public int CompareTo(GenericsRing<T> other) => Count.CompareTo(other.Count);

        public IEnumerator<T> GetEnumerator() => new RingEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        private class RingEnumerator : IEnumerator<T>
        {
            private readonly GenericsRing<T> _ring;
            private int _index;
            private T _current;

            public RingEnumerator(GenericsRing<T> ring)
            {
                _ring = ring;
                _index = -1;
                _current = default;
            }

            public T Current => _current;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_index + 1 >= _ring.Count)
                    return false;

                _index++;
                _current = _ring.Get(_index);
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default;
            }

            public void Dispose() { }
        }
    }
}
