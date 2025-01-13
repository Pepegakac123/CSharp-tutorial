namespace BibliotekaKolekcji
{
    public class Stos<T> : IStos<T>
    {
        private List<T> _list;
        public Stos()
        {
            _list = new List<T>();
        }

        public T Peek
        {
        get{
            if(IsEmpty) throw new StosEmptyException();
                return _list[Count - 1];
        }
        }

        public int Count => _list.Count;

        public bool IsEmpty => Count == 0;

        public void Clear() => _list.Clear();

        public T Pop()
        {
            if (IsEmpty) throw new StosEmptyException();
            var lastIndex = Count - 1;
            var element = _list[lastIndex];
            _list.RemoveAt(lastIndex);
            return element;
        }

        public void Push(T element) => _list.Add(element);

        public T[] ToArray() => _list.ToArray();
    }
}
