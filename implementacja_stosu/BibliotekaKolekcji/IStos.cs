using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaKolekcji
{
    public interface IStos<T>
    {
        void Push(T element);
        T Pop();

        T Peek { get; }
        int Count { get; }
        bool IsEmpty { get; }
        void Clear();
    }
}
